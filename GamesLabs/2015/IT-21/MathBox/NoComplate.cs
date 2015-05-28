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
    public partial class NoComplate : Form
    {
        public NoComplate(int B, string NL)
        {
            InitializeComponent();
            
            NameLevel.Text = NL;
            Balance.Text = B.ToString();
        }

        private void CloseWindow_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
