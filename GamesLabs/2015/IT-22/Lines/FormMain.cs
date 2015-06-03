

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Xml;
using System.Xml.Schema;

namespace linesk
{

    public partial class FormMain : Form
    {
        // Подготовим генератор псевдослучайных чисел
        // для инициализации начала игры(разброс графэлементов)
        // и добавление графэелементов в процессе игры.
        Random rand = new Random(Environment.TickCount);
        GraphItem[] GItems = null; // главные действующие лица, графические элементы
        GameSetting GameSet = new GameSetting(); // модуль запоминания настроек пользователя
        string NamePlayer = "Игрок"; // имя игрока по умолчанию

        public FormMain()
        {
            InitializeComponent();

            // Перед загрузкой формы выделим память для всех графических элементов.
            GItems = new GraphItem[Global.NumGraphItems];
            for (int i = 0; i < Global.NumGraphItems; i++)
                GItems[i] = new GraphItem(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Попытка загрузить данные из файла настройки,
            // если они существуют.
            FromFileIni();

            // Инициализация начала игры
            InitNewGame();

            // Расчет координат расположения графических элементов
            ComputeCellsCoordinate();
        }
        
        private void FormMain_MouseClick(object sender, MouseEventArgs e)
        {
            int indexClick = -1; // индекс кликнутого графэлемента

            // Если кликнули по видимому графэлементу(с геометрической фигурой), сделаем его активным,
            // т.е. мигающим размером и выходим из функции.
            for (int i = 0; i < 100; i++)
            {
                if (GItems[i].CellCoordinate.Contains(e.X, e.Y) == true)
                {
                    indexClick = i;
                    if (GItems[i].Visible == true)
                    {
                        for (int p = 0; p < Global.NumGraphItems; p++)
                        {
                            GItems[p].Active = false;
                        }
                        GItems[i].Active = true;
                        return;
                    }
                }
            }


            // Если кликнули по невидимому графэлементу(без геометрической фигуры), проводим нижеследующие
            // процедуры,
            // 1. находим активный графэлемент (если его нет выполнится просто пустой цикл)
            // 2. проверим может ли активный графэлемент переместится на указаное
            // пользователем место.
            // 3. меняем местами и свойствами активный и невидимый графэлементы.
            for (int a = 0; a < 100; a++)
            {
                if (GItems[a].Active == true)
                {
                    if (indexClick != -1)
                    {
                        if (CheckCanMoveGraphItem(GItems[a], GItems[indexClick]) == true)
                        {
                            // меняем местами и свойствами вычисленные графэлементы
                            Color color = GItems[indexClick].Color;
                            GItems[indexClick].Color = GItems[a].Color;
                            GItems[indexClick].Active = false;
                            GItems[a].Color = color;
                            GItems[a].Visible = false;
                            GItems[a].Active = false;
                            GItems[indexClick].Visible = true;
                            Invalidate();

                            // Делаем небольшую задержку данной функции, без задержки работы приложения,
                            // для акцентирования внимания пользователя на перемещение графэлемента.
                            Application.DoEvents();
                            Thread.Sleep(80);

                            // после задержки добавляем новые графэлементы
                            ShowGraphItems(3);

                            // Снова небольшая задержка для акцентирования внимания
                            // пользователя на добавление новых графэлементов.
                            Application.DoEvents();
                            Thread.Sleep(80);

                            // Если выявлена последовательность графэлементов из
                            // 5 и более штук, удаляем последовательность.
                            bool hidegraphitems = HideGraphItems();

                            // Небольшая задерхка для акцентирования внимания
                            // пользователя на исчезание шаров.
                            Application.DoEvents();
                            Thread.Sleep(50);

                            // Окончательно прорисовываем форму и 
                            // перераспределяем места игроков относительно
                            // текущего игрока.
                            Invalidate();
                            ParseDataPlayers();
                            Invalidate();

                            // Если исчезание произошло, значит проверку окончания игры 
                            // откладываем, иначе проверяем игру на полное заполнение
                            // ячеек графэлементов.
                            if (hidegraphitems == false)
                                IsEndGame();

                        }
                    }

                    break;
                }
            }
        }
        
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;


            Font font = new Font("Times New Roman", 12);
            int x = GItems[90].CellCoordinate.X + GItems[90].CellCoordinate.Width + 20;
            int y = 40 + toolStrip1.Height;
            
            g.DrawString("Играет - " + NamePlayer, font, new SolidBrush(Color.White), x, y);


            y = 60 + toolStrip1.Height;
            for (int i = 0; i < GameSet.DRH.Length; i++)
            {
                if (GameSet.DRH[i].CurrentPlayer == true)
                {
                    g.DrawString("Набранные очки - " + GameSet.DRH[i].Score.ToString(), font, new SolidBrush(Color.White), x, y);
                    break;
                }
            }


            g.DrawString("Рекордсмены:", font, new SolidBrush(Color.White), x, y + 20);

            y += 80;
            for (int i = 0; i < GameSet.DRH.Length - 0; i++)
            {
                if (GameSet.DRH[i] != null && GameSet.DRH[i].Score > 0)
                {
                    if (GameSet.DRH[i].CurrentPlayer == false)
                    {
                        g.DrawString((i + 1).ToString() + ". " + GameSet.DRH[i].Name, font, new SolidBrush(Color.White), x, y + i * 20);
                        g.DrawString("\t\t" + GameSet.DRH[i].Score.ToString(), font, new SolidBrush(Color.White), x, y + i * 20);
                    }

                    if (GameSet.DRH[i].CurrentPlayer == true)
                    {
                        Font f = new Font("Times New Roman", 12);
                        g.DrawString((i + 1).ToString() + ". " + GameSet.DRH[i].Name, f, new SolidBrush(Color.Yellow), x, y + i * 20);
                        g.DrawString("\t\t" + GameSet.DRH[i].Score.ToString(), f, new SolidBrush(Color.Yellow), x, y + i * 20);
                    }

                }
            }
           

            for (int i = 0; i < Global.NumGraphItems; i++)
            {
                GItems[i].Draw(g);
            }

            
           
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            // При изменении размеров окна корректируем
            // координаты сетки поля и положения графэлементов.
            ComputeCellsCoordinate();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // При закрытии приложения все настройки запишем в файл настроек.
            ToFileIni();
        }

