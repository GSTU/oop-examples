using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace tennis
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadXML();
            this.KeyPreview = true;
        }

        private void LoadXML()
        {
            try
            {
                XmlReaderSettings XMLSettings = new XmlReaderSettings();
                XMLSettings.Schemas.Add(null, "XMLFile1.xsd");
                XMLSettings.ValidationType = ValidationType.Schema;
                XMLSettings.ValidationEventHandler += new ValidationEventHandler(ConfigCheckValidationEventHandler);
                XmlReader XMLReader = XmlReader.Create("XMLFile1.xml", XMLSettings);
                CheckXML = true;

                while (XMLReader.Read()) { }

                if (CheckXML == true)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("XMLFile1.xml");

                    pb_Player.BackColor = Color.FromName(doc["root"]["color"]["pb_Player"].InnerText);
                    pb_Enemy.BackColor = Color.FromName(doc["root"]["color"]["pb_Enemy"].InnerText);
                    pb_Ball.BackColor = Color.FromName(doc["root"]["color"]["pb_Ball"].InnerText);
                    WorldFrame.BackColor = Color.FromName(doc["root"]["color"]["WorldFrame"].InnerText);

                    BallSpeed = int.Parse(doc["root"]["BallSpeed"].InnerText);
                    timer_Moveball.Interval = int.Parse(doc["root"]["timer_Moveball"].InnerText);
                    timer_Enemy.Interval = int.Parse(doc["root"]["timer_Enemy"].InnerText);
                    Speed_Enemy = int.Parse(doc["root"]["Speed_Enemy"].InnerText);
                    Speed_Player = int.Parse(doc["root"]["Speed_Player"].InnerText);
                }
                else
                {
                    ApplySettings();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка: " + Ex.Message + "\nБудут применены параметры по умолчанию!");
                ApplySettings();
            }
        }

        private void ConfigCheckValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                CheckXML = false; MessageBox.Show("XML файл повреждён!");
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                CheckXML = false; MessageBox.Show("Ошибка чтения данных из файла! Загружены стандартные параметры.");
            }
        }

        PictureBox[] Score_Player = new PictureBox[5]; 
        PictureBox[] Score_Enemy = new PictureBox[5];
        Color ScoreColor = Color.Silver;                
        Random rng = new Random();                      
        Boolean Player_Up, Player_Down = false;         
        Boolean BallGoingLeft = true;                  
        Boolean GameOn = false;                       

        int Speed_Player;                          
        int Speed_Enemy;
        int BallSpeed;
        int BallForce;
        int Round = 0;

        private bool CheckXML;

        public Boolean Collision_Left(PictureBox obj)
        {
            if (obj.Location.X <= 0)    
            {
                return true;
            }
            return false;
        }

        public Boolean Collision_Right(PictureBox obj)
        {
            if (obj.Location.X + obj.Width >= WorldFrame.Width) 
            {
                return true;
            }
            return false;
        }

        public Boolean Collision_Up(PictureBox obj)
        {
            if (obj.Location.Y <= 0)    
            {
                return true;
            }
            return false;
        }

        public Boolean Collision_Down(PictureBox obj)
        {
            if (obj.Location.Y + obj.Height >= WorldFrame.Height)  
            {
                return true;
            }
            return false;
        }

        public Boolean Collision_Enemy(PictureBox tar)
        {
            PictureBox temp1 = new PictureBox();   
            temp1.Bounds = pb_Enemy.Bounds;        
            temp1.SetBounds(temp1.Location.X - 1, temp1.Location.Y, 1, 10);
            if (tar.Bounds.IntersectsWith(temp1.Bounds))    
            {                                               
                BallForce = 3;                           
                return true;
            }
            temp1.SetBounds(temp1.Location.X, temp1.Location.Y + 5, 1, 10);
            if (tar.Bounds.IntersectsWith(temp1.Bounds))    
            {
                BallForce = 2;                             
                return true;
            }
            temp1.SetBounds(temp1.Location.X, temp1.Location.Y + 10, 1, 10);
            if (tar.Bounds.IntersectsWith(temp1.Bounds))   
            {
                BallForce = 1;
                return true;
            }
            temp1.SetBounds(temp1.Location.X, temp1.Location.Y + 10, 1, 10);
            if (tar.Bounds.IntersectsWith(temp1.Bounds))
            {
                BallForce = 0;
                return true;
            }
            temp1.SetBounds(temp1.Location.X, temp1.Location.Y + 10, 1, 10);
            if (tar.Bounds.IntersectsWith(temp1.Bounds))
            {
                BallForce = -1;
                return true;
            }
            temp1.SetBounds(temp1.Location.X, temp1.Location.Y + 10, 1, 10);
            if (tar.Bounds.IntersectsWith(temp1.Bounds))
            {
                BallForce = -2;
                return true;
            }
            temp1.SetBounds(temp1.Location.X, temp1.Location.Y + 10, 1, 10);
            if (tar.Bounds.IntersectsWith(temp1.Bounds))
            {
                BallForce = -3;
                return true;
            }
            return false;
        }

        public Boolean Collision_Player(PictureBox tar)
        {
            if (tar.Bounds.IntersectsWith(pb_Player.Bounds))   
            {
                PictureBox temp1 = new PictureBox();
                temp1.Bounds = pb_Player.Bounds;
                temp1.SetBounds(temp1.Location.X + temp1.Width, temp1.Location.Y, 1, 10);
                if (tar.Bounds.IntersectsWith(temp1.Bounds))    
                {
                    BallForce = 3;
                    return true;
                }
                temp1.SetBounds(temp1.Location.X, temp1.Location.Y + 5, 1, 10);
                if (tar.Bounds.IntersectsWith(temp1.Bounds))
                {
                    BallForce = 2;
                    return true;
                }
                temp1.SetBounds(temp1.Location.X, temp1.Location.Y + 10, 1, 10);
                if (tar.Bounds.IntersectsWith(temp1.Bounds))
                {
                    BallForce = 1;
                    return true;
                }
                temp1.SetBounds(temp1.Location.X, temp1.Location.Y + 10, 1, 10);
                if (tar.Bounds.IntersectsWith(temp1.Bounds))
                {
                    BallForce = 0;
                    return true;
                }
                temp1.SetBounds(temp1.Location.X, temp1.Location.Y + 10, 1, 10);
                if (tar.Bounds.IntersectsWith(temp1.Bounds))
                {
                    BallForce = -1;
                    return true;
                }
                temp1.SetBounds(temp1.Location.X, temp1.Location.Y + 10, 1, 10);
                if (tar.Bounds.IntersectsWith(temp1.Bounds))
                {
                    BallForce = -2;
                    return true;
                }
                temp1.SetBounds(temp1.Location.X, temp1.Location.Y + 10, 1, 10);
                if (tar.Bounds.IntersectsWith(temp1.Bounds))
                {
                    BallForce = -3;
                    return true;
                }
            }
            return false;
        }

        public void PaintBox(int X, int Y, int W, int H, Color C)
        {
            PictureBox Temp = new PictureBox();
            Temp.BackColor = C;
            Temp.Size = new Size(W, H);
            Temp.Location = new Point(X, Y);
            WorldFrame.Controls.Add(Temp);
        }

        public void ApplySettings()
        {   
            pb_Player.BackColor = Color.Blue;
            pb_Enemy.BackColor = Color.Red;
            pb_Ball.BackColor = Color.Black;
            WorldFrame.BackColor = Color.White;
            BallSpeed = 3;
            timer_Moveball.Interval = 1;
            timer_Enemy.Interval = 1;
            Speed_Enemy = 1;
            Speed_Player = 1;
        }

        public int ReverseInt(int x, Boolean Force = false, Boolean Negative = false)
        {
            if (Force)  
            {
                if (Negative)   
                {
                    if (x > 0)  
                    {
                        x = ~x + 1; 
                    }
                }
                else
                {   
                    x = x - (x * 2);
                }
            }
            else
            {
                if (x > 0)
                {
                    x = x - (x * 2);
                }
                else
                {
                    x = ~x + 1;
                }
            }
            return x;
        }

        public void RandomStart(Boolean x)
        {
            for (int i = 0; i < rng.Next(5, 10); i++)
            {   
                if (x)
                {
                    x = false;
                }
                else
                {
                    x = true;
                }
            }
        }

        private void timer_Moveball_Tick(object sender, EventArgs e)
        {
            if (GameOn)         
            {
                if (Player_Up && !Collision_Up(pb_Player))
                {               
                    pb_Player.Top -= Speed_Player;    
                }
                if (Player_Down && !Collision_Down(pb_Player))
                {               
                    pb_Player.Top += Speed_Player;
                }

                if (BallForce > 0)
                {   
                    pb_Ball.Top -= BallForce;
                }
                if (BallForce < 0)
                {   
                    pb_Ball.Top -= BallForce;
                }

                if (pb_Ball.Location.Y <= 1)
                {   
                    BallForce = ReverseInt(BallForce, true, true);
                }
                if (pb_Ball.Location.Y + pb_Ball.Height >= WorldFrame.Height - 1)
                {
                    BallForce = ReverseInt(BallForce, true, false);
                }

                if (BallGoingLeft)  
                {
                    if (Collision_Left(pb_Ball))    
                    {
                        AddScore(Score_Player);     
                        pb_Ball.Location = new Point(206, 67);
                        RandomStart(BallGoingLeft);
                        BallForce = 0;
                    }
                    if (!Collision_Player(pb_Ball)) 
                    {                               
                        pb_Ball.Left -= BallSpeed;
                    }
                    else
                    {                               
                        BallGoingLeft = false;
                    }
                }
                else
                {
                    if (Collision_Right(pb_Ball))  
                    {
                        AddScore(Score_Enemy);
                        pb_Ball.Location = new Point(206, 67);
                        RandomStart(BallGoingLeft);
                        BallForce = 0;
                    }
                    if (!Collision_Enemy(pb_Ball))
                    {
                        pb_Ball.Left += BallSpeed;
                    }
                    else
                    {
                        BallGoingLeft = true;
                    }
                }
            }
        }

        public void CircleThis(PictureBox pic) 
        {
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pic.Width - 3, pic.Height - 3);
            Region rg = new Region(gp);
            pic.Region = rg;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)      
            {
                case Keys.W:
                case Keys.Up:
                    Player_Down = false;
                    Player_Up = true;
                    break;
                case Keys.S:
                case Keys.Down:
                    Player_Up = false;
                    Player_Down = true;
                    break;
                case Keys.Space:   
                    GameOn = true;
                    RandomStart(BallGoingLeft);
                    label_Start.Visible = false;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    Player_Up = false;
                    break;
                case Keys.S:
                case Keys.Down:
                    Player_Down = false;
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                Score_Player[i] = PicID(i + 1);         
                Score_Enemy[i] = PicID(i + 1, true);
            }
            CircleThis(pb_Ball);        
            pb_Ball.Location = new Point(208, rng.Next(10, 190));   
            RandomStart(BallGoingLeft); 
        }

        public void AddScore(PictureBox[] Arr)
        {
            for (int i = 0; i < Arr.Length; i++)
            {   
                if (Arr[i].BackColor == ScoreColor)
                {   
                    Arr[i].BackColor = Color.Black;
                    break;
                }
            }

            if (Arr[4].BackColor == Color.Black)
            {   
                GameOn = false;
                label_Start.Visible = true;
                RestoreScore();
                pb_Ball.Location = new Point(208, rng.Next(10, 190));
                pb_Player.Location = new Point(3, 67);
                pb_Enemy.Location = new Point(409, 67);
                Round = 0;
                label_Time.Visible = false;
            }
        }

        public void RestoreScore()
        {
            for (int i = 0; i <= 5; i++)
            {   
                PicID(i).BackColor = ScoreColor;
                PicID(i, true).BackColor = ScoreColor;
            }
        }

        public PictureBox PicID(int i, Boolean Enemy = false)
        {
            if (Enemy)
            {   
                switch (i)
                {
                    case 1:
                        return enemy_1;
                    case 2:
                        return enemy_2;
                    case 3:
                        return enemy_3;
                    case 4:
                        return enemy_4;
                    case 5:
                        return enemy_5;
                }
            }
            else
            {
                switch (i)
                {
                    case 1:
                        return player_1;
                    case 2:
                        return player_2;
                    case 3:
                        return player_3;
                    case 4:
                        return player_4;
                    case 5:
                        return player_5;
                }
            }
            return pb_Ball;
        }

        private void timer_Enemy_Tick(object sender, EventArgs e)
        {
            if (GameOn) 
            {   
                if (pb_Enemy.Location.Y + 28 < pb_Ball.Location.Y)
                {   
                    pb_Enemy.Top += Speed_Enemy;
                }
                else
                {
                    pb_Enemy.Top -= Speed_Enemy;
                }
            }
        }


        private void timer_Sec_Tick(object sender, EventArgs e)
        {
            if (GameOn)
            {
                Round++;
                label_Time.Visible = true;

                TimeSpan time = TimeSpan.FromSeconds(Round);

                string str = time.ToString(@"mm\:ss");
                label_Time.Text = "Time: " + str;
            }
        }
    }
}
