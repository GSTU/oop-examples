using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mosaic
{
    // Класс диалогового окна настроек программы
    public partial class SetDlg : Form
    {
        public SetDlg()
        {
            InitializeComponent();
        }

        public int LengthSides = 3;

        // Загрузка предыдущих настроек.
        private void SetDlg_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = LengthSides;
        }

        // Изменение настроек программы.
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            LengthSides = (int)numericUpDown1.Value;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

        }

        
    }
}
