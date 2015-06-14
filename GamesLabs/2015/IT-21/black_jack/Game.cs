using System;
using System.Collections.Generic;
using System.Text;

namespace black_jack
{
    class Game
    {
        private int HumanScore=0, AIScore=0,Money,Bet=0;
        private string Name;

        public string _Name
        {
            set { Name = value; }
            get { return Name; }
        }
        public int _Bet
        {
            set { Bet = value; }
            get { return Bet; }
        }
        public int _Money
        {
            set { Money = value; }
            get { return Money; }
        }
        public int _HumanScore
        {
            set { HumanScore = 0; }
            get { return HumanScore; }
        }
        public int _CompScore
        {
            set { AIScore = 0; }
            get { return AIScore; }
        }

        public void Human(int num)
        {
            if (num < 4)
                HumanScore += 6;
            else if (num < 8)
                HumanScore += 7;
            else if (num < 12)
                HumanScore += 8;
            else if (num < 16)
                HumanScore += 9;
            else if (num >= 32 && num <= 35)
                if (HumanScore > 10)
                    HumanScore += 1;
                else
                    HumanScore += 11;
            else
                HumanScore += 10;

        }
        public void Comp(int num)
        {
            if (num < 4)
                AIScore += 6;
            else if (num < 8)
                AIScore += 7;
            else if (num < 12)
                AIScore += 8;
            else if (num < 16)
                AIScore += 9;
            else if (num >= 32 && num <= 35)
                if (AIScore > 10)
                    AIScore += 1;
                else
                    AIScore += 11;
            else                
                AIScore += 10;
        }

        public bool AI()
        {
            Random rnd = new Random();
            //int des=-1;

            if (AIScore < 15)
                return true;
                /*
            else if (AIScore < 15)
            {
                des = rnd.Next(0, 3);

                    if (des == 0) return false;
                    else if (des == 1) return true;
                    else return false;
            }
            else if (AIScore == 15)
            {
                des = rnd.Next(0, 3);

                    if (des == 0) return false;
                    else return true;
            }*/
           return false;
        }

        public int Winner()
        {
            if (AIScore > HumanScore && AIScore < 22) //COMP_WIN
            {
                Money -= Bet;
                return 0;
            }
            else if (AIScore == HumanScore)
                return 2;
            else
            {
                Money += Bet;
                return 1;
            }
        }
    }
}
