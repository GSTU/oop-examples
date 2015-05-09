using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public int wlength, hlength;
        public bool flag = false;
        private void button1_Click(object sender, EventArgs e)
        {
            wlength = Convert.ToInt32(numericUpDown1.Value);
            hlength = Convert.ToInt32(numericUpDown2.Value);
            flag = true;
            Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
