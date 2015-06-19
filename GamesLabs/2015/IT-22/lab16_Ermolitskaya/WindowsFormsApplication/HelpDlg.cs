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
    // Класс диалогового окна помощи пользователю
    public partial class HelpDlg : Form
    {
        public HelpDlg()
        {
            InitializeComponent();
        }

        public Image ImageDuplicate = null;

        // Загрузка картинки на форму посредством PictureBox.
        private void Form3_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = ImageDuplicate;
        }

        

    }
}
