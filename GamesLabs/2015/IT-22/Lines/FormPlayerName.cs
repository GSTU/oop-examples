
using System;
using System.Drawing;
using System.Windows.Forms;

namespace linesk
{
    /// <summary>
    /// Диалоговое окно изменения имени игрока
    /// </summary>
    public partial class FormPlayerName : Form
    {
        public FormPlayerName()
        {
            InitializeComponent();
        }

        GraphItem graphItemIcon = null;
        public string PlayerName = "Игрок";
        

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            PlayerName = textBoxName.Text;
        }

        private void FormPlayerName_Load(object sender, EventArgs e)
        {
            textBoxName.Text = PlayerName;          // Имя текущего игрока
            graphItemIcon = new GraphItem(this);    // Графэлемент в качестве иконки
            graphItemIcon.Visible = true;           // Геометрическая фигура видимая
            graphItemIcon.Color = Color.Blue;       // Цвет фигуры
            graphItemIcon.Active = true;            // Фигура пульсирует
            graphItemIcon.CellCoordinate = new Rectangle(10, 10, 80, 80); // положение и размеры ячейки графэлемента
        }

        
        private void FormPlayerName_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            graphItemIcon.Draw(g);
        }
    }
}
