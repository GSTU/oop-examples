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

namespace Saper
{
    public partial class Form1 : Form
    {
        public int win = 0;
        int min1 = 5, min2 = 40, min3 = 90;
        int mr1 = 10, mc1 = 10;
        //int sizeSqr = 5;
        //int sizeSqr2 = 5;
        public Form1()
        {
            InitializeComponent();
            CreatePole();
            this.NewGame();
            this.ClientSize = new Size(20 * MC + 1, 20 * MR + 23);
        }
        public void CreatePole()
        {
            for (int row = 0; row <= MR + 1; row++)
            {
                pole[row, 0] = -3;
                pole[row, MC + 1] = -3;
            }
            for (int col = 0; col <= MC + 1; col++)
            {
                pole[0, col] = -3;
                pole[MR + 1, col] += -3;
            }
        }
        public static int MR = 10;
        public static int MC = 10;
        public static int MinNum = 10;
       
        public int lose = 0;
        private int[,] pole = new int[MR + 2, MC + 2];
        private int mins, flags; 
        bool alreadySown = false;
        private int status;
        // 0 - начало игры
        // 1 - игра
        // 2 - результат
        private void Win()
        {
            
        }
        private void NewGame()
        {
            int row, col;
            int n = 0;
            int k;
            for (row = 1; row <= MR; row++)
                for (col = 1; col <= MC; col++)
                    pole[row, col] = 0;
            Random rnd = new Random();
            while (n != MinNum)
            {
                row = rnd.Next(1, MR + 1);
                col = rnd.Next(1, MC + 1);
                if (pole[row, col] != 9)
                {
                    pole[row, col] = 9;
                    n++;
                }
            }
            for (row = 1; row <= MR; row++)
                for (col = 1; col <= MC; col++)
                    if (pole[row, col] != 9)
                    {
                        k = 0;
                        if (pole[row - 1, col - 1] == 9) k++;
                        if (pole[row - 1, col] == 9) k++;
                        if (pole[row - 1, col + 1] == 9) k++;
                        if (pole[row, col - 1] == 9) k++;
                        if (pole[row, col + 1] == 9) k++;
                        if (pole[row + 1, col - 1] == 9) k++;
                        if (pole[row + 1, col] == 9) k++;
                        if (pole[row + 1, col + 1] == 9) k++;
                        pole[row, col] = k;
                    }
            status = 0;
            mins = 0;
            flags = 0;
        }
        private void showPole(Graphics g, int status)
        {
            for (int row = 1; row <= MR; row++)
                for (int col = 1; col <= MC; col++)
                    this.kletka(g, row, col, status);
        }
        private int GetTopX(int col)
        {
            return (col - 1) * 20 + 1;
        }
        private int GetTopY(int row)
        {
            return (row - 1) * 20 + 1;
        }
        private void kletka(Graphics g, int row, int col, int status)
        {
            int x = GetTopX(col);
            int y = GetTopY(row);
            if (pole[row, col] < 100)
            {
                g.FillRectangle(Brushes.Blue, x, y, x + 20, y + 20);
            }
            if (pole[row, col] >= 100)
            {
                if (pole[row, col] != 109)
                {
                    g.FillRectangle(Brushes.White, x, y, x + 20, y + 20);
                }
                else
                {
                    g.FillRectangle(Brushes.Red, x, y, x + 20, y + 20);
                    Bitmap img3 = new Bitmap("icon3.bmp");
                    g.DrawImage(img3, x - 20, y - 42);
                }
                if ((pole[row, col] >= 101) && (pole[row, col] <= 108))
                {
                    g.DrawString((pole[row, col] - 100).ToString(), new Font("Tahoma", 10, System.Drawing.FontStyle.Bold), Brushes.Green, x + 3, y + 2);
                }
            }
            if (pole[row, col] >= 200)
            {
                this.flag(g, x, y);

            }
            g.DrawRectangle(Pens.Black, x - 1, y - 1, x + 20, y + 20);
            if ((status == 2) && ((pole[row, col] % 10) == 9))
            {
                this.mina(g, x, y);
            }
        }
        private void open(int row, int col)
        {
            int x = GetTopX(col),
                y = GetTopY(row);
            if (pole[row, col] == 0)
            {
                pole[row, col] = 100;
                this.Invalidate(new Rectangle(x, y, 20, 20));
                this.open(row, col - 1);
                this.open(row - 1, col);
                this.open(row, col + 1);
                this.open(row + 1, col);
                this.open(row - 1, col - 1);
                this.open(row - 1, col + 1);
                this.open(row + 1, col - 1);
                this.open(row + 1, col + 1);
            }
            else
                if ((pole[row, col] < 100) && (pole[row, col] != -3))
                {
                    pole[row, col] += 100;
                    this.Invalidate(new Rectangle(x, y, 20, 20));
                }
        }
        private void mina(Graphics g, int x, int y)
        {
            g.FillRectangle(Brushes.Green, x + 8, y + 13, 4, 2);
            g.FillRectangle(Brushes.Green, x + 4, y + 15, 12, 2);
            g.DrawPie(Pens.Black, x + 8, y + 14, 14, 8, 0, -90);
            g.FillPie(Brushes.Green, x + 3, y + 14, 14, 8, 0, -90);
            g.DrawLine(Pens.Black, x + 6, y + 16, x + 14, y + 16);
            g.DrawLine(Pens.Black, x + 10, y + 11, x + 10, y + 13);
            g.DrawLine(Pens.Black, x + 4, y + 15, x + 3, y + 14);
            g.DrawLine(Pens.Black, x + 16, y + 15, x + 17, y + 14);
        }
        private void flag(Graphics g, int x, int y)
        {
            Point[] p = new Point[3];
            Point[] m = new Point[5];
            p[0].X = x + 2; p[0].Y = y + 2;
            p[1].X = x + 15; p[1].Y = y + 6;
            p[2].X = x + 2; p[2].Y = y + 10;
            g.FillPolygon(Brushes.Red, p);
            g.DrawLine(Pens.Black, x + 2, y + 2, x + 2, y + 18);
            m[0].X = x + 8; m[0].Y = y + 14;
            m[1].X = x + 8; m[1].Y = y + 8;
            m[2].X = x + 10; m[2].Y = y + 10;
            m[3].X = x + 12; m[3].Y = y + 8;
            m[4].X = x + 12; m[4].Y = y + 14;
            g.DrawLines(Pens.Black, m);
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (status == 2)
            {
                return;
            }
            if (status == 0)
            {
                status = 1;
            }
            int row = (int)(e.Y / 20) + 1,
                col = (int)(e.X / 20) + 1;
            int x = GetTopX(col),
                y = GetTopY(row);
            if (e.Button == MouseButtons.Left)
            {
                if (pole[row, col] == 9)
                {
                    pole[row, col] += 100;
                    status = 2;
                    BOOM(x, y);
                    this.Invalidate();
                }
                else if (pole[row, col] == 0)
                    open(row, col);
                else if (pole[row, col] < 9)
                {
                    pole[row, col] += 100;
                    this.Invalidate(new Rectangle(x, y, 20, 20));
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if (pole[row, col] <= 9)
                {
                    flags += 1;
                    if (pole[row, col] == 9)
                    {
                        mins += 1;
                    }
                    pole[row, col] += 200;

                    if ((mins == MinNum) && (flags == MinNum))
                    {
                        status = 2;
                        YouWon();
                        this.Invalidate();
                    }
                    else
                    {
                        this.Invalidate(new Rectangle(x, y, 20, 20));
                    }
                }
                else
                {
                    if (pole[row, col] >= 200)
                    {
                        flags -= 1;
                        pole[row, col] -= 200;
                        this.Invalidate(new Rectangle(x, y, 20, 20));
                    }
                }
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            this.showPole(e.Graphics, status);
        }
        private void YouWon()
        {
            alreadySown = true;
            MessageBox.Show("Вы выиграли :)", "Поздравляю!");
            win++;
            //label1.Text = Convert.ToString(win);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((mins == MinNum) && (flags == MinNum) && !alreadySown)
            {
                status = 2;
                YouWon();
            }
        }
        private void BOOM(int x, int y)
        {
            Graphics g = this.CreateGraphics();
            Bitmap img1 = new Bitmap("icon1.bmp");
            Bitmap img2 = new Bitmap("icon2.bmp");
            Bitmap img3 = new Bitmap("icon3.bmp");
            g.DrawImage(img1, x - 20, y + 2);
            DateTime dt = DateTime.Now;
            MessageBox.Show("Вы проиграли :(");
            g.DrawImage(img2, x - 20, y - 25);
            FormHelp f = new FormHelp(); f.Show(); f.Visible = false; f.Close();
            g.DrawImage(img3, x - 20, y - 42);
            lose++;
            //label2.Text = Convert.ToString(lose);
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alreadySown = false;
            this.NewGame();
            this.Invalidate();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == ''.ToString())
            {
                alreadySown = false;
                this.NewGame();
                this.Invalidate();
            }
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHelp Hlp = new FormHelp();
            Hlp.Show();
        }
        public bool exitFromMenu = false;
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exitFromMenu = true;
            Application.Exit();
        }

