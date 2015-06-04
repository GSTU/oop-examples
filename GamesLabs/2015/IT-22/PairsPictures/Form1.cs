using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Xml.Schema;


namespace WindowsFormsApplication_lab_13
{

    public partial class Form1 : Form
    {

        bool valid = true;
        private const int nw = 4,//количество клеток
           nh = 4, // количество клеток 
            np = (int)(nw * nh / 2); //количество пар парных картинок
        //рабочая графическая поверхность
        //Graphics g ;
        System.Drawing.Graphics g;
        Color back;
        //картинки, загруженные из файла
        private Bitmap pics;
        // ширина и высота клетки
        private int cw, ch;
        private int r = 1;
        //игровое поле 
        private int[,] field = new int[nw, nh];
        private int[,] picture = new int[nw, nh];
        private int xscale=96, yscale=96;
        private List<int> freePicture = new List<int>();
        //количество открытых пар картинок
        private int n = 0;
        //количество открытых в данный момент клеток
        private int count = 0;
        //координаты 1-й открытой клетки
        private int[] open1 = new int[2];
        //координаты 2-1 открытой клетки
        private int[] open2 = new int[2];
        //таймер

        public Form1()
        {
            InitializeComponent();
            XmlTextReader reader = new XmlTextReader("XMLFile1.xml");
            XmlValidatingReader validreader = new XmlValidatingReader(reader);
            validreader.Schemas.Add(null, "XMLSchema1.xsd");
            validreader.ValidationType = ValidationType.Schema;
            validreader.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);
            try
            {
                while (validreader.Read()) ;
            }
            catch
            {
                valid = false;
                MessageBox.Show("Файл конфигурации не найден\nЗагружены параметры по умолчанию");
            }
            //validreader.Close();

