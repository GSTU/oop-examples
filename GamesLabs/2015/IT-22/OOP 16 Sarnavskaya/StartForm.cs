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
    public partial class StartForm : Form
    {
        public StartForm(int x, int y)
        {
            InitializeComponent();

            this.Width = x;
            this.Height = y;
        }

       
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            Close();

        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            
            timer1.Start();
        }
    }
}
