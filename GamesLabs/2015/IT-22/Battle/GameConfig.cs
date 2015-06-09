using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battle
{
    public class GameConfig
    {
        public int Attack;
        public int Health;

        public GameConfig(int attack, int health)
        {
            this.Attack = attack;
            this.Health = health;
        }
    }
}
