using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace lb13_oop
{
    class baseBox
    {
        protected float x, y, w, h;
        protected Color color;
        public float X { get { return x; } }
        public float Y { get { return y; } }
        public float W { get { return w; } }
        public float H { get { return h; } }


        public baseBox(float x, float y, float w, float h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            color = Color.White;

        }

        virtual public void draw(PaintEventArgs e) {
            color = Color.White;
            e.Graphics.FillRectangle(new SolidBrush(color), x, y, w, h);
            color = Color.Black;
            e.Graphics.DrawRectangle(new Pen(color, 2), x, y, w, h);
        }
    }
}
