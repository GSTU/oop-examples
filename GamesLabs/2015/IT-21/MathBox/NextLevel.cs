using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace game
{
    public partial class NextLevel : Form
    {
        public NextLevel(Time T, int B, string NL)
        {
            InitializeComponent();
            
            NameLevel.Text = NL;
            Balance.Text = B.ToString();
            Time.Text = T.WasGetTime().ToString();

            if (NL == " • Уровень: профи [АС]") OK.Text = "Пройти его ещё раз?";
        }

        private void CloseWindow_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
