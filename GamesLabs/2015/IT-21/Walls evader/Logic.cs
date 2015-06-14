using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Walls_evader
{
    public class Player
    {
        public Panel Body { get; private set; }
        public Point Location;
        public int Direction;
        public int Speed { get; private set; }

        public Player(int x, int y, int speed, Color c)
        {
            this.Speed = speed;
            this.Location = new Point(x, y);
            this.Direction = 0;

            this.Body = new Panel();
            this.Body.Width = 8;
            this.Body.Height = 8;
            this.Body.Location = this.Location;
            this.Body.BackColor = c;
        }

        public void Move(object sender, EventArgs e)
        {
            switch (this.Direction)
            {
                case 1:
                    if (this.Location.X > 0)
                        this.Location.X -= this.Speed;
                    break;
                case 2:
                    if (this.Location.Y > 0)
                        this.Location.Y -= this.Speed;
                    break;
                case 3:
                    if (this.Location.X + this.Body.Width < 300)
                        this.Location.X += this.Speed;
                    break;
                case 4:
                    if (this.Location.Y + this.Body.Height < 400)
                        this.Location.Y += this.Speed;
                    break;
            }

            this.Body.Location = this.Location;
        }
        public void Reset(int x, int y)
        {
            this.Location.X = x;
            this.Location.Y = y;

            this.Body.Location = this.Location;
        }
    }
    public class Wall
    {
        public Panel Body { get; private set; }
        public Point Location;
        public int Speed { get; private set; }
        public int Direction { get; private set; }

        public Wall(int x, int y, int speed, Color c)
        {
            this.Speed = speed;
            this.Direction = 1;
            this.Location = new Point(x, y);

            this.Body = new Panel();
            this.Body.Width = 80;
            this.Body.Height = 5;
            this.Body.Location = this.Location;
            this.Body.BackColor = c;
        }

        public bool CheckCollision(Player p)
        {
            if ((p.Body.Location.X + p.Body.Width >= this.Body.Location.X) && (p.Body.Location.X <= this.Body.Location.X + this.Body.Width))
                if ((p.Body.Location.Y + p.Body.Height >= this.Body.Location.Y) && (p.Body.Location.Y <= this.Body.Location.Y + this.Body.Height))
                    return true;

            return false;
        }
        public void Move()
        {
            this.Location.X += this.Direction * this.Speed;
            this.Body.Location = this.Location;

            ChangeDirection();
        }
        private void ChangeDirection()
        {
            if ((this.Body.Location.X <= 0) || (this.Body.Location.X + this.Body.Width >= 300))
                this.Direction *= -1;
        }
    }
}