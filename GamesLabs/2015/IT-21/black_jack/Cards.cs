using System;
using System.Collections.Generic;
using System.Text;

namespace black_jack
{
    class Cards
    {
        private int [] card = new int[36] ;
        Random rnd = new Random();


        public  Cards()
        {
            for (int i = 0; i < 36; i++)
                card[i] = 0;
        }

        public int Human()
        {
            int num;
            do
                num = rnd.Next(0, 36);
            while (CheckNum(num) != true);
            card[num] = 1;
            return num;

        }

        public int Comp()
        {
            int num;
            do
                num = rnd.Next(0, 36);
            while (CheckNum(num) != true);
            card[num] = 1;
            return num;
        }

        private bool CheckNum(int num)
        {
                if (card[num] == 1)
                   return false;
            return true;
        }


    }
}
