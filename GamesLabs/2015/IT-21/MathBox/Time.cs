using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game
{
    public class Time
    {
        public int MINUTES { get; set; }
        public int SECONDS { get; set; }
        public int WASMINUTES { get; set; }
        public int WASSECONDS { get; set; }
        public Time(int level)
        {
            switch(level)
            {
                case 0: { MINUTES = 3; SECONDS = 00; } break;
                case 1: { MINUTES = 1; SECONDS = 30; } break;
                case 2: { MINUTES = 3; SECONDS = 00; } break;
                case 3: { MINUTES = 1; SECONDS = 30; } break;

                case 4: { MINUTES = 3; SECONDS = 00; } break;
                case 5: { MINUTES = 1; SECONDS = 30; } break;
                case 6: { MINUTES = 3; SECONDS = 00; } break;
                case 7: { MINUTES = 1; SECONDS = 30; } break;

                case 8: { MINUTES = 3; SECONDS = 00; } break;
                case 9: { MINUTES = 1; SECONDS = 30; } break;
                case 10: { MINUTES = 3; SECONDS = 00; } break;
                case 11: { MINUTES = 1; SECONDS = 30; } break;

                case 12: { MINUTES = 3; SECONDS = 00; } break;
                case 13: { MINUTES = 1; SECONDS = 30; } break;
                case 14: { MINUTES = 3; SECONDS = 00; } break;
                case 15: { MINUTES = 1; SECONDS = 30; } break;

                case 16: { MINUTES = 10; SECONDS = 00; } break;
                case 17: { MINUTES = 5; SECONDS = 00; } break;
                case 18: { MINUTES = 10; SECONDS = 00; } break;
                case 19: { MINUTES = 5; SECONDS = 00; } break;

                default: { MINUTES = 30; SECONDS = 30; } break;
            }
        }
        public void DecSec()
        {
            SECONDS--;
            WASSECONDS++;
        }
        public void CheckSec()
        {
            if (SECONDS == -1)
            {
                MINUTES -= 1;
                SECONDS = 59;
            }
            if (WASSECONDS == 61)
            {
                WASMINUTES += 1;
                WASSECONDS = 00;
            }
        }
        public bool CheckMin()
        {
            if (MINUTES == -1)
                return true;
            else return false;
        }
        public string GetTime()
        {
            string m = MINUTES.ToString(), s = SECONDS.ToString();   //Задаём переменные
            if (MINUTES < 10) m = "0" + m;                           //Делаем минуты 0X, еслли там однозначное число
            if (SECONDS < 10) s = "0" + s;                           //Делаем секунды 0X, еслли там однозначное число

            return  "00:" + m + ":" + s;                   //Выводим оставшееся время на экран
        }
        public string WasGetTime()
        {
            string m = WASMINUTES.ToString(), s = WASSECONDS.ToString();   //Задаём переменные
            if (WASMINUTES < 10) m = "0" + m;                           //Делаем минуты 0X, еслли там однозначное число
            if (WASSECONDS < 10) s = "0" + s;                           //Делаем секунды 0X, еслли там однозначное число

            return "00:" + m + ":" + s;                   //Выводим оставшееся время на экран
        }
    }
}
