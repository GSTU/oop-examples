using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace GAME1
{
    public partial class Form1 : Form
    {
        string strLoose;
        int count;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            MessageBox.Show("GG WP EASY!");
            Close();
        }
        private void MoveToStart()
        {
            Point startingPoint = panel1.Location;
            startingPoint.Offset(10, 10);
            Cursor.Position = PointToScreen(startingPoint);
        }
        int z;
        private void label2_MouseEnter(object sender, EventArgs e)
        {
            MoveToStart();
            z--;
            if (z == 0)
            {
                MessageBox.Show(strLoose);
            }
            label30.Text = z.ToString();
            if (z == 0)
            {
                z = count;
                label30.Text = z.ToString();
            }
        }


        
        int flag = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flag == 0)
            {
                label4.Enabled = true;
                label4.Visible = true;
                flag = 1;
            }
            else
            {
                label4.Enabled = false;
                label4.Visible = false;
                flag = 0;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        int flag1 = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (flag1 == 0)
            {
                label28.Enabled = true;
                label28.Visible = true;
                flag1 = 1;
            }
            else
            {
                label28.Enabled = false;
                label28.Visible = false;
                flag1 = 0;
            }
        }
        int flag2 = 0;
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (flag2 == 0)
            {
                label25.Enabled = true;
                label25.Visible = true;
                flag2 = 1;
            }
            else
            {
                label25.Enabled = false;
                label25.Visible = false;
                flag2 = 0;
            }
        }
        private void label30_Click(object sender, EventArgs e)
        {
  
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           

           
        }

        static bool valid = true;

        public static void ValidationHandler(object sender, ValidationEventArgs args)
        {
            MessageBox.Show(args.Severity + ":" + args.Message + "\nЗагружены стандартные параметры");
            valid = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Загрузка компонентов
            string colorBack = "#FF5F9EA0";
            string colorUp = "#FF0000FF";
            int count = 5;
            string str = "YOU LOOSER!";

            XmlTextReader reader = new XmlTextReader("config.xml");
            XmlValidatingReader validreader = new XmlValidatingReader(reader);
            validreader.Schemas.Add(null, "configSchema.xsd");
            validreader.ValidationType = ValidationType.Schema;
            validreader.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);
            
            try
            {
                while (validreader.Read()) ;
            }
            catch
            {
                valid = false;
                MessageBox.Show("Файл конфигурации не найден\nЗагружены параметры по умолчанию");
            }
            validreader.Close();

            if(valid)
            {
                reader = new XmlTextReader("config.xml");
                while (reader.Read())
                {
                    if (reader.Name == "count" && reader.NodeType != XmlNodeType.EndElement)
                        count = reader.ReadElementContentAsInt();
                    if (reader.Name == "str" && reader.NodeType != XmlNodeType.EndElement)
                        str = reader.ReadElementContentAsString();
                    if (reader.Name == "colorBack" && reader.NodeType != XmlNodeType.EndElement)
                        colorBack = reader.ReadElementContentAsString();
                    if (reader.Name == "colorUp" && reader.NodeType != XmlNodeType.EndElement)
                        colorUp = reader.ReadElementContentAsString();
                }
                reader.Close();
            }

            strLoose = str;
            this.count = count;
            z = count;
            label30.Text = z.ToString();
            label2.BackColor = ColorTranslator.FromHtml(colorBack);
            label3.BackColor = ColorTranslator.FromHtml(colorBack);
            label4.BackColor = ColorTranslator.FromHtml(colorBack);
            label5.BackColor = ColorTranslator.FromHtml(colorBack);
            label6.BackColor = ColorTranslator.FromHtml(colorBack);
            label7.BackColor = ColorTranslator.FromHtml(colorBack);
            label8.BackColor = ColorTranslator.FromHtml(colorBack);
            label9.BackColor = ColorTranslator.FromHtml(colorBack);
            label10.BackColor = ColorTranslator.FromHtml(colorBack);
            label11.BackColor = ColorTranslator.FromHtml(colorBack);
            label12.BackColor = ColorTranslator.FromHtml(colorBack);
            label13.BackColor = ColorTranslator.FromHtml(colorBack);
            label14.BackColor = ColorTranslator.FromHtml(colorBack);
            label15.BackColor = ColorTranslator.FromHtml(colorBack);
            label16.BackColor = ColorTranslator.FromHtml(colorBack);
            label17.BackColor = ColorTranslator.FromHtml(colorBack);
            label18.BackColor = ColorTranslator.FromHtml(colorBack);
            label19.BackColor = ColorTranslator.FromHtml(colorBack);
            label20.BackColor = ColorTranslator.FromHtml(colorBack);
            label21.BackColor = ColorTranslator.FromHtml(colorBack);
            label22.BackColor = ColorTranslator.FromHtml(colorBack);
            label23.BackColor = ColorTranslator.FromHtml(colorBack);
            label24.BackColor = ColorTranslator.FromHtml(colorBack);
            label25.BackColor = ColorTranslator.FromHtml(colorBack);
            label26.BackColor = ColorTranslator.FromHtml(colorBack);
            label27.BackColor = ColorTranslator.FromHtml(colorBack);
            label28.BackColor = ColorTranslator.FromHtml(colorBack);

            label2.ForeColor = ColorTranslator.FromHtml(colorBack);
            label3.ForeColor = ColorTranslator.FromHtml(colorBack);
            label4.ForeColor = ColorTranslator.FromHtml(colorBack);
            label5.ForeColor = ColorTranslator.FromHtml(colorBack);
            label6.ForeColor = ColorTranslator.FromHtml(colorBack);
            label7.ForeColor = ColorTranslator.FromHtml(colorBack);
            label8.ForeColor = ColorTranslator.FromHtml(colorBack);
            label9.ForeColor = ColorTranslator.FromHtml(colorBack);
            label10.ForeColor = ColorTranslator.FromHtml(colorBack);
            label11.ForeColor = ColorTranslator.FromHtml(colorBack);
            label12.ForeColor = ColorTranslator.FromHtml(colorBack);
            label13.ForeColor = ColorTranslator.FromHtml(colorBack);
            label14.ForeColor = ColorTranslator.FromHtml(colorBack);
            label15.ForeColor = ColorTranslator.FromHtml(colorBack);
            label16.ForeColor = ColorTranslator.FromHtml(colorBack);
            label17.ForeColor = ColorTranslator.FromHtml(colorBack);
            label18.ForeColor = ColorTranslator.FromHtml(colorBack);
            label19.ForeColor = ColorTranslator.FromHtml(colorBack);
            label20.ForeColor = ColorTranslator.FromHtml(colorBack);
            label21.ForeColor = ColorTranslator.FromHtml(colorBack);
            label22.ForeColor = ColorTranslator.FromHtml(colorBack);
            label23.ForeColor = ColorTranslator.FromHtml(colorBack);
            label24.ForeColor = ColorTranslator.FromHtml(colorBack);
            label25.ForeColor = ColorTranslator.FromHtml(colorBack);
            label26.ForeColor = ColorTranslator.FromHtml(colorBack);
            label27.ForeColor = ColorTranslator.FromHtml(colorBack);
            label28.ForeColor = ColorTranslator.FromHtml(colorBack);

            panel1.BackColor = ColorTranslator.FromHtml(colorUp);
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
