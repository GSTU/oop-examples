using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace Lab13
{
    public partial class Form1 : Form
    {
        int turn = 0, game = 0;

        Random random = new Random();
        Panel[,] player, enemy;
        Field playerField, enemyField;

        int fieldSize;
        int bX, bY;

        bool flag = true;

        string buttonText;
        string winnerText;
        string loserText;

        public Form1()
        {    
            try
            {
                if (System.IO.File.Exists("xsd.xsd"))
                {
                    XmlReaderSettings xmlreadersettings = new XmlReaderSettings();
                    xmlreadersettings.Schemas.Add(null, "xsd.xsd");
                    xmlreadersettings.ValidationType = ValidationType.Schema;
                    xmlreadersettings.ValidationEventHandler += new ValidationEventHandler(xmlreadersettingsValidationEventHandler);

                    XmlReader xmlreader = XmlReader.Create("Settings.xml", xmlreadersettings);
                    while (xmlreader.Read()) { }
                }
                else
                {
                    MessageBox.Show("Игра загружена со стандартными настройками!");
                    throw new System.IO.FileNotFoundException();
                }

                if (flag)
                {
                    XMLWithDOM xmlWorker = new XMLWithDOM("Settings.xml");
                    Config config = xmlWorker.GetConfig();
                    fieldSize = config.FieldSize;
                    bX = config.ButtonCoords.X;
                    bY = config.ButtonCoords.Y;

                    switch(config.Language)
                    {
                        case "rus":
                            {
                                buttonText = "Рестарт";
                                winnerText = "Игра окончена, вы выиграли.";
                                loserText = "Игра окончена, вы проиграли.";
                                break;
                            }
                        case "eng":
                            {
                                buttonText = "Restart";
                                winnerText = "Game over, you won.";
                                loserText = "Game over, you lose.";
                                break;
                            }
                    }
                }
                else
                    throw new Exception();
            }
            catch
            {
                fieldSize = 10;
                bX = 230;
                bY = 50;

                buttonText = "Restart";
                winnerText = "Game over, you won.";
                loserText = "Game over, you lose.";
            }

            player = new Panel[fieldSize, fieldSize];
            enemy = new Panel[fieldSize, fieldSize];

            InitializeComponent();

            restart.Text = buttonText;
            restart.Location = new Point(bX, bY);

            playerField = new Field(random.Next(0, Int32.MaxValue), fieldSize);
            enemyField = new Field(random.Next(0, Int32.MaxValue), fieldSize);

            AddFields();
            Reset();
        }

        void xmlreadersettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                MessageBox.Show("Внимание: " + e.Message);
                flag = false;
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                MessageBox.Show("Ошибка: " + e.Message);
                flag = false;
            }
        }

        private void Check()
        {
            bool gameOver = true;

            foreach (Ship s in playerField.Ships)
            {
                for (int i = s.Coordinates.Start.X; i <= s.Coordinates.End.X; ++i)
                    for (int j = s.Coordinates.Start.Y; j <= s.Coordinates.End.Y; ++j)
                        if ((playerField.Matrix[i, j] != 3))
                            gameOver = false;
            }

            if (gameOver)
            {
                game = 1;
                MessageBox.Show(loserText);
            }

            gameOver = true;

            foreach (Ship s in enemyField.Ships)
            {
                for (int i = s.Coordinates.Start.X; i <= s.Coordinates.End.X; ++i)
                    for (int j = s.Coordinates.Start.Y; j <= s.Coordinates.End.Y; ++j)
                        if ((enemyField.Matrix[i, j] != 3))
                            gameOver = false;
            }

            if (gameOver)
            {
                game = 1;
                MessageBox.Show(winnerText);
            }
        }
        private void EnemyMove()
        {
            if (game == 0)
            {
                int m, n;
                bool flag = false;

                while (!flag || (turn == 1))
                {
                    m = random.Next(0, (int)Math.Sqrt(player.Length));
                    n = random.Next(0, (int)Math.Sqrt(player.Length));

                    flag = false;

                    if (playerField.Matrix[m, n] < 2)
                    {
                        flag = true;

                        if (playerField.Matrix[m, n] == 0)
                            playerField.Matrix[m, n] = 5;
                        else
                            playerField.Action(m, n);

                        if (!((playerField.Matrix[m, n] == 2) || (playerField.Matrix[n, n] == 3)))
                            turn = 0;

                        Check();
                    }
                }
            }
        }
        private void AddFields()
        {
            int top = 20,
                left = this.DisplayRectangle.Width - 200 - 20;

            Point location = new Point();

            for (int i = 0; i < Math.Sqrt(player.Length); ++i)
            {
                left = this.DisplayRectangle.Width - 200 - 20;

                for (int j = 0; j < Math.Sqrt(player.Length); ++j)
                {
                    location.X = left;
                    location.Y = top;

                    player[i, j] = new Panel();

                    player[i, j].BackColor = SystemColors.ActiveCaption;
                    player[i, j].Location = location;
                    player[i, j].Width = player[i, j].Height = 20;
                    player[i, j].BorderStyle = BorderStyle.Fixed3D;
                    player[i, j].MouseHover += new EventHandler(panel_MouseHover);

                    this.Controls.Add(player[i, j]);

                    left += 20;
                }
                top += 20;
            }

            top = 20;
            left = 20;

            for (int i = 0; i < Math.Sqrt(enemy.Length); ++i)
            {
                left = 20;

                for (int j = 0; j < Math.Sqrt(enemy.Length); ++j)
                {
                    location.X = left;
                    location.Y = top;

                    enemy[i, j] = new Panel();
                    enemy[i, j].BackColor = SystemColors.ActiveCaption;
                    enemy[i, j].Location = location;
                    enemy[i, j].Width = enemy[i, j].Height = 20;
                    enemy[i, j].BorderStyle = BorderStyle.Fixed3D;
                    enemy[i, j].MouseHover += new EventHandler(panel_MouseHover);
                    enemy[i, j].Click += new EventHandler(panel_Click);

                    this.Controls.Add(enemy[i, j]);

                    left += 20;
                }

                top += 20;
            }
        }
        private void Redraw()
        {
            for (int i = 0; i < Math.Sqrt(player.Length); ++i)
            {
                for (int j = 0; j < Math.Sqrt(player.Length); ++j)
                {
                    switch (playerField.Matrix[i, j])
                    {
                        case 0:
                            player[i, j].BackColor = SystemColors.ActiveCaption;

                            break;
                        case 1:
                            player[i, j].BackColor = Color.White;

                            break;
                        case 2:
                            player[i, j].BackColor = Color.Orange;

                            break;
                        case 3:
                            player[i, j].BackColor = Color.DarkRed;

                            break;
                        case 4:
                            player[i, j].BackColor = Color.Tan;

                            break;
                        case 5:
                            player[i, j].BackColor = SystemColors.GrayText;

                            break;
                    }

                    switch (enemyField.Matrix[i, j])
                    {
                        case 0:
                            enemy[i, j].BackColor = SystemColors.ActiveCaption;

                            break;
                        //case 1:
                        //    enemy[i, j].BackColor = Color.White;

                        //    break;
                        case 2:
                            enemy[i, j].BackColor = Color.Orange;

                            break;
                        case 3:
                            enemy[i, j].BackColor = Color.DarkRed;

                            break;
                        case 4:
                            enemy[i, j].BackColor = Color.Tan;

                            break;
                        case 5:
                            enemy[i, j].BackColor = SystemColors.GrayText;

                            break;
                    }
                }
            }
        }
        private void Reset()
        {
            index.Text = "0:0";

            turn = 0;
            game = 0;

            playerField.Clear();
            enemyField.Clear();

            Redraw();

            playerField.AddShips();
            enemyField.AddShips();

            Redraw();
        }

        private void panel_MouseHover(object sender, EventArgs e)
        {
            for (int i = 0; i < Math.Sqrt(player.Length); ++i)
            {
                for (int j = 0; j < Math.Sqrt(player.Length); ++j)
                {
                    if ((player[i, j] == (sender as Panel)) || (enemy[i, j] == (sender as Panel)))
                    {
                        index.Text = string.Format("{0}:{1}", j + 1, i + 1);

                        return;
                    }
                }
            }
        }
        private void panel_Click(object sender, EventArgs e)
        {
            if (game == 0)
            {
                if (turn == 0)
                {
                    for (int i = 0; i < Math.Sqrt(player.Length); ++i)
                    {
                        for (int j = 0; j < Math.Sqrt(player.Length); ++j)
                        {
                            if (enemy[i, j] == (sender as Panel))
                            {
                                if (enemyField.Matrix[i, j] < 2)
                                {
                                    if (enemyField.Matrix[i, j] == 0)
                                        enemyField.Matrix[i, j] = 5;
                                    else
                                        enemyField.Action(i, j);

                                    if (!((enemyField.Matrix[i, j] == 2) || (enemyField.Matrix[i, j] == 3)))
                                    {
                                        turn = 1;

                                        EnemyMove();
                                    }

                                    Redraw();
                                    Check();
                                }

                                return;
                            }
                        }
                    }
                }
            }
        }
        private void restart_Click(object sender, EventArgs e)
        {
            Reset();
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

        private const String ROOT = "config";

        public Config GetConfig()
        {
            XmlElement root = document[ROOT];

            int x = int.Parse(root["buttonCoords"].FirstChild.InnerText);
            int y = int.Parse(root["buttonCoords"].LastChild.InnerText);

            int fieldsize = int.Parse(root["fieldSize"].InnerText);

            string lang = root["language"].InnerText;

            RestartButtonCoords coords = new RestartButtonCoords(x, y);
            Config config = new Config(coords, fieldsize, lang);

            return config;
        }
    }

    class RestartButtonCoords
    {
        public int X { get; set; }
        public int Y { get; set; }

        public RestartButtonCoords(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    class Config
    {
        public RestartButtonCoords ButtonCoords { get; set; }
        public int FieldSize { get; set; }
        public string Language;

        public Config(RestartButtonCoords coords, int fs, string lang)
        {
            ButtonCoords = coords;
            FieldSize = fs;
            Language = lang;
        }

        public override string ToString()
        {
            String viewTemplate = "Button coords: \n\tx:{0}\n\ty:{1}\nField Size: {2}";
            return String.Format(viewTemplate, ButtonCoords.X, ButtonCoords.Y, FieldSize);
        }
    }
}