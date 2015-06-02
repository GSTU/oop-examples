using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

namespace Laba13
{
    class Cell
    {
        int x, y;
        int number = -1;
        SolidBrush brush;

        public Cell(int x, int y, int number, string fieldColor)
        {
            this.x = x;
            this.y = y;
            this.number = number;
            brush = new SolidBrush(ColorTranslator.FromHtml(fieldColor));
        }

        //Свойства доступа
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        public SolidBrush SolidBrush
        {
            get { return brush; }
        }
    }
}
