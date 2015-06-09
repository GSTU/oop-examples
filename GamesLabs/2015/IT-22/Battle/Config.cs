using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace battle
{
    public class Config
    {
        public GameConfig PlayerConfig;
        public GameConfig AiConfig;
        public Color Bgcolor;
        

        public Config(GameConfig player, GameConfig ai, Color bgcolor)
        {
            PlayerConfig = player;
            AiConfig = ai;
            Bgcolor = bgcolor;
        }

        //public override string ToString()
        //{
        //    return String.Format("{0}:{1}\n{2}:{3}", PikeConfig.Speed, PikeConfig.Vision, CarpConfig.Speed, CarpConfig.Vision);
        //}
    }
}
