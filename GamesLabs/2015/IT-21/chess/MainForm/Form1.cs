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

namespace MainForm
{
    public partial class Form1 : Form
    {
        private Field field;//поле
        private Cell selectedCell;//выбранная ячейка
        private List<Cell> needUsed;//ячейка, необходимая для использования
        private int whiteCount;//количество белых
        private int blackCount;//количество черных
        private bool isWhite;//ход белых
        private bool isMove;//совершение хода
        private bool isNeedCut;// true-нужно бить фишку, false-не нужно бить фишку
        private bool isCutted;//true- фишка побита
        private static bool valid;
        public static Color player_one, player_two;
        
        public Form1()
        {
            InitializeComponent();
            InitGame();
            UpdateStatus();
        }
        /// <summary>
        /// инициализация переменных игры и игрового поля
        /// </summary>
        private void InitGame()
        {
            valid = true;
            whiteCount = 12;//количество белых
            blackCount = 12;//количество черных
            player_one = Color.Black;
            player_two = Color.Silver;
            isMove = false;
            isNeedCut = false;
            isCutted = false;
            XmlDocument doc = new XmlDocument();
            doc.Load(@"config.xml");
            XmlSchemaSet schemas = new XmlSchemaSet();
            doc.Schemas.Add("", "config.xsd");
            ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);

            doc.Validate(eventHandler);
            if (valid)
            {
                isWhite = (doc.GetElementsByTagName("first_player").Item(0).ChildNodes.Item(0).InnerText == "white") ? true : false;
                player_one = Color.FromName(doc.GetElementsByTagName("player_one").Item(0).ChildNodes.Item(0).InnerText);
                player_two = Color.FromName(doc.GetElementsByTagName("player_two").Item(0).ChildNodes.Item(0).InnerText);

            }


            field= new Field(pictureBox1);
            selectedCell = null;
            isWhite = true;

            needUsed = new List<Cell>();
          }
        /// <summary>
        /// обновление информации
        /// </summary>
        private void UpdateStatus()
        {
            string currentPlayer;
            string currentScore;

            if (isWhite == true) { currentPlayer = "белых."; currentScore = "Осталось фишек: " + whiteCount; }
            else { currentPlayer = "черных."; currentScore = "Осталось фишек: " + blackCount; }

            playerInfStripTextBox1.Text = "Ход " + currentPlayer + "  " + currentScore;

        }
        #region События элементов управления

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitGame();

            UpdateStatus();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
#endregion

