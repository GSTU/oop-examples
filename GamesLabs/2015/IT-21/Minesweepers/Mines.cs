using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml;

namespace Minesweepers
{
    class Mines
    {
        /// <summary>
        /// Таймер
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Игровое поле
        /// </summary>
        public Cell[,] GameField;

        /// <summary>
        /// Количество мин
        /// </summary>
        public int CountOfMines { get; set; }

        /// <summary>
        /// Количество флажков
        /// </summary>
        public int CountOfFlags { get; set; }

        /// <summary>
        /// Количество мин
        /// </summary>
        public int CountOfFindMines { get; set; }

        /// <summary>
        /// Высота
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Количество по высоте
        /// </summary>
        public int CountHeight { get; private set; }

        /// <summary>
        /// Количество по ширине
        /// </summary>
        public int CountWeight { get; private set; }

        /// <summary>
        /// Ширина
        /// </summary>
        public int Weight { get; private set; }

        /// <summary>
        /// Статус игры(0 - игра не начата, 1 - начата игра, 2 - игра проиграна, 3 - игра выйграна)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Уровень сложности
        /// </summary>
        public int Power { get; private set; }

        /// <summary>
        /// Считывание из XML файла
        /// </summary>
        /// <param name="power"></param>
        public Mines(int power, string nameXML)
        {
            XmlDocument DocXml = new XmlDocument();
            DocXml.Load(nameXML);
            XmlElement xRoot = DocXml.DocumentElement;
            foreach (XmlElement xnode in xRoot)
            {
                XmlNode attr = xnode.Attributes.GetNamedItem("power");
                if (attr.Value == power.ToString())
                {
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        Power = power;
                        switch (childnode.Name)
                        {
                            case "height": Height = Int32.Parse(childnode.InnerText);
                                break;
                            case "weight": Weight = Int32.Parse(childnode.InnerText);
                                break;
                            case "countOfMines": CountOfMines = Int32.Parse(childnode.InnerText);
                                break;
                            case "countHeight": CountHeight = Int32.Parse(childnode.InnerText);
                                break;
                            case "countWeight": CountWeight = Int32.Parse(childnode.InnerText);
                                break;
                        }
                    }
                }
            }
            CountOfFindMines = 0;
            GameField = new Cell[CountHeight, CountWeight];
            for (int row = 0; row < CountHeight; row++)
            {
                for (int j = 0; j < CountWeight; j++)
                {
                    GameField[row, j] = new Cell();
                    GameField[row, j].Active = 0;
                    GameField[row, j].Type = 0;
                    GameField[row, j].Number = 0;
                }
            }
            Random rnd = new Random();
            int i = 0, IndexH, IndexW;
            while (i < CountOfMines)
            {
                IndexH = rnd.Next(0, CountHeight-1);
                IndexW = rnd.Next(0, CountWeight-1);
                if (GameField[IndexH, IndexW].Type != 2)
                {
                    GameField[IndexH, IndexW].Type = 2;
                    i++;
                }
            }
            NumberAround();
        }


        /// <summary>
        /// Загрузка параметров по умолчанию 
        /// </summary>
        /// <param name="power"></param>
        public Mines(int power)
        {
            Power = power;
            Height = 28;
            Weight = 28;
            CountOfFindMines = 0;
            switch (Power)
            {
                case 0:
                    CountOfMines = 10;
                    CountHeight = 10;
                    CountWeight = 10;
                    break;
                case 1:
                    CountOfMines = 40;
                    CountHeight = 16;
                    CountWeight = 16;
                    break;
                case 2:
                    CountOfMines = 99;
                    CountHeight = 16;
                    CountWeight = 30;
                    break;
            }
            GameField = new Cell[CountHeight, CountWeight];
            for (int row = 0; row < CountHeight; row++)
            {
                for (int j = 0; j < CountWeight; j++)
                {
                    GameField[row, j] = new Cell();
                    GameField[row, j].Active = 0;
                    GameField[row, j].Type = 0;
                    GameField[row, j].Number = 0;
                }
            }
            Random rnd = new Random();
            int i = 0, IndexH, IndexW;
            while (i < CountOfMines)
            {
                IndexH = rnd.Next(0, CountHeight - 1);
                IndexW = rnd.Next(0, CountWeight - 1);
                if (GameField[IndexH, IndexW].Type != 2)
                {
                    GameField[IndexH, IndexW].Type = 2;
                    i++;
                }
            }
            NumberAround();
        }

