using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace Laba_5
{
    public partial class Form1 : Form
    {
        Keys choose;
        Timer timer;
        Brush br;
        Random rand = new Random();
        static bool flag1 = true;
        int x1 = 0,
            y1 = 0;
        int x = 10
           , y = 10;
        public int X1
        {
            get { return x1; }
            set { x1 = value; }
        }
        public int Y1
        {
            get { return y1; }
            set { y1 = value; }
        }
        public Form1()
        {
            br = Brushes.Red;
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void button1_Click(object sender, EventArgs e)
        {
                timer = new Timer();
                X = 10;
                Y = 10;
                XmlReaderSettings xmlreadersettings = new XmlReaderSettings();
                xmlreadersettings.Schemas.Add(null, "xsd.xsd");
                xmlreadersettings.ValidationType = ValidationType.Schema;
                xmlreadersettings.ValidationEventHandler += new ValidationEventHandler(xmlreadersettingsValidationEventHandler);
                XmlReader xmlreader = XmlReader.Create("xml.xml", xmlreadersettings);
                while (xmlreader.Read()) { }
                if (flag1 == true)
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load("xml.xml");
                    Color color = Color.FromName(xmldoc.DocumentElement.SelectSingleNode("color").InnerText);
                    SolidBrush sbr = new SolidBrush(color);
                    br = sbr;
                    X = Convert.ToInt32(xmldoc.DocumentElement.SelectSingleNode("Position").FirstChild.InnerText);
                    Y = Convert.ToInt32(xmldoc.DocumentElement.SelectSingleNode("Position").LastChild.InnerText);
                    timer.Interval = Convert.ToInt32(xmldoc.DocumentElement.SelectSingleNode("tick").InnerText);
                }

            Graphics g = CreateGraphics();
            button1.Visible = false;
            //button1.Location = this.PointToClient(MousePosition);
            g.Clear(Color.DarkGray);
            
            X1 = 0;
            Y1 = 0;
            CountPoint = 0;
            level = 0;
            choose = Keys.K;
            ChangeMove = 0;
            timer.Interval = 100;
            timer.Start();
            timer.Tick += new EventHandler(timer_Tick);
        }

        static void xmlreadersettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                MessageBox.Show("Внимание: " + e.Message);
                flag1 = false;
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                MessageBox.Show("Ошибка: " + e.Message);
                flag1 = false;
            }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        int changemove = 5;
        public int ChangeMove
        {
            get { return changemove; }
            set { changemove = value; }
        }
        int CountPoint = 0;
        int level = 0;
        Rectangle rect;
        bool flag = true;
        public void timer_Tick(object sender, EventArgs e)
        {
            Graphics g1 = CreateGraphics();
            g1.Clear(Color.DarkGray);
            //------------------------------------------------
            if (choose == Keys.Right)
            {
                if (ChangeMove == 3)
                {
                    timer.Stop();
                    MessageBox.Show("Boom noob!,try again");
                    button1.Visible = true;
                }
                X += 10;
                changemove = 0;
            }
            if (choose == Keys.Down)
            {
                if (ChangeMove == 2)
                {
                    timer.Stop();
                    MessageBox.Show("Boom noob!,try again");
                    button1.Visible = true;
                }
                Y += 10;
                changemove = 1;
            }
            if (choose == Keys.Up)
            {
                if (ChangeMove == 1)
                {
                    timer.Stop();
                    MessageBox.Show("Boom noob!,try again");
                    button1.Visible = true;
                }
                Y -= 10;
                changemove = 2;
            }
            if (choose == Keys.Left)
            {
                if (ChangeMove == 0)
                {
                    timer.Stop();
                    MessageBox.Show("Boom noob!,try again");
                    button1.Visible = true;
                }
                X -= 10;
                changemove = 3;
            }
           //int yc = MousePosition.X;
           //int xc = MousePosition.Y;
            //------------------------------------------------
            if (X > 938 || Y > 581 || Y < 0 || X < 0)
            {
                timer.Stop();
                MessageBox.Show("Boom noob!,try again");
                button1.Visible = true;
            }
            //-------------------------------------------------
            if (( X >= X1 - 12 && Y >= Y1 - 12) && (X <= X1 + 12 && Y <= Y1 + 12) )
            {
                flag = true;
                if (CountPoint == 2)
                {
                    CountPoint = 0;
                    level++;
                    if (timer.Interval == 20)
                    {
                        timer.Stop();
                        MessageBox.Show("Congratulations son,you win");
                        Application.Exit();
                    }
                    else
                    {
                        timer.Interval -= 20;
                    } 
                }
                Point.Text = "Point=" + CountPoint + "   " + "Level=" + level;
                CountPoint++;

            }
            if (flag)
            {
                To4ka();
                flag = false;
            }
            //----------------------------------------------------------
            g1.DrawRectangle(Pens.Red, rect = new Rectangle(X1, Y1, 6, 6));
            g1.FillRectangle(Brushes.Blue, rect);
            g1.DrawRectangle(Pens.Red, rect = new Rectangle(X, Y, 16, 16));
            g1.FillRectangle(br, rect);
            //-------------------------------------------------------------------------
            if (ChangeMove == 1)
            {
                g1.DrawRectangle(Pens.Black, rect = new Rectangle(X + 3, Y - 10, 10, 10));
                g1.FillRectangle(Brushes.Black, rect);
            }
            if (ChangeMove == 0)
            {
                g1.DrawRectangle(Pens.Black, rect = new Rectangle(X - 10, Y + 3, 10, 10));
                g1.FillRectangle(Brushes.Black, rect);
            }
            if (ChangeMove == 3)
            {
                g1.DrawRectangle(Pens.Black, rect = new Rectangle(X + 16, Y + 3, 10, 10));
                g1.FillRectangle(Brushes.Black, rect);
            }
            if (ChangeMove == 2)
            {
                g1.DrawRectangle(Pens.Black, rect = new Rectangle(X + 3, Y + 16, 10, 10));
                g1.FillRectangle(Brushes.Black, rect);
            }
            //----------------------------------------------------------------------
        }
        public void To4ka()
        {
            X1 = rand.Next(0, 938 - 50);
            Y1 = rand.Next(0, 581 - 50);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            choose = e.KeyCode;
        }


        /*static bool flag1 = true;
        public void ReadXML()
        {
            XmlReaderSettings xmlreadersettings = new XmlReaderSettings();
            xmlreadersettings.Schemas.Add(null, "xsd.xsd");
            xmlreadersettings.ValidationType = ValidationType.Schema;
            xmlreadersettings.ValidationEventHandler += new ValidationEventHandler(xmlreadersettingsValidationEventHandler);
            XmlReader xmlreader = XmlReader.Create("xml.xml", xmlreadersettings);
            while (xmlreader.Read()) { }
            if (flag1 == true)
            {
                Text = "Puzzle";
                    label2.Text = "Лучший результат: ";
                    label2.Visible = false;
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load("xml.xml");
                    bitmap = new Bitmap(xmldoc.DocumentElement.SelectSingleNode("path").InnerText);
                    wlength = Convert.ToInt32(xmldoc.DocumentElement.SelectSingleNode("size").FirstChild.InnerText);
                    hlength = Convert.ToInt32(xmldoc.DocumentElement.SelectSingleNode("size").LastChild.InnerText);
                    NewGame();
                    Text += " -> Уровень: " + xmldoc.DocumentElement.SelectSingleNode("level").InnerText;
                    label2.Text += xmldoc.DocumentElement.SelectSingleNode("result").InnerText;
                    label2.Visible = true;
                    for (int i = 0; i < picturebox.Length; i++)
                    {
                        picturebox[i].Enabled = false;
                    }
                    восстановитьКартинкуToolStripMenuItem.Enabled = false;
                    перемешатьКартинкуToolStripMenuItem.Enabled = true;
                    помощьToolStripMenuItem.Enabled = true;
                    настройкаСложностиToolStripMenuItem.Enabled = true;
                    index = -1;
            }
            
        }
        static void xmlreadersettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                MessageBox.Show("Внимание: " + e.Message);
                flag1 = false;
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                MessageBox.Show("Ошибка: " + e.Message);
                flag1 = false;
            }
        }*/
    }
}
