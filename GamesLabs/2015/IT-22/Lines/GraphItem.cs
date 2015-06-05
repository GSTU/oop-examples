
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml;

namespace linesk
{
    // Класс графического элемента, состоит из квадратной ячейки 
    // и геометрической фигуры.
    // Геометрическая фигура может принимать разные формы,
    // при выборе игроком демонстрировать активность.
    public class GraphItem
    {
        // Удобство статической переменной - ее изменение в любом месте
        // программы приведет к изменению вида геометрической фигуры во всех
        // объектах класса GraphItem
       public static TypeGraphItem CurrentTypeGraphItem;

        // Координаты ячейки графэлемента.
        public Rectangle CellCoordinate;

        // Цвет для геометрической фигуры.
        public Color Color = Color.Black;
        public enum TypeGraphItem { tRhombus, tEllipse, tRectangle };

        // Нам нужен только метод Invalidate() родительского окна данный метод наследуется от класса Control.
        // Используется в таймерах активности и исчезания.
        Control Parent = null;

        // Таймеры нам дают псевомногопоточную работу приложения.
        public Timer timerActive = new Timer();
        Timer timerVanish = new Timer();

        /// <summary>
        /// Заказной конструктор
        /// </summary>
        /// <param name="parent">ссылка на родительское окно</param>
        public GraphItem(Control parent)
        {
            Parent = parent;

            // ---- Инициализация таймеров ----

            // таймер активности геометрической фигуры
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("settings.xml");
            //MessageBox.Show(xDoc.DocumentElement.SelectNodes.ToString());
            timerActive.Interval = int.Parse(xDoc.DocumentElement.SelectSingleNode("interval").InnerText);
            timerActive.Tick += new EventHandler(timerActive_Tick);
            timerActive.Enabled = false;

            // таймер визуального исчезания геометрической фигуры
            timerVanish.Interval = int.Parse(xDoc.DocumentElement.SelectSingleNode("size").InnerText);
            timerVanish.Tick += new EventHandler(timerVanish_Tick);
            timerVanish.Enabled = false;
            deltaWidth = inflateSize;
            deltaHeight = inflateSize;
        }


        #region Рисование графэлемента в родительском окне
        /// <summary>
        /// Декоративная кисть ячейки.
        /// </summary>
        /// <returns>готовая кисть</returns>
        Brush BrushCell()
        {
            // Подготовка координат для прорисовки ромба
            Point point1 = new Point(CellCoordinate.Left + CellCoordinate.Width / 2, CellCoordinate.Top);
            Point point2 = new Point(CellCoordinate.Left + CellCoordinate.Width, CellCoordinate.Top + CellCoordinate.Height / 2);
            Point point3 = new Point(CellCoordinate.Left + CellCoordinate.Width / 2, CellCoordinate.Top + CellCoordinate.Height);
            Point point4 = new Point(CellCoordinate.Left, CellCoordinate.Top + CellCoordinate.Height / 2);

            Point[] pt = { point1, point2, point3, point4 };

            // Каков вид геометрической фигуры такая и кисть.
            GraphicsPath gp = new GraphicsPath();
            
            gp.AddRectangle(CellCoordinate);
                   
           

            // Раскраска геометрической фигуры.
            PathGradientBrush pathBrush = new PathGradientBrush(gp);
            pathBrush.SurroundColors = new Color[] { Color.FromArgb(90, 30, 30) };
            pathBrush.CenterPoint = new PointF(CellCoordinate.Left + CellCoordinate.Width / 2, CellCoordinate.Top + CellCoordinate.Height / 2);
            pathBrush.CenterColor = Color.Blue;

            return pathBrush;
        }

