using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Linq;

namespace new_zmeya
{
    public class Program : Form
    {
        public static bool isValid;
        public static int x, y,x1,x2,y1,y2;
        public static Brush color;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                XmlReaderSettings XMLSettings = new XmlReaderSettings();
                XMLSettings.Schemas.Add(null, "data.xsd");
                XMLSettings.ValidationType = ValidationType.Schema;
                //XMLSettings.ValidationEventHandler += new ValidationEventHandler(ConfigCheckValidationEventHandler);
                XmlReader XMLReader = XmlReader.Create("data.xml", XMLSettings);
                isValid = true;

                while (XMLReader.Read()) { }

                if (isValid == true)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("data.xml");

                    x = Convert.ToInt32(doc["config"]["gameField"]["x"].InnerText);
                    y = Convert.ToInt32(doc["config"]["gameField"]["y"].InnerText);
                    x1 = Convert.ToInt32(doc["config"]["gameField"]["x1"].InnerText);
                    y1 = Convert.ToInt32(doc["config"]["gameField"]["y1"].InnerText);
                    x2 = Convert.ToInt32(doc["config"]["gameField"]["x2"].InnerText);
                    y2 = Convert.ToInt32(doc["config"]["gameField"]["y2"].InnerText);
                    color = new SolidBrush(Color.FromName(doc["config"]["backgroundColor"].InnerText)); 
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                x = 20;
                y = 1;
                x1 = 20;
                y1 = 2;
                x2 = 20;
                y2 = 3;
                color = Brushes.Red; 
            }
            Application.Run(new Program());
        }

        //класс для удобства работы с координатами яблока и сегментов змеи
        public class coord
        {
            public int X;
            public int Y;
            public coord(int x, int y)
            {
                X = x; Y = y;
            }
        }

        Timer timer = new Timer();
        Random rand = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        // условная ширина "поля действия" в клетках, высота и размер клетки в пикселах
        int W = 50, H = 30, S = 10;
        // собственно змея: список сегментов(нулевой индекс в списке - голова змеи)  
        List<coord> snake = new List<coord>();
        coord apple; // координаты яблока
        int way = 0; // направление движения змеи: 0 - вверх, 1 - вправо, 2 - вниз, 3 - влево
        int apples = 0; // количество собранных яблок
        int stage = 1; // уровень игры
        int score = 0; // набранные очки в игре

        Program()
        {
            
            this.Text = "Snake"; // заголовок формы
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // мышкой нельзя растягивать форму
            this.MaximizeBox = false; // делаем недоступной кнопку "развернуть во весь экран"
            this.StartPosition = FormStartPosition.CenterScreen; // форма отображается по центру экрана
            this.DoubleBuffered = true; // для прорисовки

            int caption_size = SystemInformation.CaptionHeight; // высота шапки формы
            int frame_size = SystemInformation.FrameBorderSize.Height; // ширина границы формы
            // устанавливаем размер внутренней области формы W * H с учетом высоты шапки и ширины границ
            this.Size = new Size(W * S + 2 * frame_size, H * S + caption_size + 2 * frame_size);

            this.Paint += new PaintEventHandler(Program_Paint); // привязываем обработчик прорисовки формы
            this.KeyDown += new KeyEventHandler(Program_KeyDown); // привязываем обработчик нажатий на кнопки

            timer.Interval = 200; // таймер срабатывает раз в 200 милисекунд
            timer.Tick += new EventHandler(timer_Tick); // привязываем обработчик таймера
            timer.Start(); // запускаем таймер

            // делаем змею из трех сегментов, с начальными координатами внизу и по-центру формы
            snake.Add(new coord(x, y));
            snake.Add(new coord(x1, y1));
            snake.Add(new coord(x2, y2));

            bool q = false;
            apple = new coord(rand.Next(W), rand.Next(H)); // координаты яблока
           
           /* foreach(coord n in snake)
                while (apple == n)
                {
                    apple = new coord(rand.Next(W), rand.Next(H));
                }
             */
            
        }

        // обработка нажатий на клавиши(здесь только стрелки)
        // меняем направление движения, если оно не противоположное
        void Program_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                    if (way != 2)
                        way = 0;
                    break;
                case Keys.Right:
                    if (way != 3)
                        way = 1;
                    break;
                case Keys.Down:
                    if (way != 0)
                        way = 2;
                    break;
                case Keys.Left:
                    if (way != 1)
                        way = 3;
                    break;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            // запоминаем координаты головы змеи
            int x = snake[0].X, y = snake[0].Y;
            // в зависимости от направления вычисляем где будет голова на следующем шаге
            // сделал чтобы при достижении края формы голова появлялась с противоположной стороны 
            // и змея продолжала движение
            switch (way)
            {
                case 0:
                    y--;
                    if (y < 0)
                        y = H - 1;
                    break;
                case 1:
                    x++;
                    if (x >= W)
                        x = 0;
                    break;
                case 2:
                    y++;
                    if (y >= H)
                        y = 0;
                    break;
                case 3:
                    x--;
                    if (x < 0)
                        x = W - 1;
                    break;
            }
            for(int i=1; i<snake.Count;i++)
            {
                if (snake[0].X == snake[i].X && snake[0].Y == snake[i].Y)
                {
                    timer.Stop();
                    MessageBox.Show("game over ");
                    
                }
            }
            coord c = new coord(x, y); // сегмент с новыми координатами головы
            snake.Insert(0, c); // вставляем его в начало списка сегментов змеи(змея выросла на один сегмент)
            if (snake[0].X == apple.X && snake[0].Y == apple.Y) // если координаты головы и яблока совпали
            {
                apple = new coord(rand.Next(W), rand.Next(H)); // располагаем яблоко в новых случайных координатах
                for (int i = 1; i < snake.Count; i++)
                {
                    if (snake[i].X == apple.X && snake[i].Y == apple.Y)
                    {
                        apple = new coord(rand.Next(W), rand.Next(H));
                        i = 1;
                    }
                }
           
                apples++; // увеличиваем счетчик собранных яблок
                score += stage; // увеличиваем набранные очки в игре: за каждое яблоко прибавляем количество равное номеру уровня
                if (apples % 10 == 0) // после каждого десятого яблока
                {
                    stage++; // повышаем уровень
                    timer.Interval -= 10; // и уменьшаем интервал срабатывания яблока
                }
            }
            else // если координаты головы и яблока не совпали - убираем последний сегмент змеи(т.к. ранее добавляли новую голову)
                snake.RemoveAt(snake.Count - 1);
            Invalidate(); // перерисовываем, т.е. идет вызов Program_Paint
        }

        // собственно, отрисовка
        void Program_Paint(object sender, PaintEventArgs e)
        {
            // рисуем красным кружком яблоко, синим квадратом голову змеи и зелеными квадратами тело змеи
            e.Graphics.FillEllipse(color, new Rectangle(apple.X * S, apple.Y * S, S, S));
            e.Graphics.FillRectangle(Brushes.Yellow, new Rectangle(snake[0].X * S, snake[0].Y * S, S, S));
            for (int i = 1; i < snake.Count; i++)
                e.Graphics.FillRectangle(Brushes.Green, new Rectangle(snake[i].X * S, snake[i].Y * S, S, S));
            // сообщение о количестве собранных яблок, уровне и количестве очков
            string state = "Apples:" + apples.ToString() + "\n" +
                "Stage:" + stage.ToString() + "\n" + "Score:" + score.ToString();


            // выводим это сообщение в левом верхнем углу
            e.Graphics.DrawString(state, new Font("Arial", 10, FontStyle.Italic), Brushes.Black, new Point(5, 5));
        }
    }
}
