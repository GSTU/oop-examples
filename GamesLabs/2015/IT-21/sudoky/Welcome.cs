using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }
        Body f1 = new Body();
        fileopen f = new fileopen();
        
        private void ButtonStart_Click(object sender, EventArgs e)
        {
            f1 = new Body();
            if (f1.ShowDialog() == DialogResult.OK) { }
        }
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
