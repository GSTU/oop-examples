using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;


namespace lb13_oop
{
    

    public partial class Form1 : Form
    {
        bool move;
        int x, y;
        float time;
        Player player;
        List<Box> box;
        Box buf;
        baseBox field;
        public static bool valid;
        float speed = 0.002f;

        private void start() {
            valid = true;
            move = false;
            XmlDocument doc = new XmlDocument();
            doc.Load(@"config.xml");
            XmlSchemaSet schemas = new XmlSchemaSet();
             doc.Schemas.Add("", "config.xsd");
            ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);
            doc.Validate(eventHandler);
            if (valid)
            {
                float width = Convert.ToSingle(doc.GetElementsByTagName("player").Item(0).ChildNodes.Item(0).InnerText);
                float height = Convert.ToSingle(doc.GetElementsByTagName("player").Item(0).ChildNodes.Item(1).InnerText);
                float x = Convert.ToSingle(doc.GetElementsByTagName("player").Item(0).ChildNodes.Item(2).InnerText);
                float y = Convert.ToSingle(doc.GetElementsByTagName("player").Item(0).ChildNodes.Item(3).InnerText);
                float speed = Convert.ToSingle(doc.GetElementsByTagName("player").Item(0).ChildNodes.Item(4).InnerText);
                player = new Player(width, height, x, y, speed);
                box = new List<Box>();

                for (int i = 0; i < doc.GetElementsByTagName("box").Count; i++)
                {
                    width = Convert.ToSingle(doc.GetElementsByTagName("box").Item(i).ChildNodes.Item(0).InnerText);
                    height = Convert.ToSingle(doc.GetElementsByTagName("box").Item(i).ChildNodes.Item(1).InnerText);
                    x = Convert.ToSingle(doc.GetElementsByTagName("box").Item(i).ChildNodes.Item(2).InnerText);
                    y = Convert.ToSingle(doc.GetElementsByTagName("box").Item(i).ChildNodes.Item(3).InnerText);
                    speed = Convert.ToSingle(doc.GetElementsByTagName("box").Item(i).ChildNodes.Item(4).InnerText);
                    buf = new Box(width, height, x, y, speed);
                    box.Add(buf);
                }
            }
            else {
                player = new Player(40, 40, 320, 320, 1);
                box = new List<Box>();
                buf = new Box(100, 20, 10, 10, 1);
                box.Add(buf);
                buf = new Box(90, 90, 600, 10, -1);
                box.Add(buf);
                buf = new Box(30, 100, 10, 500, -1);
                box.Add(buf);
                buf = new Box(80, 100, 500, 500, 1);
                box.Add(buf);
            }
            this.Refresh();
            time = 0;
            label1.Text = "0";
        }


        public Form1()
        {
            
            InitializeComponent();
            field = new baseBox(50, 50, 600, 600);

            this.DoubleBuffered = true;
            timer1.Interval = 10;
            timer1.Tick += new EventHandler(timer1_Tick);

            
            start();//+
            timer1.Stop();
            



            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
                this.Refresh();
                foreach (Box b in box)
                {
                    if (player.hitTest(b))
                    {
                        timer1.Stop();
                        MessageBox.Show("Ты проиграл!!! Твоё время: " + label1.Text + " сек.");
                        start();
                        return;

                    }
                }
                if (!player.inBox(field.X, field.Y, field.W, field.H)) {
                    timer1.Stop();
                    MessageBox.Show("Ты проиграл!!! Твоё время: " + label1.Text + " сек.");
                    start();
                    return;
                
                }


                time += 10;
                label1.Text = Convert.ToString(time / 1000);

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (player.testPoint(e.X, e.Y))
            {
                move = true;
                if (move)
                {
                    if (!timer1.Enabled)
                    {
                        timer1.Start();
                    }
                    player.move(e.X, e.Y);
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.WhiteSmoke);
            e.Graphics.DrawLine(new Pen(Color.Black, 3), 0, this.Height - 100, this.Width, this.Height - 100);
            field.draw(e);
            player.draw(e);
            foreach (Box b in box) {
                b.draw(e);
                b.move(this.Width-20, this.Height-100,speed);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                player.move(e.X, e.Y);
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
        
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    MessageBox.Show(e.Message);
                    Form1.valid = false;
                    break;
                case XmlSeverityType.Warning:
                    MessageBox.Show(e.Message);
                    Form1.valid = false;
                    break;
            }

        }


    }

}
