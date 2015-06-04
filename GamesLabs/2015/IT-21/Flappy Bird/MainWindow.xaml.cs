using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Schema;

namespace Flappy_Bird
{
    public partial class MainWindow : Window
    {
        Random Random;
        DispatcherTimer Timer;

        XMLConfig Config;
        Settings Settings;
        Player Player;
        List<Wall> Walls;

        bool Game;
        bool Fail;
        int Scores;

        bool SettingsOK = true;

        public MainWindow()
        {
            InitializeComponent();
            Startup();
        }

        private void ValidateXML(object sender, ValidationEventArgs e)
        {
            if ((e.Severity == XmlSeverityType.Warning) || (e.Severity == XmlSeverityType.Error))
            {
                MessageBox.Show("Ошибка настроек: " + e.Message);

                SettingsOK = false;
            }
        }
        private void Startup()
        {
            XmlReaderSettings xmlreadersettings = new XmlReaderSettings();
            xmlreadersettings.Schemas.Add(null, "xsd.xsd");
            xmlreadersettings.ValidationType = ValidationType.Schema;
            xmlreadersettings.ValidationEventHandler += new ValidationEventHandler(ValidateXML);
            XmlReader xmlreader = XmlReader.Create("Settings.xml", xmlreadersettings);
            while (xmlreader.Read()) { }

            try
            {
                if (SettingsOK)
                {
                    XMLWithDOM xmlWorker = new XMLWithDOM("Settings.xml");
                    Config = xmlWorker.GetConfig();

                    Settings = new Settings(Config.GroundLevel, Config.HoleSize, Config.PlayerSize);
                }
                else
                    throw new Exception();
            }
            catch
            {
                Settings = new Settings(120, 140, 40);
            }

            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            Timer.Tick += new EventHandler(TimerTick);

            Random = new Random();

            this.Width = Settings.WindowSettings.Width;
            this.Height = Settings.WindowSettings.Height;
            this.scoresLabel.Height = Settings.GroundLevel;

            Game = false;
            Scores = 0;

            CreateBackground();

            Player = new Player(Settings, Random.Next(int.MaxValue));
            Walls = new List<Wall>();

            for (int i = 0; i < Settings.WallsSettings.Count; ++i)
            {
                Walls.Add(new Wall(Settings, i, Random.Next(int.MaxValue)));

                mainGrid.Children.Add(Walls[i].Body.UpperBody);
                mainGrid.Children.Add(Walls[i].Body.LowerBody);
            }

            mainGrid.Children.Add(Player.Body);
        }
        private void Reset()
        {
            Player.Reset(this.Settings);

            for (int i = 0; i < Settings.WallsSettings.Count; ++i)
                Walls[i].Reset(Settings);
        }
        private void CreateBackground()
        {
            Grid sky = new Grid();

            sky.Background = Brushes.SkyBlue;
            sky.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            sky.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            sky.Margin = new Thickness(0, 0, 0, Settings.GroundLevel);

            mainGrid.Children.Add(sky);

            Grid ground = new Grid();

            ground.Background = Brushes.Moccasin;
            ground.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            ground.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            ground.Height = Settings.GroundLevel;

            mainGrid.Children.Add(ground);
        }

        private void Click(object sender, MouseButtonEventArgs e)
        {
            if (!Game && !Fail)
            {
                this.Timer.Start();

                Game = true;
            }
            else if (Game && !Fail)
            {
                Player.VSpeed = Settings.PlayerSettings.JumpHeight;
            }
            else if (!Game && Fail)
            {
                Player.Reset(Settings);

                for (int i = 0; i < Settings.WallsSettings.Count; ++i)
                    Walls[i].Reset(Settings);

                Fail = false;
                Scores = 0;
                scoresLabel.Content = Scores;
            }
        }
        private void TimerTick(object sender, EventArgs e)
        {
            if (Game && !Fail)
            {
                for (int i = 0; i < Settings.WallsSettings.Count; ++i)
                {
                    Walls[i].Move(Settings);
                    Walls[i].ShiftToEnd(Settings);

                    Player.Move(Settings);

                    if (Walls[i].Body.UpperBody.Margin.Left + Settings.PlayerSettings.Size < Player.Margin.Left - Settings.PlayerSettings.Size)
                    {
                        if (!Walls[i].Passed)
                            scoresLabel.Content = (++Scores).ToString();

                        Walls[i].Passed = true;
                    }
                }
            }
            else if (!Game && Fail)
            {
                Player.VSpeed += 150 * Settings.Gravity;

                Player.Move(Settings);
            }

            CheckHeight();
            CheckCollisions();
        }
        private void CheckHeight()
        {
            if (Player.Body.Margin.Top < 0)
            {
                Player.Margin.Top = 0;
                Player.Body.Margin = Player.Margin;
            }
            else if (Player.Body.Margin.Top + Settings.PlayerSettings.Size > (Settings.WindowSettings.Height - 39) - Settings.GroundLevel)
            {
                Player.Margin.Top = (Settings.WindowSettings.Height - 39) - Settings.GroundLevel - Settings.PlayerSettings.Size;
                Player.Body.Margin = Player.Margin;

                this.Timer.Stop();

                Game = false;
                Fail = true;
            }
        }
        private void CheckCollisions()
        {
            for (int i = 0; i < Settings.WallsSettings.Count; ++i)
            {
                if (Walls[i].IsCollide(Player, Settings))
                {
                    Game = false;
                    Fail = true;
                }
            }
        }
    }

    class XMLWithDOM
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

        public XMLConfig GetConfig()
        {
            XmlElement root = document["root"];

            return new XMLConfig(int.Parse(root["sceneSettings"].FirstChild.InnerText),
                                 int.Parse(root["gameplaySettings"].FirstChild.InnerText),
                                 int.Parse(root["gameplaySettings"].LastChild.InnerText));
        }
    }
    class XMLConfig
    {
        public int GroundLevel { get; set; }
        public int HoleSize { get; set; }
        public int PlayerSize { get; set; }

        public XMLConfig(int gl, int hs, int ps)
        {
            this.GroundLevel = gl;
            this.HoleSize = hs;
            this.PlayerSize = ps;
        }
    }
}