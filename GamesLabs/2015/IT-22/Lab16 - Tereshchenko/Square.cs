using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Game {
    public class Square {
        protected int _width;
        protected int _height;
        protected int _y;
        protected int _x;
        protected int _yspeed;
        protected int _xspeed;
        protected Color _color;
        protected Color _defcolor;
        protected Игрушка _game;
        public Square(int width, int height, int x, int y, Color color, Игрушка game) {
            this._width=width;
            _height = height;
            _y = y;
            _x = x;
            _defcolor = this._color= color;
            this._game=game;
        }
        public int Height {
            get { return _height; }
            set { _height = value; }
        }
        public int Width {
            get { return _width; }
            set { _width = value; }
        }


        public int Y {
            get { return _y; }
            set { _y = value; }
        }

        public int X {
            get { return _x; }
            set { _x = value; }
        }

        public Color Color {
            get { return _color; }
            set { _color = value; }
        }
        public int YSpeed {
            get { return _yspeed; }
            set { _yspeed = value; }
        }
        public int XSpeed {
            get { return _xspeed; }
            set { _xspeed = value; }
        }
        public virtual void Tick() {
            this._y+=this._yspeed;
            this._yspeed+=2;
            this._color=this._defcolor;
            this._x+=this._xspeed;
        }
        public virtual bool Collide(Square s) {
            return this._collide(s);
        }
        protected bool _collide(Square s) {
            int x=s._x;
            int y=s._y;
            int width=s._width;
            int height=s._height;
            int xw=this._x+this._width;
            int xw2=x+width;
            int yh=this._y+this._height;
            int yh2=y+height;
            if(x>this._x&&x<xw) {
                
                if(this._y>y&&this._y<yh2) return true;
                else if(yh>y&&yh<yh2) return true;
            }
            if(this._x>x&&this._x<xw2) {
                if(y>this._y&&y<yh) return true;
                else if(yh2>this._y&&yh2<yh) return true;
            }
            return false;
        }
        public void Draw(Graphics g) {
            g.FillRectangle(new SolidBrush(this._color), this._x, this._y, this._width, this._height);
        }
        public virtual void React(Square s) {
        }
        public virtual void AskDeletion() {
            _game.ToDelete.Add(this);
        }
    }
    public class Obstacle:Square {
        public Obstacle(int width, int height, int x, int y, Color color, Игрушка game)
            : base(width, height, x, y, color, game) {
        }
        public override void Tick() {
            this._xspeed=0;
            this._yspeed=0;
            this._color=this._defcolor;
        }
        public override bool Collide(Square s) {
            if(s is Obstacle) return false;
            return _collide(s);
        }
    }
    public class Player:Square {
        private bool canJump, jump, down;

        public bool Jump {
            get { return jump; }
            set { jump= value; }
        }
        public bool Down {
            get { return down; }
            set { down= value; }
        }
        public Player(int width, int height, int x, int y, Color color, Игрушка game)
            : base(width, height, x, y, color, game) {
        }
        public override void React(Square s) {
            if((s is Obstacle)) {
                this.Y=s.Y-this.Height+1;
                this.YSpeed=0;
                this.canJump=true;
            } else if(s is Enemy) {
                this.AskDeletion();
            }
        }
        public override void Tick() {
            if(jump&&canJump) {
                this.YSpeed-=35;
            }
            if(!canJump&&down) {
                this.YSpeed+=35;
            }
            this.canJump=false;
            this.jump=false;
            this.down=false;
            base.Tick();
        }
        public override void AskDeletion() {
            _game.gameOver=true;
        }
    }
    public class Enemy:Square {
        private int _jumpHeight;
        public Enemy(int width, int height, int x, int y, Color color, Игрушка game)
            : base(width, height, x, y, color, game) {
            Random rnd = new Random();
            this._jumpHeight=rnd.Next(30)+10;
            this._xspeed=-rnd.Next(10)-5;
        }
        public override void React(Square s) {
            if(s is Obstacle) {
                this._yspeed=-this._jumpHeight;
            }
        }
    }
    public class Destructor:Obstacle {
        public Destructor(int width, int height, int x, int y, Color color, Игрушка game)
            : base(width, height, x, y, color, game) {
        }
        public override void React(Square s) {
            s.AskDeletion();
        }
    }
}
