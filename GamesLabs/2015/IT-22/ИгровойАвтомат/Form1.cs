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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int keshik = 100;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            l5.Text = System.Convert.ToString(keshik);
            Dviglo1.Enabled = true;
            Dviglo2.Enabled = true;
            Dviglo3.Enabled = true;
            Stoper1.Enabled = true;
            Stoper2.Enabled = true;
            Stoper3.Enabled = true;
        }

        private void Dviglo1_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            int dig = rand.Next(8);
            l1.Text = System.Convert.ToString(dig);
        }

        private void Dviglo2_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            int dig = rand.Next(8);
            l2.Text = System.Convert.ToString(dig);
        }

        private void Dviglo3_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            int dig = rand.Next(8);
            l3.Text = System.Convert.ToString(dig);
        }

        private void Stoper1_Tick(object sender, EventArgs e)
        {
            Dviglo1.Enabled = false;
            Stoper1.Enabled = false;
        }

        private void Stoper2_Tick(object sender, EventArgs e)
        {
            Dviglo2.Enabled = false;
            Stoper2.Enabled = false;
        }

        private void Stoper3_Tick(object sender, EventArgs e)
        {
            Dviglo3.Enabled = false;
            Stoper3.Enabled = false;

            if ((l1.Text == "1") && (l2.Text == "1"))
            {
                if (l3.Text != "1")
                {
                    MessageBox.Show("Выйгрыш 10$");
                    keshik += 10;
                }
            }
            if ((l1.Text == "2") && (l2.Text == "2"))
            {
                if (l3.Text != "2")
                {
                    MessageBox.Show("Выйгрыш 20$");
                    keshik += 20;
                }
            }
            if ((l1.Text == "3") && (l2.Text == "3"))
            {
                if (l3.Text != "3")
                {
                    MessageBox.Show("Выйгрыш 30$");
                    keshik += 30;
                }
            }
            if ((l1.Text == "4") && (l2.Text == "4"))
            {
                if (l3.Text != "4")
                {
                    MessageBox.Show("Выйгрыш 40$");
                    keshik += 40;
                }
            }
            if ((l1.Text == "5") && (l2.Text == "5"))
            {
                if (l3.Text != "5")
                {
                    MessageBox.Show("Выйгрыш 50$");
                    keshik += 50;
                }
            }
            if ((l1.Text == "6") && (l2.Text == "6"))
            {
                if (l3.Text != "6")
                {
                    MessageBox.Show("Выйгрыш 60$");
                    keshik += 60;
                }
            }
            if ((l1.Text == "7") && (l2.Text == "7"))
            {
                if (l3.Text != "7")
                {
                    MessageBox.Show("Выйгрыш 70$");
                    keshik += 70;
                }
            }
            if ((l1.Text == "1") && (l2.Text == "1") && (l3.Text == "1"))
            {
                MessageBox.Show("Выйгрыш 100$");
                keshik += 100;
            }
            if ((l1.Text == "2") && (l2.Text == "2") && (l3.Text == "2"))
            {
                MessageBox.Show("Выйгрыш 200$");
                keshik += 200;
            }
            if ((l1.Text == "3") && (l2.Text == "3") && (l3.Text == "3"))
            {
                MessageBox.Show("Выйгрыш 300$");
                keshik += 300;
            }
            if ((l1.Text == "4") && (l2.Text == "4") && (l3.Text == "4"))
            {
                MessageBox.Show("Выйгрыш 400$");
                keshik += 400;
            }
            if ((l1.Text == "5") && (l2.Text == "5") && (l3.Text == "5"))
            {
                MessageBox.Show("Выйгрыш 500$");
                keshik += 500;
            }
            if ((l1.Text == "6") && (l2.Text == "6") && (l3.Text == "6"))
            {
                MessageBox.Show("Выйгрыш 600$");
                keshik += 600;
            }
            if ((l1.Text == "7") && (l2.Text == "7") && (l3.Text == "7"))
            {
                MessageBox.Show("Выйгрыш 700$");
                keshik += 700;
            }
            if ((l1.Text == "7") || (l2.Text == "7") || (l3.Text == "7"))
            {
                MessageBox.Show("Выйгрыш 5$");
                keshik += 5;
            }
            l5.Text = System.Convert.ToString(keshik);
            keshik -= 5;
        }

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void правилаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pravila p = new Pravila();
            p.ShowDialog();
        }

        private void обАвтореToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObAvtore ob = new ObAvtore();
            ob.ShowDialog();
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
            string colorBack = "#FFFF0000";
            string c1 = "#FF5F9EA0";
            string c2 = "#FF0000FF";
            string c3 = "#FFFFFF00";
            int startMoney = 100;

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

            if (valid)
            {
                reader = new XmlTextReader("config.xml");
                while (reader.Read())
                {
                    if (reader.Name == "startMoney" && reader.NodeType != XmlNodeType.EndElement)
                        startMoney = reader.ReadElementContentAsInt();
                    if (reader.Name == "colorBack" && reader.NodeType != XmlNodeType.EndElement)
                        colorBack = reader.ReadElementContentAsString();
                    if (reader.Name == "c1" && reader.NodeType != XmlNodeType.EndElement)
                        c1 = reader.ReadElementContentAsString();
                    if (reader.Name == "c2" && reader.NodeType != XmlNodeType.EndElement)
                        c2 = reader.ReadElementContentAsString();
                    if (reader.Name == "c3" && reader.NodeType != XmlNodeType.EndElement)
                        c3 = reader.ReadElementContentAsString();
                }
                reader.Close();
            }

            keshik = startMoney;
            l5.Text = keshik.ToString();
            this.BackColor = ColorTranslator.FromHtml(colorBack);
            l1.BackColor = ColorTranslator.FromHtml(c1);
            l2.BackColor = ColorTranslator.FromHtml(c2);
            l3.BackColor = ColorTranslator.FromHtml(c3);
        }




    }
}
