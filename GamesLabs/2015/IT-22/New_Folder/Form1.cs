using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WindowsFormsApplication1 {
    public partial class Form1:Form {
        Graphics graphics;
        Python python;
        enum Mode { Game, Menu, GameOver, Start, Options };
        private Mode mode=Mode.Start;
        private Point cursor;
        private int NextApple=0;
        private int NextMangusto=0;
        private int score=0;
        private int time=0;
        private bool click=false;
        private double difficulty;
        private double spawnIncrease=0.1;
        private double speedIncrease=0.1;
        private double acceleration=5;
        private List<Apple> Apples=new List<Apple>();
        private List<Mangusto> Mangustos=new List<Mangusto>();
        private List<Sprite> Deletion=new List<Sprite>();
        private Random rnd=new Random();
        private int windowWidth=1024, windowHeight=768;
        private List<GUIElement> gui = new List<GUIElement>();
        private SettingsContainer options;
        private Color backgroundColor=Color.Green;
        private SettingsContainer defaultSettings=new SettingsContainer(1024, 768, 0, 128, 0, 5, 0.1, 0.1);
        public Form1() {
            InitializeComponent(); this.SetStyle(
                ControlStyles.AllPaintingInWmPaint | 
                ControlStyles.UserPaint | 
                ControlStyles.DoubleBuffer,
                true);
            this.readXml();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.setScale();
            this.NewMenu();
            this.Invalidate();
        }
        private void setScale() {
            this.Width=this.windowWidth;
            this.Height=this.windowHeight;
        }
        private void Exit() {
            Application.Exit();
        }
        private void NewMenu() {
            this.mode=Mode.Start;
            double ww=(double)this.windowWidth/(double)1024;
            double hh=(double)this.windowHeight/(double)768;
            this.gui=new List<GUIElement>();
            GUIButton StartButton=new GUIButton((int)(384*ww), (int)(384*hh), (int)(256*ww), (int)(32*hh), "Start");
            StartButton.AddAction(this.NewGame);
            GUIButton OptionsButton=new GUIButton((int)(384*ww), (int)(432*hh), (int)(256*ww), (int)(32*hh), "Options");
            OptionsButton.AddAction(this.NewOptions);
            GUIButton ExitButton=new GUIButton((int)(384*ww), (int)(480*hh), (int)(256*ww), (int)(32*hh), "Exit");
            ExitButton.AddAction(this.Exit);
            this.gui.Add(StartButton);
            this.gui.Add(OptionsButton);
            this.gui.Add(ExitButton);
        }
        private void GameOver() {
            this.mode=Mode.GameOver;
            double ww=(double)this.windowWidth/(double)1024;
            double hh=(double)this.windowHeight/(double)768;
            this.gui=new List<GUIElement>();
            GUITitle GameOverTitle=new GUITitle((int)(224*ww), (int)(192*hh), (int)(96*ww), "GAME OVER");
            GUITitle ScoreTitle=new GUITitle((int)(362*ww), (int)(320*hh), (int)(32*ww), "Your score is "+this.score);
            GUIButton MenuButton=new GUIButton((int)(384*ww), (int)(400*hh), (int)(256*ww), (int)(32*hh), "Back");
            this.gui.Add(GameOverTitle);
            this.gui.Add(ScoreTitle);
            this.gui.Add(MenuButton);
            MenuButton.AddAction(NewMenu);
        }
        private void NewOptions() {
            this.mode=Mode.Options;
            double ww=(double)this.windowWidth/(double)1024;
            double hh=(double)this.windowHeight/(double)768;
            gui=new List<GUIElement>();
            GUIButton StartButton=new GUIButton((int)(224*ww), (int)(600*hh), (int)(256*ww), (int)(32*hh), "Save");
            StartButton.AddAction(this.ApplySettings);
            GUIButton ExitButton=new GUIButton((int)(544*ww), (int)(600*hh), (int)(256*ww), (int)(32*hh), "Cancel");
            ExitButton.AddAction(this.NewMenu);
            GUINumeric WidthNumeric=new GUINumeric((int)(512*ww), (int)(64*hh), (int)(256*ww), (int)(32*hh), 512, 1440, 128);
            GUINumeric HeightNumeric=new GUINumeric((int)(512*ww), (int)(112*hh), (int)(256*ww), (int)(32*hh), 384, 1024, 128);
            GUIStrip RStrip=new GUIStrip((int)(512*ww), (int)(160*hh), (int)(256*ww), (int)(32*hh));
            GUIStrip GStrip=new GUIStrip((int)(512*ww), (int)(208*hh), (int)(256*ww), (int)(32*hh));
            GUIStrip BStrip=new GUIStrip((int)(512*ww), (int)(256*hh), (int)(256*ww), (int)(32*hh));
            GUINumeric SpeedNumeric=new GUINumeric((int)(512*ww), (int)(304*hh), (int)(256*ww), (int)(32*hh), 0, 1, 0.025);
            GUINumeric SpawnNumeric=new GUINumeric((int)(512*ww), (int)(352*hh), (int)(256*ww), (int)(32*hh), 0, 1, 0.025);
            GUINumeric AccelerationNumeric=new GUINumeric((int)(512*ww), (int)(400*hh), (int)(256*ww), (int)(32*hh), 0, 10, 0.5);
            this.gui.Add(StartButton);
            this.gui.Add(ExitButton);
            this.gui.Add(WidthNumeric);
            this.gui.Add(HeightNumeric);
            this.gui.Add(RStrip);
            this.gui.Add(GStrip);
            this.gui.Add(BStrip);
            this.gui.Add(SpeedNumeric);
            this.gui.Add(SpawnNumeric);
            this.gui.Add(AccelerationNumeric);
            WidthNumeric.Value=this.windowWidth;
            HeightNumeric.Value=this.windowHeight;
            RStrip.Value=(double)this.backgroundColor.R/256;
            GStrip.Value=(double)this.backgroundColor.G/256;
            BStrip.Value=(double)this.backgroundColor.B/256;
            SpeedNumeric.Value=this.speedIncrease;
            SpawnNumeric.Value=this.spawnIncrease;
            AccelerationNumeric.Value=this.acceleration;
            GUITitle WidthTitle=new GUITitle((int)(64*ww), (int)(64*hh), (int)(32*ww), "Window width");
            GUITitle HeightTitle=new GUITitle((int)(64*ww), (int)(112*hh), (int)(32*ww), "Window height");
            GUITitle RTitle=new GUITitle((int)(64*ww), (int)(160*hh), (int)(32*ww), "Background: red");
            GUITitle GTitle=new GUITitle((int)(64*ww), (int)(208*hh), (int)(32*ww), "Background: green");
            GUITitle BTitle=new GUITitle((int)(64*ww), (int)(256*hh), (int)(32*ww), "Background: blue");
            GUITitle SpeedTitle=new GUITitle((int)(64*ww), (int)(304*hh), (int)(32*ww), "Apple acceleration");
            GUITitle SpawnTitle=new GUITitle((int)(64*ww), (int)(352*hh), (int)(32*ww), "Difficulty increase");
            GUITitle AccelTitle=new GUITitle((int)(64*ww), (int)(400*hh), (int)(32*ww), "Click boost");
            this.gui.Add(WidthTitle);
            this.gui.Add(HeightTitle);
            this.gui.Add(RTitle);
            this.gui.Add(GTitle);
            this.gui.Add(BTitle);
            this.gui.Add(SpeedTitle);
            this.gui.Add(SpawnTitle);
            this.gui.Add(AccelTitle);
        }
        private void ApplySettings() {
            this.options.WindowWidth=(int)((GUINumeric)this.gui[2]).Value;
            this.options.WindowHeight=(int)((GUINumeric)this.gui[3]).Value;
            this.options.ColorR=(int)(((GUIStrip)this.gui[4]).Value*256);
            this.options.ColorG=(int)(((GUIStrip)this.gui[5]).Value*256);
            this.options.ColorB=(int)(((GUIStrip)this.gui[6]).Value*256);
            this.options.SpeedIncrease=((GUINumeric)this.gui[7]).Value;
            this.options.SpawnIncrease=((GUINumeric)this.gui[8]).Value;
            this.options.Acceleration=((GUINumeric)this.gui[9]).Value;
            this.setOptions();
            this.setScale();
            this.NewMenu();
        }
        private void setOptions() {
            this.windowHeight=this.options.WindowHeight;
            this.windowWidth=this.options.WindowWidth;
            this.backgroundColor=ColorTranslator.FromWin32(this.options.ColorB*256*256+this.options.ColorG*256+this.options.ColorR);
            this.spawnIncrease=this.options.SpawnIncrease;
            this.speedIncrease=this.options.SpeedIncrease;
            this.acceleration=this.options.Acceleration;
        }
        private void NewGame() {
            this.mode=Mode.Game;
            this.python=new Python(this.acceleration, this.speedIncrease);
            this.Apples=new List<Apple>();
            this.Mangustos=new List<Mangusto>();
            this.NextApple=0;
            this.NextMangusto=150;
            this.score=0;
            this.time=0;
            this.difficulty=1;
        }
        private void timer1_Tick(object sender, EventArgs e) {
            this.Invalidate();
        }
        private void DrawField() {
            this.graphics.FillRectangle(new SolidBrush(this.backgroundColor), 0, 0, this.Width, this.Height);
            foreach(PythonSegment p in python.Body) {
                p.Draw(this.graphics);
            }
            foreach(Apple p in this.Apples) {
                p.Draw(this.graphics);
            }
            foreach(Mangusto p in this.Mangustos) {
                p.Draw(this.graphics);
            }
            this.graphics.DrawLine(new Pen(Color.Gray, 32), 0, 0, 0, this.Height-32);
            this.graphics.DrawLine(new Pen(Color.Gray, 32), 0, 0, this.Width-8, 0);
            this.graphics.DrawLine(new Pen(Color.Gray, 32), this.Width-8, this.Height-32, 0, this.Height-32);
            this.graphics.DrawLine(new Pen(Color.Gray, 32), this.Width-8, this.Height-32, this.Width-8, 0);
            this.graphics.DrawString("Score: "+this.score.ToString(), new Font(FontFamily.GenericMonospace, 12, FontStyle.Bold), new SolidBrush(Color.Yellow), new PointF(8, 8));
            this.graphics.DrawString("Time: "+(this.time/50).ToString()+"."+(this.time%50*2).ToString(), new Font(FontFamily.GenericMonospace, 12, FontStyle.Bold), new SolidBrush(Color.Yellow), new PointF(8, 28));

        }
        public Graphics GetGraphics() {
            return this.graphics;
        }
        private void drawTitle() {
            int[] tx= { 32, 280, 160, 300, 350, 590, 680, 570, 750, 830, 740 };
            int[] ty= { 32, 32, 24, 40, 20, 20, 40, 130, 20, 40, 30 };
            int[] tx2= { 48, 200, 260, 400, 370, 600, 670, 690, 740, 830, 840 };
            int[] ty2= { 256, 256, 128, 20, 260, 260, 240, 110, 260, 240, 250 };
            Pen title=new Pen(Color.Plum, 16);
            double ww=(double)this.windowWidth/(double)1024;
            double hh=(double)this.windowHeight/(double)768;
            for(int i=0; i<tx.Length; i++) {
                this.graphics.DrawLine(title, (int)(tx[i]*ww), (int)(ty[i]*hh), (int)(tx2[i]*ww), (int)(ty2[i]*hh));
            }
            int[] rx1= { -80, 390, 380 };
            int[] ry1= { 36, 36, 36 };
            int[] rx2= { 200, 150, 150 };
            int[] ry2= { 100, 200, 200 };
            int[] a1= { -90, -270, 90 };
            int[] a2= { 180, 180, -240 };
            for(int i=0; i<rx1.Length; i++) {
                this.graphics.DrawArc(title, new Rectangle((int)(rx1[i]*ww), (int)(ry1[i]*hh), (int)(rx2[i]*ww), (int)(ry2[i]*hh)), a1[i], a2[i]);
            }

        }
        private void menuControl() {
            this.cursor = this.PointToClient(Cursor.Position);
            this.graphics.FillRectangle(new SolidBrush(this.backgroundColor), 0, 0, this.Width, this.Height);
            foreach(GUIElement e in this.gui) {
                e.Control(this);
            }
            this.click=false;
        }
        private void gameControl() {
            this.cursor = this.PointToClient(Cursor.Position);
            python.Control();
            Point m=python.GetMouth();
            foreach(Apple p in this.Apples) {
                if(p.CheckCollision(m, 8)) {
                    python.EatApple();
                    this.difficulty+=this.spawnIncrease;
                    this.score++;
                    this.RequestDeletion(p);
                }
            }
            foreach(Mangusto p in this.Mangustos) {
                p.Control(this);
            }
            if(this.NextApple<=0) {
                this.NextApple=75;
                bool col=false;
                Point p;
                do {
                    p=new Point(rnd.Next(this.Width-128)+64, rnd.Next(this.Height-128)+64);
                    col=python.CheckCollision(p, 32); if(!col) {
                        for(int i=0; i<this.Apples.Count&&!col; i++) {
                            col=this.Apples[i].CheckCollision(p, 32);
                        }
                    }
                    if(!col) {
                        for(int i=0; i<this.Mangustos.Count&&!col; i++) {
                            col=this.Mangustos[i].CheckCollision(p, 32);
                        }
                    }
                } while(col);
                this.Apples.Add(new Apple(p.X, p.Y));
            }
            this.NextApple--;
            if(this.NextMangusto<=0) {
                this.NextMangusto=(int)(150/this.difficulty);
                bool col=false;
                Point p;
                do {
                    p=new Point(rnd.Next(this.Width-128)+64, rnd.Next(this.Height-128)+64);
                    col=python.CheckCollision(p, 64);
                    if(!col) {
                        for(int i=0; i<this.Apples.Count&&!col; i++) {
                            col=this.Apples[i].CheckCollision(p, 64);
                        }
                    }
                    if(!col) {
                        for(int i=0; i<this.Mangustos.Count&&!col; i++) {
                            col=this.Mangustos[i].CheckCollision(p, 64);
                        }
                    }
                } while(col);
                this.Mangustos.Add(new Mangusto(p.X, p.Y));
            }
            this.NextMangusto--;
            this.time++;
            if(python.CheckCollision(m, 16)&&time>100) {
                this.GameOver();
            } else if(m.X>this.Width-32||m.X<32||m.Y>this.Height-64||m.Y<32) {
                this.GameOver();
            }
            foreach(Mangusto p in this.Mangustos) {
                if(p.CheckCollision(m, 16)) {
                    this.GameOver();
                    break;
                }
                foreach(PythonSegment b in this.python.Body) {
                    if(b.CheckCollision(p)) {
                        this.GameOver();
                        break;
                    }
                }
            }
            foreach(Sprite s in this.Deletion) {
                if(s is Mangusto) {
                    this.Mangustos.Remove(s as Mangusto);
                } else if(s is Apple) {
                    this.Apples.Remove(s as Apple);
                }
            }
            this.DrawField();
        }
        private void Form1_Paint(object sender, PaintEventArgs e) {
            this.graphics=e.Graphics;
            if(mode==Mode.Start) {
                this.menuControl();
                this.drawTitle();
            } else if(mode==Mode.Game) {
                this.gameControl();
            } else if(mode==Mode.GameOver) {
                this.menuControl();
            } else if(mode==Mode.Options) {
                this.menuControl();
            }
        }
        public Point GetCusor() {
            return this.cursor;
        }
        public void RequestDeletion(Sprite m) {
            this.Deletion.Add(m);
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e) {
            if(this.mode==Mode.Game) {
                this.python.Accelerate();
            }
            this.click=true;
        }
        public bool GetClick() {
            return this.click;
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e) {
            if(this.mode==Mode.Game) {
                this.python.Decelerate();
            }
        }
        private bool stop=false;
        private void readXml() {
            bool rewrite=false;
            try {
                XmlReaderSettings xmls = new XmlReaderSettings();
                xmls.Schemas.Add(null, "Schema.xsd");
                xmls.ValidationType = ValidationType.Schema;
                xmls.ValidationEventHandler += new ValidationEventHandler(this.xmlHandler);
                xmls.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
                xmls.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
                xmls.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                XmlReader settings = XmlReader.Create("settings.xml", xmls);
                string currentElement="";
                string currentValue="";
                while(settings.Read()) {
                    if(settings.NodeType==XmlNodeType.Element) {
                        currentElement=settings.Name;
                    }
                    if(settings.NodeType==XmlNodeType.Text) {
                        currentValue=settings.Value;
                    }
                    if(settings.NodeType==XmlNodeType.EndElement) {
                        if(!this.stop) {
                            if(currentElement=="R") {
                                this.options.ColorR=Convert.ToInt32(currentValue);
                            } else if(currentElement=="G") {
                                this.options.ColorG=Convert.ToInt32(currentValue);
                            } else if(currentElement=="B") {
                                this.options.ColorB=Convert.ToInt32(currentValue);
                            } else if(currentElement=="Acceleration") {
                                this.options.Acceleration=Convert.ToDouble(currentValue.Replace('.', ','));
                            } else if(currentElement=="Speed") {
                                this.options.SpeedIncrease=Convert.ToDouble(currentValue.Replace('.', ','));
                            } else if(currentElement=="Spawn") {
                                this.options.SpawnIncrease=Convert.ToDouble(currentValue.Replace('.', ','));
                            } else if(currentElement=="Width") {
                                this.options.WindowWidth=Convert.ToInt32(currentValue);
                            } else if(currentElement=="Height") {
                                this.options.WindowHeight=Convert.ToInt32(currentValue);
                            }
                        } else {
                            rewrite=true;
                            if(currentElement=="R") {
                                this.options.ColorR=this.defaultSettings.ColorR;
                            } else if(currentElement=="G") {
                                this.options.ColorG=this.defaultSettings.ColorG;
                            } else if(currentElement=="B") {
                                this.options.ColorB=this.defaultSettings.ColorB;
                            } else if(currentElement=="Acceleration") {
                                this.options.Acceleration=this.defaultSettings.Acceleration;
                            } else if(currentElement=="Speed") {
                                this.options.SpeedIncrease=this.defaultSettings.SpeedIncrease;
                            } else if(currentElement=="Spawn") {
                                this.options.SpawnIncrease=this.defaultSettings.SpawnIncrease;
                            } else if(currentElement=="Width") {
                                this.options.WindowWidth=this.defaultSettings.WindowWidth;
                            } else if(currentElement=="Height") {
                                this.options.WindowHeight=this.defaultSettings.WindowHeight;
                            } else {
                                throw new Exception();
                            }
                            this.stop=false;
                        }
                        currentElement="";
                        currentValue="";
                    }
                    if(this.stop) {
                        throw new Exception();
                    }
                }
            } catch(Exception e) {
                this.options=this.defaultSettings;
                rewrite=true;
            }
            this.setOptions();
            if(rewrite) {
            }
        }
        private void xmlHandler(object sender, ValidationEventArgs e) {
            this.stop=true;
        }
    }
    public class Python {
        private double speed=3;
        private double defSpeed=3;
        private double segmentRange=28;
        private double appleSpeed=0.1;
        private double acceleration=5;
        public Point newPoint;
        private List<PointF> Path=new List<PointF>();
        private List<double> Range=new List<double>();
        public List<PythonSegment> Body=new List<PythonSegment>();
        public Python(double accel, double apple) {
            this.appleSpeed=apple;
            this.acceleration=accel;
            this.Body.Add(new PythonHead(64, 64, this));
            this.Body.Add(new PythonSegment(64, 64, this));
            this.Body.Add(new PythonSegment(64, 64, this));
            this.Path.Add(new PointF((float)this.Body[0].X, (float)this.Body[0].Y));
            this.Link();
        }
        public void Control() {
            Point cursor=Program.GlobalForm.GetCusor();
            this.Body[0].Speed=this.speed;
            this.Body[0].Control();
            this.Path.Add(this.newPoint);
            this.Range.Add(this.speed);
            for(int i=1; i<this.Body.Count; i++) {
                PythonSegment p=this.Body[i];
                //p.Speed=this.speed;
                p.Control();
            }
            double mdr=this.segmentRange;
            int seg=1;
            PointF pf=this.Path[0];
            double npr=0;
            for(int i=this.Range.Count-1; i>-1; i--) {
                if(seg==this.Body.Count) {
                    pf=this.Path[i];
                    this.Path.RemoveRange(0, i+1);
                    this.Range.RemoveRange(0, i+1);
                    this.Path.Insert(0, new PointF((float)this.Body[seg-1].X, (float)this.Body[seg-1].Y));
                    this.Range.Insert(0, npr);
                } else if(mdr<this.Range[i]) {
                    double d = mdr/this.Range[i];
                    double mdx=d*(this.Path[i].X-this.Path[i+1].X);
                    double mdy=d*(this.Path[i].Y-this.Path[i+1].Y);
                    double x=this.Path[i+1].X+mdx;
                    double y=this.Path[i+1].Y+mdy;
                    this.Body[seg].Place(x, y);
                    seg++;
                    npr=mdr=this.segmentRange+Math.Sqrt(mdx*mdx+mdy*mdy);
                    i++;
                } else {
                    mdr-=this.Range[i];
                }
            }
            for(int i=seg; i<this.Body.Count; i++) {
                this.Body[i].Place(pf.X, pf.Y);
            }
        }
        public void Link() {
            for(int i=this.Body.Count-1; i>0; i--) {
                this.Body[i].Target=this.Body[i-1];
            }
        }
        public Point GetMouth() {
            return (this.Body[0] as PythonHead).GetMouth();
        }
        public bool CheckCollision(Point c, double r) {
            foreach(PythonSegment p in this.Body) {
                if(!(p is PythonHead)) {
                    if(p.CheckCollision(c, r)) {
                        return true;
                    }
                }
            }
            return false;
        }
        public void EatApple() {
            this.Body[this.Body.Count-1].NewSegment();
            this.speed+=this.appleSpeed;
            this.defSpeed+=this.appleSpeed;
        }
        public void Accelerate() {
            this.speed=this.defSpeed+this.acceleration;
        }
        public void Decelerate() {
            this.speed=this.defSpeed;
        }
    }
    public class PythonSegment:Sprite {
        protected Python parent;
        protected PythonSegment target;
        protected double maxRange=30;
        protected double maxRot=90;
        private Queue<Point> Path=new Queue<Point>();
        private Queue<double> Range=new Queue<double>();
        public PythonSegment Target {
            set {
                this.target=value;
            }
        }
        public void AddPoint(Point p, double s) {
            this.Path.Enqueue(p);
            this.Range.Enqueue(s);
        }
        public PythonSegment(double x, double y, Python parent) {
            this.collisionRange=16;
            this.parent=parent;
            this.x=x;
            this.y=y;
        }
        public PythonSegment NewSegment() {
            PythonSegment ps=new PythonSegment(this.x, this.y, this.parent);
            ps.NewQueue(new Queue<Point>(this.Path), new Queue<double>(this.Range));
            this.parent.Body.Add(ps);
            this.parent.Link();
            return ps;
        }
        public void NewQueue(Queue<Point> path, Queue<double> range) {
            this.Path=path;
            this.Range=range;
        }
        public virtual void Control() {
            /*this.Path.Enqueue(this.parent.newPoint);
            this.Range.Enqueue(this.speed);
            double spd=this.speed;
            Point prev=this.Path.Peek();
            while(spd>0) {
                Point dir=this.Path.Peek();
                double mv=this.Range.Peek();
                double rng=this.GetRange(this.target.x, this.target.y);
                if(rng<this.maxRange) {
                    //this.x=prev.X;
                    //this.y=prev.Y;
                    spd=0;
                } else {
                    this.x=dir.X;
                    this.y=dir.Y;
                    this.Range.Dequeue();
                    prev=this.Path.Dequeue();
                }
            }*/
            this.visibleRotation=this.GetDirection(this.target.X, this.target.Y);
        }
        public override double DirectTo(double x, double y) {
            double rot=base.DirectTo(x, y);
            double dAngle=this.ConvertRotation(this.rotation-rot);
            if(dAngle>this.maxRot) {
                this.rotation-=maxRot;
            } else if(dAngle<-this.maxRot) {
                this.rotation+=this.maxRot;
            } else {
                this.rotation=rot;
            }
            return rot;
        }
        public override void DrawSprite(Graphics gr) {
            gr.FillEllipse(new SolidBrush(Color.LawnGreen), new Rectangle(-16, -16, 32, 32));
        }

    }
    public class PythonHead:PythonSegment {
        public PythonHead(double x, double y, Python parent)
            : base(x, y, parent) {
            this.maxRot=7;
        }
        public override void Control() {
            Point cursor=Program.GlobalForm.GetCusor();
            this.DirectTo(cursor.X, cursor.Y);
            this.MoveToDirection(this.speed);
            this.parent.newPoint=new Point((int)this.x, (int)this.y);
            this.visibleRotation=this.rotation;
        }
        public override void DrawSprite(Graphics gr) {
            gr.FillEllipse(new SolidBrush(Color.LawnGreen), new Rectangle(-16, -16, 48, 32));
            gr.FillEllipse(new SolidBrush(Color.Black), new Rectangle(12, -12, 8, 8));
            gr.FillEllipse(new SolidBrush(Color.Black), new Rectangle(12, 4, 8, 8));
        }
        public Point GetMouth() {
            return new Point((int)(this.x+16*Math.Cos(this.rotation*Math.PI/180)), (int)(this.y+16*Math.Sin(this.rotation*Math.PI/180)));
        }
    }
    public class Apple:Sprite {
        public override void DrawSprite(Graphics gr) {
            gr.FillEllipse(new SolidBrush(Color.Yellow), new Rectangle(-12, -12, 24, 24));
            gr.DrawLine(new Pen(Color.Black, 3), 5, -15, 0, -8);
        }
        public Apple(double x, double y) {
            this.x=x;
            this.y=y;
            this.collisionRange=13;
        }
    }
    public abstract class Sprite {
        protected double x, y, rotation, speed, xscale=1, yscale=1;
        protected double visibleRotation;
        protected double collisionRange=0;
        public bool CheckCollision(Point p, double r) {
            if(this.GetRange(p.X, p.Y)<r+this.collisionRange) {
                return true;
            }
            return false;
        }
        public bool CheckCollision(double x, double y, double r) {
            if(this.GetRange(x, y)<r+this.collisionRange) {
                return true;
            }
            return false;
        }
        public bool CheckCollision(Sprite s) {
            return s.CheckCollision(this.x, this.y, this.collisionRange);
        }
        public double X {
            get {
                return this.x;
            }
        }
        public double Y {
            get {
                return this.y;
            }
        }
        public double Speed {
            set {
                this.speed=value;
            }
        }
        public void Draw(Graphics gr) {
            GraphicsContainer restore=gr.BeginContainer();
            gr.TranslateTransform((float)this.x, (float)this.y);
            gr.ScaleTransform((float)this.xscale+0.001f, (float)this.yscale+0.001f);
            gr.RotateTransform((float)this.visibleRotation);
            this.DrawSprite(gr);
            gr.EndContainer(restore);
        }
        public abstract void DrawSprite(Graphics gr);
        public void MoveToDirection(double speed) {
            this.x+=speed*Math.Cos(this.rotation*Math.PI/180);
            this.y+=speed*Math.Sin(this.rotation*Math.PI/180);
        }
        public virtual double DirectTo(double x, double y) {
            return this.GetDirection(x, y);
        }
        public double GetRange(double x, double y) {
            return Math.Sqrt((x-this.x)*(x-this.x)+(y-this.y)*(y-this.y));
        }
        public double GetDirection(double x, double y) {
            double mDx=x-this.x;
            double mDy=y-this.y;
            double rot=Math.Atan2(mDy, mDx)*180/Math.PI;
            return rot;
        }
        public double ConvertRotation(double rot) {
            if(rot<0) {
                rot=rot+360*(int)(Math.Abs(rot)/360+1);
            } else {
                rot=rot-360*(int)(Math.Abs(rot)/360);
            }
            if(rot>180) {
                rot-=360;
            }
            return rot;
        }
        public void Place(double x, double y) {
            this.x=x;
            this.y=y;
        }
    }
    public class Mangusto:Sprite {
        private int timer=500;
        private Color danger=ColorTranslator.FromHtml("#7fff0000");
        private Color body=ColorTranslator.FromHtml("#ff000000");
        private Color currentColor;
        public Mangusto(double x, double y) {
            this.x=x;
            this.y=y;
            this.xscale=1;
            this.yscale=1;
            this.collisionRange=-1000;
            this.currentColor=this.danger;
        }
        public override void DrawSprite(Graphics gr) {
            gr.FillEllipse(new SolidBrush(this.currentColor), new Rectangle(-18, -18, 36, 36));
            // gr.DrawString("M", new Font(FontFamily.GenericMonospace, 16, FontStyle.Bold), new SolidBrush(Color.Yellow), new PointF(-14, -14));

        }
        public void Control(Form1 f) {
            timer--;
            if(timer<=0) {
                f.RequestDeletion(this);
            } else if(this.timer<25) {
                this.xscale-=0.04;
                this.yscale-=0.04;
                this.collisionRange-=18.0/25;
            } else if(this.timer>275&&timer<300) {
                this.xscale+=0.04;
                this.yscale+=0.04;
                this.collisionRange+=18.0/25;
            } else if(this.timer==300) {
                this.currentColor=this.body;
                this.xscale=0;
                this.yscale=0;
                this.collisionRange=0;
            } else if(this.timer>300) {
                this.currentColor=Color.FromArgb((int)(127.5*(Math.Cos((double)(this.timer-500)/10)+1)), 255, 0, 0);
            }
        }
    }
    public abstract class GUIElement:Sprite {
        public abstract void Control(Form1 f);
        public void VoidAction() {
        }
    }
    public class GUIButton:GUIElement {
        private int state=0;
        private int width, height;
        private string text;
        public delegate void ButtonAction();
        private ButtonAction action;
        public GUIButton(int x, int y, int width, int height, string text) {
            this.x=x;
            this.y=y;
            this.width=width;
            this.height=height;
            this.text=text;
            this.AddAction(this.VoidAction);
        }
        public override void Control(Form1 f) {
            Point mouse=f.GetCusor();
            bool click=f.GetClick();
            if(mouse.X>this.x&&mouse.X<this.x+this.width) {
                if(mouse.Y>this.y&&mouse.Y<this.y+this.height) {
                    if(click) {
                        this.state=2;
                    } else if(this.state==2) {
                        this.action();
                        this.state=1;
                    } else {
                        this.state=1;
                    }
                } else {
                    this.state=0;
                }
            } else {
                this.state=0;
            }
            this.Draw(f.GetGraphics());
        }
        public override void DrawSprite(Graphics gr) {
            if(this.state==0) {
                gr.FillRectangle(new SolidBrush(Color.DarkGreen), 0, 0, this.width, this.height);
                gr.DrawRectangle(new Pen(Color.Yellow, 2), 0, 0, this.width, this.height);
            } else if(this.state==1) {
                gr.FillRectangle(new SolidBrush(Color.LightGreen), 0, 0, this.width, this.height);
                gr.DrawRectangle(new Pen(Color.Yellow, 2), 0, 0, this.width, this.height);
            } else {
                gr.FillRectangle(new SolidBrush(Color.Red), 0, 0, this.width, this.height);
                gr.DrawRectangle(new Pen(Color.Yellow, 2), 0, 0, this.width, this.height);
            }
            gr.DrawString(this.text, new Font(FontFamily.GenericMonospace, (int)(this.height*0.6)), new SolidBrush(Color.Yellow), new PointF(0, 0));

        }
        public void AddAction(ButtonAction a) {
            this.action+=a;
        }
    }
    public class GUITitle:GUIElement {
        private int height;
        private string text;
        public GUITitle(int x, int y, int height, string text) {
            this.x=x;
            this.y=y;
            this.height=height;
            this.text=text;
        }
        public override void Control(Form1 f) {
            this.Draw(f.GetGraphics());
        }
        public override void DrawSprite(Graphics gr) {
            gr.DrawString(this.text, new Font(FontFamily.GenericMonospace, (int)(this.height*0.6)), new SolidBrush(Color.Yellow), new PointF(0, 0));
        }
        public string Text {
            set {
                this.text=value;
            }
        }
    }
    public class GUINumeric:GUIElement {
        private int width, height;
        private GUIButton leftButton, rightButton;
        private GUITitle title;
        private double value, minValue, maxValue, step;
        public double Value {
            get {
                return this.value;
            }
            set {
                this.value=value;
            }
        }
        private void increaseValue() {
            this.value+=this.step;
            if(this.value>this.maxValue) {
                this.value=this.maxValue;
            }
        }
        private void decreaseValue() {
            this.value-=this.step;
            if(this.value<this.minValue) {
                this.value=this.minValue;
            }
        }
        public GUINumeric(int x, int y, int width, int height, double minValue, double maxValue, double step) {
            this.x=x;
            this.y=y;
            this.width=width;
            this.height=height;
            this.minValue=minValue;
            this.maxValue=maxValue;
            this.step=step;
            this.value=this.minValue;
            this.leftButton=new GUIButton((int)this.x, (int)this.y, this.height, this.height, "-");
            this.rightButton=new GUIButton((int)(this.x+this.width-this.height), (int)this.y, this.height, this.height, "+");
            this.title=new GUITitle((int)(this.x+this.height), (int)this.y, this.height, this.value.ToString());
            this.leftButton.AddAction(this.decreaseValue);
            this.rightButton.AddAction(this.increaseValue);
        }
        public override void Control(Form1 f) {
            this.rightButton.Control(f);
            this.leftButton.Control(f);
            this.title.Text=this.value.ToString();
            this.title.Control(f);
            this.Draw(f.GetGraphics());
        }
        public override void DrawSprite(Graphics gr) {
            gr.DrawRectangle(new Pen(Color.Yellow, 2), 0, 0, this.width, this.height);
        }
    }
    public class GUIStrip:GUIElement {
        private int width, height, state;
        private double value;
        public double Value {
            get {
                return this.value;
            }
            set {
                this.value=value;
            }
        }
        public GUIStrip(int x, int y, int width, int height) {
            this.x=x;
            this.y=y;
            this.width=width;
            this.height=height;
        }
        public override void Control(Form1 f) {
            Point mouse=f.GetCusor();
            bool click=f.GetClick();
            this.state=0;
            if(mouse.X>this.x&&mouse.X<this.x+this.width) {
                if(mouse.Y>this.y&&mouse.Y<this.y+this.height) {
                    if(click) {
                        this.value=(mouse.X-this.x)/this.width;
                    }
                    this.state=1;
                }
            }
            this.Draw(f.GetGraphics());
        }
        public override void DrawSprite(Graphics gr) {
            gr.FillRectangle(new SolidBrush(Color.DarkGreen), 0, 0, this.width, this.height);
            int ex=(int)((double)this.width*this.value);
            if(this.state==0) {
                gr.FillEllipse(new SolidBrush(Color.Green), ex-this.height/2, 0, this.height, this.height);
            } else if(this.state==1) {
                gr.FillEllipse(new SolidBrush(Color.LightGreen), ex-this.height/2, 0, this.height, this.height);
            }
            gr.DrawEllipse(new Pen(Color.Yellow, 2), ex-this.height/2, 0, this.height, this.height);
        }
    }
    public struct SettingsContainer {
        public int WindowWidth;
        public int WindowHeight;
        public int ColorR;
        public int ColorG;
        public int ColorB;
        public double Acceleration;
        public double SpawnIncrease;
        public double SpeedIncrease;
        public SettingsContainer(int ww, int wh, int cr, int cg, int cb, double a, double sw, double se) {
            this.WindowHeight=wh;
            this.WindowWidth=ww;
            this.ColorB=cb;
            this.ColorG=cg;
            this.ColorR=cr;
            this.Acceleration=a;
            this.SpawnIncrease=sw;
            this.SpeedIncrease=se;
        }
    }
}
