using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using  System.Xml;
using  System.Xml.Schema;

namespace lab13
{
    public partial class Form1 : Form
    {
        private Field gameField;
        private int _fieldSize;
        private int _winCount;
        private Color _formColor;
        Image img = new Bitmap(@"картинки\фон.png");
        Image zero = new Bitmap(@"картинки\нолик.png");
        Image cross = new Bitmap(@"картинки\крестик.png");
        Image crossH = new Bitmap(@"картинки\крестик(h).png");
        Image zeroH = new Bitmap(@"картинки\нолик(h).png");
        Image crossV = new Bitmap(@"картинки\крестик(v).png");
        Image zeroV = new Bitmap(@"картинки\нолик(v).png");
        Image crossGl = new Bitmap(@"картинки\крестик(gl).png");
        Image crossGr = new Bitmap(@"картинки\крестик(gr).png");
        Image zeroGl = new Bitmap(@"картинки\нолик(gl).png");
        Image zeroGr = new Bitmap(@"картинки\нолик(gr).png");
        private static bool validation = false;
        

        public Form1()
        {

            InitializeComponent();
            label2.Text = "";
            label4.Text = "0";
            label5.ForeColor = Color.Blue;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                XmlSchemaSet configSettings = new XmlSchemaSet();
                configSettings.Add("urn:GameConfig-schema", "XMLFile12.xsd");
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas = configSettings;
                settings.ValidationEventHandler += new ValidationEventHandler(Valid);
                XmlReader reader = XmlReader.Create("XMLFile.xml", settings);
                XmlNodeType type;
                while (reader.Read())
                {
                    type = reader.NodeType;
                    if (type == XmlNodeType.Element)
                    {
                        if (reader.Name == "FieldSize")
                        {
                            reader.Read();
                            if (validation == false)
                                _fieldSize = Convert.ToInt32(reader.Value);
                        }
                        if (reader.Name == "CountWin")
                        {
                            reader.Read();
                            if (validation == false)
                                _winCount = Convert.ToInt32(reader.Value);
                        }
                        if (reader.Name == "BackColor")
                        {
                            reader.Read();
                            if (validation == false)
                                BackColor = Color.FromName((string)reader.Value);
                        }
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Отсутствует файл конфигурации! \nИгра будет запущена со стандартными конфигурациями");
                _fieldSize = 13;
                _winCount = 5;
                BackColor = Color.Wheat;
            }
            
            gameField = new Field(_fieldSize, _winCount);
            dataGridView1.Height = _fieldSize*40 + 3;
            dataGridView1.Width = _fieldSize*40 + 3;
            dataGridView1.RowCount = _fieldSize;
            dataGridView1.ColumnCount = _fieldSize;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 40;
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Width = 40;
            }
            timer1.Start();
            NewGame();
        }

        private  void Valid(object sender, ValidationEventArgs e)
        {
            validation = true;
            MessageBox.Show("Неверные конфигурации XML! \nИгра будет запущена со стандартными конфигурациями");
            _fieldSize = 13;
            _winCount = 5;
            BackColor = Color.Wheat;
        }

        public void NewGame()
        {
            gameField.min = 0;
            gameField.sec = 0;
            for (int i = 0; i < gameField.FieldSize; i++)
            {
                for (int j = 0; j < gameField.FieldSize; j++)
                {
                    dataGridView1[j, i].Value = img;
                    gameField.matrix[i, j] = 0;
                }
            }
            gameField.steps = 0;
            gameField.Win = false;
            gameField.DeadHeat = false;
            label4.Text = "0";
            label5.ForeColor = Color.Blue;
            label5.Text = "Ходят крестики";
        }

        //таймер
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (gameField.Win != true && gameField.DeadHeat != true)
            {
                gameField.sec++;
                if (gameField.sec == 60)
                {
                    gameField.sec = 0;
                    gameField.min++;
                }
                label2.Text = gameField.min.ToString() + ":" + gameField.sec.ToString();
            }
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gameField.Win != true)
            {
                if (gameField.steps % 2 == 0)
                {
                    if (gameField.matrix[dataGridView1.CurrentCell.RowIndex, dataGridView1.CurrentCell.ColumnIndex] == 0)
                    {
                        dataGridView1.CurrentCell.Value = new Bitmap(cross, 40, 40);
                        gameField.matrix[dataGridView1.CurrentCell.RowIndex, dataGridView1.CurrentCell.ColumnIndex] = 1;
                        gameField.steps++;
                        if (gameField.steps >= gameField.WinCount*2-1)
                        {
                            gameField.HorisontalCheck(crossH, zeroH, dataGridView1);
                            gameField.VerticalCheck(crossV, zeroV,dataGridView1);
                            gameField.DiagonalLeftTopCheck(zeroGl, crossGl, dataGridView1);
                            gameField.DiagonalLeftBottomCkeck(zeroGl, crossGl,dataGridView1);
                            gameField.DiagonalRightTopCheck(zeroGr, crossGr, dataGridView1);
                            gameField.DiagonalRightBottomCheck(zeroGr, crossGr,dataGridView1);
                            //Check();
                        }
                            
                        if (gameField.Win == true)
                        {
                            label5.ForeColor    = Color.Red;
                            label5.Text = "Победа крестиков!"; 
                            MessageBox.Show("Победили крестики");
                        }
                        else if (gameField.Win != true && gameField.DeadHeat == true)
                        {
                            label5.Text = "Ничья";
                            label5.ForeColor = Color.Red;
                            MessageBox.Show("Ничья");
                        }
                        else if(gameField.Win!=true)
                            label5.Text = "Ходят нолики";
                    }
                }
                else
                {
                    if (gameField.matrix[dataGridView1.CurrentCell.RowIndex, dataGridView1.CurrentCell.ColumnIndex] == 0)
                    {
                        dataGridView1.CurrentCell.Value = new Bitmap(zero, 40, 40);
                        gameField.matrix[dataGridView1.CurrentCell.RowIndex, dataGridView1.CurrentCell.ColumnIndex] = -1;
                        gameField.steps++;
                        if (gameField.steps >= gameField.WinCount*2)
                        {
                            gameField.HorisontalCheck(crossH, zeroH, dataGridView1);
                            gameField.VerticalCheck(crossV, zeroV, dataGridView1);
                            gameField.DiagonalLeftTopCheck(zeroGl,crossGl,dataGridView1);
                            gameField.DiagonalLeftBottomCkeck(zeroGl,crossGl,dataGridView1);
                            gameField.DiagonalRightTopCheck(zeroGr,crossGr,dataGridView1);
                            gameField.DiagonalRightBottomCheck(zeroGr,crossGr,dataGridView1);
                            //Check();
                        }
                        if (gameField.Win == true)
                        {
                            label5.ForeColor = Color.Red;
                            label5.Text = "Победа ноликов!"; 
                            MessageBox.Show("Победили нолики");
                        }
                        else if (gameField.Win != true && gameField.DeadHeat == true)
                        {
                            label5.ForeColor = Color.Red;
                            label5.Text = "Ничья";
                            MessageBox.Show("Ничья!");
                        }
                        else if(gameField.Win!=true)
                            label5.Text = "Ходят крестики";
                    }
                }
                label4.Text = gameField.steps.ToString();
            }
        }

       

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