        public int inflateSize = -7; // переменная величины пульсирования геометрической фигуры
        int deltaWidth = 0;   // изменения по ширине     
        int deltaHeight = 0;  // изменение по высоте
        /// <summary>
        /// Рисование графики в графическом контексте.
        /// </summary>
        /// <param name="g">graphics окна вывода</param>
        public void Draw(Graphics g)
        {
            g.FillRectangle(BrushCell(), CellCoordinate.X + 1,
                CellCoordinate.Y + 1, CellCoordinate.Width - 1, CellCoordinate.Height - 1);

            // При невидимой геометрической фигуре прорисовывается только квадрат ячейки.
            if (visible == false) return;

            // Вспомогательный квадрат для эффекта исчезания и эффекта 
            // активности в выбранном состоянии.
            Rectangle rectInflate = CellCoordinate;
            rectInflate.Inflate(deltaWidth, deltaHeight);


            // ---------- Формирование геометрической фигуры внутри ячейки -------------------

            // Подготовка координат для прорисовки ромба
            Point point1 = new Point(CellCoordinate.Left + CellCoordinate.Width / 2, rectInflate.Top);
            Point point2 = new Point(rectInflate.Left + rectInflate.Width, CellCoordinate.Top + CellCoordinate.Height / 2);
            Point point3 = new Point(CellCoordinate.Left + CellCoordinate.Width / 2, rectInflate.Top + rectInflate.Height);
            Point point4 = new Point(rectInflate.Left, CellCoordinate.Top + CellCoordinate.Height / 2);

            Point[] pt = { point1, point2, point3, point4 };

            // Выбор вида графической фигуры.
            GraphicsPath gp = new GraphicsPath();
            switch (CurrentTypeGraphItem)
            {
                case TypeGraphItem.tEllipse:
                    gp.AddEllipse(rectInflate);
                    break;
                case TypeGraphItem.tRectangle:
                    gp.AddRectangle(rectInflate);
                    break;
                case TypeGraphItem.tRhombus:
                    gp.AddPolygon(pt);
                    break;
            }

            // При минимальном размере окна могут возникать
            // при исчезании фигур отрицательные значения ширины и высоты.
            // Для предотвращения сбоев предназначена эта строка.
            if (rectInflate.Width < 0 || rectInflate.Height < 0) return;
        

            // Раскраска геометрической фигуры.
            PathGradientBrush pathBrush = new PathGradientBrush(gp);
            pathBrush.SurroundColors = new Color[] { this.Color };
            pathBrush.CenterPoint = new PointF(CellCoordinate.Left + CellCoordinate.Width / 2, CellCoordinate.Top + CellCoordinate.Height / 2);
            pathBrush.CenterColor = Color.White;



            // Рисование текущей геометрической фигуры.
            switch (CurrentTypeGraphItem)
            {
                case TypeGraphItem.tEllipse:
                    g.FillEllipse(pathBrush, rectInflate);
                    break;
                case TypeGraphItem.tRectangle:
                    g.FillRectangle(pathBrush, rectInflate);
                    break;
                case TypeGraphItem.tRhombus:
                    g.FillPolygon(pathBrush, pt);
                    break;
            }

        }

        #endregion

        #region Свойства графэлемента - видимости, активности, исчезания

        bool visible = false;
        /// <summary>
        /// Невидимым считается даже исчезающая геометрическая фигура.
        /// </summary>
        public bool Visible
        {
            get
            {
                if (vanish == true || visible == false)
                    return false;

                return true;
            }
            set
            {
                visible = value;
            }
        }

        bool active = false;
        /// <summary>
        /// Свойство активности геометрической фигуры.
        /// </summary>
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
                
                if (active == true)
                {
                    timerActive.Enabled = true;
                }
                else
                {
                    timerActive.Enabled = false;
                    deltaWidth = inflateSize;
                    deltaHeight = inflateSize;
                }
            }
        }

        // Формируем свойство исчезания.
        bool vanish = false;
        /// <summary>
        /// Свойство плавного исчезания геометрической фигуры.
        /// </summary>
        public bool Vanish
        {
            get
            {
                return vanish;
            }
            set
            {
                vanish = value;

                if (vanish == true)
                {
                    timerVanish.Enabled = true;
                }
            }
        }

        #endregion

        #region Таймеры графэлемента - таймер активности и таймер исчезания
        int k = 1;
        /// <summary>
        /// Таймер активности, периодически увеличивает и уменьшает геометрическую фигуру,
        /// создавая эффект активности.
        /// </summary>
        void timerActive_Tick(object sender, EventArgs e)
        {
            deltaWidth += 1 * k;
            deltaHeight += 1 * k;
            if (deltaWidth >= -2)
                k = -1;
            else if (deltaWidth <= -7)
                k = 1;

            Parent.Invalidate(CellCoordinate);
        }
   
        /// <summary>
        /// Таймер исчезания, сначала графэлемент уменьшается,
        /// а потом исчезает.
        /// </summary>
        void timerVanish_Tick(object sender, EventArgs e)
        {

            deltaWidth += -2;
            deltaHeight += -2;

            if (deltaWidth < -14)
            {
                timerVanish.Enabled = false;
                visible = false;
                vanish = false;
                deltaWidth = inflateSize;
                deltaHeight = inflateSize;
            }
            
            Parent.Invalidate(CellCoordinate);
        }
        #endregion 

    }
}