        /// <summary>
        /// Расстановка чисел в сетке
        /// </summary>
        private void NumberAround()
        {
            for (int i = 0; i < CountHeight; i++)
            {
                for (int j = 0; j < CountWeight; j++)
                {
                    if (GameField[i, j].Type != 2)
                    {
                        if (CheckCell(i - 1, j - 1) && GameField[i - 1, j - 1].Type == 2) StateNumber(i, j);
                        if (CheckCell(i - 1, j) && GameField[i - 1, j].Type == 2) StateNumber(i, j);
                        if (CheckCell(i - 1, j + 1) && GameField[i - 1, j + 1].Type == 2) StateNumber(i, j);
                        if (CheckCell(i + 1, j - 1) && GameField[i + 1, j - 1].Type == 2) StateNumber(i, j);
                        if (CheckCell(i + 1, j) && GameField[i + 1, j].Type == 2) StateNumber(i, j);
                        if (CheckCell(i + 1, j + 1) && GameField[i + 1, j + 1].Type == 2) StateNumber(i, j);
                        if (CheckCell(i, j - 1) && GameField[i, j - 1].Type == 2) StateNumber(i, j);
                        if (CheckCell(i, j + 1) && GameField[i, j + 1].Type == 2) StateNumber(i, j);
                    }
                }
            }
        }

