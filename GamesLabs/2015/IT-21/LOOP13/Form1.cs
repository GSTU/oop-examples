using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace LOOP13
{
    public partial class Form1 : Form
    {
        private Aquarium a;
        private PointF   p = new Point();
        private Random   r = new Random();

        private float bX, bY;
        private bool  flag = true;

        public Form1()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            Config config;

            try
            {
                if ((System.IO.File.Exists("xsd.xsd")))
                {
                    XmlReaderSettings xmlreadersettings = new XmlReaderSettings();

                    xmlreadersettings.Schemas.Add(null, "xsd.xsd");

                    xmlreadersettings.ValidationType = ValidationType.Schema;
                    xmlreadersettings.ValidationEventHandler += new ValidationEventHandler(ValidateXML);

                    XmlReader xmlreader = XmlReader.Create("Settings.xml", xmlreadersettings);
                    while (xmlreader.Read()) { }
                }
                else
                    throw new System.IO.FileNotFoundException();
            

                if (flag)
                    config = new XMLWithDOM("Settings.xml").GetConfig();
                else
                    throw new Exception();
            }
            catch
            {
                config = new Config(new FishConfig(3, 80),
                                    new FishConfig(1, 100));
            }

            a = new Aquarium(45, config);

            t.Interval = 10;
            t.Start();
        }
        private void ValidateXML(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                MessageBox.Show("Warning: " + e.Message);

                flag = false;
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                MessageBox.Show("Error: " + e.Message);

                flag = false;
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            e.Graphics.FillRectangle(a.water, 0, 0, this.Width, this.Height);

            for (int i = 0; i < a.carps.Count; ++i)
                Draw(e.Graphics, a.carps[i], 0);

            Draw(e.Graphics, a.pike, 1);
        }
        private void t_Tick(object sender, EventArgs e)
        {
            Logic();

            this.Refresh();
        }

        private void Draw(Graphics g, object obj, uint type)
        {
            Aquarium.Fish fish = type == 0 ? fish = (Aquarium.Carp)obj : fish = (Aquarium.Pike)obj;

            g.FillEllipse(fish.FishColor, fish.Location.X - 10, fish.Location.Y - 10, 20, 20);

            if ((fish.DirectionAngle >= 90) && (fish.DirectionAngle <= 270))
                g.FillRectangle(fish.FishColor, fish.Location.X, fish.Location.Y - 5, 20, 10);
            else
                g.FillRectangle(fish.FishColor, fish.Location.X - 20, fish.Location.Y - 5, 20, 10);
        }
        private void Logic()
        {
            for (int i = 0; i < a.carps.Count; ++i)
                Move(a.carps[i], 0);
            Move(a.pike, 1);

            ChangeDirection();

            FindCarp();

            if (a.carps.Count == 0)
                t.Stop();
        }
        private void Reflect(object obj, uint type)
        {
            Aquarium.Fish fish = type == 0 ? fish = (Aquarium.Carp)obj : fish = (Aquarium.Pike)obj;

            if (bX - 10 <= 0)
            {
                if (fish.DirectionAngle <= 180)
                    fish.DirectionAngle = 180 - fish.DirectionAngle;
                else
                    fish.DirectionAngle = 540 - fish.DirectionAngle;
            }
            else if (bX + 10 >= 984)
            {
                if ((fish.DirectionAngle >= 0) && (fish.DirectionAngle < 90))
                    fish.DirectionAngle = 180 - fish.DirectionAngle;
                else if ((fish.DirectionAngle < 360) && (fish.DirectionAngle > 270))
                    fish.DirectionAngle = 540 - fish.DirectionAngle;
            }
            if ((bY - 10 <= 0) || (bY + 10 >= 561))
                fish.DirectionAngle = 360 - fish.DirectionAngle;
        }
        private void Move(object obj, uint type)
        {
            Aquarium.Fish fish = type == 0 ? fish = (Aquarium.Carp)obj : fish = (Aquarium.Pike)obj;

            bX = fish.Location.X;
            bY = fish.Location.Y;

            Reflect(fish, type);

            p.X = bX + fish.Speed * (float)Math.Cos(fish.DirectionAngle * Math.PI / 180);
            p.Y = bY - fish.Speed * (float)Math.Sin(fish.DirectionAngle * Math.PI / 180);

            fish.Location = p;
        }
        private void ChangeDirection()
        {
            for (int i = 0; i < a.carps.Count; ++i)
                if (r.Next(0, 1001) == 1000)
                    a.carps[i].DirectionAngle = r.Next(0, 361);
        }
        private void FindCarp()
        {
            double pikeAngle;

            List<double> distances = new List<double>();

            for (int i = 0; i < a.carps.Count; ++i)
            {
                distances.Add(Distance(a.carps[i], a.pike));

                if (distances[i] <= 20)
                {
                    a.carps.RemoveAt(i);

                    break;
                }

                if (distances[i] <= a.carps[i].Vision)
                {
                    pikeAngle = Math.Atan2(a.pike.Location.Y - a.carps[i].Location.Y, a.pike.Location.X - a.carps[i].Location.X) / Math.PI * 180;
                    pikeAngle = 180 - pikeAngle;

                    a.carps[i].DirectionAngle = pikeAngle;

                    if (distances[i] <= a.pike.Vision)
                    {
                        a.pike.DirectionAngle = pikeAngle;
                        a.carps[i].FishColor  = new SolidBrush(Color.Red);
                    }
                    else
                        a.carps[i].FishColor = new SolidBrush(Color.Pink);
                }
            }
        }
        private double Distance(Aquarium.Carp c, Aquarium.Pike p)
        {
            return Math.Sqrt(Math.Pow((double)p.Location.X - (double)c.Location.X, 2) + Math.Pow((double)p.Location.Y - (double)c.Location.Y, 2));
        }
    }

    public class Aquarium
    {
        public  Pike       pike;
        public  List<Carp> carps = new List<Carp>();

        private PointF     p = new Point();
        private Random     r = new Random();
        public  SolidBrush water = new SolidBrush(Color.FromArgb(65, 65, 255));

        public Aquarium(uint carpsCount, Config cfg)
        {
            this.pike = new Pike(this, cfg);

            p.X = r.Next(10, 984 - 10 + 1);
            p.Y = r.Next(10, 561 - 10 + 1);

            this.pike.Location = p;
            this.pike.DirectionAngle = r.Next(0, 361);

            for (uint i = 0; i < carpsCount; ++i)
            {
                this.carps.Add(new Carp(this, cfg));

                p.X = r.Next(10, 984 - 10 + 1);
                p.Y = r.Next(10, 561 - 10 + 1);

                this.carps[(int)i].Location = p;
                this.carps[(int)i].DirectionAngle = r.Next(0, 361);
            }
        }

        public abstract class Fish
        {
            public Aquarium parent;

            public float      Speed          { get; set; }
            public int        Vision         { get; set; }
            public double     DirectionAngle { get; set; }
            public PointF     Location       { get; set; }
            public SolidBrush FishColor      { get; set; }

            public Fish(Aquarium parent)
            {
                this.parent = parent;
            }
        }
        public class Carp : Fish
        {
            public Carp(Aquarium parent, Config cfg) : base(parent)
            {
                Speed     = cfg.CarpConfig.Speed;
                Vision    = cfg.CarpConfig.Vision;

                FishColor = new SolidBrush(Color.Pink);
            }
        }
        public class Pike : Fish
        {
            public Pike(Aquarium parent, Config cfg) : base(parent)
            {
                Speed     = cfg.PikeConfig.Speed;
                Vision    = cfg.PikeConfig.Vision;

                FishColor = new SolidBrush(Color.Black);
            }
        }
    }

    public class XMLWithDOM
    {
        private XmlDocument document;

        public XMLWithDOM(String pathToXMLFile)
        {
            try
            {
                if (!(System.IO.File.Exists(pathToXMLFile)))
                    throw new System.IO.FileNotFoundException();

                document = new XmlDocument();

                using (XmlReader reader = XmlReader.Create(pathToXMLFile))
                {
                    document.Load(reader);
                }
            }
            catch { throw; }
        }

        public Config GetConfig()
        {
            XmlElement root = document["config"];

            return new Config(new FishConfig(float.Parse(root["pike"].FirstChild.InnerText), int.Parse(root["pike"].LastChild.InnerText)), 
                              new FishConfig(float.Parse(root["carp"].FirstChild.InnerText), int.Parse(root["carp"].LastChild.InnerText)));
        }
    }
    public class FishConfig
    {
        public float Speed;
        public int   Vision;

        public FishConfig(float speed, int vision)
        {
            this.Speed  = speed;
            this.Vision = vision;
        }
    }
    public class Config
    {
        public FishConfig PikeConfig;
        public FishConfig CarpConfig;

        public Config(FishConfig pike, FishConfig carp)
        {
            PikeConfig = pike;
            CarpConfig = carp;
        }

        public override string ToString()
        {
            return String.Format("{0}:{1}\n{2}:{3}", PikeConfig.Speed, PikeConfig.Vision, CarpConfig.Speed, CarpConfig.Vision);
        }
    }
}