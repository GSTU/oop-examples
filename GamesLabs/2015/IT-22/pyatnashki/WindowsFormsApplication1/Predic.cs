using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class Predic            //Предикат для определения пустой клетки в методе Array.FindIndex
    {
        public bool IsNul(int v)
        {
            if (v == 0) return true;
            return false;
        }

    }
}
