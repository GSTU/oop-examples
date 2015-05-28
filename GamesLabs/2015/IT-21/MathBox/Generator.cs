using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game
{
    public class Generator
    {
        private Random R = new Random();
        public Generator() 
        { 
            
        }
        public int GetNumber(int left, int right)
        {
            return R.Next(left, right);
        }
    }
}
