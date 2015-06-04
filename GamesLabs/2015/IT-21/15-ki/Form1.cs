using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.Xml;
using System.Xml.Schema;

namespace _15_ki
{
    public partial class Form1 : Form
    {
        static int time = 0;
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        static bool exitFlag = false;
        public Point[,] List = new Point [100,2];
        public int[] proba = new int[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 15, 11, 13, 14, 12 };
        public int X { get; set; }
        public int Y { get; set; }
        Button[] B = new Button[16];
        Button[] B_temp = new Button[100];
        public int BTCount=0;
        public Point temp = new Point();
        static bool isValid;
        public int LocToNumb(int x, int y)
        {
            int[] mass_x = new int[5] { 0, 6, 47, 88, 129 };
            for (int i = 1; i < 5; i++)
            {
                if (mass_x[i] == x) x = i;
                if (mass_x[i] == y) y = i;
            }
            int numb = 0;
            for (int i = 1; i < 5; i++)
            {
                if (x == i)
                {
                    for (int j = 1; j < 5; j++)
                    {
                        if (y == j)
                        {
                            for (int k = 1; k <= 4 - i; k++)
                                numb += (j - 1);
                            numb += i * j;
                            i = 6;
                            j = 6;
                        }
                    }
                }
            }
            return numb;
        }       
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
            catch(Exception exc)
            {
                MessageBox.Show("Error:" + exc.Message);
                fontsize = 12;
                fontname = "Times New Roman";
                backim = "im2.png";
                sound = "sound.mp3";
            }


            music(sound);
            B[1] = b1;
            B[2] = b2;
            B[3] = b3;
            B[4] = b4;
            B[5] = b5;
            B[6] = b6;
            B[7] = b7;
            B[8] = b8;
            B[9] = b9;
            B[10] = b10;
            B[11] = b11;
            B[12] = b12;
            B[13] = b13;
            B[14] = b14;
            B[15] = b15;
            for (int i = 1; i < 16; i++)
            {
                B[i].Text = proba[i].ToString();
            }

            label1.Font = new Font(fontname, fontsize);
            button1.BackgroundImage = new Bitmap(backim);  
            myTimer.Enabled = true;
            myTimer.Tick += new EventHandler(timer1_Tick);
            myTimer.Interval = 1000;    
       

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

