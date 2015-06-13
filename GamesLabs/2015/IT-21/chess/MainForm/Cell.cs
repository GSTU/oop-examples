using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainForm
{
     public class Cell
    {
        public int X;
        public int Y;
        public Counters Counter;
        public Colors ColorCell;
        public const int PartyLength = 64;

        public Cell(Colors _colorCell, Counters _counters)
        {
            ColorCell = _colorCell;
            Counter = _counters;
            X = Y = 0;
        }
    }
}
