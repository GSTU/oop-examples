using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace game
{
    class LevelBuilder
    {
        Main LBMain;
        public GhostCheckbox[] CBL = new GhostCheckbox[20];
        private string CHECKED_ID { get; set; }
        public LevelBuilder(Main main) { LBMain = main; }

        public string[] CBLLevelName = { "стандарт", "средний", "сложный", "профи", "стандарт", "средний", "сложный", "профи" };
        private int[,] CBLLocation = new int [,] {{45, 34},{45, 51},{45, 68},{45, 85}, //располодение чекбоксов для уровней
                                                  {45, 28},{45, 45},{207, 28},{207, 45}}; //расп. ЧБ для эксперта
        public void CheckBoxLevels()
        {
            int step = 0;
            for (int i = 0; i < CBL.Length; i++, step++)
            {
                if (i < 16) if (step == 4) step = 0;

                /* Создание CheckBox'a и его свойств */
                CBL[i] = new GhostCheckbox();
                CBL[i].Tag = i;
                CBL[i].TabIndex = i;
                CBL[i].Checked = false;
                CBL[i].Transparent = false;
                CBL[i].Colors = new Bloom[0];
                CBL[i].Size = new Size(100, 17);
                CBL[i].Text = CBLLevelName[step];
                CBL[i].Name = i.ToString();
                CBL[i].Cursor = System.Windows.Forms.Cursors.Hand;
                CBL[i].Font = new System.Drawing.Font("Verdana", 8F);
                CBL[i].Anchor = System.Windows.Forms.AnchorStyles.None;
                CBL[i].Click += new System.EventHandler(CBLL_Click);
                CBL[i].Location = new Point(CBLLocation[step, 0], CBLLocation[step, 1]);
                CBL[i].Click += new EventHandler(RecordUpdate);

                /* Добавление CheckBox'a на соответствующий GroupBox */
                if (i <= 3) LBMain.x5.Controls.Add(CBL[i]);
                else if (i >= 4 && i <= 7) LBMain.x6.Controls.Add(CBL[i]);
                else if (i >= 8 && i <= 11) LBMain.x8.Controls.Add(CBL[i]);
                else if (i >= 12 && i <= 15) LBMain.x10.Controls.Add(CBL[i]);
                else if (i >= 16 && i <= 19) LBMain.expert.Controls.Add(CBL[i]);
            }
        }
        public void CBLL_Click(object sender, EventArgs e)
        {
            CheckOnChecked(sender, e);
            int i = 0;
            while (i < CBL.Length && CBL[i].Checked != true)
            {
                i++;
            }

            if (i <= CBL.Length)
            {
                int l = Convert.ToInt32(((GhostCheckbox)sender).Name);
                LBMain.LEVEL = l;
                Time T = new Time(l);
                T.GetTime().ToString();
            }
        }
        private void CheckOnChecked(object sender, EventArgs e)
        {
            CHECKED_ID = ((GhostCheckbox)sender).Name;
            foreach (GhostCheckbox Check in CBL)
            {
                if (Check.Name != CHECKED_ID) Check.Checked = false;
            }
            if (CBL.Where(x => x.Checked == false).Count() == CBL.Length) //Проверка: все ли чекбоксы пусты
            {
                LBMain.CurtainButtonPanel.Visible = true;
            }
            else 
            {
                LBMain.CurtainButtonPanel.Visible = false;
            }
        }

        private void RecordUpdate(object sender, EventArgs e)
        {
            LBMain.RecordPoints.Text = LBMain.ForPoints[Convert.ToInt32(((GhostCheckbox)sender).Tag)].ToString();
            LBMain.RecordTime.Text = LBMain.ForTime[Convert.ToInt32(((GhostCheckbox)sender).Tag)].ToString();
        }
    }
}
