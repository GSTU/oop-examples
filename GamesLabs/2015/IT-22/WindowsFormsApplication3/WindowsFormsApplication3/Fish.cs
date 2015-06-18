using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication3
{
    class Fish
    {
        int size;
        Color c;
        public Color color {
            get { return c; }
            set { c = value; }
        }

        float X, Y;
        public float cordX
        {
            get { return X; }
        }
        public float cordY
        {
            get { return Y; }
        }
        float angle;

        public float Angle
        {
            get { return angle; }
        }

        float speed;

        public Fish(Color c, float x, float y, float speed, float angle)
        {
            this.speed = speed;
            this.angle = angle;
            this.c = c;
            X = x;
            Y = y;
        }

        public void draw(Graphics g)
        {
            g.FillEllipse(new SolidBrush(c), X, Y - 10, 20, 20);
            if (Math.Abs(angle) <= 90)
            {
                g.FillRectangle(new SolidBrush(c), X - 10, Y - 5, 20, 10);
            }
            else {
                g.FillRectangle(new SolidBrush(c), X + 10, Y - 5, 20, 10);
            }
        }


        public void changeAngle(float a)
        {
            angle = a;
        }

        public void move()
        {
            
            X += speed * (float)Math.Cos(angle * Math.PI / 180);
            Y -= speed * (float)Math.Sin(angle * Math.PI / 180);
        }




    }
}