            if (valid)
            {
                reader = new XmlTextReader("XMLFile1.xml");
                while (reader.Read())
                {
                    if (reader.Name == "range" && reader.NodeType != XmlNodeType.EndElement)
                    {
                        r = reader.ReadElementContentAsInt();
                    }
                    if (reader.Name == "color" && reader.NodeType != XmlNodeType.EndElement)
                    {
                        back = ColorTranslator.FromHtml(reader.ReadElementContentAsString());
                    }
                    if (reader.Name == "x" && reader.NodeType != XmlNodeType.EndElement)
                    {
                        xscale = reader.ReadElementContentAsInt();
                    }
                    if (reader.Name == "y" && reader.NodeType != XmlNodeType.EndElement)
                    {
                        yscale = reader.ReadElementContentAsInt();
                    }
                }
                reader.Close();
                validreader.Close();
            }
            Brush b ;
            this.MouseClick += new MouseEventHandler(Form1_MouseClick);
            try
            {
                //загружаем файл с картинками
                pics = new Bitmap("1.bmp");

            }
            catch (Exception exc)
            {
                MessageBox.Show("Файл '1.bmp' не найден.\n" + exc.ToString(), "Парные картинки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            //определяем высоту и ширину клетки области формы
            cw = (int)(pics.Width / np);
            ch = pics.Height;
            this.Width = 2 + 5 * r + xscale * 4;
            this.Height = 2 + 5 * r + yscale * 4+this.menuStrip1.Height;
            this.ClientSize = new System.Drawing.Size(this.Width, this.Height);
            //рабочая графическая поверхносить
            g = this.CreateGraphics();
            timer2 = new Timer();
            timer2.Tick += new System.EventHandler(this.timer2_Tick);
            timer2.Interval = 1000;
            // newGame();

        }
        //рисует клетку поля field[i,j]
        private void cell(int i, int j)
        {

            int x = i * (cw + 2), y = j * (ch + 2);
            if (field[i, j] ==0){
                
                //для этой клетки найдена пара, её нужно убрать с поля
                GraphicsContainer gr = g.BeginContainer();
                float xs = (float)xscale / cw;
                float ys = (float)yscale / ch;
                g.ScaleTransform(xs, ys);
                g.FillRectangle(SystemBrushes.Control, x + Convert.ToInt32( r / xs * i), y + Convert.ToInt32( r / ys * j) + Convert.ToInt32(menuStrip1.Height / ys), cw+2+r, ch+2+r);
                g.EndContainer(gr);
            }
            else if ((field[i, j]==1))
            {

                //клетка открыта -вывести картинку
                GraphicsContainer gr = g.BeginContainer();
                float xs=(float)xscale/cw;
                float ys=(float)yscale/ch;
                g.ScaleTransform(xs,ys);
                g.DrawImage(pics, new Rectangle(x + Convert.ToInt32(2 / ys + r / xs * i), y + Convert.ToInt32(2 / ys + r / ys * j) + Convert.ToInt32(menuStrip1.Height / ys), cw, ch), new Rectangle(picture[i, j] * cw, 0, cw, ch), GraphicsUnit.Pixel);
                g.DrawRectangle(Pens.Black, x + Convert.ToInt32(2 / ys + r / xs * i), y + Convert.ToInt32(2 / ys + r / ys * j) + menuStrip1.Height / ys, cw, ch);
                g.EndContainer(gr);
            }
            else if ((field[i, j]==2))
            {
                //клетка закрыта
                GraphicsContainer gr = g.BeginContainer();
                float xs = (float)xscale / cw;
                float ys = (float)yscale / ch;
                g.ScaleTransform(xs, ys);
                Brush b = new SolidBrush(back);
                g.DrawRectangle(Pens.Black, x + Convert.ToInt32(2 / ys + r / xs * i), y + Convert.ToInt32(2 / ys + r / ys * j) + Convert.ToInt32(menuStrip1.Height / ys), cw, ch);
                g.FillRectangle(b, x + Convert.ToInt32(2 / ys + r / xs * i), y + Convert.ToInt32(2 / ys + 2 + r / ys * j) + Convert.ToInt32(menuStrip1.Height / ys), cw, ch);
                g.EndContainer(gr);
            }
            else
            {
                MessageBox.Show(field[i, j]+"? Што?");

            }

        }


        //отрисовывает игровое поле field
        private void drawField()
        {

            for (int i = 0; i < nw; i++)
                for (int j = 0; j < nh; j++)
                    this.cell(i, j);

        }
        //новая игра
        private void newGame()
        {

            //генератор случайных чисел
            Random rnd = new Random();

            // textBox1.Text = rnd.ToString();
            int rndN;
            int[] buf = new int[np];
            //np- количество пар парных картинок
            //в buf[i] записываемб сколько чисел i записали в массив field
            //сгенерируем игровое поле:
            //запишем в массив field случайные числа от 1 до k, каждое число должно быть записано два раза
            freePicture = new List<int>();
            for (int s = 0; s < 8; s++)
            {
                freePicture.Add(s);
                freePicture.Add(s);
            }
                for (int i = 0; i < nw; i++)
                    for (int j = 0; j < nh; j++)
                    {
                        do
                        {
                            rndN = rnd.Next(np) + 1;
                            //textBox1.Text = rndN.ToString();

                        }
                        while (buf[rndN - 1] == 2);
                        int fp=rnd.Next(freePicture.Count);
                        picture[i, j] = freePicture[fp];
                        freePicture.RemoveAt(fp);
                        field[i, j] = 2;
                        buf[rndN - 1]++;
                    }
            n = 0;
            count = 0;
            this.drawField();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {


        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            count = 0;
            timer2.Enabled = false;
            //отрисовать клетки
            this.cell(open1[0], open1[1]);
            this.cell(open2[0], open2[1]);
            // остановка таймера 
            if (n == np)
                //открыты все пары 
                MessageBox.Show("Вы справились с поставленной задачей!");
            //g.DrawString("Game over! Thanks for playing.", new Font("Tahoma", 10, FontStyle.Bold), Brushes.Black, 5, 5);

        }
        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {


        }
        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {

            newGame();
        }



        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void menuStrip1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_MenuActivate(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int i = (int)(e.X / (xscale+r)), j = (int)((e.Y - menuStrip1.Height) / (yscale+r)); // индекс клетки по горизонтали и по вертилкали( с нуля)
            if (i > 3 || j > 3) return;
//            MessageBox.Show(i + " " + j);
            //таймер запущенб т.е. только что была открыта
            //пара одинаковых картинокб но они ещё не стёрты
            //щелчок мыши производится на одно   из этих картинок

            if ((timer2.Enabled) || (field[i, j] ==0)) return;
            //щелчок на месте одной из двух уже найденных парных картинок

            //if (field[i, j] > 200) return;
            //открытых клеток нет

            if (field[i, j] == 0)
            {
                return;
            }
            if (count == 0)
            {
                    count++;
                    //записываем координаты 1-й открытой клетки
                    open1[0] = i; open1[1] = j;
                    // клетка помечается как открытая
                    //отрисовать клетку
                    field[i, j] = 1;
                    this.cell(i, j);
                return;
            }
            //открыта одна клетка, надо открыть вторую
            if (count == 1)
            {
                //записываем координаты 2-й открытой клетки
                open2[0] = i; open2[1] = j;
                //если открыта одна клетка, и щелчок сделан в той же клетке, ниечго не происходит
                if ((open1[0] == open2[0]) && (open1[1] == open2[1]))
                    return;
                else
                {
                    //теперь открыты две клетки
                    //клетка помечается как открытая
                    //отрисовать клетку
                    field[i, j] = 1;
                    this.cell(i, j);
                    //открыты 2 одинаковые картинки
                    if (picture[open1[0], open1[1]] == picture[open2[0], open2[1]])
                    {
                        n++;
                        field[open1[0], open1[1]] = 0;
                        field[open2[0], open2[1]] = 0;
                    }
                    else
                    {
                        field[open1[0], open1[1]] = 2;
                        field[open2[0], open2[1]] = 2;

                    }
                    timer2.Enabled = true;
                }
            }
        }

        private void Form1_Paint_1(object sender, PaintEventArgs e)
        {
            drawField();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 s = new Form2();
            s.Show();
            s.textBox1.Text = field[0, 2].ToString();
        }
        public void ValidationHandler(object sender, ValidationEventArgs args)
        {
            MessageBox.Show(args.Severity + ":" + args.Message + "\nÐ—Ð°Ð³Ñ€ÑƒÐ¶ÐµÐ½Ñ‹ ÑÑ‚Ð°Ð½Ð´Ð°Ñ€Ñ‚Ð½Ñ‹Ðµ Ð¿Ð°Ñ€Ð°Ð¼ÐµÑ‚Ñ€Ñ‹");
            valid = false;
        }
    }

}






