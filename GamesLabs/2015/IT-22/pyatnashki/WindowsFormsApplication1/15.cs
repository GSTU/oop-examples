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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int s = 0;
        int m = 0;
        bool ca = false;

        int[] pzl = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0 };     
        int[] pzl2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0 };    // массив для игры 
        int mpos = 0; //позиция выбранной фишки

        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawPuzzle(pzl2);        
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Point p = PointToClient(Control.MousePosition);//вычисление положения указателя относительно формы
            Predic pred=new Predic();
            int nulpos=Array.FindIndex(pzl2,pred.IsNul)+1;
            
            if (p.X <= 320 && p.Y <= 320)       // определение позиции фишки, указанной курсором
            {
                if (p.Y < 80)   mpos = (p.X) / 80 + 1;
                if (p.Y > 80 && p.Y<160) mpos = (p.X) / 80 + 5;
                if (p.Y > 160 && p.Y < 240) mpos = (p.X) / 80 + 9;   
                if (p.Y >240 && p.Y < 320) mpos = (p.X) / 80 + 13;
            }

            /*пытаемся поставить фишку на пустое место*/
            if ((mpos == (nulpos - 1) && (float)mpos%4!=0) || (mpos == (nulpos + 1) && (float)nulpos%4!=0) 
                || mpos == (nulpos - 4) || mpos == (nulpos + 4))
            { 
                pzl2[nulpos-1] = pzl2[mpos-1];
                pzl2[mpos - 1] = 0;
                this.DoubleBuffered = true;
                DrawPuzzle(pzl2); 
            }
         }
     
        public void DrawPuzzle(int[] pzl2)  //метод для рисования фишек на форме
        {
            draw15 d = new draw15(); 
            Graphics g = CreateGraphics();
            Pen pp = new Pen(Color.Black, 3);
            Font f = new System.Drawing.Font(FontFamily.GenericSerif, 25, FontStyle.Bold);
            g.Clear(this.BackColor);
            for (int i = 0; i < 16; i++)
            {
                if (pzl2[i] != 0) d.dr15(g, pp, pzl2[i], i + 1, f);
            }
            g.Dispose();
        
        }
        public void Mix(int[] pzl2) 
        {
            Predic pred = new Predic();
            int nulpos = Array.FindIndex(pzl2, pred.IsNul) + 1;
            Random r = new Random();
            switch (r.Next(4)) 
            {
                case 0: if (nulpos+1 < 16 && (float)(nulpos) % 4 != 0)
                        {
                            pzl2[nulpos - 1] = pzl2[nulpos]; pzl2[nulpos] = 0; 
                        }  DrawPuzzle(pzl2); break;
                case 1: if (nulpos > 1 && (float)(nulpos - 1) % 4 != 0)
                        {
                            pzl2[nulpos - 1] = pzl2[nulpos-2]; pzl2[nulpos-2] = 0;
                        }DrawPuzzle(pzl2); break;
                case 2: if (nulpos + 4 < 16) { pzl2[nulpos - 1] = pzl2[nulpos+3]; pzl2[nulpos+3] = 0; }
                    DrawPuzzle(pzl2);break;
                case 3: if (nulpos - 4 > 0) { pzl2[nulpos - 1] = pzl2[nulpos - 5]; pzl2[nulpos - 5] = 0; }
                    DrawPuzzle(pzl2);break;
            };
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true) { timer1.Enabled = false; button1.Text = "Перемешать"; }
            else { timer1.Enabled = true; button1.Text = "Стоп"; }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Mix(pzl2); this.DoubleBuffered = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (timer2.Enabled == false) 
            { 
                timer2.Enabled = true;
            }  
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (s > 60) { timer2.Stop(); MessageBox.Show("Время вышло"); }
            else
            {
                s++;
                if (s >= 60) { s = 0; m++; }
                if (s < 10 && m < 10) label1.Text = "0" + m + ":" + "0" + s;
                else if (s < 10 && m >= 10) label1.Text = m + ":" + "0" + s;
                else label1.Text = "0" + m + ":" + s;
                if (timer2.Enabled == true)
                {
                    for (int i = 0; i < pzl.Length; i++)
                    {
                        if (pzl[i] == pzl2[i]) ca = true;
                        else { ca = false; break; }

                    }
                    if (ca == true)
                    {
                        ca = false;
                        timer2.Enabled = false;
                        m = 0;
                        s = 0;
                        MessageBox.Show("Вы выиграли. Хотя, возможно, просто не нажали перемешать!");
                    }
                }
            }
        }

        static bool valid = true;
        public static void ValidationHandler(object sender, ValidationEventArgs args)
        {
            MessageBox.Show(args.Severity + ":" + args.Message + "\nЗагружены стандартные параметры");
            valid = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            XmlTextReader reader = new XmlTextReader("XMLFile1.xml");
            try
            {

                XmlValidatingReader validreader = new XmlValidatingReader(reader);
                validreader.Schemas.Add(null, "xsd.xsd");
                validreader.ValidationType = ValidationType.Schema;
                validreader.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);

                while (validreader.Read()) ;
            }
            catch (Exception ex)
            {
                valid = false;
                MessageBox.Show(ex.Message);
                MessageBox.Show("Файл конфигурации не найден\nЗагружены параметры по умолчанию");
            }
            if (valid)
            {
                reader = new XmlTextReader("XMLFile1.xml");

                reader.Close();
            }
        }

    }

}
