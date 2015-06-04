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
    class Box:baseBox
    {

        private float speedX;
        private float speedY;


        public Box(float width, float height, float x, float y,float speed):base(x,y,width,height)
        {
            speedX = speed;
            speedY = speed;
            color = Color.Blue;
        }

        override public void draw(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(color), x, y, w, h);
        }


        virtual public void move(float w, float h)
        {
            if (x <= 0 || x + this.w >= w)
                speedX *= -1;
            if (y <= 0 || y + this.h >= h)
                speedY *= -1;
            this.x += speedX;
            this.y += speedY;
        }

        virtual public void move(float w, float h,float speed)
        {
            if (speedX > 0)
                speedX += speed;
            else
                speedX -= speed;

            if (speedY > 0)
                speedY += speed;
            else
                speedY -= speed;

            if (x <= 0 || x+this.w>=w)
                speedX *= -1;
            if (y <= 0 || y + this.h>= h)
                speedY *= -1;
            this.x += speedX;
            this.y += speedY;
        }

        

    }
}
