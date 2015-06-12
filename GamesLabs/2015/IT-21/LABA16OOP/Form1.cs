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


namespace lb12_oop
{
    public partial class Form1 : Form
    {
        public static Graphics g;
        Player player1, player2;
        Ball ball;
        public static int formWidth = 800;
        public static int formHight = 600;
        int size;
        static bool valid;

        public Form1()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            label1.Text = "0";
            label2.Text = "0";
            g = panel1.CreateGraphics();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

            valid = true;
            XmlDocument doc = new XmlDocument();
            doc.Load(@"config.xml");
            XmlSchemaSet schemas = new XmlSchemaSet();
            doc.Schemas.Add("", "config.xsd");
            ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);
            doc.Validate(eventHandler);
            int height;
            this.size = formWidth / 50;
            g.TranslateTransform(formWidth / 2, formHight / 2);
            
            if (valid)
            {
                height = Convert.ToInt32(doc.GetElementsByTagName("player_one").Item(0).ChildNodes.Item(0).InnerText);
                player1 = new Player(size, height * size, -390, 0, Color.FromName(doc.GetElementsByTagName("player_one").Item(0).ChildNodes.Item(1).InnerText));
                height = Convert.ToInt32(doc.GetElementsByTagName("player_two").Item(0).ChildNodes.Item(0).InnerText);
                player2 = new Player(size, height * size, 375, 0, Color.FromName(doc.GetElementsByTagName("player_two").Item(0).ChildNodes.Item(1).InnerText));
                height = Convert.ToInt32(doc.GetElementsByTagName("ball").Item(0).ChildNodes.Item(0).InnerText);
                ball = new Ball(0, 0, height*size, Color.FromName(doc.GetElementsByTagName("ball").Item(0).ChildNodes.Item(1).InnerText), 9);
            }
            else {

                player1 = new Player(size, 8 * size, -390, 0, Color.Red);
                player2 = new Player(size, 8 * size, 375, 0, Color.Blue);
                ball = new Ball(0, 0, size, Color.Green, 9);
            }



            timer1.Start();
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            restart();
            draw();
            
        }

        private void restart() {
            Random rnd = new Random();
            int[] mas = new int[2];
            mas[0] = rnd.Next(-13, -3);
            mas[1] = rnd.Next(3, 13);
            double speedx = (double)mas[rnd.Next(0, 2)]/10;
            ball.SPEEDX = ball.SPEEDY * speedx;
            if (rnd.Next(0, 2) == 0)
                ball.SPEEDY *= -1; 
            player1.setStart();
            player2.setStart();
            ball.setStart();
            ball.SPEEDX *= 1.3; 
        }

        //функция отрисовки
        private void draw() {
            g.Clear(Color.White);//очистка поля
            ball.hittest(player1, player2);
            Pen p = new Pen(Color.Green, 1);
            SolidBrush b = new SolidBrush(Color.Red);
            player1.draw();
            player2.draw();
            ball.Draw();
            
        }



        bool player1_top, player1_bottom;
        bool player2_top, player2_bottom;
        


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (player1_top)
            {
                player1.move(-10);
            }
            if (player1_bottom)
            {
                player1.move(10);
            }

            if (player2_top)
            {
                player2.move(-10);
            }
            if (player2_bottom)
            {
                player2.move(10);
            }
            ball.move();
            int loose = ball.loose();
                if (loose == 1){
                    label2.Text = Convert.ToString(Convert.ToInt16(label2.Text)+1);
                    restart();
                }
                if (loose == 2){
                    label1.Text = Convert.ToString(Convert.ToInt16(label1.Text) + 1);
                    restart();
                }
            draw();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Space) ball.startMove();
                if (e.KeyCode == Keys.R) restart();

                if (e.KeyCode == Keys.W) player1_top = true;
                if (e.KeyCode == Keys.Up) player2_top = true;

                if (e.KeyCode == Keys.S) player1_bottom = true;
                if (e.KeyCode == Keys.Down) player2_bottom = true;
            }
            catch (NullReferenceException) { }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) player1_top = false;
            if (e.KeyCode == Keys.Up) player2_top = false;

            if (e.KeyCode == Keys.S) player1_bottom = false;
            if (e.KeyCode == Keys.Down) player2_bottom = false;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
