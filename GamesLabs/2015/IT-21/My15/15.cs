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
using System.Xml.Linq;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public static bool isValid, ca;
        public static int m,s;
        public static string fontname;
        public static Color buttoncolor;

        //int s = 59;
        //int m = 5;
        //bool ca = false;
        
        int[] pzl = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0 };     // эталонный массив
        int[] pzl2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0 };    // массив для игры 
        int mpos = 0; //позиция фишки, выбранной курсором

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
            Predic pred = new Predic();
            int nulpos = Array.FindIndex(pzl2, pred.IsNul) + 1;

            if (p.X <= 320 && p.Y <= 320)       // определение позиции фишки, указанной курсором
            {
                if (p.Y < 80) mpos = (p.X) / 80 + 1;
                if (p.Y > 80 && p.Y < 160) mpos = (p.X) / 80 + 5;
                if (p.Y > 160 && p.Y < 240) mpos = (p.X) / 80 + 9;
                if (p.Y > 240 && p.Y < 320) mpos = (p.X) / 80 + 13;
            }

            /*пытаемся поставить фишку на пустое место*/
            if ((mpos == (nulpos - 1) && (float)mpos % 4 != 0) || (mpos == (nulpos + 1) && (float)nulpos % 4 != 0)
                || mpos == (nulpos - 4) || mpos == (nulpos + 4))
            {
                pzl2[nulpos - 1] = pzl2[mpos - 1];
                pzl2[mpos - 1] = 0;
                this.DoubleBuffered = true;
                DrawPuzzle(pzl2);
            }
        
         }
     
        public void DrawPuzzle(int[] pzl2)  //метод для рисования фишек на форме
        {
            draw15 d = new draw15(); //
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
            if (timer1.Enabled == true) { timer1.Enabled = false; button1.Text = "MIX"; }
            else { timer1.Enabled = true; button1.Text = "STOP"; }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Mix(pzl2); this.DoubleBuffered = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (timer2.Enabled == false) {
                timer2.Enabled = true;
            }  
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (s > 60) { timer2.Stop(); MessageBox.Show("Time is out!"); }
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
                        MessageBox.Show("YOU WIN!");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                XmlReaderSettings xmlreadersettings = new XmlReaderSettings();
                xmlreadersettings.Schemas.Add(null, "xml.xsd");
                xmlreadersettings.ValidationType = ValidationType.Schema;
                XmlReader xmlreader = XmlReader.Create("xml.xml", xmlreadersettings);
                isValid = true;
                while (xmlreader.Read()) { }
                if (isValid == true)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("xml.xml");
                    s = Convert.ToInt32(doc["config"]["settings"]["s"].InnerText);
                    m = Convert.ToInt32(doc["config"]["settings"]["m"].InnerText);
                    fontname = doc["config"]["button1fontname"].InnerText;
                    buttoncolor = Color.FromName(doc["config"]["buttoncolor"].InnerText);
                    button1.Font = new Font(fontname, 10);
                    button2.Font = new Font(fontname, 10);
                    button3.Font = new Font(fontname, 10);
                    button1.BackColor = buttoncolor;
                    button2.BackColor = buttoncolor;
                    button3.BackColor = buttoncolor;

                }
            }
            catch (Exception ex)
            {
                isValid = false;
                MessageBox.Show(ex.Message);
                m = 3;
                s = 3;
                fontname = "Microsoft Sans Serif";
                buttoncolor = Color.Black;

            }
        }

    }

}
