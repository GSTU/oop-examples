
using System;
using System.Drawing;
using System.Windows.Forms;

namespace linesk
{
    // Класс модального окна настроек приложения.
    public partial class FormGameSetting : Form
    {
        /// <summary>
        /// Собственный конструктор
        /// </summary>
        /// <param name="parent">получим управление родительского окна</param>
        public FormGameSetting(Control parent)
        {
            InitializeComponent();
            formParent = parent;
        }

        Control formParent = null;
        private void FormGameSetting_Load(object sender, EventArgs e)
        {
            switch (CurrentGraphItem)
            {
                case GraphItem.TypeGraphItem.tEllipse:
                    radioButtonEllipse.Checked = true;
                    break;
                case GraphItem.TypeGraphItem.tRectangle:
                    radioButtonRectangle.Checked = true;
                    break;
                case GraphItem.TypeGraphItem.tRhombus:
                    radioButtonRhombus.Checked = true;
                    break;
            }
        }

      public Color[] ColorGraphItems = new Color[Global.AmountColorBalls];
      public GraphItem.TypeGraphItem CurrentGraphItem;
        /// <summary>
        /// Выбор вида геометрической фигуры.
        /// </summary>
        private void radioButtonsGraphTypeItem_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked == true)
            {
                if (rb.Equals(radioButtonEllipse) == true)
                {
                    GraphItem.CurrentTypeGraphItem = GraphItem.TypeGraphItem.tEllipse;
                }
                else if (rb.Equals(radioButtonRectangle) == true)
                {
                    GraphItem.CurrentTypeGraphItem = GraphItem.TypeGraphItem.tRectangle;
                }
                else
                {
                    GraphItem.CurrentTypeGraphItem = GraphItem.TypeGraphItem.tRhombus;
                }

                formParent.Invalidate();
            }

        }

        
        /// <summary>
        /// Если пользователь выбрал тип графэлемента,
        /// закрепим выбор нажатием кнопки ОК и обновим главное окно.
        /// </summary>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            CurrentGraphItem = GraphItem.CurrentTypeGraphItem;
            formParent.Invalidate();
        }

        /// <summary>
        /// Если выбор отменен, возращаем предыдущие настройки.
        /// Благодаря статической переменной GraphItem.CurrentTypeGraphItem
        /// мы легко возвращаем прежний вид геометрической фигуры.
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            GraphItem.CurrentTypeGraphItem = CurrentGraphItem;
            formParent.Invalidate();
        }

        private void FormGameSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Если окно просто закрыли, приравниваем это
            // к отмене выбранных настроек.
            buttonCancel_Click(null, null);
        }

    }
}