        private void новичокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MR = mr1; MC = mc1; MinNum = min1;
            pole = new int[MR + 2, MC + 2];
            CreatePole();
            this.NewGame();
            this.ClientSize = new Size(20 * MC + 1, 20 * MR + 23);
            новичокToolStripMenuItem.Checked = true;
            любительToolStripMenuItem.Checked = false;
            профессионалToolStripMenuItem.Checked = false;
            настройкаСложностиToolStripMenuItem.Checked = false;
            this.Invalidate();
        }

        private void любительToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MR = 16; MC = 16; MinNum = min2;
            pole = new int[MR + 2, MC + 2];
            CreatePole();
            this.NewGame();
            this.ClientSize = new Size(20 * MC + 1, 20 * MR + 23);
            любительToolStripMenuItem.Checked = true;
            новичокToolStripMenuItem.Checked = false;
            профессионалToolStripMenuItem.Checked = false;
            настройкаСложностиToolStripMenuItem.Checked = false;
            this.Invalidate();
        }

        private void профессионалToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MR = 16; MC = 30; MinNum = min3;
            pole = new int[MR + 2, MC + 2];
            CreatePole();
            this.NewGame();
            this.ClientSize = new Size(20 * MC + 1, 20 * MR + 23);
            профессионалToolStripMenuItem.Checked = true;
            новичокToolStripMenuItem.Checked = false;
            любительToolStripMenuItem.Checked = false;
            настройкаСложностиToolStripMenuItem.Checked = false;
            this.Invalidate();
        }

        private void настройкаСложностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettings FO = new FormSettings();
            FO.ShowDialog();
            pole = new int[MR + 2, MC + 2];
            CreatePole();
            this.NewGame();
            this.ClientSize = new Size(20 * MC + 1, 20 * MR + 23);
            настройкаСложностиToolStripMenuItem.Checked = true;
            новичокToolStripMenuItem.Checked = false;
            любительToolStripMenuItem.Checked = false;
            профессионалToolStripMenuItem.Checked = false;
            this.Invalidate();
        }
        public static void GetFromSettings(int mr, int mc, int minnum)
        {
            MR = mr; MC = mc; MinNum = minnum;
        }

        private void проПрограммуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"  ""Сапер""
    Версия: 1.0
    Автор: Олейник Алексей");
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.ClientSize = new Size(20 * MC + 1, 20 * MR + 23);
        }


        private void label1_Click(object sender, EventArgs e)
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
            string c1="", c2="", c3="";
            XmlTextReader reader = new XmlTextReader("XMLFile1.xml");
            try
            {
           
            XmlValidatingReader validreader = new XmlValidatingReader(reader);
            validreader.Schemas.Add(null, "XMLSchema1.xsd");
            validreader.ValidationType = ValidationType.Schema;
            validreader.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);

                while (validreader.Read()) ;
            }
            catch(Exception ex)
            {
                valid = false;
                MessageBox.Show(ex.Message);
                MessageBox.Show("Файл конфигурации не найден\nЗагружены параметры по умолчанию");
            }
            //validreader.Close();

            if (valid)
            {
                reader = new XmlTextReader("XMLFile1.xml");
                while (reader.Read())
                {
                    if (reader.Name == "min1" && reader.NodeType != XmlNodeType.EndElement)
                    {
                        min1 = reader.ReadElementContentAsInt();
                    }
                    if (reader.Name == "min2" && reader.NodeType != XmlNodeType.EndElement)
                    {
                        min2 = reader.ReadElementContentAsInt();
                    }
                    if (reader.Name == "min3" && reader.NodeType != XmlNodeType.EndElement)
                    {
                        min3 = reader.ReadElementContentAsInt();
                    }
                    if (reader.Name == "mr1" && reader.NodeType != XmlNodeType.EndElement)
                    {
                        mr1 = reader.ReadElementContentAsInt();
                    }
                    if (reader.Name == "mc1" && reader.NodeType != XmlNodeType.EndElement)
                    {
                        mc1 = reader.ReadElementContentAsInt();
                    }
                }
                reader.Close();
            }
            
        }
    }
}
