using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace game
{
    public partial class Main : Form
    {        
        public int LEVEL { get; set; } //Переменная для выбора уровня сложности
        private bool restart = true; //Флаг для определения события кнопки "рестарт"
        private bool isValid;
        public List<int> ForPoints = new List<int>();
        public List<string> ForTime = new List<string>();

        Time T;
        Errors E;
        NextLevel NL;
        MapBuilder MB;
        LevelBuilder LB;
        Records Rec = new Records();
        Functions F = new Functions();
        Generator Gen = new Generator();
        public List<int> EnList = new List<int>();

        public Main()
        {
            InitializeComponent();

            LoadXML();

            LB = new LevelBuilder(this);
            LB.CheckBoxLevels();         //Построение списка выбора уровня сложности
            Rec.ReadRecord();               //Считывание рекордов с файла XML
            
            /* Плавная анимация при запуске приложения */
            Opacity = 0;
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler((sender, e) =>
            { if ((Opacity += 0.05d) == 1) timer.Stop(); });
            timer.Interval = 25; timer.Start();
        }

        private void LoadXML()
        {
            try
            {
                XmlReaderSettings XMLSettings = new XmlReaderSettings();
                XMLSettings.Schemas.Add(null, "data.xsd");
                XMLSettings.ValidationType = ValidationType.Schema;
                XMLSettings.ValidationEventHandler += new ValidationEventHandler(ConfigCheckValidationEventHandler);
                XmlReader XMLReader = XmlReader.Create("data.xml", XMLSettings);
                isValid = true;

                while (XMLReader.Read()) { }

                if (isValid == true)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("data.xml");

                    XmlNodeList records =  doc["root"]["config"].ChildNodes;
                    foreach(XmlNode node in records)
                    {
                        ForPoints.Add(int.Parse(node["points"].InnerText));
                        ForTime.Add(node["time"].InnerText);
                    }
                }
                else
                {
                    for (int i = 0; i < 20; i++)
                    {
                        ForPoints.Add(0);
                        ForTime.Add("00:00:00");
                    }
                }
            }
            catch(Exception Ex)
            {
                string TextError = "Ошибка: " + Ex.Message;
                E = new Errors(TextError);
                if (E.ShowDialog() == DialogResult.OK) { }
                for (int i = 0; i < 20; i++)
                {
                    ForPoints.Add(0);
                    ForTime.Add("00:00:00");
                }
            }
        }

        private void ConfigCheckValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                isValid = false;
                string TextError = "Предупреждение: XML файл повреждён!";
                E = new Errors(TextError);
                if (E.ShowDialog() == DialogResult.OK) { }
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                isValid = false;
                string TextError = "Ошибка чтения данных из файла! Загружены стандартные параметры.";
                E = new Errors(TextError);
                if (E.ShowDialog() == DialogResult.OK) { }
            }
        }
        #region "Buttons For Level And ReStart Games"
        private void Restart(object sender, MouseEventArgs e)
        {
            T = new Time(LEVEL);
            MB = new MapBuilder(this, F);
            F.GLOBALSUMGEN = F.SUMBALANCE = 0;                      
            if (restart) //Если игра началась:
            {
                Theme.Text = F.NameLevel(LEVEL);
                MB.Create(LEVEL);                                   // Заполняем сцену новыми кнопками
                if (F.CheckSum(EnList))                             // Обновляем данные и создаём глобальную сумму поиска
                {
                    EnList = MB.GetEnabledButtons();
                    F.GLGenSum(EnList, LEVEL);
                }
                StartButton.Text = "Остановить";                    // Выводим текст на кнопку
                timer.Start();                                      // Запускаем обратный таймер
                GamePanel.Visible = true;                           // Показываем игровое поле
                restart = false;                                    // Меняем событие кнопки на противоположное
            }
            else EndElse();
        }

        private void Training(object sender, MouseEventArgs e)
        {
            LEVEL = -1;
            T = new Time(LEVEL);
            MB = new MapBuilder(this, F);
            F.GLOBALSUMGEN = F.SUMBALANCE = 0;
            if (restart)
            {
                Theme.Text = F.NameLevel(LEVEL);
                MB.Create(LEVEL);                                   // Заполняем сцену новыми кнопками
                if (F.CheckSum(EnList))                             // Обновляем данные и создаём глобальную сумму поиска
                {
                    EnList = MB.GetEnabledButtons();
                    F.GLGenSum(EnList, LEVEL);
                }
                StartButton.Text = "Закончить тренировку";                    //Выводим текст на кнопку
                timer.Start();                                      //Запускаем обратный таймер
                GamePanel.Visible = true;
                CurtainButtonPanel.Visible = false;
                restart = false;                                    //Меняем событие кнопки на противоположное
            }
            else EndElse();
        }
        public void NextLevel()
        {
            F.END = false;                                       // При "END == true" игра остановилась, делаем обратно false
            timer.Stop();                                        // Останавливаем отсчёт времени  
            string ReturnNameLevel = F.NameLevel(LEVEL);           // Запрашиваем название пройденного уровня
            NL = new NextLevel(T, F.SUMBALANCE, ReturnNameLevel);// Отправляем данные в новое окно
            if (NL.ShowDialog() == DialogResult.OK)              // Открываем это диалоговое окно
            {
                if (LEVEL < 19) LEVEL += 1;                      // Если уровень не максимальный - увеличиваем сложность
                Theme.Text = F.NameLevel(LEVEL);                   // Изменяем название формы на название уровня
                T = new Time(LEVEL);                             // Устанавливаем время в зависимости от урвоня
                MB = new MapBuilder(this, F);                    // Генерируем игровое поле в зависимости от урвоня
                F.GLOBALSUMGEN = F.SUMBALANCE = 0;               // Обнуляем счётчик набранных очков
                MB.Create(LEVEL);                                // Заполняем сцену новыми кнопками
                if (F.CheckSum(EnList))                          // Обновляем данные и создаём глобальную сумму поиска
                {
                    EnList = MB.GetEnabledButtons();
                    F.GLGenSum(EnList, LEVEL);
                }
                StartButton.Text = "Остановить";                 // Выводим текст на кнопку
                timer.Start();                                   // Запускаем таймер
                GamePanel.Visible = true;                        // Делаем игровое поле видимым
            }
            else EndElse();
        }

        public void NoComplate()
        { 
            timer.Stop();                                        // Останавливаем отсчёт времени  
            string ReturnNameLevel = F.NameLevel(LEVEL);         // Запрашиваем название пройденного уровня
            NoComplate NC = new NoComplate(F.SUMBALANCE, ReturnNameLevel);  // Выводим сообщение
            if (NC.ShowDialog() == DialogResult.OK)
            {
                Theme.Text = F.NameLevel(LEVEL);
                F.GLOBALSUMGEN = F.SUMBALANCE = 0;               // Обнуляем счётчик набранных очков
                T = new Time(LEVEL);                             // Устанавливаем время в зависимости от урвоня
                MB = new MapBuilder(this, F);                    // Генерируем игровое поле в зависимости от урвоня
                GamePanel.Controls.Clear();                      // Очищаем поле с кнопками, чтобы добавить новые
                MB.Create(LEVEL);                                // Заполняем сцену новыми кнопками
                if (F.CheckSum(EnList))                          // Обновляем данные и создаём глобальную сумму поиска
                {
                    EnList = MB.GetEnabledButtons();
                    F.GLGenSum(EnList, LEVEL);
                }
                StartButton.Text = "Остановить";                 // Выводим текст на кнопку
                timer.Start();                                   // Запускаем таймер
                GamePanel.Visible = true;                        // Делаем игровое поле видимым
            }
            else EndElse();
        }

        private void EndElse()
        {
            Theme.Text = "-+ MathBox +-";                        // Меняем название формы
            LB = new LevelBuilder(this);                         // Инициализируем чекбоксы
            LB.CheckBoxLevels();                                 // Чтобы переписать чекбоксам свойства
            GamePanel.Visible = false;                           // Скрываем игровое поле
            GamePanel.Controls.Clear();                          // Очищаем игровое поле
            StartButton.Text = "Начать заново";                  // Меняем текст на кнопке
            TimeLable.Text = "00:00:00";                         // Меняем счётчик времени
            restart = true;                                      // Меняме событие кнопки на противоположное
            timer.Stop();                                        // Останавливаем таймер
        }

        private void OpenInfoWindows(object sender, MouseEventArgs e)
        { 
            Info window = new Info();
            window.ShowDialog();
        }
        #endregion

        #region "Designer And Ivents"

        private void timer1_Tick(object sender, EventArgs e)
        {  //ТАЙМЕР ОСТАВШЕГОСЯ НА ИГРУ ВРЕМЕНИ
            T.DecSec();
            T.CheckSec();
            if (T.CheckMin()) //Если время вышло:
            {
                timer.Stop();
                NoComplate();
            }
            else //Если время ещё есть:
            {
                TimeLable.Text = T.GetTime(); //Выводим кол-во оставшегося времени
            }
        }

        private void stream_Tick(object sender, EventArgs e)
        {
            if (timer.Enabled == false) SearchGenSum.Text = "?";
            else SearchGenSum.Text = F.GLOBALSUMGEN.ToString();
            Balance.Text = F.SUMBALANCE.ToString();

            if (F.END == true) NextLevel();
        }
        private void TurnProgram(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void CloseProgram(object sender, EventArgs e)
        {
            this.Close();
        }
        private void StartButton_MouseEnter(object sender, EventArgs e)
        {
            StartButton.Color = Color.Gray;
        }
        private void StartButton_MouseLeave(object sender, EventArgs e)
        {
            StartButton.Color = Color.Transparent;
        }
        private void StartButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (restart) StartButton.Color = Color.Green;
            else StartButton.Color = Color.Red;
        }
        #endregion
    }
}