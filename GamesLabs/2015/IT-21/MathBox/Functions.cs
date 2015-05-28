using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace game
{
    public class Functions
    {
        public bool END { get; set; } 
        private Generator Gener = new Generator();
        private List<GhostButton> PressedButtons = new List<GhostButton>();
        private List<int> TempIndex = new List<int>();

        public int SUMPRESS { get; set; }
        internal GhostButton[,] K { get; set; }
        public int GLOBALSUMGEN { get;  set; }
        public int SUMBALANCE { get; set; }
        public int ALLBUTTONS { get; set; }
        public float FONTSIZETEXT { get; set; }
        public int SumPress(Color color, string value, object sender)
        {
            if (color == Color.Black)
            {
                SUMPRESS += Convert.ToInt32(value);
                VisualButton(color, sender);
                PressedButtons.Add(sender as GhostButton);
            }
            else /* if (color == Color.White) */
            {
                SUMPRESS -= Convert.ToInt32(value);
                VisualButton(color, sender);
                PressedButtons.Remove(sender as GhostButton);
            }
            return SUMPRESS;
        }
        public void GLGenSum(List<int> list, int level)
        {
            int SuccessNumber = 0, index = -1;
            switch (level)
            {
                case -1: SuccessNumber = 3; break; // Тренировка

                case 0: case 1: SuccessNumber = 2; break; // Стандартный
                case 2: case 3: SuccessNumber = 4; break;
                
                case 4: case 5: SuccessNumber = 3; break; // Средний
                case 6: case 7: SuccessNumber = 5; break;

                case 8: case 9: SuccessNumber = 3; break; // Сложный
                case 10: case 11: SuccessNumber = 5; break;

                case 12: case 13: SuccessNumber = 3; break; // Профи
                case 14: case 15: SuccessNumber = 5; break;

                case 16: case 17: SuccessNumber = 3; break; // Эксперт
                case 18: case 19: SuccessNumber = 5; break;

                default: SuccessNumber = 3; break; // на всякий случай
            }
            GLOBALSUMGEN = 0;
            Equals(list);
            if (TempIndex.Count <= SuccessNumber)
            {
                foreach (int val in list)
                {
                    GLOBALSUMGEN += val;
                }
            }
            else
            {
                for (int i = 0; i < SuccessNumber; i++)
                {
                    index = Gener.GetNumber(0, TempIndex.Count);
                    GLOBALSUMGEN += TempIndex[index];
                    TempIndex.RemoveAt(index);
                }
            }
        }
        public bool CheckSum(List<int> list)
        {   
            bool EqualSumPress = false;
            if (SUMPRESS == GLOBALSUMGEN)
            {
                CheckPressButton(list);
                ClearSumpress();
                EqualSumPress = true;
            }
            return EqualSumPress;
        }
        public void VisualButton(Color color, object sender)
        {
            if (color == Color.White)
            {
                ((GhostButton)sender).Color = Color.Black;
                ((GhostButton)sender).Font = new Font("Verdana", FONTSIZETEXT, FontStyle.Regular);
            }
            else
            {
                ((GhostButton)sender).Color = Color.White;
                ((GhostButton)sender).Font = new Font("Verdana", FONTSIZETEXT, FontStyle.Bold);
            }
        }
        private void Equals(List<int> list)
        {
            TempIndex.Clear();
            int i = 0;
            while (i<list.Count)
            {
                TempIndex.Add(list[i]);
                i++;
            }
        }
        private void ClearSumpress()
        {
            SUMPRESS = 0;
        }
        private void CheckPressButton(List<int> List)
        {
            int i, j, n = 0, step = 0;
            while (n < PressedButtons.Count)
            {
                bool fl = false;
                step = 0;
                for (i = 0; i < Convert.ToInt32(Math.Sqrt(ALLBUTTONS)) && fl==false; i++)
                    for (j = 0; j < Convert.ToInt32(Math.Sqrt(ALLBUTTONS)) && fl==false; j++)
                    {
                        if (K[i,j].Enabled == true)
                        {
                            if (K[i, j].Name == PressedButtons[n].Name)
                            {
                                List.RemoveAt(step);
                                K[i, j].Enabled = false;
                                fl = true;
                            }
                        step += 1;
                        }
                    }
                n++;
            }
            PressedButtons.ForEach(x => x.Size = new Size(0, 0)); //Эмулирует удаление со сцены кнопки путём уменьшения её размеров до 0.            
            PressedButtons.Clear();
            SUMBALANCE += GLOBALSUMGEN * 2;

            if (SUMBALANCE != 0)
            {
                if (List.Count == 0)
                {
                    END = true;
                    SUMBALANCE = 0;
                }
            }
        }

        internal string NameLevel(int Level)
        {
            string NL = "";

            /*
            string[] N = { "стандарт: ", "средний: ", "сложный: ", "профи: " };
            string L = { "5x5", "6x6", "8x8", "10x10", "AC" };
             * отказался от этого метода, так как не хочу ещё больше засорять память
             * объявлением лишних массивов и загружать всякими методами конкатенациями. */

            switch (Level)
            {
                case -1: NL = " • Тренировка :)"; break;
                case 0: NL = " • Уровень: стандарт [5х5]"; break;
                case 1: NL = " • Уровень: средний [5х5]"; break;
                case 2: NL = " • Уровень: сложный [5х5]"; break;
                case 3: NL = " • Уровень: профи [5х5]"; break;
                case 4: NL = " • Уровень: стандарт [6x6]"; break;
                case 5: NL = " • Уровень: средний [6x6]"; break;
                case 6: NL = " • Уровень: сложный [6x6]"; break;
                case 7: NL = " • Уровень: профи [6x6]"; break;
                case 8: NL = " • Уровень: стандарт [8x8]"; break;
                case 9: NL = " • Уровень: средний [8x8]"; break;
                case 10: NL = " • Уровень: сложный [8x8]"; break;
                case 11: NL = " • Уровень: профи [8x8]"; break;
                case 12: NL = " • Уровень: стандарт [10x10]"; break;
                case 13: NL = " • Уровень: средний [10x10]"; break;
                case 14: NL = " • Уровень: сложный [10x10]"; break;
                case 15: NL = " • Уровень: профи [10x10]"; break;
                case 16: NL = " • Уровень: стандарт [АС]"; break;
                case 17: NL = " • Уровень: средний [АС]"; break;
                case 18: NL = " • Уровень: сложный [АС]"; break;
                case 19: NL = " • Уровень: профи [АС]"; break;
            }
            return NL;
        }
    }
}
