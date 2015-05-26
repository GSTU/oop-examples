using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace _13
{
    public partial class Form1 : Form
    {
        private static int SizeField;
        public static int countwin;
        Graphics g;
        public static string color;
        public static string b, gr, r;
        Pen blue=new Pen(Color.Blue,3);
        Pen red = new Pen(Color.Red,3);
        Pen green = new Pen(Color.Green,3);
        int MouseX, MouseY;
        int msec=0;
        int sec=0;
        int min = 0;
        int[,] flags;
        bool win=false;
        public static bool valid = false;

        public void XSDCheck()
        {
            try
            {
                XmlSchemaSet configSettings = new XmlSchemaSet();
                configSettings.Add("urn:GameConfig-schema", "Setting1.xsd");
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas = configSettings;
                settings.ValidationEventHandler += new ValidationEventHandler(Validation);
                XmlReader reader = XmlReader.Create("Setting.xml", settings);
                XmlNodeType type;
                while (reader.Read())
                {
                    type = reader.NodeType;
                    if (type == XmlNodeType.Element)
                    {
                        if (reader.Name == "SizeField")
                        {
                            reader.Read();
                            if (valid == false)
                                SizeField = Convert.ToInt32(reader.Value);
                        }
                        if (reader.Name == "Count")
                        {
                            reader.Read();
                            if (valid == false)
                                countwin = Convert.ToInt32(reader.Value);
                        }
                        if (reader.Name == "ColorZero")
                        {
                            reader.Read();
                            b = reader.Value;
                            if (valid == false)
                                blue = new Pen(Color.FromName(b), 3);
                        }
                        if (reader.Name == "ColorCross")
                        {
                            reader.Read();
                            gr = reader.Value;
                            if (valid == false)
                                green = new Pen(Color.FromName(gr), 3);
                        }
                        if (reader.Name == "ColorLineWin")
                        {
                            reader.Read();
                            r = reader.Value;
                            if (valid == false)
                                red = new Pen(Color.FromName(r), 3);
                        }
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Ошибка отсутствует XML-файл или XSD-файл!!!\n Игра будет загружена с стандартными параметрами!!!");
                SizeField = 13;
                countwin = 5;
            }
        }

        private static void Validation(object sender, ValidationEventArgs e)
        {
            valid = true;
            MessageBox.Show("\t\tОшибка в XML-файле!!!\n Игра будет загружена с стандартными параметрами!!!");
            SizeField = 13;
            countwin = 5;
           
        }

        public Form1()
        {
            InitializeComponent();
            XSDCheck();
            g = panel1.CreateGraphics();
            flags = new int[SizeField, SizeField];
        }
       
            public void Cross(int x,int y)
            {
                Point p1 = new Point(x-15,y-15);
                Point p2 = new Point(x+15,y+15);
                Point p3 = new Point(x+15, y-15);
                Point p4 = new Point(x-15, y+15);
                g.DrawLine(blue,p1,p2);
                g.DrawLine(blue, p3, p4);
                label3.Text = "Ходят нолики";

            }
            public void Zero(int x,int y)
            {
                g.DrawEllipse(green,x-16,y-16,33,33);
                label3.Text = "Ходят крестики";
            }

            public void DrawLineDiog1(int y, int x)
            {
                g.DrawLine(red, x+countwin*2, y +countwin*2, x-countwin*34, y -countwin*34);
            }

            public void DrawLineDiog(int y, int x)
            {
                g.DrawLine(red, x-countwin*2, y+countwin*2 , x+countwin*34 , y -countwin*34);
            }

            public void DrawLineVert(int x,int y)
            {
                g.DrawLine(red,x-countwin*8,y,x-countwin*40,y);
            }

            public void DrawLineHor(int x, int y)
            {
                g.DrawLine(red, x, y-countwin*8, x, y-countwin*40);
            }

            public void ProverkaHor()
            {
                int figure = 2; //фигура, 2 - никакая, -1 - нолик, 1 - крестик
                int count = 0;
                for (int j = 0; j <SizeField; j++)
                {
                    for (int i = 0; i < SizeField; i++)
                    {
                        if (count == countwin && figure == -1)
                        {
                            int x = Math.Abs((i+1) * 40 - 20);
                            int y = Math.Abs((j+1)  * 40 - 20);
                            DrawLineHor(y, x);
                            timer1.Stop();
                            label3.Text = "Выиграли нолики";
                            count = 0;
                            win = true;
                            break;
                        }
                        else if (count == countwin && figure == 1)
                        {
                            int x = Math.Abs((i + 1) * 40 - 20);
                            int y = Math.Abs((j + 1) * 40 - 20);
                            DrawLineHor(y, x);
                            timer1.Stop();
                            label3.Text = "Выиграли крестики";
                            count = 0;
                            win = true;
                            break;
                        }
                        else if (count != countwin && m == SizeField*SizeField && win == false)
                        {
                            timer1.Stop();
                            label3.Text = "Ничья";
                            win = true;
                            break;
                        }
                        else if (figure == flags[i, j] && win == false)
                        {
                            count++;
                        }
                        else if (flags[i, j] != 0 && win == false)
                        {
                            figure = flags[i, j];
                            count = 1;
                        }
                        else
                        {
                            figure = 2;
                            count = 0;
                        }

                    }
                }
            }
       
        public void ProverkaDiog()
            {
                int currentFigure = 2; //фигура, 2 - никакая, -1 - нолик, 1 - крестик
                int count = 0;
                int i = 0;
                int j = 0;
                //диагональная проверка c 0,0 и вправо (верхняя часть)
                int n = SizeField;
                int f = 0;
                while (win != true && j < SizeField)
                {
                    while (win != true && i < n)
                    {
                        f++;
                        if (count == countwin && currentFigure == -1)
                        {
                            //int  x= Math.Abs((i + 1) * 40 - 20);
                            //int  y= Math.Abs((j + 1) * 40 - 20);
                            int x = MouseX;
                            int y = MouseY;
                            DrawLineDiog1(y, x);
                            timer1.Stop();
                            label3.Text = "Выиграли нолики";
                            count = 0;
                            win = true;
                        }
                        else if (count == countwin && currentFigure == 1)
                        {
                            //int x = Math.Abs((i + 1) * 40 - 20);
                            //int y = Math.Abs((j + 1) * 40 - 20);
                            int x = MouseX;
                            int y = MouseY;
                            DrawLineDiog1(y, x);
                            timer1.Stop();
                            label3.Text = "Выиграли крестики";
                            count = 0;
                            win = true;
                        }

                        else if (flags[i, j] == currentFigure)
                        {
                            count++;
                            i++;
                            j++;

                        }
                        else if (flags[i, j] != 0)
                        {
                            currentFigure = flags[i, j];
                            count = 1;
                            i++;
                            j++;

                        }
                        else
                        {
                            currentFigure = 2;
                            count = 0;
                            i++;
                            j++;
                        }
                    }
                    i--;
                    j--;
                    if (win != true)
                    {
                        n--;
                        i--;
                    }
                    while (win != true && i >= 0)
                    {

                        if (count == countwin && currentFigure == -1)
                        {
                            //int x = Math.Abs((i + 1) * 40 - 20);
                            //int y = Math.Abs((j + 1) * 40 - 20);
                            int x = MouseX;
                            int y = MouseY;
                            DrawLineDiog1(y, x);
                            timer1.Stop();
                            label3.Text = "Выиграли нолики";
                            count = 0;
                            win = true;
                            i--;
                            j--;
                        }
                        else if (count == countwin && currentFigure == 1)
                        {
                            //int x = Math.Abs((i + 1) * 40 - 20);
                            //int y = Math.Abs((j + 1) * 40 - 20);
                            int x = MouseX;
                            int y = MouseY;
                            DrawLineDiog1(y, x);
                            timer1.Stop();
                            label3.Text = "Выиграли крестики";
                            count = 0;
                            win = true;
                            i--;
                            j--;
                        }

                        else if (flags[i, j] == currentFigure)
                        {
                            count++;

                            i--;
                            j--;
                        }
                        else if (flags[i, j] != 0)
                        {
                            currentFigure = flags[i, j];
                            count = 1;
                            i--;
                            j--;
                        }
                        else
                        {
                            currentFigure = 2;
                            count = 0;
                            i--;
                            j--;
                        }
                    }
                    i++;
                    j++;
                    if (win != true)
                    {
                        j++;
                        n--;
                    }
                }
          }
        
        public void ProverkaDiog1()
            {
                int currentFigure = 2; //фигура, 2 - никакая, -1 - нолик, 1 - крестик
                int count = 0;
                int i;
                int j;
                int n;
               //диагональная проверка c 0,0 и вправо (нижняя часть)
                j = 0;
                i = 1;
                n = SizeField-1;
                while (win != true && i < SizeField)
                {
                    while (win != true && j < n)
                    {
                        if (count == countwin && currentFigure == -1)
                        {
                            int x = MouseX;
                            int y = MouseY;
                            DrawLineDiog1(y, x);
                            timer1.Stop();
                            label3.Text = "Выиграли нолики";
                            count = 0;
                            win = true;
                        }
                        else if (count == countwin && currentFigure == 1)
                        {
                            int x = MouseX;
                            int y = MouseY;
                            DrawLineDiog1(y, x);
                            timer1.Stop();
                            label3.Text = "Выиграли крестики";
                            count = 0;
                            win = true;
                        }

                        else if (flags[i, j] == currentFigure)
                        {
                            count++;
                            i++;
                            j++;
                        }
                        else if (flags[i, j] != 0)
                        {
                            currentFigure = flags[i, j];
                            count = 1;
                            i++;
                            j++;
                        }
                        else
                        {
                            currentFigure = 2;
                            count = 0;
                            i++;
                            j++;
                        }
                    }
                    i--;
                    j--;
                    if (win != true)
                    {
                        n--;
                        j--;
                    }
                    while (win != true && j >= 0)
                    {
                        if (count == countwin && currentFigure == -1)
                        {
                            int x = MouseX;
                            int y = MouseY;
                            DrawLineDiog1(y, x);
                            timer1.Stop();
                            label3.Text = "Выиграли нолики";
                            count = 0;
                            win = true;
                        }
                        else if (count == countwin && currentFigure == 1)
                        {
                            int x = MouseX;
                            int y = MouseY;
                            DrawLineDiog1(y, x);
                            timer1.Stop();
                            label3.Text = "Выиграли крестики";
                            count = 0;
                            win = true;
                        }

                        else if (flags[i, j] == currentFigure)
                        {
                            count++;
                            i--;
                            j--;
                        }
                        else if (flags[i, j] != 0)
                        {
                            currentFigure = flags[i, j];
                            count = 1;
                            i--;
                            j--;
                        }
                        else
                        {
                            currentFigure = 2;
                            count = 0;
                            i--;
                            j--;
                        }
                    }
                    i++;
                    j++;
                    if (win != true)
                    {
                        i++;
                        n--;
                    }
                }
             }
        
        public void ProverkaDiog2()
            {
                int currentFigure = 2; //фигура, 2 - никакая, -1 - нолик, 1 - крестик
                int count = 0;
                int i;
                int j;
                int n;
            
                //диагональная проверка с 0,12 влево (верхняя часть)
                i = 0;
                j = SizeField-1;
                n = SizeField;
                while (win != true && j > 3)
                {
                    while (win != true && i < n)
                    {
                        if (count == countwin && currentFigure == -1)
                        {
                            int x = MouseX;
                            int y = MouseY;
                            DrawLineDiog(y, x);
                            timer1.Stop();
                            label3.Text = "Выиграли нолики";
                            count = 0;
                            win = true;
                        }
                        else if (count == countwin && currentFigure == 1)
                        {

                            int x = MouseX;
                            int y = MouseY;
                            DrawLineDiog(y, x);
                            timer1.Stop();
                            label3.Text = "Выиграли крестики";
                            count = 0;
                            win = true;
                        }

                        else if (flags[i, j] == currentFigure)
                        {
                            count++;
                            i++;
                            j--;
                        }
                        else if (flags[i, j] != 0)
                        {
                            currentFigure = flags[i, j];
                            count = 1;
                            i++;
                            j--;
                        }
                        else
                        {
                            currentFigure = 2;
                            count = 0;
                            i++;
                            j--;
                        }
                    }
                    i--;
                    j++;
                    if (win != true)
                    {
                        n--;
                        i--;
                    }
                    while (win != true && i >= 0)
                    {
                        if (count == countwin && currentFigure == -1)
                        {

                            int x = MouseX;
                            int y = MouseY;
                            DrawLineDiog(y, x);
                            timer1.Stop();
                            label3.Text = "Выиграли нолики";
                            count = 0;
                            win = true;
                        }
                        else if (count == countwin && currentFigure == 1)
                        {

                            int x = MouseX;
                            int y = MouseY;
                            DrawLineDiog(y, x);
                            timer1.Stop();
                            label3.Text = "Выиграли крестики";
                            count = 0;
                            win = true;
                        }

                        else if (flags[i, j] == currentFigure)
                        {
                            count++;
                            i--;
                            j++;
                        }
                        else if (flags[i, j] != 0)
                        {
                            currentFigure = flags[i, j];
                            count = 1;
                            i--;
                            j++;
                        }
                        else
                        {
                            currentFigure = 2;
                            count = 0;
                            i--;
                            j++;
                        }
                    }
                    i++;
                    j--;
                    if (win != true)
                    {
                        j--;
                        n--;
                    }
                }
         }
        
        public void ProverkaDiog3()
            {
                int currentFigure = 2; //фигура, 2 - никакая, -1 - нолик, 1 - крестик
                int count = 0;
                int i;
                int j;
                int n;
                //диагональная проверка с 0,12 влево (нижняя часть)
                i = 1;
                j = SizeField-1;
                n = 0;
                while (win != true && i < SizeField)
                {
                    while (win != true && j > n)
                    {
                        if (count == countwin && currentFigure == -1)
                        {

                            int x = MouseX;
                            int y = MouseY;
                            DrawLineDiog(y, x);
                            timer1.Stop();
                            label3.Text = "Выиграли нолики";
                            count = 0;
                            win = true;
                        }
                        else if (count == countwin && currentFigure == 1)
                        {

                            int x = MouseX;
                            int y = MouseY;
                            DrawLineDiog(y, x);
                            timer1.Stop();
                            label3.Text = "Выиграли крестики";
                            count = 0;
                            win = true;
                        }

                        else if (flags[i, j] == currentFigure)
                        {
                            count++;
                            i++;
                            j--;
                        }
                        else if (flags[i, j] != 0)
                        {
                            currentFigure = flags[i, j];
                            count = 1;
                            i++;
                            j--;
                        }
                        else
                        {
                            currentFigure = 2;
                            count = 0;
                            i++;
                            j--;
                        }
                    }
                    i--;
                    j++;
                    if (win != true)
                    {
                        n++;
                        j++;
                    }
                    while (win != true && j < SizeField)
                    {
                        if (count == 5 && currentFigure == -1)
                        {

                            int x = MouseX;
                            int y = MouseY;
                            DrawLineDiog(y, x);
                            timer1.Stop();
                            label3.Text = "Выиграли нолики";
                            count = 0;
                            win = true;
                        }
                        else if (count == countwin && currentFigure == 1)
                        {

                            int x = MouseX;
                            int y = MouseY;
                            DrawLineDiog(y, x);
                            timer1.Stop();
                            label3.Text = "Выиграли крестики";
                            count = 0;
                            win = true;
                        }

                        else if (flags[i, j] == currentFigure)
                        {
                            count++;
                            i--;
                            j++;
                        }
                        else if (flags[i, j] != 0)
                        {
                            currentFigure = flags[i, j];
                            count = 1;
                            i--;
                            j++;
                        }
                        else
                        {
                            currentFigure = 2;
                            count = 0;
                            i--;
                            j++;
                        }
                    }
                    i++;
                    j--;
                    if (win != true)
                    {
                        i++;
                        n++;
                    }
                }
                   
               
            }
        
        public void ProverkaVert()
            {
                int figure = 2; //фигура, 2 - никакая, -1 - нолик, 1 - крестик
                int count = 0; //кол-во повторений одинаковых фигур
                for (int i = 0; i < SizeField; i++)
                {
                    for (int j = 0; j < SizeField; j++)
                    {
                        if (count == countwin && figure == -1)
                        {
                            int x = Math.Abs((i +1) * 40 - 20);
                            int y = Math.Abs((j +1) * 40 - 20);
                            DrawLineVert(y,x);
                            timer1.Stop();
                            label3.Text="Выиграли нолики";
                            count = 0;
                            win = true;
                            break;
                        }
                        else if(count == countwin && figure == 1)
                        {
                            int x = Math.Abs((i +1) * 40 - 20);
                            int y = Math.Abs((j +1) * 40 - 20);                           
                            DrawLineVert(y, x);
                            timer1.Stop();
                            label3.Text ="Выиграли крестики";
                            count = 0;
                            win = true;
                            break;
                        }
                        else if (count != countwin && m == SizeField*SizeField)
                        {
                            timer1.Stop();
                            label3.Text = "Ничья";
                            win = true;
                            break;
                        }
                        else if (figure == flags[i, j])
                        {
                            count++;
                        }
                        else if (flags[i,j] != 0)
                        {
                            figure = flags[i, j];
                            count = 1;
                        }
                        else 
                        {
                            figure = 2;
                            count = 0;
                        }
                    }
                }
          
            }

        public void Draw()
        {

            for (int i = 0; i < SizeField; i++)
            {
                for (int j = 0; j < SizeField; j++)
                {
                    g.DrawRectangle(System.Drawing.Pens.Black, i * 40, j * 40, 40, 40);
                    flags[i, j] = 0;
                }
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label3.Text = "Первыми ходят крестики";
            label5.Text = "";
            label2.Text = "";
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Draw();
            
        }

        int m = 0;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
           timer1.Start();
           MouseX = e.X;
           MouseY = e.Y;
           label5.Text = (m+1).ToString();
           
           
           if (e.X >= 0 && e.X <= 40)  MouseX = 20; 
           if (e.X > 40 && e.X <= 80)  MouseX = 60; 
           if (e.X > 80 && e.X <= 120) MouseX = 100; 
           if (e.X > 120 && e.X <= 160) MouseX = 140; 
           if (e.X > 160 && e.X <= 200) MouseX = 180; 
           if (e.X > 200 && e.X <= 240) MouseX = 220; 
           if (e.X > 240 && e.X <= 280) MouseX = 260; 
           if (e.X > 280 && e.X <= 320) MouseX = 300; 
           if (e.X > 320 && e.X <= 360) MouseX = 340; 
           if (e.X > 360 && e.X <= 400) MouseX = 380; 
           if (e.X > 400 && e.X <= 440) MouseX = 420; 
           if (e.X > 440 && e.X <= 480) MouseX = 460; 
           if (e.X > 480 && e.X <= 520)  MouseX = 500; 

           if (e.Y >= 0 && e.Y <= 40) MouseY = 20;
           if (e.Y > 40 && e.Y <= 80) MouseY = 60;
           if (e.Y > 80 && e.Y <= 120) MouseY = 100;
           if (e.Y > 120 && e.Y <= 160) MouseY = 140;
           if (e.Y > 160 && e.Y <= 200) MouseY = 180;
           if (e.Y > 200 && e.Y <= 240) MouseY = 220;
           if (e.Y > 240 && e.Y <= 280) MouseY = 260;
           if (e.Y > 280 && e.Y <= 320) MouseY = 300;
           if (e.Y > 320 && e.Y <= 360) MouseY = 340;
           if (e.Y > 360 && e.Y <= 400) MouseY = 380;
           if (e.Y > 400 && e.Y <= 440) MouseY = 420;
           if (e.Y > 440 && e.Y <= 480) MouseY = 460;
           if (e.Y > 480 && e.Y <= 520) MouseY = 500;

           if (m % 2 == 0)
           {
               if (flags[((MouseY + 20) / 40) - 1, ((MouseX + 20) / 40) - 1] == 0 && win==false)
               {
                   Cross(MouseX, MouseY);
                   flags[((MouseY + 20) / 40) - 1, ((MouseX + 20) / 40) - 1] = 1;
                   m++;
               }
              
           }

           if (m % 2 != 0)
           {
               if (flags[((MouseY + 20) / 40) - 1, ((MouseX + 20) / 40) - 1] == 0 && win == false)
               {
               Zero(MouseX, MouseY);
               flags[ ((MouseY + 20) / 40)-1, ((MouseX + 20) / 40)-1] = -1;
               m++;
                }
              
           }
           
              ProverkaVert();
              ProverkaHor();
              ProverkaDiog();
              ProverkaDiog1();
              ProverkaDiog2();
              ProverkaDiog3();
           
           
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            msec++;
            if (msec == 9)
            {
                msec = 0;
                sec++;
            }
            if (sec == 60)
            {
                sec = 0; 
                min++;
            }

            label2.Text = min+":" + sec+","+msec;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void правилаИгрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Игроки по очереди ходят крестиками и ноликами.\nПобедитель тот у кого по вертикали или по горизонтали либо накрест будут подряд стоять 5 крестиков или ноликов!");
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
  
            msec = 0;
            sec = 0;
            min = 0;
            m = 0;
            win = false;

            if (m == 0)
            {
                timer1.Stop();
                label2.Text = "";
                label5.Text="";
            }
            else
            {
                timer1.Start();
                label2.Text = m.ToString();
            }

            label3.Text = "Первыми ходят крестики";
            Refresh();
            Draw();
        }

    }
}

