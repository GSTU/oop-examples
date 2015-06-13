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

namespace PingPongGame
{
    public partial class Form1 : Form
    {
        public int speed_left = 4;                    // начальная скорость
        public int speed_top = 4;
        public int points = 0;                        // начальный счет

        public Form1()
        {
            InitializeComponent();

            timer1.Enabled = true;
            Cursor.Hide();                                             // прячим курсор

            this.FormBorderStyle = FormBorderStyle.None;               // прячит другие границы
            this.TopMost = true;                                       // на передний план
            this.Bounds = Screen.PrimaryScreen.Bounds;                 // делает во весь экран 

            stick.Top = playground.Bottom  - (playground.Bottom / 20); // устанавливает позицию "бревнышка"
 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            stick.Left = Cursor.Position.X - (stick.Width / 2);        // устанавливает по центру курсор на "бревнышке"
            gameover_lbl.Visible = false;
            ball.Left += speed_left;                                   // движения "мяча" 
            ball.Top += speed_top;


            if (ball.Bottom >= stick2.Top && ball.Bottom <= stick2.Bottom + 20 && ball.Left >= stick2.Left && ball.Right <= stick2.Right) // коллизии верхнего "бруска"
            {
                speed_top -= 1;
                speed_left -= 1;
                speed_top = -speed_top;
                points += 1;
                points_lbl.Text = points.ToString();

                //Random r = new Random();
                //playground.BackColor = Color.FromArgb(r.Next(150, 255), r.Next(150, 255), r.Next(150, 255)); // рандомный фон после очередного набранного очка
            }
            

            if (ball.Bottom <= stick2.Top)
            
            {
                
                timer1.Enabled = false;                      // остановить игру,когда мяч не в игре 
                gameover_lbl.Visible = true;                 // показать меню

            }

            if (ball.Bottom >= stick.Top && ball.Bottom <= stick.Bottom && ball.Left >= stick.Left && ball.Right <= stick.Right) // коллизии нижниге "бруска"
            {
                speed_top += 1;
                speed_left += 1;
                speed_top = -speed_top;
                points += 1;
                points_lbl.Text = points.ToString();

                //Random r = new Random();
                //playground.BackColor = Color.FromArgb(r.Next(150,255),r.Next(150,255),r.Next(150,255)); // рандомный фон после очередного набранного очка
            }

            if(ball.Left <= playground.Left)
            {
                speed_left = -speed_left;
            }
            if (ball.Right >= playground.Right)
            {
                speed_left = -speed_left;
            }
            if (ball.Top <= playground.Top)
            {
                speed_top = -speed_top;
            }
            if (ball.Bottom >= playground.Bottom)
            {
                timer1.Enabled = false;                      // остановить игру,когда мяч не в игре 
                gameover_lbl.Visible = true;                 // показать меню               
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)

        {
            if (e.KeyCode == Keys.Right) { stick2.Left += 100; }   // движение верхнего "бруска" на кнопки
            if (e.KeyCode == Keys.Left) { stick2.Left -= 100; }
            if (e.KeyCode == Keys.A) { stick2.Left -= 75; }
            if (e.KeyCode == Keys.D) { stick2.Left += 75; }
            if (e.KeyCode == Keys.Escape) { this.Close();}         // выход из игры
            if (e.KeyCode == Keys.F1)                              // перезапуск игры
            {
                ball.Top = 50;
                ball.Left = 50;
                speed_left = 4;
                speed_top = 4;
                points = 0;
                points_lbl.Text = "0";
                timer1.Enabled = true;
                gameover_lbl.Visible = false;
                playground.BackColor = Color.White;
            }

        }

        private void point_lbl_Click(object sender, EventArgs e)
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
            //Загрузка стандартных компонентов
            int score = 0;
            string backColor = "#FF5F9EA0";
            string b1 = "#FF0000FF";
            string b2 = "#FFFFFF00";

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
                    if (reader.Name == "score" && reader.NodeType != XmlNodeType.EndElement)
                        score = reader.ReadElementContentAsInt();
                    if (reader.Name == "b1" && reader.NodeType != XmlNodeType.EndElement)
                        b1 = reader.ReadElementContentAsString();
                    if (reader.Name == "b2" && reader.NodeType != XmlNodeType.EndElement)
                        b2 = reader.ReadElementContentAsString();
                }
                reader.Close();
            }

            points = score;
            points_lbl.Text = points.ToString();

            playground.BackColor = ColorTranslator.FromHtml(backColor);
            stick.BackColor = ColorTranslator.FromHtml(b1);
            stick2.BackColor = ColorTranslator.FromHtml(b2);
        }
    }
}
