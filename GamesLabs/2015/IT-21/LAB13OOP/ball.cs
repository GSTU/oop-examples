using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace lb12_oop
{
    class Ball
    {
        double R;
        double y;
        double x;

        public double X {
            get { return x; }
        }

        public double Y
        {
            get { return y; }
        }
        public double Radius
        {
            get { return R; }
        }

        int speedY;
        double speedX;
        Color color;
        bool moved;

        public Ball(double x, double y, double R, Color c, int speed)
        {
            this.x = x;
            this.y = y;
            this.R = R;
            this.color = c;
            this.speedX = speed;
            this.speedY = speed;
        }

        public double SPEEDX {
            get { return speedX; }
            set { speedX = value; }

        }

        public int SPEEDY {
            get { return speedY; }
            set { speedY = value; }
        }

        public void Draw()
        {
            Form1.g.FillEllipse(new SolidBrush(this.color), (float)this.x, (float)this.y, (float)this.R, (float)this.R);

        }

        public void move() {
            if (moved)
            {
                if ((this.y + speedY <= -Form1.formHight / 2) || (this.y + this.R + speedY >= Form1.formHight / 2))
                {
                    speedY *= -1;
                }
                this.y += speedY;
                this.x += speedX;
            }
        }

        public int loose() {
            if (this.x + speedX <= -400)
            {
                 return 1;
             }
             if (this.x + speedX >=400) {
                 return 2;
             }
             return 0;
        }


        public void startMove() {
            this.moved = true;
        }

        public void stopMove() {
            this.moved = false;
        }

        public void setStart(){
            this.x = 0;
            this.y = 0;
            this.moved = false;
        }

        public void  hittest(Player p1, Player p2){
            if (this.x - this.R-10 <= p1.X+p1.W)
            {
                if (this.y<= p1.Y + p1.H && this.y>= p1.Y)
                {
                    this.speedX *= -1.3;
                }
            }

            if (this.x + this.R >= p2.X-p1.W)
            {
                if (this.y <= p2.Y + p2.H && this.y  >= p2.Y)
                {
                    this.speedX *= -1.3;
                }
            }
        }
    }
}
