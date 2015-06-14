using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Linq;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace MindPuzzle
{
    
    public partial class MindPuzzle : Form
    {
        private int CheckMin = 0;
        private bool isValid;
        private List<string> ForTimer = new List<string>(); // игровое врямя!!!!!!!!
        private int ROW_COUNT = 4;
        private int COLUMN_COUNT = 4;
        private Thread timerThread = null;
        Random r;
        private Label[] label = new Label[16];// массив ячеек
        private int[,] gridVal, checkVal;
        ArrayList genArr;
        int Num, num;
        static int x, y; // для 0 val 
        static int z; // для label[i]. 
        static int min = 0;
        static int keyCount = 0;
        string gameStr;

        // 0   1   2   3

        //0    // 0   1   2   3
        //1    // 4   5   6   7
        //2    // 8   9   10  11
        //3    // 12  13  14  15

        
        public MindPuzzle()
        {
            LoadXML();
            genArr = new ArrayList(16);
            r = new Random();
            InitializeComponent();
            Intialize();

            T1.Text = ForTimer[0] + " минут";
            T2.Text = ForTimer[1] + " минут";
            T3.Text = ForTimer[2] + " минут";
        }

        public void LoadXML()
        {
            try
            {
                XmlReaderSettings XMLSettings = new XmlReaderSettings();
                XMLSettings.Schemas.Add(null, "XMLFile1.xsd");
                XMLSettings.ValidationType = ValidationType.Schema;
                XMLSettings.ValidationEventHandler += new ValidationEventHandler(ConfigCheckValidationEventHandler);
                XmlReader XMLReader = XmlReader.Create("XMLFile1.xml", XMLSettings);
                isValid = true;

                while (XMLReader.Read()) { }

                if (isValid == true)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("XMLFile1.xml");

                    XmlNodeList timers = doc["root"]["config"].ChildNodes;

                    foreach (XmlNode node in timers)
                    {
                        ForTimer.Add(node["min"].InnerText);
                    }
                    this.BackColor = Color.FromName(doc["root"]["labcolor"].InnerText);
                }
                else
                {
                    for (int i = 1; i <= 3; i++)
                    {
                        int res = i * 10;
                        ForTimer.Add(res.ToString());
                    }
                    this.BackColor = Color.Red;
                }
            
            }
            catch (Exception Ex)
            {
                for (int i = 1; i <= 3; i++)
                {
                    int res = i * 10;
                    ForTimer.Add(res.ToString());
                }
                this.BackColor = Color.Red;
                MessageBox.Show("Ошибка: " + Ex.Message);
            }
        }
        private void ConfigCheckValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                isValid = false; MessageBox.Show("XML файл повреждён! Загружены стандартные параметры.");
            }
        }


        private void MindPuzzle_Load(object sender, EventArgs e)
        {
            
        }

        private void StartGame()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            createlabels();
            timerThread = new Thread(new ThreadStart(timerFunc));
            timerThread.IsBackground = true;
            timerThread.Start();
        }

        private void Key_Down(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Left:
                    {
                        moveLeft();
                        keyCount++;
                    }
                    break;

                case Keys.Right:
                    {
                        moveRight();
                        keyCount++;
                    }
                    break;

                case Keys.Up:
                    {
                        moveUp();
                        keyCount++;
                    }
                    break;

                case Keys.Down:
                    {
                        moveDown();
                        keyCount++;
                    }
                    break;

                default:
                    System.Console.Beep();
                    break;
            }

            if (check())
            {
                timerThread.Abort();
                gameStr = string.Format("You took {0} moves to arrange numbers", keyCount);
                MessageBox.Show(gameStr, "Game Over!");
                Application.Exit();
            }

        }
        
        private void swap(int a, int b)
        {
            int swval;
            swval = gridVal[x, y];   // текущая пустая ячейка
            gridVal[x, y] = gridVal[a, b];
            gridVal[a, b] = swval;
        }
        private void moveDown()
        {
            if ((x - 1) < 0) return;

            this.label[z].BackColor = Color.SeaGreen;
            this.label[z].BorderStyle = BorderStyle.FixedSingle;
            this.label[z].Visible = true;
            this.label[z].Text = gridVal[x - 1, y].ToString();

            swap(x - 1, y);
            x = x - 1;
            z = z - 4;

            this.label[z].BackColor = Color.Transparent;
            this.label[z].BorderStyle = BorderStyle.None;
            this.label[z].Visible = false;
            this.label[z].Text = gridVal[x, y].ToString();

        }
        private void moveUp()
        {
            if ((x + 1) > 3) return;

            this.label[z].BackColor = Color.SeaGreen;
            this.label[z].BorderStyle = BorderStyle.FixedSingle;
            this.label[z].Visible = true;
            this.label[z].Text = gridVal[x + 1, y].ToString();

            swap(x + 1, y);
            x = x + 1;
            z = z + 4;

            this.label[z].BackColor = Color.Transparent;
            this.label[z].BorderStyle = BorderStyle.None;
            this.label[z].Visible = false;
            this.label[z].Text = gridVal[x, y].ToString();
        }
        private void moveRight()
        {
            if ((y - 1) < 0) return;

            this.label[z].BackColor = Color.SeaGreen;
            this.label[z].BorderStyle = BorderStyle.FixedSingle;
            this.label[z].Visible = true;
            this.label[z].Text = gridVal[x, y - 1].ToString();

            swap(x, y - 1);
            y = y - 1;
            z = z - 1;

            this.label[z].BackColor = Color.Transparent;
            this.label[z].BorderStyle = BorderStyle.None;
            this.label[z].Visible = false;
            this.label[z].Text = gridVal[x, y].ToString();
        }
        private void moveLeft()
        {
            if ((y + 1) > 3) return;

            this.label[z].BackColor = Color.SeaGreen;
            this.label[z].BorderStyle = BorderStyle.FixedSingle;
            this.label[z].Visible = true;
            this.label[z].Text = gridVal[x, y + 1].ToString();

            swap(x, y + 1);
            y = y + 1;
            z = z + 1;

            this.label[z].BackColor = Color.Transparent;
            this.label[z].BorderStyle = BorderStyle.None;
            this.label[z].Visible = false;
            this.label[z].Text = gridVal[x, y].ToString();
        }
        public void Intialize()
        {
            gridVal = new int[4, 4] {
                                        {0, 0, 0, 0},
                                        {0, 0, 0, 0},
                                        {0, 0, 0, 0},
                                        {0, 0, 0, 0},
                                     };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if ((i == 3) && (j == 3))
                    {
                        x = i;
                        y = j;
                    }
                    else
                    {
                        gridVal[i, j] = genRandomVal();
                    }

                }
            }

        }
    
        public int Random()
        {
            Num = r.Next(1, 16);
            for (int k = 0; k < genArr.Count; k++)
            {
                if (genArr[k].Equals(Num))
                {
                    Random();
                }
            }

            return Num;
        }
        public int genRandomVal()
        {
            num = Random();
            genArr.Add(num);
            return num;

        }
        public void createlabels() // стиль ячеек
        {
            int i = 0; int val;

            for (int j = 0; j < ROW_COUNT; j++)
                for (int k = 0; k < COLUMN_COUNT; k++)
                {

                    val = gridVal[j, k];
                    label[i] = new Label();
                    label[i].Click += myClick; // последние изменения


                    label[i].Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

                    label[i].Width = 60;
                    label[i].Height = 60;
                    label[i].Left = k * 60;
                    label[i].Top = j * 63;

                    label[i].Tag = 0;

                    label[i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    if (i != 15)
                    {
                        label[i].BackColor = Color.SeaGreen;
                        label[i].BorderStyle = BorderStyle.FixedSingle;
                        label[i].Text = val.ToString();
                    }
                    else
                    {
                        label[i].BackColor = Color.Transparent;
                        label[i].BorderStyle = BorderStyle.None;
                        label[i].Text = "";
                        label[i].Visible = false;
                       // label[i].Click+= label;//назначение функции каждому лейблу
                        z = i;
                    }

                    this.Controls.Add(label[i]);
                    i++;
                }
        }
        private void timerFunc() // игровое время
        {
            string str;
            for (min = 0; min < 10; min++)
            {
                for (int sec = 0; sec < 60; sec++)
                {
                    str = string.Format("Mind Puzzle             00:0{0}:{1} ", min, sec);
                    this.Text = str;
                    Thread.Sleep(1000);
                }
            }

            if (min == CheckMin)
            {
                this.Enabled = false;
                MessageBox.Show(" Better Luck next time ", "You Lose the Game!");
                Application.Exit();
            }
        }
        private bool check() // сверяем получившийся "результат" с правильным ответом
        {
            checkVal = new int[4, 4] {
                                        {1, 2, 3, 4},
                                        {5, 6, 7, 8},
                                        {9, 10, 11, 12},
                                        {13, 14, 15, 0},
                                     };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (gridVal[i, j] != checkVal[i, j])
                        return false;
                }
            }

            return true;
        }
       /* private void myClick (object sender, EventArgs e) // 
        {
            var lab = (Label )sender;
            MessageBox.Show(lab.Text);*/
            /*int myIndex = Array.IndexOf(gridVal, lab.Text);
            if (myIndex == x && myIndex < y)
                moveLeft();*/
        

      /*  private void MindPuzzle_MouseClick(object sender, MouseEventArgs e)
        {
         int swval;
            swval = gridVal[x, y];   // текущая пустая ячейка
            //for (int i = 0; i < 2; i++ )

                //  gridVal[x, y] = gridVal[ a, b];
                //  gridVal[a, b] = swval;

                // swap(int a,int b);
                //if ((x + 1) > 3) return;
                moveUp();
            //if ((x - 1) < 0) return;
             
                moveDown(); 
            
        //  if ((y + 1) > 3) return;
           moveLeft();
           // if ((y - 1) < 0) return;
            moveRight();


        }*/
        public Point findInGridVal(int val)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (gridVal[i, j] == val)
                    {
                        return new Point(i, j);
                    }
                }
            }
            return new Point(-1, -1);
        }

        private void myClick(object sender, EventArgs e)
        {
            var lab = (Label)sender;

            Point b = findInGridVal(0);
            Point a = findInGridVal(Convert.ToInt32(lab.Text));
            if (a.X + 1 == b.X && a.Y == b.Y)
            {
                moveDown();
                keyCount++;
            }
            if (a.X - 1 == b.X && a.Y == b.Y)
            {
                moveUp();
                keyCount++;
            }
            if (a.X == b.X && a.Y - 1 == b.Y)
            {
                moveLeft();
                keyCount++;
            }
            if (a.X == b.X && a.Y + 1 == b.Y)
            {
                moveRight();
                keyCount++;
            }

        }

        private void T1_Click(object sender, EventArgs e)
        {
            CloseMenu(); StartGame();
            CheckMin = int.Parse(ForTimer[0]);
        }

        private void T2_Click(object sender, EventArgs e)
        {
            CloseMenu(); StartGame();
            CheckMin = int.Parse(ForTimer[1]);
        }

        private void T3_Click(object sender, EventArgs e)
        {
            CloseMenu(); StartGame();
            CheckMin = int.Parse(ForTimer[2]);
        }

        private void CloseMenu()
        {
            Title.Visible = PanelMenu.Visible = T1.Visible = T2.Visible = T3.Visible = false;
        }

    }
}
