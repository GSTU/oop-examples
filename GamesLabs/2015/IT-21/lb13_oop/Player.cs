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
    class Player:Box
    {
        private float moveX, moveY;
        public Player(float width, float height, float x, float y, float speed)
            : base(width, height, x, y,speed)
        {
            color = Color.Red;
        }


        public bool testPoint(float x, float y)
        {
            if (x >= this.x && x <= this.x + this.w)
                if (y >= this.y && y <= this.y + this.h)
                {
                    moveX = x-this.x;
                    moveY = y-this.y;
                    return true;
                }
            return false;
        }

        override public void move(float x, float y)
        {
            this.x = x - moveX; ;
            this.y = y - moveY;
        }

        public bool inBox(float x, float y, float w,  float h)
        {
            if (this.x > x && (this.x + this.w < x + w))
                if (this.y > y && (this.y + this.h < y + h))
                    return true;
            return false;
        }

        public bool hitTest(Box b) {
            var XColl = false;
            var YColl = false;

            if ((b.X + b.W >= this.x) && (b.X <= this.x + this.w)) XColl = true;
            if ((b.Y + b.H >= this.y) && (b.Y <= this.y + this.h)) YColl = true;

            if (XColl & YColl) { 
                return true; 
            }
            return false;
        }
    }
}