        /// <summary>
        /// проверка поля на возможность "битвы фишек"
        /// </summary>
        private void CheckCut()
        {
            needUsed.Clear();

            if (isWhite)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (field[i, j].Counter == Counters.White)
                        {
                            int numItm = 0;

                            if (IsOnBoard(i + 1, j - 1))
                            {
                                if (field[i + 1, j - 1].Counter != Counters.White
                                    && field[i + 1, j - 1].Counter != Counters.None)
                                {
                                    if (IsOnBoard(i + 2, j - 2)
                                        && field[i + 2, j - 2].Counter == Counters.None)
                                    {
                                        numItm++;
                                    }
                                }
                            }
                            if (IsOnBoard(i + 1, j + 1))
                            {
                                if (field[i + 1, j + 1].Counter != Counters.White
                                    && field[i + 1, j + 1].Counter != Counters.None)
                                {
                                    if (IsOnBoard(i + 2, j + 2)
                                        && field[i + 2, j + 2].Counter == Counters.None)
                                    {
                                        numItm++;
                                    }
                                }
                            }
                            if (IsOnBoard(i - 1, j - 1))
                            {
                                if (field[i - 1, j - 1].Counter != Counters.White
                                    && field[i - 1, j - 1].Counter != Counters.None)
                                {
                                    if (IsOnBoard(i - 2, j - 2)
                                        && field[i - 2, j - 2].Counter == Counters.None)
                                    {
                                        numItm++;
                                    }
                                }
                            }
                            if (IsOnBoard(i - 1, j + 1))
                            {
                                if (field[i - 1, j + 1].Counter != Counters.White
                                    && field[i - 1, j + 1].Counter != Counters.None)
                                {
                                    if (IsOnBoard(i - 2, j + 2)
                                        && field[i - 2, j + 2].Counter == Counters.None)
                                    {
                                        numItm++;
                                    }
                                }
                            }
                            field[i, j].Y = i;
                            field[i, j].X = j;
                            if (numItm > 0) needUsed.Add(field[i, j]);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        int numItm = 0;

                        if (field[i, j].Counter == Counters.Black)
                        {
                            if (IsOnBoard(i + 1, j - 1))
                            {
                                if (field[i + 1, j - 1].Counter != Counters.Black
                                    && field[i + 1, j - 1].Counter != Counters.None)
                                {
                                    if (IsOnBoard(i + 2, j - 2)
                                        && field[i + 2, j - 2].Counter == Counters.None)
                                    {
                                        numItm++;
                                    }
                                }
                            }
                            if (IsOnBoard(i + 1, j + 1))
                            {
                                if (field[i + 1, j + 1].Counter != Counters.Black
                                    && field[i + 1, j + 1].Counter != Counters.None)
                                {
                                    if (IsOnBoard(i + 2, j + 2)
                                        && field[i + 2, j + 2].Counter == Counters.None)
                                    {
                                        numItm++;
                                    }
                                }
                            }
                            if (IsOnBoard(i - 1, j - 1))
                            {
                                if (field[i - 1, j - 1].Counter != Counters.Black
                                    && field[i - 1, j - 1].Counter != Counters.None)
                                {
                                    if (IsOnBoard(i - 2, j - 2)
                                        && field[i - 2, j - 2].Counter == Counters.None)
                                    {
                                        numItm++;
                                    }
                                }
                            }
                            if (IsOnBoard(i - 1, j + 1))
                            {
                                if (field[i - 1, j + 1].Counter != Counters.Black
                                    && field[i - 1, j + 1].Counter != Counters.None)
                                {
                                    if (IsOnBoard(i - 2, j + 2)
                                        && field[i - 2, j + 2].Counter == Counters.None)
                                    {
                                        numItm++;
                                    }
                                }
                            }

                            field[i, j].Y = i;
                            field[i, j].X = j;
                            if (numItm > 0) needUsed.Add(field[i, j]);
                        }
                    }
                }
            }

            if (needUsed.Count > 0)
            {
                isNeedCut = true;
            }
            else
            {
                isNeedCut = false;
            }
        }

        /// <summary>
        /// Проверка поля на выйгрыш
        /// </summary>
        /// <returns>true - выйгрыш достигнут, false - не достигнут</returns>
        private bool CheckWin()
        {
            if (blackCount != 0 && whiteCount == 0)
            {
                MessageBox.Show("Белые проиграли!", "Проигрыш", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return true;
            }
            else if (whiteCount != 0 && blackCount == 0)
            {
                MessageBox.Show("Черные проиграли!", "Проигрыш", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Проверка игроков
        /// </summary>
        /// <param name="Y">Y - координата выбранной ячейки</param>
        /// <param name="X">X - координата выбранной ячейки</param>
        /// <returns>true - выбранная ячейка является ячейкой текущего игрока, false - не является</returns>
        private bool CheckPlayer(int Y, int X)
        {
            if (isWhite)
            {
                if (field[Y, X].Counter != Counters.White) return false;
            }
            else
            {
                if (field[Y, X].Counter != Counters.Black) return false;
            }

            return true;
        }

        /// <summary>
        /// Проверка ячейки на соприкосновение с 
        /// верхней или нижней границей поля
        /// </summary>
        /// <param name="current">Ячейка для проверки</param>
        private void CheckBorder(Cell current)
        {
            if (current.Counter == Counters.Black)
            {
                if (current.Y == 7)
                {
                    field[current.Y, current.X].Counter = Counters.None;
                    blackCount--;
                }
            }
            else if (current.Counter == Counters.White)
            {
                if (current.Y == 0)
                {
                    field[current.Y, current.X].Counter = Counters.None;
                    whiteCount--;
                }
            }
        }


        /// <summary>
        /// Преобразование координат pictureBox
        /// в индексы поля
        /// </summary>
        /// <param name="e">Координата pictureBox</param>
        /// <returns>Индекс поля</returns>
        private int Coord(double e)
        {
            return (int)e / Cell.PartyLength;
        }

        /// <summary>
        /// Проверка координат на нахождение их на игровом поле
        /// </summary>
        /// <param name="Y">Y - координата проверяемой ячейки</param>
        /// <param name="X">Y - координата проверяемой ячейки</param>
        /// <returns>true - находится на поле, false - не находится</returns>
        private bool IsOnBoard(int Y, int X)
        {
            if ((Y >= 8 || Y < 0) || (X >= 8 || X < 0))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Подсвечивание возможных ячеек для
        /// хода на игровом поле
        /// </summary>
        /// <param name="current">Выбранная ячейка</param>
        /// <param name="_X">X - координата выбранной ячейки</param>
        /// <param name="_Y">Y - координата выбранной ячейки</param>
        /// <returns>Количество подсвеченных ячеек</returns>
        private int IlluminateCells(Cell current, int _X, int _Y)
        {
            int X = _X;
            int Y = _Y;

            int numOfIlluminated = 0;
            if (!isNeedCut)
            {
                if (current.Counter == Counters.Black)
                {
                    if (IsOnBoard(Y + 1, X - 1))
                    {
                        if (field[Y + 1, X - 1].Counter == Counters.None)
                        { field[Y + 1, X - 1].ColorCell = Colors.Illuminated; numOfIlluminated++; }
                    }
                    if (IsOnBoard(Y + 1, X + 1))
                    {
                        if (field[Y + 1, X + 1].Counter == Counters.None)
                        { field[Y + 1, X + 1].ColorCell = Colors.Illuminated; numOfIlluminated++; }
                    }
                }
                else if (current.Counter == Counters.White)
                {
                    if (IsOnBoard(Y - 1, X - 1))
                    {
                        if (field[Y - 1, X - 1].Counter == Counters.None)
                        { field[Y - 1, X - 1].ColorCell = Colors.Illuminated; numOfIlluminated++; }
                    }
                    if (IsOnBoard(Y - 1, X + 1))
                    {
                        if (field[Y - 1, X + 1].Counter == Counters.None)
                        { field[Y - 1, X + 1].ColorCell = Colors.Illuminated; numOfIlluminated++; }
                    }
                }
            }
            else
            {
                if (IsOnBoard(Y + 1, X - 1))
                {
                    if (field[Y + 1, X - 1].Counter != current.Counter
                        && field[Y + 1, X - 1].Counter != Counters.None)
                    {
                        if (IsOnBoard(Y + 2, X - 2)
                            && field[Y + 2, X - 2].Counter == Counters.None)
                        {
                            field[Y + 2, X - 2].ColorCell = Colors.Illuminated;
                            numOfIlluminated++;
                        }
                    }
                }
                if (IsOnBoard(Y + 1, X + 1))
                {
                    if (field[Y + 1, X + 1].Counter != current.Counter
                        && field[Y + 1, X + 1].Counter != Counters.None)
                    {
                        if (IsOnBoard(Y + 2, X + 2)
                            && field[Y + 2, X + 2].Counter == Counters.None)
                        {
                            field[Y + 2, X + 2].ColorCell = Colors.Illuminated;
                            numOfIlluminated++;
                        }
                    }
                }
                if (IsOnBoard(Y - 1, X - 1))
                {
                    if (field[Y - 1, X - 1].Counter != current.Counter
                        && field[Y - 1, X - 1].Counter != Counters.None)
                    {
                        if (IsOnBoard(Y - 2, X - 2)
                            && field[Y - 2, X - 2].Counter == Counters.None)
                        {
                            field[Y - 2, X - 2].ColorCell = Colors.Illuminated;
                            numOfIlluminated++;
                        }
                    }
                }
                if (IsOnBoard(Y - 1, X + 1))
                {
                    if (field[Y - 1, X + 1].Counter != current.Counter
                        && field[Y - 1, X + 1].Counter != Counters.None)
                    {
                        if (IsOnBoard(Y - 2, X + 2)
                            && field[Y - 2, X + 2].Counter == Counters.None)
                        {
                            field[Y - 2, X + 2].ColorCell = Colors.Illuminated;
                            numOfIlluminated++;
                        }
                    }
                }
            }

            field.Draw(pictureBox1, Form1.player_one, Form1.player_two);

            return numOfIlluminated;
        }

        /// <summary>
        /// Удаление подсветки ячеек
        /// </summary>
        private void UnilluminateCells()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (field[i, j].ColorCell == Colors.Illuminated)
                        field[i, j].ColorCell = Colors.Sienna;
                }
            }

            field.Draw(pictureBox1, Form1.player_one, Form1.player_two);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (CheckWin()) return;

            int Y = Coord(e.Y);
            int X = Coord(e.X);

            if (isMove)
            {
                #region MOVE

                if (selectedCell == field[Y, X])
                {
                    UnilluminateCells();

                    isMove = false;
                }

                if (field[Y, X].ColorCell == Colors.Illuminated)
                {
                    bool cutted = false;

                    if (Math.Abs(Y - selectedCell.Y) == 2)
                    {
                        int y = Y - selectedCell.Y;
                        int x = X - selectedCell.X;

                        x = (x < 0) ? x + 1 : x - 1;
                        y = (y < 0) ? y + 1 : y - 1;

                        if (field[selectedCell.Y + y, selectedCell.X + x].Counter == Counters.Black)
                        {
                            blackCount--;
                        }
                        else if (field[selectedCell.Y + y, selectedCell.X + x].Counter == Counters.White)
                        {
                            whiteCount--;
                        }

                        field[selectedCell.Y + y, selectedCell.X + x].Counter = Counters.None;

                        cutted = true;
                    }
                    else cutted = false;

                    Cell buf = field[Y, X];
                    field[Y, X] = field[selectedCell.Y, selectedCell.X];
                    field[selectedCell.Y, selectedCell.X] = buf;
                    field[Y, X].Y = Y;
                    field[Y, X].X = X;

                    field.Draw(pictureBox1, Form1.player_one, Form1.player_two);
                    UnilluminateCells();
                    if (cutted)
                    {
                        CheckCut();

                        bool flag = false;
                        for (int i = 0; i < needUsed.Count; i++)
                        {
                            if (needUsed[i].Y == Y && needUsed[i].X == X)
                            {
                                flag = true;
                                break;
                            }
                        }

                        if (flag) isCutted = true;
                        else isCutted = false;

                    }
                    if (isCutted)
                    {
                        CheckCut();
                        if (!isNeedCut)
                        {
                            isWhite = (isWhite) ? false : true;
                            CheckBorder(field[Y, X]);
                        }
                    }
                    else
                    {
                        isWhite = (isWhite) ? false : true;
                        CheckBorder(field[Y, X]);
                    }

                    field.Draw(pictureBox1, Form1.player_one, Form1.player_two);
                    isMove = false;
                }

                #endregion
            }
            else
            {
                if (!CheckPlayer(Y, X)) return;

                CheckCut();

                selectedCell = field[Y, X];
                selectedCell.Y = Y;
                selectedCell.X = X;

                if (isNeedCut)
                {
                    bool flag = false;
                    for (int i = 0; i < needUsed.Count; i++)
                    {
                        if (needUsed[i].Y == Y && needUsed[i].X == X)
                        {
                            flag = true;
                            break;
                        }
                    }

                    if (!flag) return;
                }

                if (IlluminateCells(field[Y, X], X, Y) > 0) isMove = true;
            }

            UpdateStatus();
            CheckWin();
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    MessageBox.Show(e.Message);
                    Form1.valid = false;
                    break;
                case XmlSeverityType.Warning:
                    MessageBox.Show(e.Message);
                    Form1.valid = false;
                    break;
            }

        }

       
    }
}



        


    