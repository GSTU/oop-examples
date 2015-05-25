using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace Mine_Sweeper
{
    public partial class SettingsForm : Form
    {
        MainForm Main;

        Config config;

        bool flag = true;

        public SettingsForm(MainForm main)
        {
            try
            {
                if ((System.IO.File.Exists("xsd.xsd")))
                {
                    XmlReaderSettings xmlreadersettings = new XmlReaderSettings();

                    xmlreadersettings.Schemas.Add(null, "xsd.xsd");

                    xmlreadersettings.ValidationType = ValidationType.Schema;
                    xmlreadersettings.ValidationEventHandler += new ValidationEventHandler(ValidateXML);

                    XmlReader xmlreader = XmlReader.Create("Settings.xml", xmlreadersettings);
                    while (xmlreader.Read()) { }
                }
                else
                    throw new System.IO.FileNotFoundException();


                if (flag)
                    config = new XMLWithDOM("Settings.xml").GetConfig();
                else
                    throw new Exception();
            }
            catch
            {
                config = new Config(40, 16, 25);
            }

            InitializeComponent();

            Main = main;

            this.size.Value       = config.FieldSize;
            this.minesCount.Value = config.MinesCount;
        }

        private void ValidateXML(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                MessageBox.Show("Warning: " + e.Message);

                flag = false;
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                MessageBox.Show("Error: " + e.Message);

                flag = false;
            }
        }
        private void size_ValueChanged(object sender, EventArgs e)
        {
            minesCount.Maximum = (decimal)Math.Pow((double)this.size.Value, 2);
        }
        private void ok_Click(object sender, EventArgs e)
        {
            Main.size       = (int)this.size.Value;
            Main.minesCount = (int)this.minesCount.Value;
            Main.cellSize   = config.CellSize;

            Main.Startup();

            this.Close();
        }
    }

    public class XMLWithDOM
    {
        private XmlDocument document;

        public XMLWithDOM(String pathToXMLFile)
        {
            try
            {
                if (!(System.IO.File.Exists(pathToXMLFile)))
                    throw new System.IO.FileNotFoundException();

                document = new XmlDocument();

                using (XmlReader reader = XmlReader.Create(pathToXMLFile))
                {
                    document.Load(reader);
                }
            }
            catch { throw; }
        }

        public Config GetConfig()
        {
            XmlElement root = document["config"];

            return new Config(int.Parse(root["game"].FirstChild.InnerText), int.Parse(root["game"].LastChild.InnerText), int.Parse(root["window"].FirstChild.InnerText));
        }
    }
    public class Config
    {
        public int MinesCount;
        public int FieldSize;
        public int CellSize;

        public Config(int mc, int fs, int cs)
        {
            MinesCount = mc;
            FieldSize = fs;
            CellSize = cs;
        }
    }
}
