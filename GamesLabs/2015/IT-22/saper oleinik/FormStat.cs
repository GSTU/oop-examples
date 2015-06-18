using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Saper
{
    public partial class FormStat : Form
    {
        public FormStat()
        {
            InitializeComponent();
        }
        int w, l;
        
        private void FormStat_Load(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            label1.Text = Convert.ToString(f1.win);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
