using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battle
{
    public class Config
    {
        public GameConfig PlayerConfig;
        public GameConfig AiConfig;

        public Config(GameConfig player, GameConfig ai)
        {
            PlayerConfig = player;
            AiConfig = ai;
        }

        //public override string ToString()
        //{
        //    return String.Format("{0}:{1}\n{2}:{3}", PikeConfig.Speed, PikeConfig.Vision, CarpConfig.Speed, CarpConfig.Vision);
        //}
    }
}
