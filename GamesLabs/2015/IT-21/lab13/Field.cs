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

namespace lab13
{

    class Field
    {   
        int kof;                        //коэффициент для рисования линий
        int currentFigure = 2;          //текущая фигура (2 - никакая)
        int count = 0;    //количество повторений одинаковых фигур подряд
        private static int winCount = 5; //количество фигур для победы //5
        private static int fieldSize  = 13; //размер поля  //13

        public Field (int size, int win)
        {
            fieldSize = size;
            winCount = win;
            matrix = new int[fieldSize, fieldSize];        //13,13               //значения -1, 0, 1
        }

        public int FieldSize
        {
            set { fieldSize = value;}
            get { return fieldSize; }
        }

        public int WinCount
        {
            set { fieldSize = value; }
            get { return winCount; }
        }

        public int sec = 0;    //секунды
        public int min = 0;    //минуты
        public int milSec = 0;    //миллисекунды

        public int[,] matrix; 
        //-1 - нолик
        //0 - пусто
        //1 - крестик
        public int steps = 0;            //количество шагов
        private bool win = false;        //флаг победы
        private bool deadHeat = false;   //ничья

        public bool Win
        {
            get { return win; }
            set { win = value; }
        }

        public bool DeadHeat
        {
            get { return deadHeat; }
            set { deadHeat = value; }
        }
        

        public void miniCheck(int[,] matrix, ref int currentFigure, int i, int j, ref int count, ref bool win)
        {
            if (count != winCount && steps == fieldSize*fieldSize)
            {
                deadHeat = true;
            }
            else if (matrix[i, j] == currentFigure)
            {
                count++;
                if (count == winCount)
                    win = true;
            }
            else if (matrix[i, j] != 0)
            {
                currentFigure = matrix[i, j];
                count = 1;
            }
            else
            {
                currentFigure = 2;
                count = 0;
            }
        }

        public void HorisontalCheck(Image crossH, Image zeroH, DataGridView dataGridView1)
        {
            count = 0;
            currentFigure = 2;
            //горизонтальная проверка
            int i = 0, j = 0;
            while (win != true && deadHeat !=true && i < fieldSize)
            {
                j = 0;
                while (win != true && j < fieldSize)
                {
                    miniCheck(matrix, ref currentFigure, i, j, ref count, ref win);
                    j++;
                }
                if (win != true && j == fieldSize)
                    currentFigure = 2;
                i++;
            }
            if (win == true && currentFigure == 1)
            {
                i--;
                j--;
                kof = j;
                while (j != kof - winCount && j >= 0)
                {
                    dataGridView1[j, i].Value = new Bitmap(crossH, 40, 40);
                    j--;
                }

            }
            else if (win == true && currentFigure == -1)
            {
                i--;
                j--;
                kof = j;
                while (j != kof - winCount && j >= 0)
                {
                    dataGridView1[j, i].Value = new Bitmap(zeroH, 40, 40);
                    j--;
                }
            }
        }

        public void VerticalCheck(Image crossV, Image zeroV, DataGridView dataGridView1)
        {
            count = 0;
            currentFigure = 2;
            //вертикальная проверка
            int i, j;
            i = 0;
            j = 0;
            while (win != true && deadHeat != true && j < fieldSize)
            {
                i = 0;
                while (win != true && i < fieldSize)
                {
                    miniCheck(matrix, ref currentFigure, i, j, ref count, ref win);
                    i++;
                }
                if (win != true && i == fieldSize)
                    currentFigure = 2;
                j++;
            }
            if (win == true && currentFigure == 1)
            {
                j--;
                i--;
                kof = i;
                while (i != kof - winCount && i >= 0)
                {
                    dataGridView1[j, i].Value = new Bitmap(crossV, 40, 40);
                    i--;
                }

            }
            else if (win == true && currentFigure == -1)
            {
                j--;
                i--;
                kof = i;
                while (i != kof - winCount && i >= 0)
                {
                    dataGridView1[j, i].Value = new Bitmap(zeroV, 40, 40);
                    i--;
                }
            }
        }

