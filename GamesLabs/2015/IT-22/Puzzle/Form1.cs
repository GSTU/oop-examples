using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Puzzle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string str = Application.StartupPath;
            s = str.Split('\\');
            s[0] += '\\';
            for (int i = 1; i < s.Length - 2; i++)
            {
                s[0] += s[i] + '\\';
            }
            Start();
        }
        PictureBox[] picturebox = null;
        string[] s;
        int index = -1;
        int wlength = 3;
        int hlength = 3;
        int seconds = 0;
        static bool flag = true;
        Bitmap bitmap = null;
        public void NewGame()
        {
            label1.Text = "Время: 0 c.";
            CreatePictureRegion();
            timer1.Stop();
            seconds = 0;
            восстановитьКартинкуToolStripMenuItem.Enabled = false;
        }
        void CreatePictureRegion()
        {
            if (picturebox != null)
            {
                for (int i = 0; i < picturebox.Length; i++)
                {
                    picturebox[i].Dispose();
                }
                picturebox = null;
            }
            picturebox = new PictureBox[wlength * hlength];
            int width = panel1.Width / wlength;
            int height = panel1.Height / hlength;
            int countX = 0;
            int countY = 0;
            for (int i = 0; i < picturebox.Length; i++)
            {
                picturebox[i] = new PictureBox();
                picturebox[i].Width = width;
                picturebox[i].Height = height;
                picturebox[i].Left = 0 + countX * picturebox[i].Width;
                picturebox[i].Top = 0 + countY * picturebox[i].Height;
                Point point = new Point();
                point.X = picturebox[i].Left;
                point.Y = picturebox[i].Top;
                picturebox[i].Tag = point;
                countX++;
                if (countX == wlength)
                {
                    countX = 0;
                    countY++;
                }
                picturebox[i].Parent = panel1;
                picturebox[i].BorderStyle = BorderStyle.None;
                picturebox[i].SizeMode = PictureBoxSizeMode.StretchImage;
                picturebox[i].Click += new EventHandler(picturebox_Click);
            }
            DrawPicture();
        }
        void DrawPicture()
        {
            if (bitmap == null) return;
            int countX = 0;
            int countY = 0;
            int width = bitmap.Width / wlength;
            int height = bitmap.Height / hlength;
            for (int i = 0; i < picturebox.Length; i++)
            {
                picturebox[i].Image = bitmap.Clone(new RectangleF(countX * width, countY * height, width, height), bitmap.PixelFormat);
                countX++;
                if (countX == wlength)
                {
                    countX = 0;
                    countY++;
                }
            }
        }
        void picturebox_Click(object sender, EventArgs e)
        {
            PictureBox picturebox1 = (PictureBox)sender;
            if (index == -1)
            {
                index = picturebox.ToList<PictureBox>().IndexOf(picturebox1);
                picturebox1.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                Point point1 = picturebox[index].Location;
                picturebox[index].Location = picturebox1.Location;
                picturebox1.Location = point1;
                picturebox[index].BorderStyle = BorderStyle.FixedSingle;
                index = -1;
                for (int j = 0; j < picturebox.Length; j++)
                {
                    Point point2 = (Point)picturebox[j].Tag;
                    if (picturebox[j].Location != point2)
                    {
                        return;
                    }
                }
                for (int j = 0; j < picturebox.Length; j++)
                {
                    picturebox[j].Visible = true;
                    picturebox[j].BorderStyle = BorderStyle.None;
                    picturebox[j].Enabled = false;
                }
                label1.Text = "Время: 0 c.";
                timer1.Stop();
                if (seconds / 60 > 0)
                {
                    MessageBox.Show("Картинка собрана!\n Время: " + seconds / 60 + " м. " + seconds % 60 + " с.");
                }
                else
                {
                    MessageBox.Show("Картинка собрана!\nВремя: " + seconds + " с.");
                }
                восстановитьКартинкуToolStripMenuItem.Enabled = false;
                seconds = 0;
            }
        }
        public void Start()
        {
            try
            {
                XmlReaderSettings xmlreadersettings = new XmlReaderSettings();
                xmlreadersettings.Schemas.Add(null, s[0] + "xsd.xsd");
                xmlreadersettings.ValidationType = ValidationType.Schema;
                xmlreadersettings.ValidationEventHandler += new ValidationEventHandler(xmlreadersettingsValidationEventHandler);
                XmlReader xmlreader = XmlReader.Create(s[0] + "xml.xml", xmlreadersettings);
                while (xmlreader.Read()) { }
                if (flag == true)
                {
                    Text = "Puzzle";
                    label2.Text = "Лучший результат: ";
                    label2.Visible = false;
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(s[0] + "xml.xml");
                    bitmap = new Bitmap(xmldoc.DocumentElement.SelectSingleNode("path").InnerText);
                    wlength = Convert.ToInt32(xmldoc.DocumentElement.SelectSingleNode("size").FirstChild.InnerText);
                    hlength = Convert.ToInt32(xmldoc.DocumentElement.SelectSingleNode("size").LastChild.InnerText);
                    NewGame();
                    Text += " -> Уровень: " + xmldoc.DocumentElement.SelectSingleNode("level").InnerText;
                    label2.Text += xmldoc.DocumentElement.SelectSingleNode("result").InnerText;
                    label2.Visible = true;
                    for (int i = 0; i < picturebox.Length; i++)
                    {
                        picturebox[i].Enabled = false;
                    }
                    восстановитьКартинкуToolStripMenuItem.Enabled = false;
                    перемешатьКартинкуToolStripMenuItem.Enabled = true;
                    помощьToolStripMenuItem.Enabled = true;
                    настройкаСложностиToolStripMenuItem.Enabled = true;
                    index = -1;
                }
                else
                {
                    bitmap = new Bitmap(s[0] + "Chrysanthemum.jpg");
                    wlength = 3;
                    hlength = 3;
                    NewGame();
                    восстановитьКартинкуToolStripMenuItem.Enabled = false;
                    перемешатьКартинкуToolStripMenuItem.Enabled = true;
                    помощьToolStripMenuItem.Enabled = true;
                    настройкаСложностиToolStripMenuItem.Enabled = true;
                    index = -1;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Ошибка: " + exception.Message);
                bitmap = new Bitmap(s[0] + "Chrysanthemum.jpg");
                wlength = 3;
                hlength = 3;
                NewGame();
                восстановитьКартинкуToolStripMenuItem.Enabled = false;
                перемешатьКартинкуToolStripMenuItem.Enabled = true;
                помощьToolStripMenuItem.Enabled = true;
                настройкаСложностиToolStripMenuItem.Enabled = true;
                index = -1;
            }
        }
        static void xmlreadersettingsValidationEventHandler(object sender, ValidationEventArgs e)
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
        private void перемешатьКартинкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bitmap == null) return;
            Random random = new Random();
            int rand = 0;
            for (int i = 0; i < picturebox.Length; i++)
            {
                picturebox[i].Visible = true;
                picturebox[i].Enabled = true;
                rand = random.Next(0, picturebox.Length);
                Point pointR = picturebox[rand].Location;
                Point pointI = picturebox[i].Location;
                picturebox[i].Location = pointR;
                picturebox[rand].Location = pointI;
                picturebox[i].BorderStyle = BorderStyle.FixedSingle;
            }
            восстановитьКартинкуToolStripMenuItem.Enabled = true;
            index = -1;
            timer1.Start();
            seconds = 0;
            label1.Text = "Время: 0 c.";
        }
        private void восстановитьКартинкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < picturebox.Length; i++)
            {
                Point point = (Point)picturebox[i].Tag;
                picturebox[i].Location = point;
                picturebox[i].Visible = true;
                picturebox[i].Enabled = false;
                picturebox[i].BorderStyle = BorderStyle.FixedSingle;
            }
            index = -1;
            label1.Text = "Время: 0 c.";
            timer1.Stop();
            seconds = 0;
            восстановитьКартинкуToolStripMenuItem.Enabled = false;
        }
        private void настройкаСложностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            if (form2.flag == true)
            {
                wlength = form2.wlength;
                hlength = form2.hlength;
                NewGame();
                for (int i = 0; i < picturebox.Length; i++)
                {
                    picturebox[i].Enabled = false;
                }
                index = -1;
            }
        }
        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.image = bitmap;
            form3.ShowDialog();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds++;
            if (seconds / 60 > 0)
            {
                label1.Text = "Время: " + seconds / 60 + " м. " + seconds % 60 + " с.";
            }
            else
            {
                label1.Text = "Время: " + seconds + " с.";
            }
        }
    }
}