        private void toolStripButtonNewGame_Click(object sender, EventArgs e)
        {
            InitNewGame();
        }


        private void toolStripButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButtonGameSetting_Click(object sender, EventArgs e)
        {
            FormGameSetting fgs = new FormGameSetting(this);
            fgs.TopMost = this.TopMost;
            // Цвета клонируем, иначе простое присваивание просто присвоит fgs.ColorBalls адрес ColorBalls
            // и изменение будут происходить одновременно в обоих массивах.
            fgs.ColorGraphItems = (Color[])GameSet.GraphItems.Clone();
            fgs.CurrentGraphItem = GameSet.CurrentGraphItem;
            if (fgs.ShowDialog() == DialogResult.OK)
            {
                // Запомним данные игры для сохранения в файл.
                GameSet.CurrentGraphItem = fgs.CurrentGraphItem;
                // Изменим тип графэлемента во всех объектах класса GraphItem.
               // GraphItem.CurrentTypeGraphItem = GameSet.CurrentGraphItem;
            }
        }

        private void toolStripButtonChangePlayersName_Click(object sender, EventArgs e)
        {
            // Изменение имени текущего игрока.
            FormPlayerName fpn = new FormPlayerName();
            fpn.TopMost = this.TopMost;
            fpn.PlayerName = NamePlayer;
            if (fpn.ShowDialog() == DialogResult.OK)
            {
                NamePlayer = fpn.PlayerName;
                for (int p = 0; p < GameSet.DRH.Length; p++)
                {
                    if (GameSet.DRH[p].CurrentPlayer == true)
                    {
                        GameSet.DRH[p].Name = NamePlayer;
                        break;
                    }
                }
                Invalidate();
            }
        }

        #region Инициализация игры, проверка окончания

