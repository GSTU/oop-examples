using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweepers
{
    class Cell
    {
        /// <summary>
        /// Тип клетки (0 - пустая клетка, 1 - число в клетке, 2 - мина)
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Номер числа
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Статус клетки (0 - неактивная клетка, 1 - активная клетка, 2 - флаг, 3 - вопрос)
        /// </summary>
        public int Active { get; set; }
    }
}