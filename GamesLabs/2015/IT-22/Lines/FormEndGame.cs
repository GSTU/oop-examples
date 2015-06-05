

using System;
using System.Drawing;
using System.Windows.Forms;

namespace linesk
{
    /// <summary>
    /// Диалоговое окно окончания игры, появляется после окончания игры.
    /// </summary>
    public partial class FormEndGame : Form
    {
        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        /// <param name="score">количество набранных игроком очков</param>
        public FormEndGame(int score)
        {
            InitializeComponent();

            // Перед показом окна подготовим
            // заключительную информацию для игрока.
            labelTablo.Text = "Игра закончена!\n";
            labelTablo.Text += "Вы набрали " + score.ToString() + " очков!\n\n";

            labelTablo.Text += "Продолжить игру?";
        }

        GraphItem graphItemIcon = null;

        private void FormEndGame_Load(object sender, EventArgs e)
        {
            InitGraphItem(ref graphItemIcon, labelPlaceGraphItem);
        }

        /// <summary>
        /// Инициализация использования графэлемента в качестве анимационной иконки.
        /// </summary>
        /// <param name="graphItem">графэлемент</param>
        /// <param name="parent">окно рисования</param>
        void InitGraphItem(ref GraphItem graphItem, Control parent)
        {
            graphItem = new GraphItem(parent);
            graphItem.Visible = true;
            graphItem.Color = Color.Red;
            graphItem.Active = true;
            graphItem.CellCoordinate = new Rectangle(-1, -1, parent.Width, parent.Height);
        }

        private void labelPlaceGraphItem_Paint(object sender, PaintEventArgs e)
        {
            graphItemIcon.Draw(e.Graphics);
        }
    }
}
