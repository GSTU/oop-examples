using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Laba13
{
    class Player
    {
        Cell[,] cells = new Cell[12, 12];
        bool state = false;
        string fieldColor;
        string missColor;
        string popColor;
        string killColor;

        public Player(string fieldColor, string missColor, string popColor, string killColor)
        {
            this.fieldColor = fieldColor;
            this.missColor = missColor;
            this.popColor = popColor;
            this.killColor = killColor;

            //Создание пустого поля
            for (int i = 1; i < 11; i++)
                for (int j = 1; j < 11; j++)
                    cells[j, i] = new Cell(((i - 1) * 30 + 1 + i - 1), ((j - 1) * 30 + 1 + j - 1), 0, fieldColor);
            for (int j = 0; j < 12; j++)
                cells[j, 0] = new Cell(0, 0, -1, fieldColor);
            for (int i = 1; i < 12; i++)
                cells[11, i] = new Cell(0, 0, -1, fieldColor);
            for (int j = 0; j < 11; j++)
                cells[j, 11] = new Cell(0, 0, -1, fieldColor);
            for (int i = 1; i < 11; i++)
                cells[0, i] = new Cell(0, 0, -1, fieldColor);
            
            //Расстановка кораблей
            SetShips.Set(this);

            //отображение своих кораблей
            /*for (int i = 1; i < 11; i++)
                for (int j = 1; j < 11; j++)
                {
                    if (cells[i, j].Number != 0 && cells[i, j].Number != 2)
                        cells[i, j].SolidBrush.Color = ColorTranslator.FromHtml("#FF008000");
                    if (cells[i, j].Number == 2)
                        cells[i, j].SolidBrush.Color = Color.Yellow;
                }*/
        }

        //Обработка атаки
        public bool Fire(int x, int y)
        {
            int kol;

            for (int i = 1; i < 11; i++)
                for (int j = 1; j < 11; j++)
                {
                    if (x >= cells[i, j].X && y >= cells[i, j].Y && x <= cells[i, j].X + 30 &&
                        y <= cells[i, j].Y + 30 && cells[i, j].SolidBrush.Color == ColorTranslator.FromHtml(fieldColor))
                    {
                        //проверка попадания
                        if (cells[i, j].Number == 0 || cells[i, j].Number == 2) //ячейка пуста
                        {
                            cells[i, j].SolidBrush.Color = ColorTranslator.FromHtml(missColor);
                            state = false;
                            return true;
                        }
                        else //ячейка не пуста
                        {
                            kol = 0;

                            //есть ли еще нетронутые ячейки
                            for (int k = 1; k < 11; k++)
                                for (int l = 1; l < 11; l++)
                                {
                                    if (cells[k, l].Number == cells[i, j].Number &&
                                        cells[k, l].SolidBrush.Color == ColorTranslator.FromHtml(fieldColor))
                                        kol++;
                                }

                            if (kol == 1) //только текущая
                            {
                                for (int k = 1; k < 11; k++)
                                    for (int l = 1; l < 11; l++)
                                        if (cells[k, l].Number == cells[i, j].Number)
                                        {
                                            cells[k, l].SolidBrush.Color = ColorTranslator.FromHtml(killColor);

                                            for (int a = l - 1; a < l + 2; a++)
                                            {
                                                if (cells[k - 1, a].SolidBrush.Color == ColorTranslator.FromHtml(fieldColor))
                                                    cells[k - 1, a].SolidBrush.Color = ColorTranslator.FromHtml(missColor);
                                                if (cells[k + 1, a].SolidBrush.Color == ColorTranslator.FromHtml(fieldColor))
                                                    cells[k + 1, a].SolidBrush.Color = ColorTranslator.FromHtml(missColor);
                                            }
                                            for (int a = l - 1; a < l + 2; a += 2)
                                                if (cells[k, a].SolidBrush.Color == ColorTranslator.FromHtml(fieldColor))
                                                    cells[k, a].SolidBrush.Color = ColorTranslator.FromHtml(missColor);
                                        }
                            }
                            else //есть еще нетронутые
                                cells[i, j].SolidBrush.Color = ColorTranslator.FromHtml(popColor);
                        }
                    }
                }
            return false;
        }

        //Проверки победы
        public bool Win(int p)
        {
            //проверка наличия кораблей
            int kol = 0;

            for (int i = 1; i < 11; i++)
                for (int j = 1; j < 11; j++)
                    if (cells[i, j].SolidBrush.Color == ColorTranslator.FromHtml(killColor))
                        kol++;

            if (kol == 20)
                if (MessageBox.Show(p + " игрок победил!", "Победа", MessageBoxButtons.OK, MessageBoxIcon.Information) ==
                    DialogResult.OK)
                    return true;
            return false;
        }

        //Свойства доступа
        public Cell this[int i, int j]
        {
            get { return cells[i, j]; }
            set { cells[i, j] = value; }
        }

        public bool State
        {
            get { return state; }
            set { state = value; }
        }
    }
}
