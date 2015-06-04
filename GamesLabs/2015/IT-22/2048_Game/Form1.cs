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

namespace _2048_Game
{
    public partial class Form1 : Form
    {
        static bool isValid;
        //public Point[,] List = new Point[100, 2];
        //public int[] proba = new int[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 15, 11, 13, 14, 12 };
        //public int X { get; set; }
        //public int Y { get; set; }
        //Button[] B = new Button[16];
        //Button[] B_temp = new Button[100];
        //public int BTCount = 0;
        //public Point temp = new Point();
        public Form1()
        {
            InitializeComponent();
        }
        public void music(string sound)
        {
            WMPLib.WindowsMediaPlayer a = new WMPLib.WindowsMediaPlayer();
            a.URL = sound;
            a.controls.play();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            int fontsize;
            string fontname;
            string backim;
            string sound;
            try
            {
                XmlReaderSettings xmlreadersettings = new XmlReaderSettings();
                xmlreadersettings.Schemas.Add(null, "settings.xsd");
                xmlreadersettings.ValidationType = ValidationType.Schema;
                xmlreadersettings.ValidationEventHandler += new ValidationEventHandler(ConfigsSettingsValidationEventHandler);
                XmlReader xmlreader = XmlReader.Create("settings.xml", xmlreadersettings);
                isValid = true;
                while (xmlreader.Read()) { }

                if (isValid == true)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("settings.xml");
                    XmlNodeList xn = doc["root"].ChildNodes;

                    fontsize = int.Parse(doc["root"]["button"]["fontsize"].InnerText);
                    fontname = doc["root"]["button"]["fontname"].InnerText;
                    backim = doc["root"]["backgroundim"].InnerText;
                    sound = doc["root"]["soundtrack"].InnerText;

                }
                else
                {
                    fontsize = 12;
                    fontname = "Times New Roman";
                    backim = "im2.png";
                    sound = "sound.mp3";
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error:" + exc.Message);
                fontsize = 12;
                fontname = "Times New Roman";
                backim = "im2.png";
                sound = "sound.mp3";
            }


            music(sound);
            
            label1.Font = new Font(fontname, fontsize);
            button1.BackgroundImage = new Bitmap(backim); 



            background = new PictureBox();
            this.Focus();
            background.Location = new System.Drawing.Point(0, 40);
            background.Size = new System.Drawing.Size(278, 278);
            background.Name = "bk";
            background.TabIndex = 0;
            background.TabStop = false;
            background.BackgroundImage = global::_2048_Game.Properties.Resources.background;
            this.Controls.Add(background);
        }

