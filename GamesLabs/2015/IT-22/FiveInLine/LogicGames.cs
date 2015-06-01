using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab_rab_13
{
    class LogicGames
    {
        int?[,] table;
        int sizeCellX;
        int sizeCellY;
        int difficulty;
        public LogicGames(int _sizeCellX, int _sizeCellY, int diff)// Конструктор с парамертрами
        {
            sizeCellX = _sizeCellX;
            sizeCellY = _sizeCellY;
            difficulty = diff;
            table = new int?[sizeCellX, sizeCellY];
        }

        public int?[,] Table // Свойство, возвращающее таблицу заполнения ходов
        {
            get { return (table); }
        }

        public bool Check(int x, int y)// Метод, для проверки содержится ли в таблице значение
        {
            if (table[y, x] == null)
                return (true);
            else
                return (false);
        }

        public void Add(int x, int y, bool zero)// Метод, для добавления значения в таблицу
        {
            if (zero)
                table[y, x] = 0;
            else
                table[y, x] = 1;
        }

        public bool CheckUpAndDown(int x, int y, bool zero)// Проверка по вертикали
        {
            int element;
            if (zero)
                element = 0;
            else
                element = 1;
            int result = 0;
            for (int i = y + 1; i < 13; i++)
                if (!Check(x, i))
                    if (table[i, x] == element)
                        result++;
                    else
                        break;
                else
                    break;
            for (int i = y; i >= 0; i--)
                if (!Check(x,i))
                    if (table[i, x] == element)
                        result++;
                    else
                        break;
                else
                    break;
            if (result >= difficulty)
                return (true);
            else
                return (false);
        }

        public bool CheckLeftAndRight(int x, int y, bool zero)// Проверка по горизонтали
        {
            int element;
            if (zero)
                element = 0;
            else
                element = 1;
            int result = 0;
            for (int i = x + 1; i < 13; i++)
                if (!Check(i, y))
                    if (table[y, i] == element)
                        result++;
                    else
                        break;
                else
                    break;
            for (int i = x; i >= 0; i--)
                if (!Check(i, y))
                    if (table[y, i] == element)
                        result++;
                    else
                        break;
                else
                    break;
            if (result >= difficulty)
                return (true);
            else
                return (false);
        }

        public bool CheckLeftUpAndRightDown(int x, int y, bool zero)// Проверка с левого верхнего угла в нижний правый угол
        {
            int element;
            if (zero)
                element = 0;
            else
                element = 1;
            int result = 0;
            for (int i = y + 1, j = x + 1; i < 13 && j < 13; i++, j++)
                if (!Check(j, i))
                    if (table[i, j] == element)
                        result++;
                    else
                        break;
                else
                    break;
            for (int i = y, j = x; i >= 0 && j >= 0; i--, j--)
                if (!Check(j, i))
                    if (table[i, j] == element)
                        result++;
                    else
                        break;
                else
                    break;
            if (result >= difficulty)
                return (true);
            else
                return (false);
        }

        public bool CheckLeftDownAndRightUp(int x, int y, bool zero)// Проверка с левого нижнего угла в правый верхний угол
        {
            int element;
            if (zero)
                element = 0;
            else
                element = 0;
            int result = 0;
            for (int i = y, j = x; i >=0 && j <13; i--, j++)
                if (!Check(j, i))
                    if (table[i, j] == element)
                        result++;
                    else
                        break;
                else
                    break;
            for (int i = y+1, j = x-1; i <13 && j >=0; i++, j--)
                if (!Check(j, i))
                    if (table[i, j] == element)
                        result++;
                    else
                        break;
                else
                    break;
            if (result >= difficulty)
                return (true);
            else
                return (false);
        }
    }
}
