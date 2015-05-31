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
    public partial class Choice : Form
    {
        public Choice()
        {
            InitializeComponent();
        }
        private string figure;

        private void strn(string str)
        {
            figure = str;
            this.DialogResult = DialogResult.OK;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            strn("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            strn("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            strn("3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            strn("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            strn("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            strn("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            strn("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            strn("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            strn("9");
        }

        public string Figure()
        {
            return figure;
        }

        private void Choice_Load(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            figure = null;
            this.Close();
        }
    }
}
