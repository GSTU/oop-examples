using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MindPuzzle
{
    public partial class ViborTime : MindPuzzle
    {
        public ViborTime()
        {
            LoadXML();

            InitializeComponent();

            RB1.Text = ForTimer[0] + " минут";
            RB2.Text = ForTimer[1] + " минут";
            RB3.Text = ForTimer[2] + " минут";
        }

        private void RB1_CheckedChanged(object sender, EventArgs e)
        {
            CheckMin = int.Parse(ForTimer[0]);
        }

        private void RB2_CheckedChanged(object sender, EventArgs e)
        {
            CheckMin = int.Parse(ForTimer[1]);
        }

        private void RB3_CheckedChanged(object sender, EventArgs e)
        {
            CheckMin = int.Parse(ForTimer[2]);
        }


    }
}