        public void DiagonalLeftTopCheck(Image zeroGl, Image crossGl, DataGridView dataGridView1)
        {
            //диагональная проверка c 0,0 и вправо (верхняя часть)
            currentFigure = 2;
            count = 0;
            int i, j;
            i = j = 0;
            int n = fieldSize;
            while (win != true && deadHeat != true && j < fieldSize - winCount + 1)
            {
                while (win != true && i < n)
                {
                    miniCheck(matrix, ref currentFigure, i, j, ref count, ref win);
                    i++;
                    j++;
                }
                i--;
                j--;

                //drawlines
                //
                //
                if (win == true && currentFigure == 1)
                {
                    kof = i;
                    while (i != kof - winCount && i >= 0)
                    {
                        dataGridView1[j, i].Value = new Bitmap(crossGl, 40, 40);
                        i--;
                        j--;
                    }

                }
                else if (win == true && currentFigure == -1)
                {
                    kof = i;
                    while (i != kof - winCount && i >= 0)
                    {
                        dataGridView1[j, i].Value = new Bitmap(zeroGl, 40, 40);
                        i--;
                        j--;
                    }
                }
                //
                //
                //
                if (win != true)
                {
                    n--;
                    i--;
                }
                while (win != true && i >= 0)
                {
                    miniCheck(matrix, ref currentFigure, i, j, ref count, ref win);
                    i--;
                    j--;
                }
                i++;
                j++;

                if (win == true && currentFigure == 1)
                {
                    kof = i;
                    while (i != kof + winCount)
                    {
                        dataGridView1[j, i].Value = new Bitmap(crossGl, 40, 40);
                        i++;
                        j++;
                    }

                }
                else if (win == true && currentFigure == -1)
                {
                    kof = i;
                    while (i != kof + winCount) //!=
                    {
                        dataGridView1[j, i].Value = new Bitmap(zeroGl, 40, 40);
                        i++;
                        j++;
                    }
                }
                if (win != true)
                {
                    j++;
                    n--;
                }
            }
        }

        public void DiagonalLeftBottomCkeck(Image zeroGl, Image crossGl, DataGridView dataGridView1)
        {
            //диагональная проверка c 0,0 и вправо (нижняя часть)
            currentFigure = 2;
            count = 0;
            int i, j;
            j = 0;
            i = 1;
            int n;
            n = fieldSize-1;
            while (win != true && deadHeat != true && i < fieldSize - winCount + 1)
            {
                while (win != true && j < n)
                {
                    miniCheck(matrix, ref currentFigure, i, j, ref count, ref win);
                    i++;
                    j++;
                }
                i--;
                j--;

                //
                //
                //расстановка красныъх линий
                if (win == true && currentFigure == 1)
                {
                    int kof = j;
                    while (j != kof - winCount && j >= 0)
                    {
                        dataGridView1[j, i].Value = new Bitmap(crossGl, 40, 40);
                        j--;
                        i--;
                    }

                }
                else if (win == true)
                {
                    int kof = j;
                    while (j != kof - winCount && j >= 0)
                    {
                        dataGridView1[j, i].Value = new Bitmap(zeroGl, 40, 40);
                        j--;
                        i--;
                    }
                }
                //конец расстановки
                //
                //
                if (win != true)
                {
                    n--;
                    j--;
                }
                while (win != true && j >= 0)
                {
                    miniCheck(matrix, ref currentFigure, i, j, ref count, ref win);
                    i--;
                    j--;
                }
                i++;
                j++;
                //
                //
                //расстановка красныъх линий
                if (win == true && currentFigure == 1)
                {
                    int kof = j;
                    while (j != kof + winCount)
                    {
                        dataGridView1[j, i].Value = new Bitmap(crossGl, 40, 40);
                        j++;
                        i++;
                    }

                }
                else if (win == true)
                {
                    int kof = j;
                    while (j != kof + winCount)
                    {
                        dataGridView1[j, i].Value = new Bitmap(zeroGl, 40, 40);
                        j++;
                        i++;
                    }
                }
                //конец расстановки
                //
                //
                if (win != true)
                {
                    i++;
                    n--;
                }
            }
        }

