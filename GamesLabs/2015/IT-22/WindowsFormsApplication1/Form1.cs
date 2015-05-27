using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Xml;
using System.Xml.Schema;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int element = 1;
        int[,] arr = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        double msek = 0;
        int sek = 0;
        bool bDragStatys;
        Point ClickPoint;
        int MouseX, MouseY;
        int xx, yy;
        string path1, path2, level, xod;
        static bool flag = true;
        public Form1()
        {
            InitializeComponent();
            this.timer2_GameMetod.Stop();
            this.TransparencyKey = Color.White;
            this.BackgroundImage = new Bitmap("FonSetka.png");
            this.menuStrip1.BackgroundImage = new Bitmap("FonSetka.png");
            this.label1.Image = new Bitmap("fonTimer.png");
            this.pictureBox1.Image = new Bitmap("Close.png");
            this.pictureBox2.Image = new Bitmap("Svernut.png");
            this.label2.Image = new Bitmap("fonLabel.png");
            List<Exception> exp = new List<Exception>();
            try
            {
                XmlReaderSettings xmlreadersettings = new XmlReaderSettings();
                xmlreadersettings.Schemas.Add(null,"xsd.xsd");
                xmlreadersettings.ValidationType = ValidationType.Schema;
                xmlreadersettings.ValidationEventHandler += new ValidationEventHandler(xmlreadersettingsValidationEventHandler);
                XmlReader xmlreader = XmlReader.Create("xml.xml", xmlreadersettings);
                while (xmlreader.Read()) {}
                if (flag == true)
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load("xml.xml");
                    xx = Convert.ToInt32(xmldoc.DocumentElement.SelectSingleNode("size").FirstChild.InnerText);
                    yy = Convert.ToInt32(xmldoc.DocumentElement.SelectSingleNode("size").LastChild.InnerText);
                    path1 = xmldoc.DocumentElement.SelectSingleNode("pathKrest").InnerText;
                    path2 = xmldoc.DocumentElement.SelectSingleNode("pathNol").InnerText;
                    level = xmldoc.DocumentElement.SelectSingleNode("level").InnerText;
                    xod = xmldoc.DocumentElement.SelectSingleNode("xod").InnerText;
                    if (level == "Слабый")
                    {
                        слабыйToolStripMenuItem1.CheckState = CheckState.Checked;
                        сильныйToolStripMenuItem1.CheckState = CheckState.Unchecked;
                    }
                    else
                    {
                        сильныйToolStripMenuItem1.CheckState = CheckState.Checked;
                        слабыйToolStripMenuItem1.CheckState = CheckState.Unchecked;
                    }
                    if (xod == "Человек")
                    {
                        element = 2;
                        ходToolStripMenuItem.CheckState = CheckState.Checked;
                        интеллектИИToolStripMenuItem.CheckState = CheckState.Unchecked;
                    }
                    else
                    {
                        element = 1;
                        интеллектИИToolStripMenuItem.CheckState = CheckState.Checked;
                        ходToolStripMenuItem.CheckState = CheckState.Unchecked;
                    }
                }
                else
                {
                    xx = 50;
                    yy = 50;
                    path1 = "Krest.png";
                    path2 = "Nol.png";
                }
            }
            catch (Exception exception)
            {
                xx = 50;
                yy = 50;
                path1 = "Krest.png";
                path2 = "Nol.png";
                MessageBox.Show("Ошибка: " + exception.Message);
            }
            this.timer2_GameMetod.Start();    
        }
        static void xmlreadersettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            flag = false;
            if (e.Severity == XmlSeverityType.Warning)
            {
                MessageBox.Show("Внимание: " + e.Message);
                
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                MessageBox.Show("Ошибка: " + e.Message);
            }
        }

        private void LineGor(int x, int y)
        {
            Graphics g = CreateGraphics();
            g.SmoothingMode = SmoothingMode.HighQuality;
            Image i = new Bitmap("LineGor.png");
            g.DrawImage(i, x, y, 150, 50);
            g.Dispose();
        }

        private void LineVer(int x, int y)
        {
            Graphics g = CreateGraphics();
            g.SmoothingMode = SmoothingMode.HighQuality;
            Image i = new Bitmap("LineVer.png");
            g.DrawImage(i, x, y, 50, 150);
            g.Dispose();
        }

        private void Line45(int x, int y)
        {
            Graphics g = CreateGraphics();
            g.SmoothingMode = SmoothingMode.HighQuality;
            Image i = new Bitmap("Line45.png");
            g.DrawImage(i, x, y, 150, 150);
            g.Dispose();
        }

        private void Line_45(int x, int y)
        {
            Graphics g = CreateGraphics();
            g.SmoothingMode = SmoothingMode.HighQuality;
            Image i = new Bitmap("Line-45.png");
            g.DrawImage(i, x, y, 150, 150);
            g.Dispose();
        }

        private void Setka(int x, int y)
        {
            Graphics g = CreateGraphics();
            g.SmoothingMode = SmoothingMode.HighQuality;
            Image i = new Bitmap("Setca.png");
            g.DrawImage(i, x, y, 150, 150);
            g.Dispose();
        }

        private void Krestic(int x, int y)
        {
            Graphics g = CreateGraphics();
            g.SmoothingMode = SmoothingMode.HighQuality;
            Image i = new Bitmap(path1);
            g.DrawImage(i, x, y, xx, xx);
            g.Dispose();
        }

        private void Nolic(int x, int y)
        {
            Graphics g = CreateGraphics();
            g.SmoothingMode = SmoothingMode.HighQuality;
            Image i = new Bitmap(path2);
            g.DrawImage(i, x, y, xx, yy);
            g.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NewGame();
        }

        private int Proverka() // Метод проверки возвращает 0 при неудачной проверке, 1 при выйгрыше крестиков, 2 при выйгрыше ноликов, 3 при ничейном результате
        {
            // Проверка крестиков по горизонтали
            if (arr[0, 0] == 1 && arr[0, 1] == 1 && arr[0, 2] == 1)
            {
                LineGor(50, 50);
                return 1;
            }
            if (arr[1, 0] == 1 && arr[1, 1] == 1 && arr[1, 2] == 1)
            {
                LineGor(50, 100);
                return 1;
            }
            if (arr[2, 0] == 1 && arr[2, 1] == 1 && arr[2, 2] == 1)
            {
                LineGor(50, 150);
                return 1;
            }
            // Проверка крестиков по вертикали
            if (arr[0, 0] == 1 && arr[1, 0] == 1 && arr[2, 0] == 1)
            {
                LineVer(50, 50);
                return 1;
            }
            if (arr[0, 1] == 1 && arr[1, 1] == 1 && arr[2, 1] == 1)
            {
                LineVer(100, 50);
                return 1;
            }
            if (arr[0, 2] == 1 && arr[1, 2] == 1 && arr[2, 2] == 1)
            {
                LineVer(150, 50);
                return 1;
            }
            // Проверка крестиков по кресту
            if (arr[0, 0] == 1 && arr[1, 1] == 1 && arr[2, 2] == 1)
            {
                Line_45(50, 50);
                return 1;
            }
            if (arr[0, 2] == 1 && arr[1, 1] == 1 && arr[2, 0] == 1)
            {
                Line45(50, 50);
                return 1;
            }
            // Проверка ноликов по горизонтали
            if (arr[0, 0] == 2 && arr[0, 1] == 2 && arr[0, 2] == 2)
            {
                LineGor(50, 50);
                return 2;
            }
            if (arr[1, 0] == 2 && arr[1, 1] == 2 && arr[1, 2] == 2)
            {
                LineGor(50, 100);
                return 2;
            }
            if (arr[2, 0] == 2 && arr[2, 1] == 2 && arr[2, 2] == 2)
            {
                LineGor(50, 150);
                return 2;
            }
            // Проверка ноликов по вертикали
            if (arr[0, 0] == 2 && arr[1, 0] == 2 && arr[2, 0] == 2)
            {
                LineVer(50, 50);
                return 2;
            }
            if (arr[0, 1] == 2 && arr[1, 1] == 2 && arr[2, 1] == 2)
            {
                LineVer(100, 50);
                return 2;
            }
            if (arr[0, 2] == 2 && arr[1, 2] == 2 && arr[2, 2] == 2)
            {
                LineVer(150, 50);
                return 2;
            }
            // Проверка ноликов по кресту
            if (arr[0, 0] == 2 && arr[1, 1] == 2 && arr[2, 2] == 2)
            {
                Line_45(50, 50);
                return 2;
            }
            if (arr[0, 2] == 2 && arr[1, 1] == 2 && arr[2, 0] == 2)
            {
                Line45(50, 50);
                return 2;
            }
            if (arr[0, 0] != 0 && arr[0, 1] != 0 && arr[0, 2] != 0 && arr[1, 0] != 0 && arr[1, 1] != 0 && arr[1, 2] != 0 && arr[2, 0] != 0 && arr[2, 1] != 0 && arr[2, 2] != 0)
            {
                return 3; //Ничья
            }
            else
            {
                return 0;
            }
        }

        private void NewGame()
        {
            timer2_GameMetod.Stop();
            MouseX = 0;
            MouseY = 0;
            msek = 0;
            sek = 0;
            timer1.Start();
            timer2_GameMetod.Start();
            this.BackgroundImage = new Bitmap("FonSetka.png");
            int stroka = arr.GetLength(0), stolbec = arr.GetLength(1);
            for (int i = 0; i < stroka; i++)
            {
                for (int j = 0; j < stolbec; j++)
                {
                    arr[i, j] = 0;
                }
            }
            if (ходToolStripMenuItem.CheckState == CheckState.Checked)
            {
                element = 2;
            }
            if (интеллектИИToolStripMenuItem.CheckState == CheckState.Checked)
            {
                element = 1;
            }
        }

        private void выходToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            timer2.Enabled = true;
        }

        private void новаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void Form1_MouseDown_1(object sender, MouseEventArgs e)
        {
            /* if (e.X > 50 && e.X < 100) MouseX = 50;
             if (e.X > 100 && e.X < 150) MouseX = 100;
             if (e.X > 150 && e.X < 200) MouseX = 150;

             if (e.Y > 50 && e.Y < 100) MouseY = 50;
             if (e.Y > 100 && e.Y < 150) MouseY = 100;
             if (e.Y > 150 && e.Y < 200) MouseY = 150;*/
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            msek++;
            if (msek == 9)
            {
                msek = 0;
                sek++;
            }
            label1.Text = " " + Convert.ToString(sek) + "." + Convert.ToString(msek);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer2.Enabled = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                bDragStatys = true;
                ClickPoint = new Point(e.X, e.Y);
            }
            else
            {
                bDragStatys = false;
            }
        }

        private void menuStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            if (bDragStatys)
            {
                Point PointMovieTo;
                PointMovieTo = this.PointToScreen(new Point(e.X, e.Y));
                PointMovieTo.Offset(-ClickPoint.X, -ClickPoint.Y);
                this.Location = PointMovieTo;
            }
        }

        private void menuStrip1_MouseUp(object sender, MouseEventArgs e)
        {
            bDragStatys = false;
        }

        private void AiLow() // Компьютер ходит ноликами = 2
        {
            if (arr[1, 1] == 0 && element == 1) // Слабый искусств. интеллект ставит нолик в центр если он свободен
            {
                arr[1, 1] = 2;
                Nolic(100, 100);
                element = 2;
            }
            if (element == 1)
            {
                Random rnd = new Random();
                int i = rnd.Next(0, 3);
                int j = rnd.Next(0, 3);
                int x = 0;
                int y = 0;
                if (i == 0) x = 50;
                if (i == 1) x = 100;
                if (i == 2) x = 150;
                if (j == 0) y = 50;
                if (j == 1) y = 100;
                if (j == 2) y = 150;

                if (arr[j, i] == 0)
                {
                    arr[j, i] = 2;
                    Nolic(x, y);
                    element = 2; // Компьютер нажал нолик и ход передается человеку
                }
            }
        }

        private void AiHard() // Компьютер ходит ноликами = 2
        {
            if (element == 1) // Если компьютер ходит первым если хода нет в базе, то ходит слабый искусств. интеллект
            { // Алгоритм если компьютер ходит первым
                // Алгоритм выйгрыша по вертикали и горизонтали
                if (arr[0, 0] == 2 && arr[1, 1] == 2 && arr[2, 2] == 0 && element == 1)
                { // Алгоритм выйгрыша
                    arr[2, 2] = 2;
                    Nolic(150, 150);
                    element = 2;
                }
                if (arr[0, 0] == 0 && arr[1, 1] == 2 && arr[2, 2] == 2 && element == 1)
                { // Алгоритм выйгрыша
                    arr[0, 0] = 2;
                    Nolic(50, 50);
                    element = 2;
                }
                if (arr[2, 0] == 2 && arr[1, 1] == 2 && arr[0, 2] == 0 && element == 1)
                { // Алгоритм выйгрыша
                    arr[0, 2] = 2;
                    Nolic(150, 50);
                    element = 2;
                }
                if (arr[0, 2] == 2 && arr[1, 1] == 2 && arr[2, 0] == 0 && element == 1)
                { // Алгоритм выйгрыша
                    arr[2, 0] = 2;
                    Nolic(50, 150);
                    element = 2;
                }
                if (arr[0, 0] == 2 && arr[0, 1] == 2 && arr[0, 2] == 0 && element == 1)
                { // Алгоритм выйгрыша
                    arr[0, 2] = 2;
                    Nolic(150, 50);
                    element = 2;
                }
                if (arr[0, 2] == 2 && arr[0, 1] == 2 && arr[0, 0] == 0 && element == 1)
                { // Алгоритм выйгрыша
                    arr[0, 0] = 2;
                    Nolic(50, 50);
                    element = 2;
                }
                if (arr[1, 0] == 2 && arr[1, 1] == 2 && arr[1, 2] == 0 && element == 1)
                { // Алгоритм выйгрыша
                    arr[1, 2] = 2;
                    Nolic(150, 100);
                    element = 2;
                }
                if (arr[1, 0] == 0 && arr[1, 1] == 2 && arr[1, 2] == 2 && element == 1)
                { // Алгоритм выйгрыша
                    arr[1, 0] = 2;
                    Nolic(50, 100);
                    element = 2;
                }
                if (arr[2, 0] == 2 && arr[2, 1] == 2 && arr[2, 2] == 0 && element == 1)
                { // Алгоритм выйгрыша
                    arr[2, 2] = 2;
                    Nolic(150, 150);
                    element = 2;
                }
                if (arr[2, 2] == 2 && arr[2, 1] == 2 && arr[2, 0] == 0 && element == 1)
                { // Алгоритм выйгрыша
                    arr[2, 0] = 2;
                    Nolic(50, 150);
                    element = 2;
                }
                if (arr[0, 0] == 2 && arr[1, 0] == 2 && arr[2, 0] == 0 && element == 1)
                { // Алгоритм выйгрыша
                    arr[2, 0] = 2;
                    Nolic(50, 150);
                    element = 2;
                }
                if (arr[2, 0] == 2 && arr[1, 0] == 2 && arr[0, 0] == 0 && element == 1)
                { // Алгоритм выйгрыша
                    arr[0, 0] = 2;
                    Nolic(50, 50);
                    element = 2;
                }
                if (arr[0, 1] == 2 && arr[1, 1] == 2 && arr[2, 1] == 0 && element == 1)
                { // Алгоритм выйгрыша
                    arr[2, 1] = 2;
                    Nolic(100, 150);
                    element = 2;
                }
                if (arr[2, 1] == 2 && arr[1, 1] == 2 && arr[0, 1] == 0 && element == 1)
                { // Алгоритм выйгрыша
                    arr[0, 1] = 2;
                    Nolic(100, 50);
                    element = 2;
                }
                if (arr[0, 2] == 2 && arr[1, 2] == 2 && arr[2, 2] == 0 && element == 1)
                { // Алгоритм выйгрыша
                    arr[2, 2] = 2;
                    Nolic(150, 150);
                    element = 2;
                }
                if (arr[2, 2] == 2 && arr[1, 2] == 2 && arr[0, 2] == 0 && element == 1)
                { // Алгоритм выйгрыша
                    arr[0, 2] = 2;
                    Nolic(150, 50);
                    element = 2;
                }
                // Конец алгоритма выйгрыша по вертикали и горизонтали
                if (arr[0, 0] == 0 && arr[0, 1] == 0 && arr[0, 2] == 0 && arr[1, 0] == 0 && arr[1, 1] == 0 && arr[1, 2] == 0 && arr[2, 0] == 0 && arr[2, 1] == 0 && arr[2, 2] == 0 && element == 1)
                { // Первый ход 1 вариант
                    arr[1, 1] = 2;
                    Nolic(100, 100);
                    element = 2;
                }
                if (arr[0, 0] == 1 && arr[0, 1] == 0 && arr[0, 2] == 0 && arr[1, 0] == 0 && arr[1, 1] == 2 && arr[1, 2] == 0 && arr[2, 0] == 0 && arr[2, 1] == 0 && arr[2, 2] == 0 && element == 1)
                { // 2 ход 1 вариант
                    arr[0, 2] = 2;
                    Nolic(150, 50);
                    element = 2;
                }
                if (arr[0, 0] == 0 && arr[0, 1] == 0 && arr[0, 2] == 1 && arr[1, 0] == 0 && arr[1, 1] == 2 && arr[1, 2] == 0 && arr[2, 0] == 0 && arr[2, 1] == 0 && arr[2, 2] == 0 && element == 1)
                { // 2 ход 1 вариант
                    arr[2, 2] = 2;
                    Nolic(150, 150);
                    element = 2;
                }
                if (arr[0, 0] == 0 && arr[0, 1] == 0 && arr[0, 2] == 0 && arr[1, 0] == 0 && arr[1, 1] == 2 && arr[1, 2] == 0 && arr[2, 0] == 0 && arr[2, 1] == 0 && arr[2, 2] == 1 && element == 1)
                { // 2 ход 1 вариант
                    arr[2, 0] = 2;
                    Nolic(50, 150);
                    element = 2;
                }
                if (arr[0, 0] == 0 && arr[0, 1] == 0 && arr[0, 2] == 0 && arr[1, 0] == 0 && arr[1, 1] == 2 && arr[1, 2] == 0 && arr[2, 0] == 1 && arr[2, 1] == 0 && arr[2, 2] == 0 && element == 1)
                { // 2 ход 1 вариант
                    arr[0, 0] = 2;
                    Nolic(50, 50);
                    element = 2;
                }
                if (arr[0, 0] == 1 && arr[0, 1] == 0 && arr[0, 2] == 2 && arr[1, 0] == 0 && arr[1, 1] == 2 && arr[1, 2] == 0 && arr[2, 0] == 1 && arr[2, 1] == 0 && arr[2, 2] == 0 && element == 1)
                { // 3 ход 1 вариант
                    arr[1, 0] = 2;
                    Nolic(50, 100);
                    element = 2;
                }
                if (arr[0, 0] == 1 && arr[0, 1] == 0 && arr[0, 2] == 1 && arr[1, 0] == 0 && arr[1, 1] == 2 && arr[1, 2] == 0 && arr[2, 0] == 0 && arr[2, 1] == 0 && arr[2, 2] == 2 && element == 1)
                { // 3 ход 1 вариант
                    arr[0, 1] = 2;
                    Nolic(100, 50);
                    element = 2;
                }
                if (arr[0, 0] == 0 && arr[0, 1] == 0 && arr[0, 2] == 1 && arr[1, 0] == 0 && arr[1, 1] == 2 && arr[1, 2] == 0 && arr[2, 0] == 2 && arr[2, 1] == 0 && arr[2, 2] == 1 && element == 1)
                { // 3 ход 1 вариант
                    arr[1, 2] = 2;
                    Nolic(150, 100);
                    element = 2;
                }
                if (arr[0, 0] == 2 && arr[0, 1] == 0 && arr[0, 2] == 0 && arr[1, 0] == 0 && arr[1, 1] == 2 && arr[1, 2] == 0 && arr[2, 0] == 1 && arr[2, 1] == 0 && arr[2, 2] == 1 && element == 1)
                { // 3 ход 1 вариант
                    arr[2, 1] = 2;
                    Nolic(100, 150);
                    element = 2;
                }
                if (arr[0, 0] == 1 && arr[0, 1] == 0 && arr[0, 2] == 2 && arr[1, 0] == 2 && arr[1, 1] == 2 && arr[1, 2] == 1 && arr[2, 0] == 1 && arr[2, 1] == 0 && arr[2, 2] == 0 && element == 1)
                { // 4 ход 1 вариант
                    arr[2, 2] = 2;
                    Nolic(150, 150);
                    element = 2;
                }
                if (arr[0, 0] == 1 && arr[0, 1] == 2 && arr[0, 2] == 1 && arr[1, 0] == 0 && arr[1, 1] == 2 && arr[1, 2] == 0 && arr[2, 0] == 0 && arr[2, 1] == 1 && arr[2, 2] == 2 && element == 1)
                { // 4 ход 1 вариант
                    arr[2, 0] = 2;
                    Nolic(50, 150);
                    element = 2;
                    MessageBox.Show("5");
                }
                if (arr[0, 0] == 0 && arr[0, 1] == 0 && arr[0, 2] == 1 && arr[1, 0] == 1 && arr[1, 1] == 2 && arr[1, 2] == 2 && arr[2, 0] == 2 && arr[2, 1] == 0 && arr[2, 2] == 1 && element == 1)
                { // 4 ход 1 вариант
                    arr[0, 0] = 2;
                    Nolic(50, 50);
                    element = 2;
                }
                if (arr[0, 0] == 2 && arr[0, 1] == 1 && arr[0, 2] == 0 && arr[1, 0] == 0 && arr[1, 1] == 2 && arr[1, 2] == 0 && arr[2, 0] == 1 && arr[2, 1] == 2 && arr[2, 2] == 1 && element == 1)
                { // 4 ход 1 вариант
                    arr[0, 2] = 2;
                    Nolic(150, 50);
                    element = 2;
                }
                if (arr[0, 0] == 1 && arr[0, 1] == 0 && arr[0, 2] == 2 && arr[1, 0] == 2 && arr[1, 1] == 2 && arr[1, 2] == 1 && arr[2, 0] == 1 && arr[2, 1] == 1 && arr[2, 2] == 2 && element == 1)
                { // 5 ход 1 вариант
                    arr[0, 1] = 2;
                    Nolic(100, 50);
                    element = 2;
                }
                if (arr[0, 0] == 1 && arr[0, 1] == 2 && arr[0, 2] == 1 && arr[1, 0] == 1 && arr[1, 1] == 2 && arr[1, 2] == 0 && arr[2, 0] == 2 && arr[2, 1] == 1 && arr[2, 2] == 2 && element == 1)
                { // 5 ход 1 вариант
                    arr[1, 2] = 2;
                    Nolic(150, 100);
                    element = 2;
                }
                if (arr[0, 0] == 2 && arr[0, 1] == 0 && arr[0, 2] == 1 && arr[1, 0] == 1 && arr[1, 1] == 2 && arr[1, 2] == 2 && arr[2, 0] == 2 && arr[2, 1] == 1 && arr[2, 2] == 1 && element == 1)
                { // 5 ход 1 вариант
                    arr[0, 1] = 2;
                    Nolic(100, 50);
                    element = 2;
                }
                if (arr[0, 0] == 2 && arr[0, 1] == 1 && arr[0, 2] == 2 && arr[1, 0] == 0 && arr[1, 1] == 2 && arr[1, 2] == 1 && arr[2, 0] == 1 && arr[2, 1] == 2 && arr[2, 2] == 1 && element == 1)
                { // 5 ход 1 вариант
                    arr[1, 0] = 2;
                    Nolic(50, 100);
                    element = 2;
                }
                if (arr[0, 0] == 1 && arr[0, 1] == 1 && arr[0, 2] == 2 && arr[1, 0] == 2 && arr[1, 1] == 2 && arr[1, 2] == 1 && arr[2, 0] == 1 && arr[2, 1] == 0 && arr[2, 2] == 2 && element == 1)
                { // 6 ход 1 вариант
                    arr[2, 1] = 2;
                    Nolic(100, 150);
                    element = 2;
                }
                if (arr[0, 0] == 1 && arr[0, 1] == 2 && arr[0, 2] == 1 && arr[1, 0] == 0 && arr[1, 1] == 2 && arr[1, 2] == 1 && arr[2, 0] == 2 && arr[2, 1] == 1 && arr[2, 2] == 2 && element == 1)
                { // 6 ход 1 вариант
                    arr[1, 0] = 2;
                    Nolic(50, 100);
                    element = 2;
                }
                if (arr[0, 0] == 2 && arr[0, 1] == 1 && arr[0, 2] == 1 && arr[1, 0] == 1 && arr[1, 1] == 2 && arr[1, 2] == 2 && arr[2, 0] == 2 && arr[2, 1] == 0 && arr[2, 2] == 1 && element == 1)
                { // 6 ход 1 вариант
                    arr[2, 1] = 2;
                    Nolic(100, 150);
                    element = 2;
                }
                if (arr[0, 0] == 2 && arr[0, 1] == 1 && arr[0, 2] == 2 && arr[1, 0] == 1 && arr[1, 1] == 2 && arr[1, 2] == 0 && arr[2, 0] == 1 && arr[2, 1] == 2 && arr[2, 2] == 1 && element == 1)
                { // 6 ход 1 вариант
                    arr[1, 2] = 2;
                    Nolic(150, 100);
                    element = 2;
                }

                if (arr[0, 0] == 0 && arr[0, 1] == 0 && arr[0, 2] == 0 && arr[1, 0] == 1 && arr[1, 1] == 2 && arr[1, 2] == 0 && arr[2, 0] == 0 && arr[2, 1] == 0 && arr[2, 2] == 0 && element == 1)
                { // 1 ход 2 вариант
                    arr[0, 2] = 2;
                    Nolic(150, 50);
                    element = 2;
                }
                if (arr[0, 0] == 0 && arr[0, 1] == 1 && arr[0, 2] == 0 && arr[1, 0] == 0 && arr[1, 1] == 2 && arr[1, 2] == 0 && arr[2, 0] == 0 && arr[2, 1] == 0 && arr[2, 2] == 0 && element == 1)
                { // 2 ход 2 вариант
                    arr[2, 2] = 2;
                    Nolic(150, 150);
                    element = 2;
                }
                if (arr[0, 0] == 0 && arr[0, 1] == 0 && arr[0, 2] == 0 && arr[1, 0] == 0 && arr[1, 1] == 2 && arr[1, 2] == 1 && arr[2, 0] == 0 && arr[2, 1] == 0 && arr[2, 2] == 0 && element == 1)
                { // 3 ход 2 вариант
                    arr[2, 0] = 2;
                    Nolic(50, 150);
                    element = 2;
                }
                if (arr[0, 0] == 0 && arr[0, 1] == 0 && arr[0, 2] == 0 && arr[1, 0] == 0 && arr[1, 1] == 2 && arr[1, 2] == 0 && arr[2, 0] == 0 && arr[2, 1] == 1 && arr[2, 2] == 0 && element == 1)
                { // 4 ход 2 вариант
                    arr[0, 0] = 2;
                    Nolic(50, 50);
                    element = 2;
                }
                if (arr[0, 0] == 0 && arr[0, 1] == 0 && arr[0, 2] == 2 && arr[1, 0] == 1 && arr[1, 1] == 2 && arr[1, 2] == 0 && arr[2, 0] == 1 && arr[2, 1] == 0 && arr[2, 2] == 0 && element == 1)
                { // 1-2 ход 2 вариант
                    arr[0, 0] = 2;
                    Nolic(50, 50);
                    element = 2;
                }
                if (arr[0, 0] == 1 && arr[0, 1] == 1 && arr[0, 2] == 0 && arr[1, 0] == 0 && arr[1, 1] == 2 && arr[1, 2] == 0 && arr[2, 0] == 0 && arr[2, 1] == 0 && arr[2, 2] == 2 && element == 1)
                { // 2-2 ход 2 вариант
                    arr[0, 2] = 2;
                    Nolic(150, 50);
                    element = 2;
                }
                if (arr[0, 0] == 0 && arr[0, 1] == 0 && arr[0, 2] == 1 && arr[1, 0] == 0 && arr[1, 1] == 2 && arr[1, 2] == 1 && arr[2, 0] == 2 && arr[2, 1] == 0 && arr[2, 2] == 0 && element == 1)
                { // 3-2 ход 2 вариант
                    arr[2, 2] = 2;
                    Nolic(150, 150);
                    element = 2;
                }
                if (arr[0, 0] == 2 && arr[0, 1] == 0 && arr[0, 2] == 0 && arr[1, 0] == 0 && arr[1, 1] == 2 && arr[1, 2] == 0 && arr[2, 0] == 0 && arr[2, 1] == 1 && arr[2, 2] == 1 && element == 1)
                { // 4-2 ход 2 вариант
                    arr[2, 0] = 2;
                    Nolic(50, 150);
                    element = 2;
                } //Конец алгоритма если компьютер ходит первым
                if (arr[0, 0] == 0 && arr[0, 1] == 0 && arr[0, 2] == 0 && arr[1, 0] == 0 && arr[1, 1] == 1 && arr[1, 2] == 0 && arr[2, 0] == 0 && arr[2, 1] == 0 && arr[2, 2] == 0 && element == 1)
                { // Компьютер ходит вторым
                    arr[0, 0] = 2;
                    Nolic(50, 50);
                    element = 2;
                }
                if (arr[0, 0] == 2 && arr[0, 1] == 1 && arr[0, 2] == 0 && arr[1, 0] == 0 && arr[1, 1] == 1 && arr[1, 2] == 0 && arr[2, 0] == 0 && arr[2, 1] == 0 && arr[2, 2] == 0 && element == 1)
                { // Компьютер ходит вторым 1 ход
                    arr[2, 1] = 2;
                    Nolic(100, 150);
                    element = 2;
                }
                if (arr[0, 0] == 2 && arr[0, 1] == 0 && arr[0, 2] == 1 && arr[1, 0] == 0 && arr[1, 1] == 1 && arr[1, 2] == 0 && arr[2, 0] == 0 && arr[2, 1] == 0 && arr[2, 2] == 0 && element == 1)
                { // Компьютер ходит вторым 2 ход
                    arr[2, 0] = 2;
                    Nolic(50, 150);
                    element = 2;
                }
                if (arr[0, 0] == 2 && arr[0, 1] == 0 && arr[0, 2] == 0 && arr[1, 0] == 0 && arr[1, 1] == 1 && arr[1, 2] == 1 && arr[2, 0] == 0 && arr[2, 1] == 0 && arr[2, 2] == 0 && element == 1)
                { // Компьютер ходит вторым 3 ход
                    arr[1, 0] = 2;
                    Nolic(50, 100);
                    element = 2;
                }
                if (arr[0, 0] == 2 && arr[0, 1] == 0 && arr[0, 2] == 0 && arr[1, 0] == 0 && arr[1, 1] == 1 && arr[1, 2] == 0 && arr[2, 0] == 0 && arr[2, 1] == 0 && arr[2, 2] == 1 && element == 1)
                { // Компьютер ходит вторым 4 ход
                    arr[0, 1] = 2;
                    Nolic(100, 50);
                    element = 2;
                }
                if (arr[0, 0] == 2 && arr[0, 1] == 0 && arr[0, 2] == 0 && arr[1, 0] == 0 && arr[1, 1] == 1 && arr[1, 2] == 0 && arr[2, 0] == 0 && arr[2, 1] == 1 && arr[2, 2] == 0 && element == 1)
                { // Компьютер ходит вторым 5 ход
                    arr[0, 1] = 2;
                    Nolic(100, 50);
                    element = 2;
                }
                if (arr[0, 0] == 2 && arr[0, 1] == 0 && arr[0, 2] == 0 && arr[1, 0] == 0 && arr[1, 1] == 1 && arr[1, 2] == 0 && arr[2, 0] == 1 && arr[2, 1] == 0 && arr[2, 2] == 0 && element == 1)
                { // Компьютер ходит вторым 6 ход
                    arr[0, 2] = 2;
                    Nolic(150, 50);
                    element = 2;
                }
                if (arr[0, 0] == 2 && arr[0, 1] == 0 && arr[0, 2] == 0 && arr[1, 0] == 1 && arr[1, 1] == 1 && arr[1, 2] == 0 && arr[2, 0] == 0 && arr[2, 1] == 0 && arr[2, 2] == 0 && element == 1)
                { // Компьютер ходит вторым 7 ход
                    arr[1, 2] = 2;
                    Nolic(150, 100);
                    element = 2;
                }
                // Алгоритм блокировки крестиков
                if (arr[2, 2] == 1 && arr[1, 1] == 1 && arr[0, 0] == 0 && element == 1)
                {
                    arr[0, 0] = 2;
                    Nolic(50, 50);
                    element = 2;
                }
                if (arr[0, 0] == 1 && arr[1, 1] == 1 && arr[2, 2] == 0 && element == 1)
                {
                    arr[2, 2] = 2;
                    Nolic(150, 150);
                    element = 2;
                }
                if (arr[0, 2] == 1 && arr[1, 1] == 1 && arr[2, 0] == 0 && element == 1)
                {
                    arr[2, 0] = 2;
                    Nolic(50, 150);
                    element = 2;
                }
                if (arr[2, 0] == 1 && arr[1, 1] == 1 && arr[0, 2] == 0 && element == 1)
                {
                    arr[0, 2] = 2;
                    Nolic(150, 50);
                    element = 2;
                }
                if (arr[0, 0] == 1 && arr[0, 1] == 1 && arr[0, 2] == 0 && element == 1)
                {
                    arr[0, 2] = 2;
                    Nolic(150, 50);
                    element = 2;
                }
                if (arr[0, 2] == 1 && arr[0, 1] == 1 && arr[0, 0] == 0 && element == 1)
                {
                    arr[0, 0] = 2;
                    Nolic(50, 50);
                    element = 2;
                }
                if (arr[1, 0] == 1 && arr[1, 1] == 1 && arr[1, 2] == 0 && element == 1)
                {
                    arr[1, 2] = 2;
                    Nolic(150, 100);
                    element = 2;
                }
                if (arr[1, 2] == 1 && arr[1, 1] == 1 && arr[1, 0] == 0 && element == 1)
                {
                    arr[1, 0] = 2;
                    Nolic(50, 150);
                    element = 2;
                }
                if (arr[2, 0] == 1 && arr[2, 1] == 1 && arr[2, 2] == 0 && element == 1)
                {
                    arr[2, 2] = 2;
                    Nolic(150, 150);
                    element = 2;
                }
                if (arr[2, 2] == 1 && arr[2, 1] == 1 && arr[2, 0] == 0 && element == 1)
                {
                    arr[2, 0] = 2;
                    Nolic(50, 150);
                    element = 2;
                }
                if (arr[0, 0] == 1 && arr[1, 0] == 1 && arr[2, 0] == 0 && element == 1)
                {
                    arr[2, 0] = 2;
                    Nolic(50, 150);
                    element = 2;
                }
                if (arr[2, 0] == 1 && arr[1, 0] == 1 && arr[0, 0] == 0 && element == 1)
                {
                    arr[0, 0] = 2;
                    Nolic(50, 50);
                    element = 2;
                }
                if (arr[0, 1] == 1 && arr[1, 1] == 1 && arr[2, 1] == 0 && element == 1)
                {
                    arr[2, 1] = 2;
                    Nolic(100, 150);
                    element = 2;
                }
                if (arr[2, 1] == 1 && arr[1, 1] == 1 && arr[0, 1] == 0 && element == 1)
                {
                    arr[0, 1] = 2;
                    Nolic(100, 50);
                    element = 2;
                }
                if (arr[0, 2] == 1 && arr[1, 2] == 1 && arr[2, 2] == 0 && element == 1)
                {
                    arr[2, 2] = 2;
                    Nolic(150, 150);
                    element = 2;
                }
                if (arr[2, 2] == 1 && arr[1, 2] == 1 && arr[0, 2] == 0 && element == 1)
                {
                    arr[0, 2] = 2;
                    Nolic(150, 50);
                    element = 2;
                }
                if (arr[0, 0] == 1 && arr[1, 1] == 0 && arr[2, 2] == 1 && element == 1)
                {
                    arr[1, 1] = 2;
                    Nolic(100, 100);
                    element = 2;
                }
                if (arr[0, 2] == 1 && arr[1, 1] == 0 && arr[2, 0] == 1 && element == 1)
                {
                    arr[1, 1] = 2;
                    Nolic(100, 100);
                    element = 2;
                }
                if (arr[0, 0] == 1 && arr[0, 1] == 0 && arr[0, 2] == 1 && element == 1)
                {
                    arr[0, 1] = 2;
                    Nolic(100, 50);
                    element = 2;
                }
                if (arr[1, 0] == 1 && arr[1, 1] == 0 && arr[1, 2] == 1 && element == 1)
                {
                    arr[1, 1] = 2;
                    Nolic(100, 100);
                    element = 2;
                }
                if (arr[2, 0] == 1 && arr[2, 1] == 0 && arr[2, 2] == 1 && element == 1)
                {
                    arr[2, 1] = 2;
                    Nolic(100, 150);
                    element = 2;
                }
                if (arr[0, 0] == 1 && arr[1, 0] == 0 && arr[2, 0] == 1 && element == 1)
                {
                    arr[1, 0] = 2;
                    Nolic(50, 100);
                    element = 2;
                }
                if (arr[0, 1] == 1 && arr[1, 1] == 0 && arr[2, 1] == 1 && element == 1)
                {
                    arr[1, 1] = 2;
                    Nolic(100, 100);
                    element = 2;
                }
                if (arr[0, 2] == 1 && arr[1, 2] == 0 && arr[2, 2] == 1 && element == 1)
                {
                    arr[1, 2] = 2;
                    Nolic(150, 100);
                    element = 2;
                }
                // Конец алгоритм блокировки крестиков
                AiLow(); // Ходит слабый искусств. интеллект если хода нет.
            }
        }

        private void timer2_GameMetod_Tick(object sender, EventArgs e)
        {
            int x = MouseX;
            int y = MouseY;

            if (слабыйToolStripMenuItem1.CheckState == CheckState.Checked)
            {
                AiLow();
            }
            if (сильныйToolStripMenuItem1.CheckState == CheckState.Checked)
            {
                AiHard();
            }

            if (element == 2) label2.Text = "Ходит крестик"; // Человек
            if (element == 1) label2.Text = "Ходит нолик"; // Компьютер

            if (element == 2) // Крестик = 1
            {
                if (x > 0 && x < 200 && y > 0 && y < 200)
                {
                    if (x == 50 && y == 50 && arr[0, 0] == 0)
                    {
                        arr[0, 0] = 1;
                        Krestic(x, y);
                        element = 1; // То есть нажат был крестик и следующий нолик;
                        // Обнуление
                        x = 0;
                        y = 0;
                    }
                    if (x == 100 && y == 50 && arr[0, 1] == 0)
                    {
                        arr[0, 1] = 1;
                        Krestic(x, y);
                        element = 1; // То есть нажат был крестик и следующий нолик;
                        // Обнуление
                        x = 0;
                        y = 0;
                    }
                    if (x == 150 && y == 50 && arr[0, 2] == 0)
                    {
                        arr[0, 2] = 1;
                        Krestic(x, y);
                        element = 1; // То есть нажат был крестик и следующий нолик;
                        // Обнуление
                        x = 0;
                        y = 0;
                    }
                    if (x == 50 && y == 100 && arr[1, 0] == 0)
                    {
                        arr[1, 0] = 1;
                        Krestic(x, y);
                        element = 1; // То есть нажат был крестик и следующий нолик;
                        // Обнуление
                        x = 0;
                        y = 0;
                    }
                    if (x == 100 && y == 100 && arr[1, 1] == 0)
                    {
                        arr[1, 1] = 1;
                        Krestic(x, y);
                        element = 1; // То есть нажат был крестик и следующий нолик;
                        // Обнуление
                        x = 0;
                        y = 0;
                    }
                    if (x == 150 && y == 100 && arr[1, 2] == 0)
                    {
                        arr[1, 2] = 1;
                        Krestic(x, y);
                        element = 1; // То есть нажат был крестик и следующий нолик;
                        // Обнуление
                        x = 0;
                        y = 0;
                    }
                    if (x == 50 && y == 150 && arr[2, 0] == 0)
                    {
                        arr[2, 0] = 1;
                        Krestic(x, y);
                        element = 1; // То есть нажат был крестик и следующий нолик;
                        // Обнуление
                        x = 0;
                        y = 0;
                    }
                    if (x == 100 && y == 150 && arr[2, 1] == 0)
                    {
                        arr[2, 1] = 1;
                        Krestic(x, y);
                        element = 1; // То есть нажат был крестик и следующий нолик;
                        // Обнуление
                        x = 0;
                        y = 0;
                    }
                    if (x == 150 && y == 150 && arr[2, 2] == 0)
                    {
                        arr[2, 2] = 1;
                        Krestic(x, y);
                        element = 1; // То есть нажат был крестик и следующий нолик;
                        // Обнуление
                        x = 0;
                        y = 0;
                    }
                }
            }

            int g = Proverka();
            if (g == 1)
            {
                timer2_GameMetod.Stop();
                timer2_GameMetod.Dispose();
                timer1.Stop();
                label2.Text = "Выйграли крестики";
            }
            if (g == 2)
            {
                timer2_GameMetod.Stop();
                timer2_GameMetod.Dispose();
                timer1.Stop();
                label2.Text = "Выйграли нолики";
            }
            if (g == 3)
            {
                timer2_GameMetod.Stop();
                timer2_GameMetod.Dispose();
                timer1.Stop();
                label2.Text = "Ничья";
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.1;
            if (this.Opacity <= 0)
            {
                this.Close();
            }
        }

        private void ходToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (ходToolStripMenuItem.CheckState == CheckState.Checked)
            {
                интеллектИИToolStripMenuItem.CheckState = CheckState.Unchecked;
                NewGame();
            }
        }

        private void интеллектИИToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (интеллектИИToolStripMenuItem.CheckState == CheckState.Checked)
            {
                ходToolStripMenuItem.CheckState = CheckState.Unchecked;
                NewGame();
            }
        }

        private void слабыйToolStripMenuItem1_CheckedChanged(object sender, EventArgs e)
        {
            if (слабыйToolStripMenuItem1.CheckState == CheckState.Checked)
            {
                сильныйToolStripMenuItem1.CheckState = CheckState.Unchecked;
                NewGame();
            }
        }

        private void сильныйToolStripMenuItem1_CheckedChanged(object sender, EventArgs e)
        {
            if (сильныйToolStripMenuItem1.CheckState == CheckState.Checked)
            {
                слабыйToolStripMenuItem1.CheckState = CheckState.Unchecked;
                NewGame();
            }
        }

        private void правилаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Игроки по очереди ставят на свободные клетки поля 3х3 знаки (один всегда крестики, другой всегда нолики). Первый, выстроивший в ряд 3 своих фигур по вертикали, горизонтали или диагонали, выигрывает. В настройках игры можно менять уровень интеллекта компьютера.");
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.X > 50 && e.X < 100) MouseX = 50;
            if (e.X > 100 && e.X < 150) MouseX = 100;
            if (e.X > 150 && e.X < 200) MouseX = 150;

            if (e.Y > 50 && e.Y < 100) MouseY = 50;
            if (e.Y > 100 && e.Y < 150) MouseY = 100;
            if (e.Y > 150 && e.Y < 200) MouseY = 150;
        }
    }
}