        static void ConfigsSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                isValid = false;
                MessageBox.Show("Предупреждение! ");

            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                isValid = false;
                MessageBox.Show("Ошибка чтения данных из XML файла!!! Загружается стандартная конфигруация");

            }
        }


        PictureBox background = new PictureBox();
        bool[,] stack = new bool[4, 4];
        bool press_key = false, can_spawn = false;
        Random random = new Random();
        int[,] num_matrix = new int[4, 4];
        int[] spawn_num = { 2, 2, 2, 4, 2, 2, 4, 2, 2, 2 };
        List<PictureBox> picture_matrix = new List<PictureBox>();
        PictureBox[,] num_pictures = new PictureBox[4,4];

        public void Stack()
        {
            for (int i = 0; i < stack.GetLength(1); i++)
                for (int j = 0; j < stack.GetLength(1); j++)
                {
                    stack[i, j] = false;
                }

        }
        public void SpawnNumbers()
        {
            List<int> unuse = new List<int>();
            bool tr = false;
            while (!tr)
                for (int i = 0; i < num_matrix.GetLength(1) && !tr; i++)
                    for (int j = 0; j < num_matrix.GetLength(0) && !tr; j++)
                    {
                        if (num_matrix[i, j] == 0 || num_matrix[i, j] == null)
                        {
                            int[] randnum = { 0, 0, 1, 0, 0, 1, 0, 0, 0, 0 };
                            int rr = randnum[random.Next(randnum.Length - 1)];
                            if (rr == 1)
                            {
                                int rr1 = spawn_num[random.Next(spawn_num.Length - 1)];
                                PictureBox pb = new PictureBox();

                                pb.Location = new System.Drawing.Point(i * 57 + (i + 1) * 10, 40 + (j * 57 + (j + 1) * 10));
                                pb.Size = new System.Drawing.Size(57, 57);
                                pb.Name = "pb" + picture_matrix.Count + 1;
                                pb.TabIndex = picture_matrix.Count + 1;
                                pb.TabStop = false;
                                if (rr1 == 2)
                                {
                                    pb.BackgroundImage = global::_2048_Game.Properties.Resources._2;
                                    num_matrix[i, j] = 2;
                                }
                                else
                                {
                                    pb.BackgroundImage = global::_2048_Game.Properties.Resources._4;
                                    num_matrix[i, j] = 4;
                                }
                                this.Controls.Add(pb);
                                pb.BringToFront();
                                picture_matrix.Add(pb);
                                tr = true;
                            }
                        }
                    }
        }

        public void Draw()
        {
            if (!GameOwer.Visible)
            {
                foreach (PictureBox i in picture_matrix)
                {
                    this.Controls.Remove(i);
                }
                picture_matrix = new List<PictureBox>();
                for (int i = 0; i < num_matrix.GetLength(1); i++)
                    for (int j = 0; j < num_matrix.GetLength(0); j++)
                        if (num_matrix[i, j] != 0 && num_matrix[i, j] != null)
                        {
                            PictureBox pb = new PictureBox();
                            pb.Location = new System.Drawing.Point(i * 57 + (i + 1) * 10, 40 + (j * 57 + (j + 1) * 10));
                            pb.Size = new System.Drawing.Size(57, 57);
                            pb.Name = "pb" + picture_matrix.Count + 1;
                            pb.TabIndex = picture_matrix.Count + 1;
                            pb.TabStop = false;
                            switch (num_matrix[i, j])
                            {
                                case 2: pb.BackgroundImage = global::_2048_Game.Properties.Resources._2; break;
                                case 4: pb.BackgroundImage = global::_2048_Game.Properties.Resources._4; break;
                                case 8: pb.BackgroundImage = global::_2048_Game.Properties.Resources._8; break;
                                case 16: pb.BackgroundImage = global::_2048_Game.Properties.Resources._16; break;
                                case 32: pb.BackgroundImage = global::_2048_Game.Properties.Resources._32; break;
                                case 64: pb.BackgroundImage = global::_2048_Game.Properties.Resources._64; break;
                                case 128: pb.BackgroundImage = global::_2048_Game.Properties.Resources._128; break;
                                case 256: pb.BackgroundImage = global::_2048_Game.Properties.Resources._256; break;
                                case 512: pb.BackgroundImage = global::_2048_Game.Properties.Resources._512; break;
                                case 1024: pb.BackgroundImage = global::_2048_Game.Properties.Resources._1024; break;
                                case 2048: pb.BackgroundImage = global::_2048_Game.Properties.Resources._2048; break;
                            }
                            this.Controls.Add(pb);
                            pb.BringToFront();
                            picture_matrix.Add(pb);
                        }
            }
        }

        public void Down()
        {
            for (int i = 0; num_matrix.GetLength(1) > i; i++)
            {
                for (int j = num_matrix.GetLength(0) - 1; j >= 0; j--)
                {
                    if (num_matrix[i, j] != 0 && num_matrix[i, j] != null)
                    {
                        for (int k = j + 1; k < num_matrix.GetLength(0); k++)
                        {
                            if (num_matrix[i, k] == 0 || num_matrix[i, k] == null)
                            {
                                num_matrix[i, k] = num_matrix[i, k - 1];
                                num_matrix[i, k - 1] = 0;
                                stack[i, k] = stack[i, k - 1];
                                stack[i, k - 1] = false;
                                can_spawn = true;
                            }
                            if (num_matrix[i, k] == num_matrix[i, k - 1] && !stack[i, k] && !stack[i, k - 1])
                            {
                                num_matrix[i, k] *= 2;
                                num_matrix[i, k - 1] = 0;
                                stack[i, k] = true;
                                scores.Text = (Convert.ToInt32(scores.Text) + num_matrix[i, k]).ToString();
                                can_spawn = true;
                            }
                        }
                    }
                }
            }
        }
        public void Left()
        {
            for (int j = 0; num_matrix.GetLength(1) > j; j++)
            {
                for (int i = 0; num_matrix.GetLength(0) > i; i++)
                {
                    if (num_matrix[i, j] != 0 && num_matrix[i, j] != null)
                    {
                        for (int k = i - 1; k >= 0; k--)
                        {
                            if (num_matrix[k, j] == 0 || num_matrix[k, j] == null)
                            {
                                num_matrix[k, j] = num_matrix[k + 1, j];
                                num_matrix[k + 1, j] = 0;
                                stack[k, j] = stack[k + 1, j];
                                stack[k + 1, j] = false;
                                can_spawn = true;
                            }
                            if (num_matrix[k, j] == num_matrix[k + 1, j] && !stack[k, j] && !stack[k + 1, j])
                            {
                                num_matrix[k, j] *= 2;
                                num_matrix[k + 1, j] = 0;
                                stack[k, j] = true;
                                scores.Text = (Convert.ToInt32(scores.Text) + num_matrix[k, j]).ToString();
                                can_spawn = true;
                            }
                        }
                    }
                }
            }
        }
        public void Up()
        {
            for (int i = 0; num_matrix.GetLength(1) > i; i++)
            {
                for (int j = 0; num_matrix.GetLength(0) > j; j++)
                {
                    if (num_matrix[i, j] != 0 && num_matrix[i, j] != null)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (num_matrix[i, k] == 0 || num_matrix[i, k] == null)
                            {
                                num_matrix[i, k] = num_matrix[i, k + 1];
                                num_matrix[i, k + 1] = 0;
                                stack[i, k] = stack[i, k + 1];
                                stack[i, k + 1] = false;
                                can_spawn = true;
                            }
                            if (num_matrix[i, k] == num_matrix[i, k + 1] && !stack[i, k] && !stack[i, k + 1])
                            {
                                num_matrix[i, k] *= 2;
                                num_matrix[i, k + 1] = 0;
                                stack[i, k] = true;
                                scores.Text = (Convert.ToInt32(scores.Text) + num_matrix[i, k]).ToString();
                                can_spawn = true;
                            }
                        }
                    }
                }
            }
        }
        public void Right()
        {
            for (int j = 0; num_matrix.GetLength(1) > j; j++)
            {
                for (int i = num_matrix.GetLength(0) - 1; i >= 0; i--)
                {
                    if (num_matrix[i, j] != 0 && num_matrix[i, j] != null)
                    {
                        for (int k = i + 1; k < num_matrix.GetLength(0); k++)
                        {
                            if (num_matrix[k, j] == 0 || num_matrix[k, j] == null)
                            {
                                num_matrix[k, j] = num_matrix[k - 1, j];
                                num_matrix[k - 1, j] = 0;
                                stack[k, j] = stack[k - 1, j];
                                stack[k - 1, j] = false;
                                can_spawn = true;
                            }
                            if (num_matrix[k, j] == num_matrix[k - 1, j] && !stack[k, j] && !stack[k - 1, j])
                            {
                                num_matrix[k, j] *= 2;
                                num_matrix[k - 1, j] = 0;
                                stack[k, j] = true;
                                scores.Text = (Convert.ToInt32(scores.Text) + num_matrix[k, j]).ToString();
                                can_spawn = true;
                            }
                        }
                    }
                }
            }
        }

        private void K_D(object sender, KeyEventArgs e)
        {
            if (!GameOwer.Visible)
            {
                switch (e.KeyCode)
                {
                    case Keys.A: Left(); break;
                    case Keys.D: Right(); break;
                    case Keys.S: Down(); break;
                    case Keys.W: Up(); break;
                }
                Stack();
                if (can_spawn)
                    SpawnNumbers();
                else
                    IFGameOwer();
                Draw();
                can_spawn = false;
            }
        }

        private void start_Click_1(object sender, EventArgs e)
        {
            scores.Text = "0";
            UpDate.Start();
            if (GameOwer.Visible)
                Form1_Load(sender, e);
            GameOwer.Visible = false;
            foreach (PictureBox i in picture_matrix)
            {
                this.Controls.Remove(i);
            }
            num_matrix = new int[4, 4];
            picture_matrix = new List<PictureBox>();
            SpawnNumbers();
            SpawnNumbers();
        }

        public void IFGameOwer()
        {
            bool tr = false;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (num_matrix[i, j] == 0)
                        tr = true;

                }
            if (!tr)
            {
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                    {
                        try
                        {
                            if (num_matrix[i, j] == num_matrix[i, j + 1] || num_matrix[i, j] == num_matrix[i + 1, j])
                                tr = true;
                        }
                        catch { }
                    }
                if (!tr)
                {
                    foreach (PictureBox i in picture_matrix)
                    {
                        this.Controls.Remove(i);
                    }
                    UpDate.Stop();
                    this.Controls.Remove(background);
                    GameOwer.BringToFront();
                    GameOwer.Visible = true;
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
