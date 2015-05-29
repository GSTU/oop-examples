using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace game
{
    public class MapBuilder
    {
        Main MBMain;
        Functions F;
        Generator random = new Generator();
        public List<int> EnabledButtons = new List<int>();
        //public MapBuilder() { }
        public MapBuilder(Main main, Functions _f) { MBMain = main; F = _f; }
        public void Create(int level)
        {
            switch (level)
            {
                /* ([уровень], [кол-во кнопок]^2, [размер кнопки], [отступ от границ], [размер цифр]) */
                case 0:  case 1:  case 2:  case 3:  Build(level, 5, 66, 1, 22);  break; // 5x5
                case 4:  case 5:  case 6:  case 7:  Build(level, 6, 55, 1, 18);  break; // 6x6
                case 8:  case 9:  case 10: case 11: Build(level, 8, 41, 2, 14);  break; // 8x8
                case 12: case 13: case 14: case 15: Build(level, 10, 33, 1, 10); break; // 10x10
                case 16: case 17: case 18: case 19: Build(level, 10, 33, 1, 10); break; // 10x10: эксперт
                default: Build(level, 5, 66, 1, 22); break; // тренировка и прочее
            }
        }
        public void Build(int level, int count, int size, int padding, float FontSizeText)
        {
            int step = 0;
            F.FONTSIZETEXT = FontSizeText;
            F.ALLBUTTONS = count * count;
            F.K = new GhostButton[count, count];
            for (int i = 0; i < count; i++)
                for (int j = 0; j < count; j++)
                {
                    F.K[i, j] = new GhostButton();
                    F.K[i, j].Location = new Point(padding + i * size, padding + j * size);
                    if (level < 15) F.K[i, j].Text = Convert.ToString(random.GetNumber(1, 10));
                    else F.K[i, j].Text = Convert.ToString(random.GetNumber(10, 100));
                    F.K[i, j].Size = new Size(size, size);
                    F.K[i, j].Color = Color.Black;
                    F.K[i, j].Enabled = true;
                    F.K[i, j].Tag = step;
                    F.K[i, j].Name = step.ToString();
                    F.K[i, j].Font = new Font("Verdana", FontSizeText, FontStyle.Regular);
                    F.K[i, j].Click += new EventHandler(PressButton);
                    MBMain.GamePanel.Controls.Add(F.K[i, j]);
                    step += 1;
                }
        }
        public void PressButton(object sender, EventArgs e)
        {
            F.SumPress(((GhostButton)sender).Color, ((GhostButton)sender).Text, sender);
            if (F.CheckSum(MBMain.EnList))    //Обновляем данные и создаём глобальную сумму поиска
            {
                F.GLGenSum(MBMain.EnList, MBMain.LEVEL);
            }
        }
        public List<int> GetEnabledButtons()
        { 
            foreach (GhostButton But in F.K)
            {
                if (But.Enabled == true) 
                { 
                    EnabledButtons.Add(Convert.ToInt32(But.Text));
                }
            }
            return EnabledButtons;
        }
    }
}
