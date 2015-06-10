using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Schema;

namespace WindowsFormsApplication2
{
    [Serializable]
    public partial class Form1 : Form
    {

        private int
          MR = 10, // кол-во клеток по вертикали
          MC = 10, // кол-во клеток по горизонтали
          NM = 10, // кол-во мин
          W = 40,  // ширина клетки
          H = 40;  // высота клетки

        SolidBrush closedKletka;

        // игровое (минное) поле
        private int[,] Pole;
        // значение элемента массива:
        // 0..8 - количество мин в соседних клетках
        // 9 - в клетке мина
        // 100..109 - клетка открыта
        // 200..209 - в клетку поставлен флаг

        private int nMin;  // кол-во найденных мин
        private int nFlag; // кол-во поставленных флагов

        // статус игры
        private int status;
        // 0 - начало игры,
        // 1 - игра,
        // 2 – результат

        Config config;

        bool flagConfig = true;

        // графическая поверхность формы
        private System.Drawing.Graphics g;

        public Form1()
        {
            try
            {
                if ((System.IO.File.Exists("xsd.xsd")))
                {
                    XmlReaderSettings xmlreadersettings = new XmlReaderSettings();

                    xmlreadersettings.Schemas.Add(null, "xsd.xsd");

                    xmlreadersettings.ValidationType = ValidationType.Schema;
                    xmlreadersettings.ValidationEventHandler += new ValidationEventHandler(ValidateXML);

                    XmlReader xmlreader = XmlReader.Create("Settings.xml", xmlreadersettings);
                    while (xmlreader.Read()) { }
                }
                else
                    throw new System.IO.FileNotFoundException();


                if (flagConfig)
                    config = new XMLWithDOM("Settings.xml").GetConfig();
                else
                    throw new Exception();
            }
            catch
            {
                config = new Config(10, 10, 40, Color.Gray);
            }

            MR = MC = config.FieldSize;
            W = H = config.CellSize;
            NM = config.MinesCount;
            closedKletka = new SolidBrush(config.Color);

            if (NM > W * W)
                NM = W * W;

            InitializeComponent();

            Pole = new int[MR + 2, MC + 2];

            // В неотображаемые эл-ты массива, соответствующие
            // клеткам границы игрового поля запишем число -3.
            // Это значение используется процедурой open()
            // для завершения рекурсивного процесса открытия
            // соседних пустых клеток
            for (int row = 0; row <= MR + 1; row++)
            {
                Pole[row, 0] = -3;
                Pole[row, MC + 1] = -3;
            }

            for (int col = 0; col <= MC + 1; col++)
            {
                Pole[0, col] = -3;
                Pole[MR + 1, col] = -3;
            }

            // устанавливаем размер формы в соответствии
            // с размером игрового поля
            this.ClientSize = new Size(W * MC + 1, H * MR + menuStrip1.Height + 1);

            newGame(); // новая игра

            // графическая поверхность
            g = panel1.CreateGraphics();
        }
        private void ValidateXML(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                MessageBox.Show("Warning: " + e.Message);

                flagConfig = false;
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                MessageBox.Show("Error: " + e.Message);

                flagConfig = false;
            }
        }
        // новая игра
        private void newGame()
        {
            int row, col;    // индексы клетки
            int n = 0;       // количество поставленных мин
            int k;           // кол-во мин в соседних клетках


            // очистить поле
            for (row = 1; row <= MR; row++)
                for (col = 1; col <= MC; col++)
                    Pole[row, col] = 0;

            // инициализация генератора случайных чисел
            Random rnd = new Random();

            // расставим мины
            do
            {
                row = rnd.Next(MR) + 1;
                col = rnd.Next(MC) + 1;

                if (Pole[row, col] != 9)
                {
                    Pole[row, col] = 9;
                    n++;
                }
            }
            while (n != NM);

            // для каждой клетки вычислим кол-во 
            // мин в соседних клетках
            for (row = 1; row <= MR; row++)
                for (col = 1; col <= MC; col++)
                    if (Pole[row, col] != 9)
                    {
                        k = 0;

                        if (Pole[row - 1, col - 1] == 9) k++;
                        if (Pole[row - 1, col] == 9) k++;
                        if (Pole[row - 1, col + 1] == 9) k++;
                        if (Pole[row, col - 1] == 9) k++;
                        if (Pole[row, col + 1] == 9) k++;
                        if (Pole[row + 1, col - 1] == 9) k++;
                        if (Pole[row + 1, col] == 9) k++;
                        if (Pole[row + 1, col + 1] == 9) k++;

                        Pole[row, col] = k;
                    }

            status = 0;      // начало игры
            nMin = 0;      // нет обнаруженных мин
            nFlag = 0;      // нет поставленных флагов
        }