        public void DiagonalRightTopCheck(Image zeroGr, Image crossGr, DataGridView dataGridView1)
        {
            //диагональная проверка с 0,12 влево (верхняя часть)
            currentFigure = 2;
            count = 0;
            int i, j;
            int n;
            i = 0;
            j = fieldSize-1;
            n = fieldSize;
            while (win != true && deadHeat != true && j > winCount - 2)
            {
                while (win != true && i < n)
                {
                    miniCheck(matrix, ref currentFigure, i, j, ref count, ref win);
                    i++;
                    j--;
                }
                i--;
                j++;
                //
                //
                //расстановка красныъх линий
                if (win == true && currentFigure == 1)
                {
                    int kof = j;
                    while (j != kof + winCount)
                    {
                        dataGridView1[j, i].Value = new Bitmap(crossGr, 40, 40);
                        j++;
                        i--;
                    }

                }
                else if (win == true)
                {
                    int kof = j;
                    while (j != kof + winCount)
                    {
                        dataGridView1[j, i].Value = new Bitmap(zeroGr, 40, 40);
                        j++;
                        i--;
                    }
                }
                //конец расстановки
                //
                //
                if (win != true)
                {
                    n--;
                    i--;
                }
                while (win != true && i >= 0)
                {
                    miniCheck(matrix, ref currentFigure, i, j, ref count, ref win);
                    i--;
                    j++;
                }
                i++;
                j--;
                //
                //
                //расстановка красныъх линий
                if (win == true && currentFigure == 1)
                {
                    int kof = j;
                    while (j != kof - winCount && j >= 0)
                    {
                        dataGridView1[j, i].Value = new Bitmap(crossGr, 40, 40);
                        j--;
                        i++;
                    }

                }
                else if (win == true)
                {
                    int kof = j;
                    while (j != kof - winCount && j >= 0)
                    {
                        dataGridView1[j, i].Value = new Bitmap(zeroGr, 40, 40);
                        j--;
                        i++;
                    }
                }
                //конец расстановки
                //
                //
                if (win != true)
                {
                    j--;
                    n--;
                }
            }
        }

        public void DiagonalRightBottomCheck(Image zeroGr, Image crossGr, DataGridView dataGridView1)
        {
            //диагональная проверка с 0,12 влево (нижняя часть)
            currentFigure = 2;
            count = 0;
            int i, j;
            int n;
            i = 1;
            j = fieldSize-1;
            n = 0;
            while (win != true && deadHeat != true && i < fieldSize - 3)
            {
                while (win != true && j > n)
                {
                    miniCheck(matrix, ref currentFigure, i, j, ref count, ref win);
                    i++;
                    j--;
                }
                i--;
                j++;
                //
                //
                //расстановка красныъх линий
                if (win == true && currentFigure == 1)
                {
                    int kof = j;
                    while (j != kof + winCount)
                    {
                        dataGridView1[j, i].Value = new Bitmap(crossGr, 40, 40);
                        j++;
                        i--;
                    }

                }
                else if (win == true)
                {
                    int kof = j;
                    while (j != kof + winCount)
                    {
                        dataGridView1[j, i].Value = new Bitmap(zeroGr, 40, 40);
                        j++;
                        i--;
                    }
                }
                //конец расстановки
                //
                //
                if (win != true)
                {
                    n++;
                    j++;
                }
                while (win != true && j < fieldSize)
                {
                    miniCheck(matrix, ref currentFigure, i, j, ref count, ref win);
                    i--;
                    j++;
                }
                i++;
                j--;
                //
                //
                //расстановка красныъх линий
                if (win == true && currentFigure == 1)
                {
                    int kof = j;
                    while (j != kof - winCount && j >= 0)
                    {
                        dataGridView1[j, i].Value = new Bitmap(crossGr, 40, 40);
                        j--;
                        i++;
                    }

                }
                else if (win == true)
                {
                    int kof = j;
                    while (j != kof - winCount && j >= 0)
                    {
                        dataGridView1[j, i].Value = new Bitmap(zeroGr, 40, 40);
                        j--;
                        i++;
                    }
                }
                //конец расстановки
                //
                //
                if (win != true)
                {
                    i++;
                    n++;
                }
            }
        }
    }
}
