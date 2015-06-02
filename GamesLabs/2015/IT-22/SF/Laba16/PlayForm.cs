using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml;
using System.IO;
using System.Xml.Schema;

namespace Laba13
{
    public partial class PlayForm : Form
    {
        Player playerLeft;
        Player playerRight;

        public PlayForm()
        {
            InitializeComponent();
        }

        //Работа с формой
        #region PlayForm

        bool place = false;
        int x, y;

        private void PlayForm_MouseDown(object sender, MouseEventArgs e)
        {
            place = true;
            x = e.X;
            y = e.Y;
        }

        private void PlayForm_MouseUp(object sender, MouseEventArgs e)
        {
            place = false;
        }

        private void PlayForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (place == true)
            {
                int x0 = this.Location.X + e.X - x;
                int y0 = this.Location.Y + e.Y - y;
                this.Location = new Point(x0, y0);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        #endregion

        static bool valid = true;

        public static void ValidationHandler(object sender, ValidationEventArgs args)
        {
            MessageBox.Show(args.Severity + ":" + args.Message + "\nЗагружены стандартные параметры");
            valid = false;
        }

        private void PlayForm_Load(object sender, EventArgs e)
        {
            //Загрузка компонентов
            string fieldColor = "#FF5F9EA0";
            string missColor = "#FF0000FF";
            string popColor = "#FFFFFF00";
            string killColor = "#FFFF0000";
            int startNumber = 0;
            int bgImage = 0;

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
                    if (reader.Name == "fieldColor" && reader.NodeType != XmlNodeType.EndElement)
                        fieldColor = reader.ReadElementContentAsString();
                    if (reader.Name == "missColor" && reader.NodeType != XmlNodeType.EndElement)
                        missColor = reader.ReadElementContentAsString();
                    if (reader.Name == "popColor" && reader.NodeType != XmlNodeType.EndElement)
                        popColor = reader.ReadElementContentAsString();
                    if (reader.Name == "killColor" && reader.NodeType != XmlNodeType.EndElement)
                        killColor = reader.ReadElementContentAsString();
                    if (reader.Name == "startNumber" && reader.NodeType != XmlNodeType.EndElement)
                        startNumber = reader.ReadElementContentAsInt();
                    if (reader.Name == "bgImage" && reader.NodeType != XmlNodeType.EndElement)
                        bgImage = reader.ReadElementContentAsInt();
                }
                reader.Close();
            }

            //Установка фона
            if (bgImage == 0)
                this.BackgroundImage = Image.FromFile("0.png");
            else if (bgImage == 1)
                this.BackgroundImage = Image.FromFile("1.png");
            else if (bgImage == 2)
                this.BackgroundImage = Image.FromFile("2.png");
            else
                this.BackgroundImage = Image.FromFile("0.png");

            //Создание игроков
            playerLeft = new Player(fieldColor, missColor, popColor, killColor);
            playerRight = new Player(fieldColor, missColor, popColor, killColor);
            
            //Выбор первого ходящего
            if (startNumber == 1) //Первый игрок
                playerLeft.State = true;
            else if (startNumber == 2) //Второй игрок
                playerRight.State = true;
            else //Случайный выбор
            {
                Random random = new Random();
                int rand = random.Next(0, 100);
                if (rand < 25 || rand > 75)
                    playerLeft.State = true;
                else
                    playerRight.State = true;
            }
        }

        //Перерисовка левого поля
        private void pictureBoxLeft_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int i = 1; i < 11; i++)
                for (int j = 1; j < 11; j++)
                    g.FillRectangle(playerLeft[i, j].SolidBrush, playerLeft[i, j].X, playerLeft[i, j].Y, 30, 30);
        }

        //Перерисовка правого поля
        private void pictureBoxRight_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int i = 1; i < 11; i++)
                for (int j = 1; j < 11; j++)
                    g.FillRectangle(playerRight[i, j].SolidBrush, playerRight[i, j].X, playerRight[i, j].Y, 30, 30);
        }

        //Перерисовка состояния хода
        private void PlayForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            SolidBrush brushLeft = new SolidBrush(Color.White);
            SolidBrush brushRight = new SolidBrush(Color.White);
            if (playerLeft.State && !playerRight.State)
            {
                brushLeft.Color = Color.Green;
                brushRight.Color = Color.Red;
            }
            if (!playerLeft.State && playerRight.State)
            {
                brushLeft.Color = Color.Red;
                brushRight.Color = Color.Green;
            }

            g.FillRectangle(brushLeft, 80, 6, 100, 15);
            g.FillRectangle(brushRight, 450, 6, 100, 15);
        }

        //Атака по левому
        private void pictureBoxLeft_MouseClick(object sender, MouseEventArgs e)
        {
            if(playerLeft.State)
            {
                playerRight.State = playerLeft.Fire(e.X, e.Y);

                pictureBoxLeft.Invalidate();
                this.Invalidate();

                if (playerLeft.Win(1))
                    this.Close();
            }
        }

        //Атака по правому
        private void pictureBoxRight_MouseClick(object sender, MouseEventArgs e)
        {
            if(playerRight.State)
            {
                playerLeft.State = playerRight.Fire(e.X, e.Y);

                pictureBoxRight.Invalidate();
                this.Invalidate();

                if (playerRight.Win(2))
                    this.Close();
            }
        }
    }
}
