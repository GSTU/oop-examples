using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace WindowsFormsApplication1
{
    public partial class Body : Form
    {
        private bool isValid;
        public Body()
        {
            InitializeComponent();
            InitializationLabel();
            k = 0;//по умолчанию загружается судоку "1"
            LoadGame();
            LoadXML();
        }

        ////////////////////////////////////////////////////////////
        private void LoadXML()
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
                    this.BackColor = Color.FromName(doc["root"]["config"]["backgroundColor"].InnerText);
                    this.ForeColor = Color.FromName(doc["root"]["config"]["forecolor"].InnerText);
                    this.button1.Font = new System.Drawing.Font(doc["root"]["but"].InnerText, 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.button1.BackColor = Color.FromName(doc["root"]["butcolor"].InnerText);
                    this.ClientSize = new System.Drawing.Size(int.Parse(doc["root"]["x"].InnerText), int.Parse(doc["root"]["y"].InnerText));
                }
                else
                {
                    this.BackColor = Color.Red;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка: " + Ex.Message);
                this.BackColor = Color.Red;
            }
        }

        private void ConfigCheckValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                isValid = false;
                MessageBox.Show("Предупреждение: XML файл повреждён!");
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                isValid = false;
                MessageBox.Show("Ошибка чтения данных из файла! Загружены стандартные параметры.");
            }
        }
        ////////////////////////////////////////////////////////////
        private bool flagXY = true;//флаг
        private bool flag = true;//флаг

        int mistake=0;//ошибки, связанные с неправильным выбором 
        List<Label> item = new List<Label>();//для хранения неправильных chois'ов

        private Choice number = new Choice();//связь с формой выбора
        fileopen favfile = new fileopen();//для работы с файлом

        private void Form1_Load(object sender, EventArgs e)
        {}

        private Label[,] lib = new Label[9, 9];//поле 9x9
        void InitializationLabel()
        {
            lib[0, 0] = label1;
            lib[0, 1] = label2;
            lib[0, 2] = label3;
            lib[0, 3] = label4;
            lib[0, 4] = label5;
            lib[0, 5] = label6;
            lib[0, 6] = label7;
            lib[0, 7] = label8;
            lib[0, 8] = label9;

            lib[1, 0] = label10;
            lib[1, 1] = label11;
            lib[1, 2] = label12;
            lib[1, 3] = label13;
            lib[1, 4] = label14;
            lib[1, 5] = label15;
            lib[1, 6] = label16;
            lib[1, 7] = label17;
            lib[1, 8] = label18;

            lib[2, 0] = label19;
            lib[2, 1] = label20;
            lib[2, 2] = label21;
            lib[2, 3] = label22;
            lib[2, 4] = label23;
            lib[2, 5] = label24;
            lib[2, 6] = label25;
            lib[2, 7] = label26;
            lib[2, 8] = label27;

            lib[3, 0] = label28;
            lib[3, 1] = label29;
            lib[3, 2] = label30;
            lib[3, 3] = label31;
            lib[3, 4] = label32;
            lib[3, 5] = label33;
            lib[3, 6] = label34;
            lib[3, 7] = label35;
            lib[3, 8] = label36;

            lib[4, 0] = label37;
            lib[4, 1] = label38;
            lib[4, 2] = label39;
            lib[4, 3] = label40;
            lib[4, 4] = label41;
            lib[4, 5] = label42;
            lib[4, 6] = label43;
            lib[4, 7] = label44;
            lib[4, 8] = label45;

            lib[5, 0] = label46;
            lib[5, 1] = label47;
            lib[5, 2] = label48;
            lib[5, 3] = label49;
            lib[5, 4] = label50;
            lib[5, 5] = label51;
            lib[5, 6] = label52;
            lib[5, 7] = label53;
            lib[5, 8] = label54;

            lib[6, 0] = label55;
            lib[6, 1] = label56;
            lib[6, 2] = label57;
            lib[6, 3] = label58;
            lib[6, 4] = label59;
            lib[6, 5] = label60;
            lib[6, 6] = label61;
            lib[6, 7] = label62;
            lib[6, 8] = label63;

            lib[7, 0] = label64;
            lib[7, 1] = label65;
            lib[7, 2] = label66;
            lib[7, 3] = label67;
            lib[7, 4] = label68;
            lib[7, 5] = label69;
            lib[7, 6] = label70;
            lib[7, 7] = label71;
            lib[7, 8] = label72;

            lib[8, 0] = label73;
            lib[8, 1] = label74;
            lib[8, 2] = label75;
            lib[8, 3] = label76;
            lib[8, 4] = label77;
            lib[8, 5] = label78;
            lib[8, 6] = label79;
            lib[8, 7] = label80;
            lib[8, 8] = label81;
        }
        
        void Element(string[] st)//заполняем поле числами
        {
            for (int i = 0, y = 0; i < 9; i++)//i - строка
            {
                for (int j = 0; j < 9; j++, y++)//j - столбец 
                {
                    if (st[y] != "*")//y - элементы из файла
                    {
                        lib[i, j].Text = st[y];
                        lib[i, j].Enabled = false;
                    }
                    else
                    {
                        lib[i, j].Text = " ";
                    }
                }
            }
        }

        private void InsertNumber(int x, int y)
        {
            if (number.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (lib[x, i].Text != number.Figure() && lib[i, y].Text != number.Figure())//проверяем столбец и строку, соответствующую элементу
                    {
                        flagXY = true;//если все в порядке
                    }
                    else
                    {
                        flagXY = false;//в случае повторяющихся элементов

                        break;
                    }
                }

                if ((x == 0 || x < 3) && (y == 0 || y < 3))//правила хода для каждого блока
                {
                    rules(0, 0, 3, 3);
                }
                else if ((x == 0 || x < 3) && (y == 3 || y < 6))
                {
                    rules(0, 3, 3, 6);
                }
                else if ((x == 0 || x < 3) && (y == 6 || y < 9))
                {
                    rules(0, 6, 3, 9);
                }
                else if ((x == 3 || x < 6) && (y == 0 || y < 3))
                {
                    rules(3, 0, 6, 3);
                }
                else if ((x == 3 || x < 6) && (y == 3 || y < 6))
                {
                    rules(3, 3, 6, 6);
                }
                else if ((x == 3 || x < 6) && (y == 6 || y < 9))
                {
                    rules(3, 6, 6, 9);
                }
                else if ((x == 6 || x < 9) && (y == 0 || y < 3))
                {
                    rules(6, 0, 9, 3);
                }
                else if ((x == 6 || x < 9) && (y == 3 || y < 6))
                {
                    rules(6, 3, 9, 6);
                }
                else if ((x == 6 || x < 9) && (y == 6 || y < 9))
                {
                    rules(6, 6, 9, 9);
                }
            }
        }

        private void check(int x, int y)//проверяем
        {
            int i = 0;

            if (flagXY == true && flag == true)
            {
                lib[x, y].Text = number.Figure();

                if (mistake > 0)//проверка на ошибки
                {
                    foreach (Label l in item) 
                    { 
                        if (lib[x, y] == l) mistake--; 
                        //MessageBox.Show("mistake" + mistake); 
                    }
                }
            }
            else
            {
                lib[x, y].Text = number.Figure();
                item.Add(lib[x, y]);
                mistake++;
                //MessageBox.Show("mistake"+mistake);
            }

            for (int z = 0; z < 9; z++)//Проверяем на заполненность
            {
                for (int c = 0; c < 9; c++)
                {
                   // lib[z, c].BackColor = Color.Transparent;
                    if (lib[z, c].Text.ToString() != " ")
                    {
                        i++;
                    }
                }
            }

            if (i == 81 && mistake==0)//Если все ячейки заполнены и ошибок нет
            {
                MessageBox.Show("end.");
            }
        }

        void rules(int x, int y, int c, int b)
        {
            for (int t = x; t < c; t++)
            {
                for (int v = y; v < b; v++)
                {
                    if (lib[t, v].Text == number.Figure())
                    {
                        flag = false;
                        //lib[t, v].BackColor = Color.Red;//в пределах блока
                    }

                }
            }

        }

        void Click(object sender, EventArgs e)
        {
            int x = 0, y = 0;
            for (int indexX = 0; indexX < 9; indexX++)
            {
                for (int indexY = 0; indexY < 9; indexY++)
                {
                    if (sender as Label == lib[indexX, indexY])
                    {
                        x = indexX;
                        y = indexY;
                        break;
                    }
                }
            }
            InsertNumber(x, y);
            check(x, y);
            flag = true;
        }

        private void button1_Click(object sender, EventArgs e)//завершить игру
        {
            DialogResult exit = MessageBox.Show("Вы уверены, что хотите завершить игру?", "Выход", MessageBoxButtons.YesNo);
            if (exit == DialogResult.Yes)
            { this.Close(); }
        }

        private void button2_Click(object sender, EventArgs e)//правила игры
        {
            RulesOfGame f1 = new RulesOfGame();
            f1.Show();
        }

        private void Clear_Click(object sender, EventArgs e)//очистить игровое поле
        {
            favfile.readpazl(k);
            Element(favfile.Mass);
            mistake = 0;
        }
        int k;
        private void LoadGame()//новая игра
        {
            for (int i = 0, y = 0; i < 9; i++)//i - строка
            {
                for (int j = 0; j < 9; j++, y++)//j - столбец 
                {
                    {
                        lib[i, j].Enabled = true;
                    }
                }
            }
            favfile.readpazl(k);
            Element(favfile.Mass);
        }
        private void CheckThat_Click(object sender, EventArgs e)
        {
           if(mistake>0) MessageBox.Show("Были допущены ошибки");
           else MessageBox.Show("Ошибок нет");
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            k = 0;
            LoadGame();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            k = 1;
            LoadGame();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            k = 2;
            LoadGame();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            k = 3;
            LoadGame();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            k = 4;
            LoadGame();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            k = 5;
            LoadGame();
        }

    }
}
