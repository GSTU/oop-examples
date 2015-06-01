using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace Lab_rab_13
{
    public partial class MainForm : Form
    {
        int x, y, victory;
        Color background;
        public MainForm()
        {
            InitializeComponent();
            Files();
            gameGraphics = panelGame.CreateGraphics();
            stroke = panelStroke.CreateGraphics();
            sizePanel = panelGame.Height;
            sizeCellX = sizePanel / x;
            sizeCellY = sizePanel / y;
        }

        LogicGames game;
        Graphics gameGraphics;
        Graphics stroke;
        int sizePanel;
        int sizeCellX;
        int sizeCellY;
        bool zero = true;
        bool play = false;
        static bool flag = true;

        public void DrawGrid()// Метод, для прорисовки поля игры
        {
            Pen p = new Pen(Color.Green, 2);
            gameGraphics.Clear(background);
            for (int i = 0; i <= 13; i++)
            {
                gameGraphics.DrawLine(p, 0, i * sizeCellY, panelGame.Height, i * sizeCellY);
                gameGraphics.DrawLine(p, i * sizeCellX, 0, i * sizeCellX, panelGame.Height);
            }
        }

        private void ToolStripMenuItemNewGame_Click(object sender, EventArgs e)// Метод, для начала новой игры
        {
            panelGame.Enabled = true;
            zero = true;
            play = true;
            DrawGrid();
            game = new LogicGames(sizeCellX,sizeCellY,victory);
            DrawCourse(zero);
        }

        private void panelGame_MouseClick(object sender, MouseEventArgs e)// Событие, возникающее при щелчке мышкой по панеле
        {
            if (play)
            {
                int indexX = (int)Math.Truncate((double)e.X / sizeCellX);
                int indexY = (int)Math.Truncate((double)e.Y / sizeCellY);
                if (game.Check(indexX, indexY))
                {
                    DrawNoughtsAndCrosses(indexX, indexY);
                }
            }
        }

        public void DrawNoughtsAndCrosses(int x, int y)// Метод, для прорисовки крестика или нолика
        {
            if (!zero)
            {
                Pen P = new Pen(Color.Red, 3);
                gameGraphics.DrawLine(P, x * sizeCellX + 3, y * sizeCellY + 3, (x + 1) * sizeCellX - 3, (y + 1) * sizeCellY - 3);
                gameGraphics.DrawLine(P, x * sizeCellX + 3, (y + 1) * sizeCellY - 3, (x + 1) * sizeCellX - 3, y * sizeCellY + 3);
                zero = !zero;
            }
            else
            {
                Pen P = new Pen(Color.Black, 3);
                gameGraphics.DrawEllipse(P, x * sizeCellX + 3, y * sizeCellY + 3, sizeCellX - 6, sizeCellY - 6);
                zero = !zero;
            }
            game.Add(x, y, !zero);
            if (game.CheckUpAndDown(x, y, !zero) || game.CheckLeftAndRight(x, y, !zero) || game.CheckLeftUpAndRightDown(x, y, !zero) || game.CheckLeftDownAndRightUp(x, y, !zero))
            {
                stroke.Clear(Color.Bisque);
                play = false;
                if (!zero)
                    MessageBox.Show("Победили нолики", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Победили крестики", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                DrawCourse(zero);
        }

        public void DrawCourse(bool _zero)// Метод, для прорисовки какой игрок ходит
        {
            if (!_zero)
            {
                Pen P = new Pen(Color.Red, 3);
                stroke.Clear(Color.Bisque);
                stroke.DrawLine(P, 5, 5, panelStroke.Height - 5, panelStroke.Height - 5);
                stroke.DrawLine(P, 5, panelStroke.Height - 5, panelStroke.Height - 5, 5);
            }
            else
            {
                Pen P = new Pen(Color.Black, 3);
                stroke.Clear(Color.Bisque);
                stroke.DrawEllipse(P, 3, 3, panelStroke.Height - 5, panelStroke.Height - 5);
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)// Событие, возникающее при нажатии на кнопку "Выход"
        {
            Close();
        }

        public void Files()
        {
            int R, G, B;
            try
            {
                XmlReaderSettings xmlreadersettings = new XmlReaderSettings();
                xmlreadersettings.Schemas.Add(null, "xsd.xsd");
                xmlreadersettings.ValidationType = ValidationType.Schema;
                xmlreadersettings.ValidationEventHandler += new ValidationEventHandler(xmlreadersettingsValidationEventHandler);
                XmlReader xmlreader = XmlReader.Create("xml.xml", xmlreadersettings);
                while (xmlreader.Read()){}
                if (flag)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("xml.xml");
                    x = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("sizeCell").FirstChild.InnerText);
                    y = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("sizeCell").LastChild.InnerText);
                    victory = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("victoryCondition").InnerText);
                    R = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("backgroundColor").ChildNodes[0].InnerText);
                    G = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("backgroundColor").ChildNodes[1].InnerText);
                    B = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("backgroundColor").ChildNodes[2].InnerText);
                    background = ColorTranslator.FromWin32(B * 256 * 256 + G * 256 + R);
                }
                else
                {
                    x = 13;
                    y = 13;
                    victory = 5;
                    background = Color.White;

                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Ошибка: " + exception.Message);
                x = 13;
                y = 13;
                victory = 5;
                background = Color.White;
            }
        }

        static void xmlreadersettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                MessageBox.Show("Внимание: " + e.Message);
                flag = false; 
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                MessageBox.Show("Ошибка: " + e.Message);
                flag = false;
            }
        }

        private void panelGame_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Произошёл двойной клик мыши");
        }
    }
}
