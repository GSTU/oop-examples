using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WindowsFormsApplication1
{
    class draw15
    {
        public draw15() { }
            
        public void dr15(Graphics g,Pen p,int n,int pos,Font f)
        {
            int[,] xy = {{0,0}};   

            if (pos > 0 && pos < 5)
            {
                xy[0,0] = (pos - 1) * 80; xy[0,1] = 0;
            }
            else if (pos > 4 && pos < 9)
            {
                xy[0, 0] = (pos - 5) * 80; xy[0, 1] = 80;
            }
            else if (pos > 8 && pos < 13)
            {
                xy[0, 0] = (pos - 9) * 80; xy[0, 1] = 160;
            }
            else if (pos > 12 && pos <= 16)
            {
                xy[0, 0] = (pos - 13) * 80; xy[0, 1] = 240;
            }

            g.DrawRectangle(p, xy[0, 0]+2, xy[0, 1]+2, 76, 76);
            g.DrawRectangle(p, xy[0, 0] + 7, xy[0, 1] + 7, 66, 66);
            g.FillRectangle(Brushes.GreenYellow, xy[0, 0] + 7, xy[0, 1] + 7, 67, 67);
            string s = "" + n;
            g.DrawString(s, f, Brushes.Black, xy[0, 0]+18, xy[0, 1]+18);
           
        }
        
    }
}