        // рисует поле
        private void showPole(Graphics g, int status)
        {
            for (int row = 1; row <= MR; row++)
                for (int col = 1; col <= MC; col++)
                    this.kletka(g, row, col, status);
        }

        // рисует клетку
        private void kletka(Graphics g,
            int row, int col, int status)
        {

            int x, y;// координаты левого верхнего угла клетки

            x = (col - 1) * W + 1;
            y = (row - 1) * H + 1;

            // не открытые клетки 
            if (Pole[row, col] < 100)
                g.FillRectangle(closedKletka,
                    x - 1, y - 1, W, H);

            // открытые или помеченные клетки
            if (Pole[row, col] >= 100)
            {

                // открываем клетку, открытые - белые
                if (Pole[row, col] != 109)
                    g.FillRectangle(Brushes.White,
                        x - 1, y - 1, W, H);
                else
                    // на этой мине подорвались!
                    g.FillRectangle(Brushes.Red,
                        x - 1, y - 1, W, H);

                // если в соседних клетках есть мины,
                // указываем их количество
                if ((Pole[row, col] >= 101) && (Pole[row, col] <= 108))
                    g.DrawString((Pole[row, col] - 100).ToString(),
                        new Font("Tahoma", 15,
                            System.Drawing.FontStyle.Regular),
                        Brushes.Purple, x + 11, y + 7);
            }

            // в клетке поставлен флаг
            if (Pole[row, col] >= 200)
                this.flag(g, x, y);

            // рисуем границу клетки
            g.DrawRectangle(Pens.Black,
                x - 1, y - 1, W, H);

            // если игра завершена (status = 2),
            // показываем мины

            if ((status == 2) && ((Pole[row, col] % 10) == 9))
            {
                this.mina(g, x, y);

            }

        }

        // открывает текущую и все соседние с ней клетки,
        // в которых нет мин
        private void open(int row, int col)
        {
            // координаты области вывода
            int x = (col - 1) * W + 1,
                y = (row - 1) * H + 1;

            if (Pole[row, col] == 0)
            {
                Pole[row, col] = 100;

                // отобразить содержимое клетки
                this.kletka(g, row, col, status);

                // открыть примыкающие клетки
                // слева, справа, сверху, снизу
                this.open(row, col - 1);
                this.open(row - 1, col);
                this.open(row, col + 1);
                this.open(row + 1, col);

                //примыкающие диагонально
                this.open(row - 1, col - 1);
                this.open(row - 1, col + 1);
                this.open(row + 1, col - 1);
                this.open(row + 1, col + 1);
            }
            else
                if ((Pole[row, col] < 100) &&
                     (Pole[row, col] != -3))
                {
                    Pole[row, col] += 100;

                    // отобразить содержимое клетки
                    this.kletka(g, row, col, status);
                }
        }

        // рисует мину
        private void mina(Graphics g, int x, int y)
        {
            Pen pn = new Pen(Brushes.Black);
            g.DrawRectangle(pn, x - 1, y - 1, W, H);
            g.FillEllipse(Brushes.Black, x + 12, y + 12, 14, 14);
            g.DrawLine(Pens.Black, x + 19, y + 19, x + 11, y + 11);
            g.DrawLine(Pens.Black, x + 19, y + 19, x + 27, y + 27);
            g.DrawLine(Pens.Black, x + 19, y + 19, x + 11, y + 27);
            g.DrawLine(Pens.Black, x + 19, y + 19, x + 27, y + 11);
            g.DrawLine(Pens.Black, x + 19, y + 19, x + 11, y + 19);
            g.DrawLine(Pens.Black, x + 19, y + 19, x + 27, y + 19);
            g.DrawLine(Pens.Black, x + 19, y + 19, x + 19, y + 11);
            g.DrawLine(Pens.Black, x + 19, y + 19, x + 19, y + 27);
            g.FillEllipse(Brushes.White, x + 14, y + 14, 5, 5);
        }

