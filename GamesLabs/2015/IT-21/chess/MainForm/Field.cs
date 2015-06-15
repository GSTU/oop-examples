using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace MainForm
{
    class Field
    {
        Cell[,] Cells;//матрица ячеек игрового поля
        public Cell this[int i, int j]
        {
            set { Cells[i, j] = value; }
            get { return Cells[i, j]; }
        }
        public Field(PictureBox pb)
        {
            Reset(pb);
        }
        /// <summary>
        /// Сброс информации
        /// </summary>
        /// <param name="pb"></param>
        public void Reset(PictureBox pb)
        {
            Cells = new Cell[8,8];
            Colors c = Colors.Transparent;
            Counters b = Counters.None;
            for (int i = 0;i< 8;i++)
            {
                if (i % 2 ==0) c = Colors.White;
                else c = Colors.Sienna;
                if (i==0 || i ==1 || i==2) b = Counters.Black;
                else if (i==5 || i==6 || i==7) b = Counters.White;
                else b = Counters.None;
                for (int j=0;j<8; j++)
                {
                    Counters buf;
                    if ((i==0 || i==1 || i==2 || i==5 || i==6 || i==7)&& c==Colors.Sienna) buf =b;
                    else buf = Counters.None;
                    Cells[i,j] = new Cell(c,buf);
                    if (c== Colors.White) c= Colors.Sienna;
                    else c = Colors.White;
                }
            }

            Draw(pb, Form1.player_one, Form1.player_two);
        }
        /// <summary>
        /// Изображение поля
        /// </summary>
        /// <param name="pb"></param>
        public void Draw(PictureBox pb,Color p1, Color p2)
        {

            const int sideLength = 512;
            Bitmap bmp = new Bitmap(sideLength,sideLength);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                Brush c;
                Brush b;
                for (int i = 0; i<8;i++)
                {
                    for (int j = 0; j<8;j++)
                    {
                        if (Cells[i,j].ColorCell == Colors.White){c=Brushes.White;}
                        else if (Cells[i,j].ColorCell == Colors.Illuminated){c=Brushes.SkyBlue;}
                        else c = Brushes.Sienna;
                        if (Cells[i, j].Counter == Counters.Black) { b = new SolidBrush(p1); }
                        else if (Cells[i, j].Counter == Counters.White) { b = new SolidBrush(p2);}
                        else b = Brushes.Transparent;

                         g.FillRectangle(c, j * Cell.PartyLength, i * Cell.PartyLength, Cell.PartyLength, Cell.PartyLength);
                        g.FillEllipse(b, j * Cell.PartyLength + 3, i * Cell.PartyLength + 3, Cell.PartyLength - 6, Cell.PartyLength - 6);

                    }
                }
            }
            pb.Image = bmp;
        }
 

        }
    }

