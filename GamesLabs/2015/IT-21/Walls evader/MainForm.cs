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

namespace Walls_evader
{
    public partial class MainForm : Form
    {
        int y, step = 20;
        bool pressed = false;
        bool flag = true;

        Random r;

        Config cfg;

        List<Wall> Walls;
        Player Player;

        public MainForm()
        {
            XmlReaderSettings xmlreadersettings = new XmlReaderSettings();
            xmlreadersettings.Schemas.Add(null, "xsd.xsd");
            xmlreadersettings.ValidationType = ValidationType.Schema;
            xmlreadersettings.ValidationEventHandler += new ValidationEventHandler(ValidateXML);
            XmlReader xmlreader = XmlReader.Create("Settings.xml", xmlreadersettings);
            while (xmlreader.Read()) { }

            try
            {
                if (flag)
                {
                    XMLWithDOM xmlWorker = new XMLWithDOM("Settings.xml");

                    cfg = xmlWorker.GetConfig();
                }
                else
                    throw new Exception();
            }
            catch
            {
                cfg = new Config(3, 8, "Red", "Black");
            }

            r = new Random();

            Walls = new List<Wall>();
            Player = new Player(this.DisplayRectangle.Width / 2, 380, cfg.PlayerSpeed, cfg.PlayerColor);

            InitializeComponent();
            AddSceneObjects();

            this.Timer.Start();
        }
        private void ValidateXML(object sender, ValidationEventArgs e)
        {
            if ((e.Severity == XmlSeverityType.Warning) || (e.Severity == XmlSeverityType.Error))
            {
                MessageBox.Show("Ошибка настроек: " + e.Message);

                flag = false;
            }
        }

        private void AddSceneObjects()
        {
            this.Controls.Add(this.Player.Body);

            for (y = step; y <= 360; y += step)
            {
                this.Walls.Add(new Wall(r.Next(0, 300 - 80 + 1), y, cfg.WallSpeed, cfg.WallColor));

                this.Controls.Add(this.Walls.Last<Wall>().Body);
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.Player.Body.Location.Y <= 0)
            {
                this.Timer.Stop();

                MessageBox.Show("Вы выиграли!");

                return;
            }

            foreach (Wall w in this.Walls)
            {
                w.Move();

                if (w.CheckCollision(this.Player))
                    this.Player.Reset(this.DisplayRectangle.Width / 2, 380);
            }
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!pressed)
            {
                switch (e.KeyData)
                {
                    case Keys.Left:
                        this.Player.Direction = 1;
                        this.Timer.Tick += this.Player.Move;

                        pressed = true;

                        break;
                    case Keys.Up:
                        this.Player.Direction = 2;
                        this.Timer.Tick += this.Player.Move;

                        pressed = true;

                        break;
                    case Keys.Right:
                        this.Player.Direction = 3;
                        this.Timer.Tick += this.Player.Move;

                        pressed = true;

                        break;
                    case Keys.Down:
                        this.Player.Direction = 4;
                        this.Timer.Tick += this.Player.Move;

                        pressed = true;

                        break;
                }
            }
        }
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            pressed = false;

            this.Timer.Tick -= this.Player.Move;
        }
    }

    class XMLWithDOM
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
            XmlElement root = document["root"];

            return new Config(int.Parse(root["playerspeed"].InnerText),
                              int.Parse(root["wallspeed"].InnerText),
                              root["colors"].FirstChild.InnerText,
                              root["colors"].LastChild.InnerText);
        }
    }
    class Config
    {
        public int PlayerSpeed { get; set; }
        public int WallSpeed { get; set; }
        public Color PlayerColor { get; set; }
        public Color WallColor { get; set; }

        public Config(int ps, int ws, string pc, string wc)
        {
            this.PlayerSpeed = ps;
            this.WallSpeed = ws;
            this.PlayerColor = Color.FromName(pc);
            this.WallColor = Color.FromName(wc);
        }
    }
}