        /// <summary>
        /// Инициализация данных графических элементов и текущего игрока
        /// </summary>
        void InitNewGame()
        {
            // Создаем сразу все графэлементы, распределенными
            // по игровому полю в случайным порядке,
            // присвоим  графическим элементам случайные цвета,
            // и все графические элементы пока будут невидимы.
            try
            {
                XmlReaderSettings Settings = new XmlReaderSettings();
                Settings.Schemas.Add(null, "xsd.xsd");
                Settings.ValidationType = ValidationType.Schema;
                Settings.ValidationEventHandler += new ValidationEventHandler(ValidateXML);
                XmlReader XmlR = XmlReader.Create("settings.xml", Settings);
                while (XmlR.Read()) { }
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("settings.xml");
                GameSet.DRH[Global.NumPlayers - 1].Name = xDoc.DocumentElement.SelectSingleNode("glob").ChildNodes[0].InnerText;
                GameSet.DRH[Global.NumPlayers - 1].Score=int.Parse(xDoc.DocumentElement.SelectSingleNode("glob").ChildNodes[1].InnerText);
                
            }
            catch
            {
                MessageBox.Show("error");
            }
            for (int i = 0; i < Global.NumGraphItems; i++)
            {
                int r = rand.Next(Global.AmountColorBalls);
                GItems[i].Color = GameSet.GraphItems[r];
                GItems[i].Visible = false;
            }

            // Установка типа графического элемента для всего массива,
            // с помощью статической переменной GraphItem.CurrentTypeGraphItem.
            GraphItem.CurrentTypeGraphItem = GameSet.CurrentGraphItem;

            // Для начала игры сделаем видимыми первые пять графэлементов.
            ShowGraphItems(5);

            // Снимаем метку текущий игрок у предыдущего игрока,
            // это происходит при повторных играх.
            for (int i = 0; i < GameSet.DRH.Length; i++)
            {
                if (GameSet.DRH[i] != null)
                    GameSet.DRH[i].CurrentPlayer = false;
            }
            
            // Добавим нового игрока в рейтинг игроков,
            // на последнее-невидимое место, присваиваем новому игроку текущее имя.
            GameSet.DRH[Global.NumPlayers - 1].CurrentPlayer = true;
            //GameSet.DRH[Global.NumPlayers - 1].Name = NamePlayer;
            
            //GameSet.DRH[Global.NumPlayers - 1].Score = 200;

        }
        private void ValidateXML(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                MessageBox.Show("Warning: " + e.Message);

                //flag = false;
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                MessageBox.Show("Error: " + e.Message);

                //flag = false;
            }
        }
        // Проверка окончания игры.
        bool IsEndGame()
        {
            // Если есть хоть один невидимый графэлемент,
            // значит игра еще не закончилась.
            for (int i = 0; i < Global.NumGraphItems; i++)
            {
                if (GItems[i].Visible == false)
                {
                    return false;
                }
            }

            // Получим количество очков текущего игрока
            int score = 0;
            for (int i = 0; i < GameSet.DRH.Length; i++)
            {
                if (GameSet.DRH[i].CurrentPlayer == true)
                {
                    score = GameSet.DRH[i].Score;
                    break;
                }
            }

            FormEndGame formEndGame = new FormEndGame(score);
            formEndGame.TopMost = this.TopMost;
            if (formEndGame.ShowDialog() == DialogResult.OK)
            {
                toolStripButtonNewGame_Click(null, null);
            }
            return true;
        }

        #endregion

        #region Работа с графэлементами: показ, вычисление координат, возможность перемещения

        /// <summary>
        /// Показать графэлементы
        /// </summary>
        /// <param name="numballs">заказное количество показываемых графэлементов</param>
        void ShowGraphItems(int numballs)
        {
            // Для работы приложения необходим список невидимых геометрических фигур графэлементов.
            List<GraphItem> hideGraphItems = new List<GraphItem>();
            for (int i = 0; i < Global.NumGraphItems; i++)
            {
                if (GItems[i].Visible == false)
                    hideGraphItems.Add(GItems[i]);
            }

            // При каждом новом показе изменяем цвета геометрических элементов,
            // иначе игра может зациклится. Т.е. если в одном месте стоят четыре
            // фигуры одинакового цвета и появится пятая, то они исчезнут,
            // но при следующем показе (если цвета не изменить) цикл повторится.
            for (int i = 0; i < hideGraphItems.Count; i++)
            {
                int r = rand.Next(Global.AmountColorBalls);
                hideGraphItems[i].Color = GameSet.GraphItems[r];
            }
            
            // Новые фигуры графэлементов появятся только на тех местах,
            // где они невидимы.
            for (int i = 0; i < numballs; i++)
            {
                int r = rand.Next(hideGraphItems.Count);

                // выберем невидимую фигуру
                int count = 0;
                while (count < hideGraphItems.Count)
                {
                    if (hideGraphItems[r].Visible == true)
                    {
                        // Если попали на видимую фигуру,
                        // то далее по кругу, до тех пор
                        // пока не найдем невидимую фигуру.
                        r++;
                        if (r == hideGraphItems.Count)
                            r = 0;
                    }
                    else
                    {
                        break;
                    }

                    count++;
                }

                hideGraphItems[r].Visible = true;
                
            }

            Invalidate();
        }