        // рисует флаг
        private void flag(Graphics g, int x, int y)
        {
            Point[] p = new Point[3];
            Point[] m = new Point[5];

            // флажок
            p[0].X = x + 4; p[0].Y = y + 4;
            p[1].X = x + 30; p[1].Y = y + 12;
            p[2].X = x + 4; p[2].Y = y + 20;
            g.FillPolygon(Brushes.Red, p);

            // древко
            g.DrawLine(Pens.Brown,
                x + 4, y + 4, x + 4, y + 35);

            // буква M на флажке
            m[0].X = x + 8; m[0].Y = y + 14;
            m[1].X = x + 8; m[1].Y = y + 8;
            m[2].X = x + 10; m[2].Y = y + 10;
            m[3].X = x + 12; m[3].Y = y + 8;
            m[4].X = x + 12; m[4].Y = y + 14;
            g.DrawLines(Pens.Black, m);
        }

        // щелчок кнопкой в клетке игрового поля
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            // игра завершена
            if (status == 2) return;

            // первый щелчок
            if (status == 0) status = 1;

            // преобразуем координаты мыши в индексы
            // клетки поля, в которой был сделан щелчок;
            // (e.X, e.Y) - координаты точки формы,
            // в которой была нажата кнопка мыши;            
            int row = (int)(e.Y / H) + 1,
                col = (int)(e.X / W) + 1;

            // координаты области вывода
            int x = (col - 1) * W + 1,
                y = (row - 1) * H + 1;

            // щелчок левой кнопки мыши
            if (e.Button == MouseButtons.Left)
            {
                // открыта клетка, в которой есть мина                   
                if (Pole[row, col] == 9)
                {
                    Pole[row, col] += 100;

                    // игра закончена
                    status = 2;

                    // перерисовать форму
                    this.panel1.Invalidate();
                }
                else
                    if (Pole[row, col] < 9)
                        this.open(row, col);
            }

            // щелчок правой кнопки мыши
            if (e.Button == MouseButtons.Right)
            {

                // в клетке не было флага, ставим его
                if (Pole[row, col] <= 9)
                {
                    nFlag += 1;

                    if (Pole[row, col] == 9)
                        nMin += 1;

                    Pole[row, col] += 200;

                    if ((nMin == NM) && (nFlag == NM))
                    {
                        // игра закончена
                        status = 2;

                        // перерисовываем все игровое поле
                        this.Invalidate();
                    }
                    else
                        // перерисовываем только клетку
                        this.kletka(g, row, col, status);
                }
                else
                    // в клетке был поставлен флаг,
                    // повторный щелчок правой кнопки мыши
                    // убирает его и закрывает клетку
                    if (Pole[row, col] >= 200)
                    {
                        nFlag -= 1;
                        Pole[row, col] -= 200;

                        // перерисовываем клетку                
                        this.kletka(g, row, col, status);
                    }
            }
        }

        // команда Новая игра
        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGame();
            showPole(g, status);
        }

        // обработка события Paint панели
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            showPole(g, status);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public class XMLWithDOM
    {
        private XmlDocument document;

        public XMLWithDOM(String pathToXMLFile)
        {
            try
            {
                if (!(System.IO.File.Exists(pathToXMLFile)))
                    throw new System.IO.FileNotFoundException();

                document = new XmlDocument();

                using (XmlReader reader = XmlReader.Create(pathToXMLFile))
                {
                    document.Load(reader);
                }
            }
            catch { throw; }
        }

        public Config GetConfig()
        {
            XmlElement root = document["config"];

            return new Config(int.Parse(root["game"].FirstChild.InnerText), int.Parse(root["game"].LastChild.InnerText), int.Parse(root["window"].FirstChild.InnerText), Color.FromName(root["closedKletkaColor"].InnerText));
        }
    }
    public class Config
    {
        public int MinesCount;
        public int FieldSize;
        public int CellSize;
        public Color Color;

        public Config(int mc, int fs, int cs, Color c)
        {
            MinesCount = mc;
            FieldSize = fs;
            CellSize = cs;
            Color = c;
        }
    }
}