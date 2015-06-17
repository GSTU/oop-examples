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

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        Bitmap mbit;
        Graphics graph;
        static bool valid;
        float width_box;
        float height_box;
        int map_width = 20;
        int map_height = 20;
        Point player;
        string xml_path = @"config.xml";
        Point finish;
        int[,] map = new int[20, 20];
        int[,] save_map = new int[20, 20]{
            {0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {1,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,1,0,1,0,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
            {0,1,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0},
            {0,1,0,1,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0},
            {0,1,0,1,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,1,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0},
            {0,1,0,1,0,1,0,1,0,1,0,1,1,1,1,1,0,0,0,0},
            {0,1,0,1,0,1,0,1,0,1,0,0,0,0,0,1,0,0,0,0},
            {0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,0,0,0,0},
            {0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,0,0,0,0,0},
            {0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,0,0,0,0,0},
            {0,1,0,1,0,1,0,1,0,0,0,1,0,1,0,0,0,0,0,0},
            {0,1,0,1,0,1,0,1,0,1,1,1,0,1,0,0,0,0,0,0},
            {0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,0,0,0,0,0},
            {0,1,0,1,0,0,1,1,1,1,1,0,1,1,1,1,1,1,1,0},
            {0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0}

            };


        public void inicialize()
        {
            map = save_map;
            valid = true;
            
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xml_path);
                XmlSchemaSet schemas = new XmlSchemaSet();
                doc.Schemas.Add("", "config.xsd");
                ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);
                doc.Validate(eventHandler);
                if (valid)
                {
                    map_width = Convert.ToInt32(doc.GetElementsByTagName("size").Item(0).ChildNodes.Item(0).InnerText);
                    map_height = Convert.ToInt32(doc.GetElementsByTagName("size").Item(0).ChildNodes.Item(1).InnerText);
                    map = new int[map_width, map_height];

                    for (int i = 0; i < map_width; i++)
                        for (int j = 0; j < map_height; j++)
                            map[i, j] = 0;

                    int x, y;

                    //insert player
                    x = Convert.ToInt32(doc.GetElementsByTagName("player").Item(0).ChildNodes.Item(0).InnerText);
                    y = Convert.ToInt32(doc.GetElementsByTagName("player").Item(0).ChildNodes.Item(1).InnerText);
                    if (x < map_width && y < map_height)
                    {
                        map[x, y] = 2;
                    }
                    else
                    {
                        throw new Exception("Неверная координата игрока");
                    }
                    player = new Point(x, y);


                    //insert finish
                    x = Convert.ToInt32(doc.GetElementsByTagName("finish").Item(0).ChildNodes.Item(0).InnerText);
                    y = Convert.ToInt32(doc.GetElementsByTagName("finish").Item(0).ChildNodes.Item(1).InnerText); 
                    if (x < map_width && y < map_height)
                    {
                        map[x, y] = 3;
                    }
                    else
                    {
                        throw new Exception("Неверная координата выхода");
                    }
                    finish = new Point(x, y);


                    for (int i = 0; i < doc.GetElementsByTagName("box").Count; i++)
                    {
                        x = Convert.ToInt32(doc.GetElementsByTagName("box").Item(i).ChildNodes.Item(0).InnerText);
                        y = Convert.ToInt32(doc.GetElementsByTagName("box").Item(i).ChildNodes.Item(1).InnerText);
                        if (x < map_width && y < map_height)
                        {
                            map[x, y] = 1;
                        }
                    }

                }
                else
                {
                    map_width = 10;
                    map_height = 10;
                    player = new Point(0, 0);
                    finish = new Point(8, 8);
                    map = new int[map_width, map_height];
                    for (int x = 2; x < 10; x++)
                    {
                        map[x, 0] = 1;
                        map[x, 9] = 1;
                        map[0, x] = 1;
                        map[9, x] = 1;
                    }
                        map[player.X, player.Y] = 2;
                    map[finish.X, finish.Y] = 3;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                map_width = 10;
                map_height = 10;
                player = new Point(0, 0);
                finish = new Point(8, 8);
                map = new int[map_width, map_height]; 
                for (int x = 2; x < 10; x++)
                {
                    map[x, 0] = 1;
                    map[x, 9] = 1;
                    map[0, x] = 1;
                    map[9, x] = 1;
                }
                map[player.X, player.Y] = 2;
                map[finish.X, finish.Y] = 3;
            }
            width_box = pictureBox1.Width / map_width;
            height_box = pictureBox1.Height / map_height;
        }


        public Form1()
        {
            InitializeComponent();



        }

        private void movePlayer(int x, int y)
        {
            if (player.X + x >= 0 && player.X + x < map_width)
            {
                if (player.Y + y >= 0 && player.Y + y < map_width)
                {
                    if (map[player.X + x, player.Y + y] != 1)
                    {
                        map[player.X + x, player.Y + y] = 2;
                        map[player.X, player.Y] = 0;
                        player.X += x;
                        player.Y += y;
                        Draw();
                        if (player == finish)
                        {
                            MessageBox.Show("you win");
                            Application.Exit();

                        }
                    }
                }
            }
        }

        private void Draw()
        {
            int i, j;
            for (j = 0; j < map_width; j++)
            {
                for (i = 0; i < map_height; i++)
                {
                    if (map[i, j] == 1)
                    {
                        graph.FillRectangle(new SolidBrush(Color.Red), i * width_box, j * height_box, width_box, height_box);
                    }
                    else if (map[i, j] == 2)
                    {
                        graph.FillRectangle(new SolidBrush(Color.Blue), i * width_box, j * height_box, width_box, height_box);
                    }
                    else if (map[i, j] == 3)
                    {
                        graph.FillRectangle(new SolidBrush(Color.Green), i * width_box, j * height_box, width_box, height_box);
                    }
                    else
                    {
                        graph.FillRectangle(new SolidBrush(Color.White), i * width_box, j * height_box, width_box, height_box);
                    }
                }
            }
            pictureBox1.Image = mbit;
        }



        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {

                movePlayer(0, -1);
            }
            if (e.KeyCode == Keys.Down)
            {
                movePlayer(0, 1);
            }
            if (e.KeyCode == Keys.Right)
            {
                movePlayer(1, 0);
            }
            if (e.KeyCode == Keys.Left)
            {
                movePlayer(-1, 0);

            }
        }

        private void pictureBox1_MouseDown_1(object sender, MouseEventArgs e)
        {
            Draw();
            pictureBox1.Image = mbit;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mbit = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graph = Graphics.FromImage(mbit);
            inicialize();
            Draw();
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            Form1.valid = false;
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    MessageBox.Show(e.Message);
                    break;
                case XmlSeverityType.Warning:
                    MessageBox.Show(e.Message);
                    break;
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void changeXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                xml_path = openFileDialog1.FileName;
                inicialize();
                Draw();
            }
        }
    }

}