        void ComputeCellsCoordinate()
        {
            int num = (Global.NumGraphItems / 10);
            int lenside = 0;// длина стороны квадрата области расположения графэлементов

            // используем размеры клиентской области
            int width = this.ClientRectangle.Width;
            int height = this.ClientRectangle.Height - toolStrip1.Height;

            // длина стороны области графэлементов чуть меньше клиентской высоты
            lenside = height - 20;

            // разница между длиной стороны области графэлементов
            // и высотой
            int deltaY = height - lenside;

            // длина стороны ячейки
            int lenCell = lenside / num;

            // по колонно расчитываем расположение ячеек графэлементов
            int count = 0;
            for (int x = 0; x < num; x++)
            {
                for (int y = 0; y < num; y++)
                {
                    GItems[count].CellCoordinate = new Rectangle(x * lenCell + 10, y * lenCell + deltaY / 2 + toolStrip1.Height, lenCell, lenCell);
                    count++;
                }
            }

            // В итоге ячейки расмещены в таком порядке:
            //////////////////////////////////
            // 0 10 20 30 40 50 60 70 80 90 //
            // 1 11 21 31 41 51 61 71 81 91 //
            // 2 12 22 32 42 52 62 72 82 92 //
            // 3 13 23 33 43 53 63 73 83 93 //
            // 4 14 24 34 44 54 64 74 84 94 //
            // 5 15 25 35 45 55 65 75 85 95 //
            // 6 16 26 36 46 56 66 76 86 96 //
            // 7 17 27 37 47 57 67 77 87 97 //
            // 8 18 28 38 48 58 68 78 88 98 //
            // 9 19 29 39 49 59 69 79 89 99 //
            //////////////////////////////////

            Invalidate();
        }

        /// <summary>
        /// Проверкана возможность перемещения графэлемента на указанное
        /// игроком место
        /// </summary>
        /// <param name="activeBall">активный графэлемент</param>
        /// <param name="placeBall">предполагаемое новое место размещения</param>
        /// <returns>false - нельзя, true - можно</returns>
        bool CheckCanMoveGraphItem(GraphItem activeGraphItem, GraphItem placeGraphItem)
        {
            int x = activeGraphItem.CellCoordinate.X;
            int y = activeGraphItem.CellCoordinate.Y;



            // Перемещать фигуру можно только по горизонтали или вертикали,
            // по диагонали нельзя.
            if (x != placeGraphItem.CellCoordinate.X && y != placeGraphItem.CellCoordinate.Y) return false;

            // Если если есть исчезающие графэлементы, перемещеть активный тоже нельзя,
            // это может произойти когда игрок попробует переместить графэлемент во время исчезания
            // последовательности, в итоге исчезающие графэлементы изменят цвет еще оставаясь видимыми.
            for (int vanish = 0; vanish < GItems.Length; vanish++)
            {
                if (GItems[vanish].Vanish == true) return false;
            }

            // Вычислим по какой координате будем проверять свободность пути
            // перемещения активного шара.
            // Проверка по оси Y
            if (x == placeGraphItem.CellCoordinate.X)
            {
                // Y координата нового места размещения
                int yPlaceGraphItem = placeGraphItem.CellCoordinate.Y;
                for (int i = 0; i < 100; i++)
                {
                    // Если хоть один исследуемый графический элемент расположенный оси Y видим,
                    // и находится на пути между новым указанным пользователем местом
                    // и самим активным элементом перемещать активный графэлемент нельзя.
                    if (GItems[i].CellCoordinate.X == x && GItems[i].Visible == true &&
                        GItems[i].CellCoordinate.Y > Math.Min(y, yPlaceGraphItem) &&
                        GItems[i].CellCoordinate.Y < Math.Max(y, yPlaceGraphItem))
                    {
                        return false;
                    }
                }

                // Если на пути нет ни одного графэлемента перемещать активный можно.
                //if (listCheck.Count == 0) return true;
            }
            // Проверка по оси Х
            else if (y == placeGraphItem.CellCoordinate.Y)
            {
                int xPlaceGraphItem = placeGraphItem.CellCoordinate.X;
                for (int i = 0; i < 100; i++)
                {
                    if (GItems[i].CellCoordinate.Y == y && GItems[i].Visible == true &&
                        GItems[i].CellCoordinate.X > Math.Min(x, xPlaceGraphItem) &&
                        GItems[i].CellCoordinate.X < Math.Max(x, xPlaceGraphItem))
                    {
                        //listCheck.Add(GItems[i]);
                        return false;
                    }
                }
            }



            // В остальных случаях перемещать активный графэлемент нельзя.
            return true;
        }

