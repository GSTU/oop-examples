using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace lb12_oop
{
    class Player
    {
        int width;
        int height;
        int x, y;
        Color color;

        public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }
        public int H
        {
            get { return height; }
        }
        public int W
        {
            get { return width; }
        }


        public Color Color {
            get { return color; }
        }

        public Player(int w, int h, int x, int y, Color color)
        {
            this.x = x;
            this.y = y;
            this.width = w;
            this.height = h;
            this.color = color;
        }

        public void move(int shag) {
            if (this.y + shag >= -Form1.formHight / 2 )
            {
                if (this.y + this.height + shag <= Form1.formHight/2) {
                    this.y += shag;
                }
            }
        }

        public void draw() { 
           SolidBrush b = new SolidBrush(this.color);
           Form1.g.FillRectangle(b, this.x, this.y, this.width, this.height);
        }

        public void setStart() {
            this.y = 0;
        }
    }
}
