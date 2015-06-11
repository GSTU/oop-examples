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

namespace Игра
{
    public partial class Form1 : Form
    {
        public int speed_left=4;// скорость мячика
        public int speed_top = 4;
        public int point = 0;
        public int start = 0;
        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;
            label2.Visible = false;
            label4.Visible = false;
            racket.Visible = false;
            ball.Visible = false;
            label4.Left = 500;
            label5.Left = 500;
            label6.Left = 500;
            Cursor.Hide(); //скрывает курсор

            this.FormBorderStyle = FormBorderStyle.None;//удаляет границы
            this.TopMost = true;
            this.Bounds = Screen.PrimaryScreen.Bounds;//на весь экран

            racket.Top = playground.Bottom - (playground.Bottom / 10);//позиция ракетки
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (start == 1)
            {
                label2.Visible = true;
                label5.Visible = false;
                label6.Visible = false;
                racket.Visible = true;
                ball.Visible = true;
                racket.Left = Cursor.Position.X - (racket.Width / 2);

                ball.Left += speed_left;//перемещение мячика
                ball.Top += speed_top;

                if (ball.Bottom >= racket.Top && ball.Bottom <= racket.Bottom && ball.Left >= racket.Left && ball.Right <= racket.Right)
                {
                    speed_top += 2;
                    speed_left += 2;
                    speed_top = -speed_top;//изменение направления
                    point += 1;
                    label2.Text ="Speed " + Convert.ToString(point);
                }

                if (ball.Left <= playground.Left)
                {
                    speed_left = -speed_left;
                }

                if (ball.Right >= playground.Right)
                { speed_left = -speed_left; }

                if (ball.Top <= playground.Top)
                { speed_top = -speed_top; }

                if (ball.Bottom >= playground.Bottom)
                { timer1.Enabled = false;
                label4.Visible = true;
                //label4.Location = ;
                label4.Text = "Your best speed " + Convert.ToString(point);
                } //окончание игры
                

            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Close(); }//выход
            if (e.KeyCode == Keys.Enter) { start=1; }
           // if (e.KeyCode == Keys.Left) { racket.Left -= 10; }
           // if (e.KeyCode == Keys.Right) { racket.Left += 10; }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
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
            string c1 = "#FF5F9EA0", c2 = "", c3 = "";
            int colorNumber=0, count = 0;


            XmlTextReader reader = new XmlTextReader("XMLFile1.xml");
            XmlValidatingReader validreader = new XmlValidatingReader(reader);
            validreader.Schemas.Add(null, "XMLSchema1.xsd");
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
            //validreader.Close();

            if (valid)
            {
                reader = new XmlTextReader("XMLFile1.xml");
                while (reader.Read())
                {
                    if (reader.Name == "count" && reader.NodeType != XmlNodeType.EndElement)
                    {
                        count = reader.ReadElementContentAsInt();
                    }

                    if (reader.Name == "colorNumber" && reader.NodeType != XmlNodeType.EndElement)
                        colorNumber = reader.ReadElementContentAsInt();
                        if (reader.Name == "c1" && reader.NodeType != XmlNodeType.EndElement)
                            c1 = reader.ReadElementContentAsString();
                             if (reader.Name == "c2" && reader.NodeType != XmlNodeType.EndElement)
                                c2 = reader.ReadElementContentAsString();
                                 if (reader.Name == "c3" && reader.NodeType != XmlNodeType.EndElement)
                                 {
                                     c3 = reader.ReadElementContentAsString();
                                 }
                }
                reader.Close();
            }

            if(colorNumber == 0)
                racket.BackColor = ColorTranslator.FromHtml(c1);
            else if (colorNumber == 1)
                racket.BackColor = ColorTranslator.FromHtml(c2);
            else if (colorNumber == 2)
                racket.BackColor = ColorTranslator.FromHtml(c3);

            start = point = count;
            speed_left=count*2+2;
            speed_top = count*2+2;
            label2.Text = "Speed " + Convert.ToString(point);
            
        }    
  
    }
}