        /// <summary>
        /// Проверка клетки
        /// </summary>
        /// <param name="i">Номер строки</param>
        /// <param name="j">Номер столбца</param>
        /// <returns></returns>
        private bool CheckCell(int i, int j)
        {
            try
            {
                bool flag = true;
                if (i < 0 || i > CountHeight - 1 || j < 0 || j > CountWeight - 1) flag = false;
                return flag;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Установление числа в клетку
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void StateNumber(int i, int j)
        {
            if (GameField[i, j].Type != 1) GameField[i, j].Type = 1;
            GameField[i, j].Number++;
        }

        public int this[int i, int j]
        {
            get { return GameField[i, j].Number; }
            set { GameField[i, j].Number = value; }
        }

        public int Active(int i, int j)
        {
            return GameField[i, j].Active;
        }

        /// <summary>
        /// Проиграл
        /// </summary>
        public void Lose(Graphics g)
        {
            for (int i = 0; i < CountHeight; i++)
            {
                for (int j = 0; j < CountWeight; j++)
                {
                    if (GameField[i, j].Type == 2)
                    {
                        GameField[i, j].Active = 1;
                        PaintCell(g, i, j);
                    }
                }
            }
        }

        /// <summary>
        /// Открытие пустой клетки
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void OpenNull(int row, int col, Graphics g)
        {
            if (CheckCell(row, col) && GameField[row, col].Type == 0 && GameField[row, col].Active == 0)
            {
                GameField[row, col].Active = 1;
                PaintCell(g, row, col);
                OpenNull(row + 1, col + 1, g);
                OpenNull(row + 1, col - 1, g);
                OpenNull(row + 1, col, g);
                OpenNull(row - 1, col + 1, g);
                OpenNull(row - 1, col - 1, g);
                OpenNull(row - 1, col, g);
                OpenNull(row, col + 1, g);
                OpenNull(row, col - 1, g);
            }
            else
            {
                if (CheckCell(row, col) && GameField[row, col].Type == 1 && GameField[row, col].Active == 0) 
                {
                    GameField[row, col].Active = 1;
                    PaintCell(g, row, col);
                }
            }
        }

        /// <summary>
        /// Совойство для проверки открытых клеток
        /// </summary>
        public bool CountOfActiv
        {
            get
            {
                bool flag = false;
                int count = 0;
                foreach (Cell c in GameField)
                {
                    if (c.Active == 1) count++;
                }
                if (count == CountHeight * CountWeight - CountOfMines) flag = true;
                return flag;
            }
        }

        /// <summary>
        /// Прорисовка мины
        /// </summary>
        /// <param name="g"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void MinePaint(Graphics g, int x, int y)
        {
            Pen pn = new Pen(Brushes.Black);
            g.DrawRectangle(pn, x - 1, y - 1, Weight, Height);
            g.FillEllipse(Brushes.Black, x + 7, y + 7, 14, 14);
            g.DrawLine(Pens.Black, x + 14, y + 14, x + 6, y + 6);
            g.DrawLine(Pens.Black, x + 14, y + 14, x + 22, y + 22);
            g.DrawLine(Pens.Black, x + 14, y + 14, x + 6, y + 22);
            g.DrawLine(Pens.Black, x + 14, y + 14, x + 22, y + 6);
            g.DrawLine(Pens.Black, x + 14, y + 14, x + 6, y + 14);
            g.DrawLine(Pens.Black, x + 14, y + 14, x + 22, y + 14);
            g.DrawLine(Pens.Black, x + 14, y + 14, x + 14, y + 6);
            g.DrawLine(Pens.Black, x + 14, y + 14, x + 14, y + 22);
            g.FillEllipse(Brushes.White, x + 9, y + 9, 5, 5);
        }

        /// <summary>
        /// Прорисовка вопроса
        /// </summary>
        /// <param name="g"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void QuestionPaint(Graphics g, int x, int y)
        {
            g.DrawString("?", new Font("Tahoma", 16, System.Drawing.FontStyle.Regular), Brushes.Black, x + 3, y + 2);
        }

        /// <summary>
        /// Прорисовка флага
        /// </summary>
        /// <param name="g"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void FlagPaint(Graphics g, int x, int y)
        {
            Pen pn = new Pen(Brushes.Black);
            g.DrawRectangle(pn, x - 1, y - 1, Weight, Height);
            g.FillRectangle(Brushes.Blue, x - 1, y - 1, Weight, Height);
            Point[] p = new Point[3];
            Point[] m = new Point[5];
            p[0].X = x + 14; p[0].Y = y + 3;
            p[1].X = x + 2; p[1].Y = y + 10;
            p[2].X = x + 14; p[2].Y = y + 13;
            g.FillPolygon(Brushes.Red, p);
            g.DrawLine(Pens.Black, x + 14, y + 4, x + 14, y + 17);
            Rectangle Rec = new Rectangle(x + 6, y + 17, 15, 5);
            g.FillRectangle(Brushes.Black, Rec);
        }

        /// <summary>
        /// Прорисовка клетки
        /// </summary>
        /// <param name="g"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void PaintCell(Graphics g, int row, int col)
        {
            int x, y;
            x = (row * Height) + 1;
            y = (col * Weight) + 1;
            switch (Active(row, col))
            {
                case 0:
                    g.FillRectangle(Brushes.Blue, y - 1, x - 1, Weight, Height);
                    break;
                case 1:
                    switch (GameField[row, col].Type)
                    {
                        case 0: g.FillRectangle(Brushes.White, y - 1, x - 1, Weight, Height);
                        break;
                        case 1:
                            g.FillRectangle(Brushes.White, y - 1, x - 1, Weight, Height);
                            g.DrawString(GameField[row, col].Number.ToString(), new Font("Tahoma", 16, System.Drawing.FontStyle.Regular), Brushes.Blue, y + 3, x + 2);
                        break;
                        case 2:
                            g.FillRectangle(Brushes.Red, y - 1, x - 1, Weight, Height);
                            MinePaint(g, y, x);
                        break;
                    }
                    break;
                case 2:
                    g.FillRectangle(Brushes.Blue, y - 1, x - 1, Weight, Height);
                    FlagPaint(g, y, x);
                    break;
                case 3:
                    g.FillRectangle(Brushes.Blue, y - 1, x - 1, Weight, Height);
                    QuestionPaint(g, y, x);
                    break;
            }
            g.DrawRectangle(Pens.Black, y - 1, x - 1, Weight, Height);
        }
    }
}
