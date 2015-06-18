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

namespace Игруля
{
    public partial class Form1 : Form
    {
        int scoreint;
        string score;
        string scoreformat;

        public Form1()
        {
            InitializeComponent();
        }

        static bool valid = true;

        public static void ValidationHandler(object sender, ValidationEventArgs args)
        {
            MessageBox.Show(args.Severity + ":" + args.Message + "\nЗагружены стандартные параметры");
            valid = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button11.Enabled = false;
            buttontext();

            //Загрузка компонентов
            string backColor = "#FF5F9EA0";
            string popColor = "#FFFFFF00";
            int scoreint = 0;
            string label = "Начать";

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
                    if (reader.Name == "backColor" && reader.NodeType != XmlNodeType.EndElement)
                        backColor = reader.ReadElementContentAsString();
                    if (reader.Name == "popColor" && reader.NodeType != XmlNodeType.EndElement)
                        popColor = reader.ReadElementContentAsString();
                    if (reader.Name == "score" && reader.NodeType != XmlNodeType.EndElement)
                        scoreint = reader.ReadElementContentAsInt();
                    if (reader.Name == "label" && reader.NodeType != XmlNodeType.EndElement)
                        label = reader.ReadElementContentAsString();
                }
                reader.Close();
            }

            this.BackColor = ColorTranslator.FromHtml(backColor);

            button1.BackColor = ColorTranslator.FromHtml(popColor);
            button2.BackColor = ColorTranslator.FromHtml(popColor);
            button3.BackColor = ColorTranslator.FromHtml(popColor);
            button4.BackColor = ColorTranslator.FromHtml(popColor);
            button5.BackColor = ColorTranslator.FromHtml(popColor);
            button6.BackColor = ColorTranslator.FromHtml(popColor);
            button7.BackColor = ColorTranslator.FromHtml(popColor);
            button8.BackColor = ColorTranslator.FromHtml(popColor);
            button9.BackColor = ColorTranslator.FromHtml(popColor);

            this.scoreint = scoreint;
            label1.Text = "Score: " + this.scoreint.ToString();

            button10.Text = label;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            button10.Enabled = false;
            button11.Enabled = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            button10.Enabled = true;
            button11.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            buttontext();
            Random rnumber = new Random();
            int number = rnumber.Next(1, 10);

            if (number == 1) 
            {
                button1.Text = "*";
            }

            if (number == 2)
            {
                button2.Text = "*";
            } 
            
            if (number == 3)
            {
                button3.Text = "*";
            } 
            
            if (number == 4)
            {
                button4.Text = "*";
            } 
            
            if (number == 5)
            {
                button5.Text = "*";
            } 
            
            if (number == 6)
            {
                button6.Text = "*";
            } 
            
            if (number == 7)
            {
                button7.Text = "*";
            } 
            
            if (number == 8)
            {
                button8.Text = "*";
            } 
            
            if (number == 9)
            {
                button9.Text = "*";
            }
        }
        public void buttontext ()
        {
            Button[] buttons = { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
            for (int i =0; i<buttons.Length; i++)
            {
                buttons[i].Text = "+";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "*")
            {
                scoreint++;
                score = scoreint.ToString();
                scoreformat = string.Format("Score: {0}", score);
                label1.Text = scoreformat;
            }
            else
            {
                scoreint--;
                score = scoreint.ToString();
                scoreformat = string.Format("Score: {0}", score);
                label1.Text = scoreformat;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "*")
            {
                scoreint++;
                score = scoreint.ToString();
                scoreformat = string.Format("Score: {0}", score);
                label1.Text = scoreformat;
            }
            else
            {
                scoreint--;
                score = scoreint.ToString();
                scoreformat = string.Format("Score: {0}", score);
                label1.Text = scoreformat;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "*")
            {
                scoreint++;
                score = scoreint.ToString();
                scoreformat = string.Format("Score: {0}", score);
                label1.Text = scoreformat;
            }
            else
            {
                scoreint--;
                score = scoreint.ToString();
                scoreformat = string.Format("Score: {0}", score);
                label1.Text = scoreformat;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "*")
            {
                scoreint++;
                score = scoreint.ToString();
                scoreformat = string.Format("Score: {0}", score);
                label1.Text = scoreformat;
            }
            else
            {
                scoreint--;
                score = scoreint.ToString();
                scoreformat = string.Format("Score: {0}", score);
                label1.Text = scoreformat;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "*")
            {
                scoreint++;
                score = scoreint.ToString();
                scoreformat = string.Format("Score: {0}", score);
                label1.Text = scoreformat;
            }
            else
            {
                scoreint--;
                score = scoreint.ToString();
                scoreformat = string.Format("Score: {0}", score);
                label1.Text = scoreformat;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text == "*")
            {
                scoreint++;
                score = scoreint.ToString();
                scoreformat = string.Format("Score: {0}", score);
                label1.Text = scoreformat;
            }
            else
            {
                scoreint--;
                score = scoreint.ToString();
                scoreformat = string.Format("Score: {0}", score);
                label1.Text = scoreformat;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (button7.Text == "*")
            {
                scoreint++;
                score = scoreint.ToString();
                scoreformat = string.Format("Score: {0}", score);
                label1.Text = scoreformat;
            }
            else
            {
                scoreint--;
                score = scoreint.ToString();
                scoreformat = string.Format("Score: {0}", score);
                label1.Text = scoreformat;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (button8.Text == "*")
            {
                scoreint++;
                score = scoreint.ToString();
                scoreformat = string.Format("Score: {0}", score);
                label1.Text = scoreformat;
            }
            else
            {
                scoreint--;
                score = scoreint.ToString();
                scoreformat = string.Format("Score: {0}", score);
                label1.Text = scoreformat;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (button9.Text == "*")
            {
                scoreint++;
                score = scoreint.ToString();
                scoreformat = string.Format("Score: {0}", score);
                label1.Text = scoreformat;
            }
            else
            {
                scoreint--;
                score = scoreint.ToString();
                scoreformat = string.Format("Score: {0}", score);
                label1.Text = scoreformat;
            }
        }
    }

}
