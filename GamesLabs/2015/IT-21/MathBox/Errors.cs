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
    public partial class Errors : Form
    {
        public Errors(string TextError)
        {
            InitializeComponent();

            InfoError.Text = TextError + "\r\n\r\nP.S.: пожалуйста, сообщите о данной ошибке разработчику игры. Спасибо за помощь! :)";
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