        private void b1_Click(object sender, EventArgs e)
        {
            Option(b1, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            Proof();
        }

        int i = 0, min = 1, sec = 40;
        void timer1_Tick(object sender, EventArgs e)
        {
            if (i > 100)
            {
                myTimer.Stop();
                if (MessageBox.Show("Время вышло!!!", "Таймер окончился: ",
                   MessageBoxButtons.OK) == DialogResult.OK)
                {
                    Application.Exit();
                }

            }
            else 
            { 
                min=Convert.ToInt32(minute.Text);
                sec=Convert.ToInt32(seconds.Text);
               // progressBar1.Value = i; i += 1;
                sec--;
                if (sec < 0)
                {
                    sec = 60;
                    min -= 1;
                }
                minute.Text = min.ToString();
                seconds.Text = sec.ToString();

            }
        }
        public void Proof()
        {
            Boolean flag = false;
            int i = 1;
            while (i < 16)
            {
                if (B[i].Text == LocToNumb(B[i].Location.X, B[i].Location.Y).ToString())
                {
                    flag = true;
                }
                else
                {
                    flag = false; i = 17;//17 чтобы не входил в следующую проверку окончания игры
                }
                i++;
            }
            if (i == 16 && flag == true)
            {
                MessageBox.Show("Вы победили!"); myTimer.Stop();
            }
        }
        public void Option(Button b, int x, int y)
        {
            Point temp1 = new Point(b.Location.X, b.Location.Y);
            Point temp = new Point(x,y);
            Boolean flag= true;
            if ((b.Location.Y == (temp.Y - 41)) & (b.Location.X == temp.X))
            {
                b.Location = temp;
                temp = temp1;
            }
            else if ((b.Location.Y == (temp.Y + 41)) & (b.Location.X == temp.X))
            {
                temp1 = temp;
                temp = b.Location;
                b.Location = temp1;
            }
            else if ((b.Location.X == (temp.X + 41)) & (b.Location.Y == temp.Y))
            {
                b.Location = temp;
                temp = temp1;
            }
            else if ((b.Location.X == (temp.X - 41)) & (b.Location.Y == temp.Y))
            {
                temp1 = temp;
                temp = b.Location;
                b.Location = temp1;
            }
            else flag = false;
            if (flag)
            {

                textBox3.Text = (Convert.ToInt32(textBox3.Text) + 1).ToString();
                B_temp[BTCount + 1] = b;
                List[BTCount + 1, 0] = b.Location;//координаты кнопки
                List[BTCount + 1, 1] = temp;//координаты пустоты
                BTCount++;
                Memo.Text += b.Text + "->(" + b.Location.X + ";" + b.Location.Y + ");(" + temp.X + ";" + temp.Y + ")\n";
                textBox1.Text = temp.X.ToString();
                textBox2.Text = temp.Y.ToString();
            }
        }


        private void b2_Click(object sender, EventArgs e)
        {
            Option(b2, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            Proof();
        }
        private void b3_Click(object sender, EventArgs e)
        {
            Option(b3, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            Proof();
        }
        private void b4_Click(object sender, EventArgs e)
        {
            Option(b4, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            Proof();
        }
        private void b5_Click(object sender, EventArgs e)
        {
            Option(b5, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            Proof();
        }

        void sdf(object sender, EventArgs e) {
            MessageBox.Show("df");
        }

        private void b6_Click(object sender, EventArgs e)
        {
            
            //b1.Click += sdf;
            Option(b6, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            Proof();
        }

        private void b7_Click(object sender, EventArgs e)
        {
            Option(b7, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            Proof();
        }

        private void b8_Click(object sender, EventArgs e)
        {
            Option(b8, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            Proof();
        }

        private void b9_Click(object sender, EventArgs e)
        {
            Option(b9, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            Proof();
        }
        private void b10_Click(object sender, EventArgs e)
        {
            Option(b10, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text)); 
            Proof();
        }

        private void b11_Click(object sender, EventArgs e)
        {
            Option(b11, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            Proof();
        }

        private void b12_Click(object sender, EventArgs e)
        {
            Option(b12, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            Proof();
        }

        private void b13_Click(object sender, EventArgs e)
        {
            Option(b13, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            Proof();
        }
        private void b14_Click(object sender, EventArgs e)
        {
            Option(b14, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            Proof();
        }

        private void b15_Click(object sender, EventArgs e)
        {
            Option(b15, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            Proof();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (BTCount > 0)
            {
                temp = List[BTCount, 0];//проверка на количество элементов в массиве  B_temp
                B_temp[BTCount].Location = List[BTCount, 1];
                BTCount--;
                textBox1.Text = temp.X.ToString();
                textBox2.Text = temp.Y.ToString();
                List<string> myList = Memo.Lines.ToList();
                if (myList.Count > 0)
                {
                    myList.RemoveAt(myList.Count - 2);
                    Memo.Lines = myList.ToArray();
                    Memo.Refresh();
                }
                textBox3.Text = (Convert.ToInt32(textBox3.Text) - 1).ToString();
            }
            else
            {
                if (MessageBox.Show("Может начнете игру заново?", "Вы находитесь на своем первом ходу! ",
                  MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    minute.Text = "1";
                    seconds.Text = "40";
                    myTimer.Start();                    
                }
                else Application.Exit();
                
            }
        }

        private void b1_Click_1(object sender, EventArgs e)
        {
            Option(b1, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            Proof();
        }
    }

}
