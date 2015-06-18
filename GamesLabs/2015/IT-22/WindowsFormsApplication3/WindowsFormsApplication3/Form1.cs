using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;


namespace WindowsFormsApplication3
{

    public partial class Form1 : Form
    {

        Fish carp;
        List<Fish> fish = new List<Fish>();
        Random rand;
        public static bool valid;
        bool win;

        public Form1()
        {
            InitializeComponent();
            int count_fish = 50;
            valid = true;
            XmlDocument doc = new XmlDocument();
            if (!File.Exists(@"conf.xml"))
            {
                valid = false;
                throw new FileNotFoundException("conf.xml не найден");
            }
            else
            {
                doc.Load(@"conf.xml");
                XmlSchemaSet schemas = new XmlSchemaSet();
                doc.Schemas.Add("", "conf.xsd");
                ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);
                doc.Validate(eventHandler);
                this.DoubleBuffered = true;
                rand = new Random();
                
                if (valid)
                {
                    count_fish = Convert.ToInt32(doc.GetElementsByTagName("count_fish").Item(0).InnerText);


                    carp = new Fish(Color.FromName(doc.GetElementsByTagName("player").Item(0).ChildNodes.Item(2).InnerText), Convert.ToInt32(doc.GetElementsByTagName("player").Item(0).ChildNodes.Item(0).InnerText), Convert.ToInt32(doc.GetElementsByTagName("player").Item(0).ChildNodes.Item(1).InnerText), 8, 3);

                    for (int i = 0; i < count_fish; i++)
                    {
                        fish.Add(new Fish(Color.FromName(doc.GetElementsByTagName("color_fish").Item(0).InnerText), rand.Next(50, this.Width - 50), rand.Next(50, this.Height - 50), 3, rand.Next(0, 355)));
                    }
                }
                else {
                    valid = true;
                    carp = new Fish(Color.Black, this.Width / 2, this.Height / 2, 8, 3);
                    for (int i = 0; i < count_fish; i++)
                    {
                        fish.Add(new Fish(Color.Yellow, rand.Next(50, this.Width - 50), rand.Next(50, this.Height - 50), 3, rand.Next(0, 355)));
                    }
                }

            }


        }


        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Form1.valid = false;
                    MessageBox.Show(e.Message);
                    
                    break;
                case XmlSeverityType.Warning:
                    Form1.valid = false;
                    MessageBox.Show(e.Message);
                    
                    break;
            }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (valid)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                e.Graphics.FillRectangle(new SolidBrush(Color.Blue), 0, 0, this.Width, this.Height);
                hittest();


                if (fish.Count == 0 && !win)
                {
                    win = true;
                    MessageBox.Show("YOU WIN!!!");
                }
                if (!win)
                {
                    carp.draw(e.Graphics);
                    for (int i = 0; i < fish.Count; i++)
                    {
                        fish[i].draw(e.Graphics);
                    }
                }
            }
        }

        private void move() {
            foreach (Fish f in fish) {
                if ((f.cordX - 25 <= 0 || f.cordX + 45 >= this.Width) )
                {
                    f.changeAngle(180 - (f.Angle));
                }
                if (f.cordY - 25 <= 0 || f.cordY + 45 >= this.Height) {
                    f.changeAngle(-1*f.Angle);
                }
                f.move();
            }
            if (carp != null)
            {
                try
                {
                    carp.move();
                }
                catch (NullReferenceException) { 
                
                }
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            move();
            this.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            move();
            this.Refresh();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            double A = Math.Atan2(e.Y - carp.cordY, e.X - carp.cordX) / Math.PI * 180;
            carp.changeAngle(-(float)A);

        }


        public void hittest()
        {
            for (int i = fish.Count - 1; i >= 0; i--)
            {


                if (Math.Abs(fish[i].cordX - carp.cordX) < 80 && Math.Abs(fish[i].cordY - carp.cordY) < 80)
                {
                    if (fish[i].color != Color.Red)
                    {
                        fish[i].color = Color.Red;
                        fish[i].changeAngle(-1 * fish[i].Angle);
                    }
                }
                else {
                    fish[i].color = Color.Yellow;
                }

                if (Math.Abs(fish[i].cordX - carp.cordX) < 15 && Math.Abs(fish[i].cordY - carp.cordY) < 15)
                {
                    fish.RemoveAt(i);
                }

            }
        }


    }
}