        #endregion
        
        #region Проверка непрерывной последовательности из 5 и более геометрических фигур

        // Проверка по колонно
        bool CheckColumn()
        {
            List<bool> listBool = new List<bool>();
            
            // Проверим последовательно все колонны игровой области
            for (int col = 0; col < 10; col++)
            {
                List<GraphItem> column1 = new List<GraphItem>();
                column1.Add(GItems[10 * col + 0]);
                column1.Add(GItems[10 * col + 1]);
                column1.Add(GItems[10 * col + 2]);
                column1.Add(GItems[10 * col + 3]);
                column1.Add(GItems[10 * col + 4]);
                column1.Add(GItems[10 * col + 5]);
                column1.Add(GItems[10 * col + 6]);
                column1.Add(GItems[10 * col + 7]);
                column1.Add(GItems[10 * col + 8]);
                column1.Add(GItems[10 * col + 9]);


                listBool.Add(CheckList(column1));

            }

            for (int i = 0; i < listBool.Count; i++)
            {
                if (listBool[i] == true)
                    return true;
            }
            return false;
        }

        // Проверка построчно на предмет повторяющейся последовательности 5 и более графэлементов.
        bool CheckRow()
        {
            List<bool> listBool = new List<bool>();
            // Проверка всех строк игрового на обнаружение последовательности.
            for (int col = 0; col < 10; col++)
            {
                List<GraphItem> column1 = new List<GraphItem>();

                column1.Add(GItems[0 + col]);
                column1.Add(GItems[10 + col]);
                column1.Add(GItems[20 + col]);
                column1.Add(GItems[30 + col]);
                column1.Add(GItems[40 + col]);
                column1.Add(GItems[50 + col]);
                column1.Add(GItems[60 + col]);
                column1.Add(GItems[70 + col]);
                column1.Add(GItems[80 + col]);
                column1.Add(GItems[90 + col]);


                listBool.Add(CheckList(column1));

            }

            for (int i = 0; i < listBool.Count; i++)
            {
                if (listBool[i] == true)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Проверка по диагонали вверх направо на предмет повторяющейся 
        /// последовательности из 5 и более графэлементов.
        /// </summary>
        bool CheckDiagonal()
        {
            // Диагонали  
            // Первая колонка вверх направо
            List<bool> listBool = new List<bool>();
            int count = 0;
            int k = 1;
            List<GraphItem> dList = new List<GraphItem>();

            // Диагональные места расположения графэлементов отличаются
            // разностью индексов которая всегда равна 9,
            // т.е. начинаем с левого 5 места сверху(4 место и выше имеют менее 5 диагональных мест на линии),
            // и прибавляем каждый раз 9, таким образом появляется цепочка мест по диагонали вверх направо,
            // как только проверили 5, начинаем 6 и +9 = 15 + 9 = 24 + 9 = 33 + 9 = 42 + 9 = 51 + 9 = 60 и т.д.
            for (int i = 0; i < (5 + count); i++)
            {
                int d1 = (4 + count) + 9 * i;
                dList.Add(GItems[d1]);
               

                if (k == -1 && count == 4)
                    break;

                if ((5 + count) == (i + 1))
                {
                    i = -1;

                    if (count == 5)
                        k = -1;

                    count += k;
                    listBool.Add(CheckList(dList));
                    dList.Clear();

                }


            }

            

            // Остатки диагонали после середины вверх направо
            // Подобно как и выше - 19 + 9 = 28 + 9 = 37 + 9 = 46 + 9 = 55 + 9 = 64 + 9 = 73 + 9 = 82 + 9 = 91,
            // и далее 29 + 9 = 38 + 9 = 47 + 9 = 56 + 9 = 65 + 9 = 74 + 9 = 83 + 9 = 92 и т.д.
            count = 4;
            k = -1;
            dList.Clear();
            for (int i = 0; i < (5 + count); i++)
            {
                int d1 = 0;

                d1 = 19 + 10*(4-count)  + 9*i;
                dList.Add(GItems[d1]);

                if (k == 1 && count == 1)
                    break;

                if ((5 + count) == (i + 1))
                {
                    i = -1;

                    if (count == 0)
                        k = 1;

                    count += k;
                    listBool.Add(CheckList(dList));
                    dList.Clear();


                }

                
               
            }

            for (int i = 0; i < listBool.Count; i++)
            {
                if (listBool[i] == true)
                    return true;
            }

            return false;
                                         
        }

        /// <summary>
        /// Проверка по диагонали вниз направо на предмет повторяющейся 
        /// последовательности из 5 и более графэлементов.
        /// </summary>
        bool CheckDiagonal2()
        {
            // Диагонали  
            // Первая колонка вниз направо,
            // начиная с левой колонки снизу до нуля
            //bool[] killballs = new bool[2] { false, false };
            List<bool> listBool = new List<bool>();
            int count = 0;
            int k = 1;
            List<GraphItem> dList = new List<GraphItem>();

            // Подобно первой диагонали только различие индексов на 11 единиц.
            // Начиная с 0 места самой длинной диагональю
            // 0 + 11 = 11 + 11 = 22 + 11 = 33 + 11 = 44 + 11 = 55 + 11 = 66 + 11 = 77 + 11 = 88 + 11 = 99, далее на 1 место
            // 1 + 11 = 12 + 11 = 23 + 11 = 34 + 11 = 45 + 11 = 56 + 11 = 67 + 11 = 78 + 11 = 89 и т.д.
            for (int i = 0; i < (5 + count); i++)
            {
                int d1 = 0;

                d1 = (5 - count) + 11 * i;
                dList.Add(GItems[d1]);

                if (k == -1 && count == 4)
                    break;

                if ((5 + count) == (i + 1))
                {
                    i = -1;

                    if (count == 5)
                        k = -1;

                    count += k;

                    listBool.Add(CheckList(dList));
                    dList.Clear();
                }

             
            }
            

            

            // Остатки диагонали после середины вниз направо, начиная с 10 до 50.
            // Диагональные места расположения графэлементов отличаются
            // разностью индексов которая всегда равна 11,
            // т.е. начинаем с левого 10 места сверху и до 50(60 место и правее имеют менее 5 диагональных мест на линии),
            // и прибавляем каждый раз 11, таким образом появляется цепочка мест по диагонали вниз направо,
            // 10 + 11 = 21 + 11 = 32 + 11 = 43 + 11 = 54 + 11 = 65 + 11 = 76 + 11 = 87 + 11 = 98, переходим на 20 место
            // 20 + 11 = 31 + 11 = 42 + 11 = 53 + 11 = 64 + 11 = 75 + 11 = 86 + 11 = 97 и т.д.
            count = 4;
            k = -1;
            dList.Clear();
            for (int i = 0; i < (5 + count); i++)
            {
                int d1 = 0;

                d1 = 10 + 10 * (4 - count) + 11 * i;
                dList.Add(GItems[d1]);

                if (k == 1 && count == 1)
                    break;

                if ((5 + count) == (i + 1))
                {
                    i = -1;

                    if (count == 0)
                        k = 1;

                    count += k;

                    listBool.Add(CheckList(dList));
                    dList.Clear();

                }

            }


            for (int i = 0; i < listBool.Count; i++)
            {
                if (listBool[i] == true)
                    return true;
            }
            
            return false;
        }

        /// <summary>
        /// Проверка каждой линии на предмет расположенных последовательно графических элементов
        /// одного цвета 5 и более штук.
        /// </summary>
        /// <param name="parseList">Исследуемый линия графических элементов</param>
        /// <returns>true - есть последовательность графических элементов из 5 и более штук, иначе - false </returns>
        bool CheckList(List<GraphItem> parseList)
        {
            bool hidegraphitems = false;
            
            Color color;
            // Вспомогательный список для исчезающих графэлементов
            List<GraphItem> hideList = new List<GraphItem>();
            for (int i = 0; i < parseList.Count; i++)
            { 
                color = parseList[i].Color; // запомним цвет исследуемого впереди идущего графэлемента
                hideList.Add(parseList[i]); // добавим во вспомогательный список графэлемент из исследуемой линии

                // если графэлемент уже невидим очищаем вспомогательный список 
                // и переходим к следующему циклу.
                if (parseList[i].Visible == false) { hideList.Clear(); continue; }

                // сравним цвет впереди идущего графэлемента с далее идущими
                for (int p = i + 1; p < parseList.Count; p++)
                {
                    // Если цвет последующего графэлемента сопадает с впереди идущим
                    // добавляем его в список. Как только попадается другой цвет 
                    // данный цикл сразу останавливаем.
                    if (parseList[p].Visible == true && parseList[p].Color == color)
                        hideList.Add(parseList[p]);
                    else
                    {
                        break;
                    }
                }

                // Теперь проверим вспомогательный список, если в нем 
                // 5 и более графэлементов, поставим на исчезновение.
                if (hideList.Count >= 5)
                {
                    // все графэлементы во вспомогательном поставим на исчезание
                    for (int n = 0; n < hideList.Count; n++)
                    {
                        hideList[n].Vanish = true;
                    }
                    // за каждый исчезающий графэлемент прибавляем игроку 10 очков
                    for (int cp = 0; cp < GameSet.DRH.Length; cp++)
                    {
                        if (GameSet.DRH[cp].CurrentPlayer == true)
                        {
                            GameSet.DRH[cp].Score += hideList.Count * 10;
                            break;
                        }
                    }

                    // Если нашли даже одну последовательность из 5 и более графэлементов,
                    // возврат помечаем как true.
                    hidegraphitems = true;

                    // Воспроизведем звук исчезновения графэлементов.
                    // Ресурс получаем по схеме ("namespace.pathresource");
                    Stream res = this.GetType().Assembly.GetManifestResourceStream("linesk.soundvanish.wav");
                    SoundPlayer sp = new SoundPlayer(res);
                    sp.PlaySync();
                    //sp.Play();
                    
                }

                hideList.Clear();
            }

            return hidegraphitems;
        }

        /// <summary>
        /// Вычисление последовательности из 5 и более графэлементов
        /// и последующее их исчезновение. Возращаемое значение
        /// используем для правильного определения
        /// окончания игры.
        /// </summary>
        bool HideGraphItems()
        {
            // Проверим все линии из 5 и более графэлементов,
            // по вертикали, горизонтали, по диагоналям.
            // Если такая последовательность найдена возвращаем true
            // для правильного определения окончания игры.

            List<bool> listBool = new List<bool>();
            listBool.Add(CheckColumn());
            listBool.Add(CheckRow());
            listBool.Add(CheckDiagonal());
            listBool.Add(CheckDiagonal2());

            // Если хоть одна последовательность нашлась значит игра еще не окончена.
            for (int i = 0; i < listBool.Count; i++)
            {
                if (listBool[i] == true)
                    return true;
            }

            return false;

        }

        #endregion

        #region Чтение из файла и запись в файл настроек игры

        /// <summary>
        /// Запись всех настроек в файл .lin на диск в
        /// папку где находится исполняемый файл.
        /// </summary>
        void ToFileIni()
        {
            // На всякий случай работу с файлом заключим в блок try 
            // поскольку файл внешнее добавление к приложению, 
            // стало быть полностью не подконтролен приложению.
            try
            {
                // Запомним расположение и размеры главного окна приложения.
                GameSet.Bounds = this.Bounds;

                // Файл настройки всегда будет создаваться в месте расположения файла приложения.
                string filePath = Application.StartupPath + "\\settings.lin";
                FileStream fileStream = File.Create(filePath);

                // Данные будем хранить в двоичном виде
                BinaryFormatter bf = new BinaryFormatter();

                // При закрытии приложения сбросим метку текущего игрока.
                for (int i = 0; i < GameSet.DRH.Length; i++)
                {
                    GameSet.DRH[i].CurrentPlayer = false;
                }
                bf.Serialize(fileStream, GameSet);
                fileStream.Close();
            }
            catch
            {
                
            }

        }

        /// <summary>
        /// Чтение настроек из файла.
        /// </summary>
        void FromFileIni()
        {
            FileStream fileStream = null;
            try
            {
                string filePath = Application.StartupPath + "\\settings.lin";

                FileInfo fi = new FileInfo(filePath);
                if (fi.Exists == false)
                    return;

                fileStream = File.OpenRead(filePath);
                BinaryFormatter bf = new BinaryFormatter();
                GameSet = (GameSetting)bf.Deserialize(fileStream);

                // При загрузке данных игры игроков удалим лишних игроков
                // с одинаковыми именами и оставим из них одного с наибольшими очками, после данной
                // процедуры массив данных игры просортируем по очкам.
                for (int i = 0; i < GameSet.DRH.Length; i++)
                {
                    for (int j = i+1; j < GameSet.DRH.Length; j++)
                    {
                        if (GameSet.DRH[j].Score != 0 && GameSet.DRH[i].Name == GameSet.DRH[j].Name)
                        {
                            GameSet.DRH[j].Score = 0; // игрок имеющий ноль очков удаляется из списка
                        }
                    }
                }

                Array.Sort(GameSet.DRH, new SortRecordHolders());
                
            }
            catch
            {
                
            }
            finally // этот блок выполнится в любом случае
            {
                // В любом случае попытаемся после использования файла настроек закрыть его.
                if(fileStream != null)
                    fileStream.Close();

                // Если размеры окна очень большие и очень маленькие или окно возникло за пределами экрана 
                // восстановим окно приложения до приемлемых размеров и положения.
                if (GameSet.Bounds.Width > (Screen.PrimaryScreen.Bounds.Width - 20) ||
                    GameSet.Bounds.Height > (Screen.PrimaryScreen.Bounds.Height - 20) ||
                    GameSet.Bounds.Width < 500 || GameSet.Bounds.Height < 300 ||
                    GameSet.Bounds.Left < 0 || GameSet.Bounds.Top < 0 ||
                    GameSet.Bounds.Left > Screen.PrimaryScreen.Bounds.Right || GameSet.Bounds.Top > Screen.PrimaryScreen.Bounds.Bottom
                    )
                {
                    this.Width = 640;
                    this.Height = 480;

                }
                else
                {
                    this.Bounds = GameSet.Bounds;
                }
            }

        }

        #endregion
        
        #region Сортировка игроков по очкам

        // Исследуем данные игроков на предмет количества очков и рапределения
        // на основе этого призовых мест.
        // Алгоритм высчитывания присуждение призового места текущему игроку.
        // 1. Начинается новая игра - добавляем игрока на шестое место в массив данных игроков.
        // 2. После каждого хода сортируем массив игроков:
	    // 1) Если индекс игрока уменьшился, значит он переместился на более высокое призовое место.
        void ParseDataPlayers()
        {
            // Сравним индекс игрока до хода и после хода.
            int index = -1;
            for (int i = 0; i < GameSet.DRH.Length; i++)
            {
                if (GameSet.DRH[i].CurrentPlayer == true)
                {
                    index = i;
                    Array.Sort(GameSet.DRH, new SortRecordHolders());
                    break;
                }
            }

            // Если после сортировки индекс игрока уменьшился,
            // значит он перешел на новое более высокое
            // призовое место. 
            for (int i = 0; i < GameSet.DRH.Length; i++)
            {
                if (GameSet.DRH[i].CurrentPlayer == true)
                {
                    // Если игрок перешел на первое место, поздравим его.
                    if (i < index && i == 0)
                    {
                        // Добавить музыку поздрвления, туш например
                        Stream res = (Stream)this.GetType().Assembly.GetManifestResourceStream("linesk.tush.wav");
                        SoundPlayer sp = new SoundPlayer(res);
                        sp.Play();
                    }
                    break;
                }
            }
            
        }

        // Вспомогательный класс для сортировки данных игроков, реализующий интерфейс IComparer,
        // интерфейс позволяет программисту создавать классы с специальными(разработанными под себя)
        // функциями сравнения чего угодно.
        // Объявлен внутри главного класса.
        public class SortRecordHolders : IComparer
        {
            // реализация интерфейса IComparer
            public int Compare(object o1, object o2)
            {
                DataRecordsman drh1 = (DataRecordsman)o1;
                DataRecordsman drh2 = (DataRecordsman)o2;

                // Сравнение по очкам, преимущество имеют большие очки.
                int result = drh2.Score.CompareTo(drh1.Score);

                // Если очки равные, преимущество имеет тот игрок который 
                // завоевал очки раньше текущего игрока.
                if (result == 0)
                    result = drh1.CurrentPlayer.CompareTo(drh2.CurrentPlayer);

                return result;
            }

        }

        #endregion

    }


    
}
