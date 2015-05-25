using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mine_Sweeper
{
    public partial class MainForm : Form
    {
        private readonly Location start = new Location(25, 75);
        public int cellSize = 25;

        private DateTime now;

        public int size = 16, minesCount = 40;

        private bool game = false;

        private Panel[,] panels;
        private Field field;

        public MainForm()
        {
            InitializeComponent();

            new SettingsForm(this).ShowDialog();
        }

        public void Startup()
        {
            Resize();

            panels = new Panel[size, size];

            field = new Field(size, minesCount);
            field.PlantMines();
            field.CountMines();

            AddPanels();

            game = true;

            now = DateTime.Now;

            timer.Start();
        }
        private void Resize()
        {
            this.Width = size * cellSize + 50 + 16;
            this.Height = size * cellSize + 100 + 39;

            this.timerLabel.Location = new Point(this.DisplayRectangle.Width / 2 - this.timerLabel.Width / 2, 35);
        }
        private void DeletePanels()
        {
            for (int i = 0; i < Math.Sqrt(panels.Length); ++i)
                for (int j = 0; j < Math.Sqrt(panels.Length); ++j)
                    this.Controls.Remove(panels[i, j]);
        }
        private void AddPanels()
        {
            if (panels != null)
                DeletePanels();

            int left = start.X,
                top = start.Y;

            for (int i = 0; i < Math.Sqrt(panels.Length); ++i)
            {
                left = start.X;

                for (int j = 0; j < Math.Sqrt(panels.Length); ++j)
                {
                    panels[i, j] = new Panel();

                    panels[i, j].Size = new Size(cellSize, cellSize);
                    panels[i, j].Location = new Point(left, top);
                    panels[i, j].BorderStyle = BorderStyle.Fixed3D;
                    panels[i, j].MouseClick += new MouseEventHandler(panel_Click);

                    left += cellSize;

                    this.Controls.Add(panels[i, j]);
                }

                top += cellSize;
            }

            Redraw();
        }
        private void Redraw()
        {
            for (int i = 0; i < Math.Sqrt(panels.Length); ++i)
            {
                for (int j = 0; j < Math.Sqrt(panels.Length); ++j)
                {
                    if (field.Cells[i, j].State == CellState.Closed)
                        panels[i, j].BackColor = Color.Gray;
                    else if (field.Cells[i, j].State == CellState.Opened)
                    {
                        if (field.Cells[i, j].Type == CellType.Mine)
                        {
                            Graphics g = panels[i, j].CreateGraphics();

                            g.Clear(Color.Red);
                            g.FillEllipse(new SolidBrush(Color.Black), 2, 2, cellSize - 9, cellSize - 9);
                        }
                        else
                        {
                            Graphics g = panels[i, j].CreateGraphics();

                            Label l = new Label();

                            l.BackColor = Color.Azure;

                            l.Text = field.Cells[i, j].MinesAround.ToString();

                            panels[i, j].Controls.Add(l);
                        }
                    }
                    else
                        panels[i, j].BackColor = Color.Blue;
                }
            }
        }
        private void CheckForVictory()
        {
            int opened = 0;
            int marked = 0;

            foreach (Cell cell in field.Cells)
            {
                if ((cell.State == CellState.Marked) && (cell.Type == CellType.Mine))
                    ++marked;

                if ((cell.State == CellState.Opened) && (cell.Type == CellType.Empty))
                    ++opened;
            }

            if (field.Cells.Length == opened + marked)
            {
                game = false;

                foreach (Cell cell in field.Cells)
                    if (cell.Type == CellType.Mine)
                        cell.State = CellState.Opened;

                Redraw();

                timer.Stop();

                MessageBox.Show("Вы выйграли");
            }
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Stop();
            this.timerLabel.Text = "00:00";

            if (panels != null)
                DeletePanels();

            new SettingsForm(this).ShowDialog();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            timerLabel.Text = string.Format("{0:mm:ss}", new DateTime().Add(DateTime.Now - now));

            this.timerLabel.Location = new Point(this.DisplayRectangle.Width / 2 - this.timerLabel.Width / 2, 35);
        }
        private void panel_Click(object sender, MouseEventArgs e)
        {
            if (game)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    for (int i = 0; i < Math.Sqrt(panels.Length); ++i)
                    {
                        for (int j = 0; j < Math.Sqrt(panels.Length); ++j)
                        {
                            if (panels[i, j] == sender as Panel)
                            {
                                field.Cells[i, j].State = CellState.Opened;

                                for (int x = field.Cells[i, j].Border.Start.X; x <= field.Cells[i, j].Border.End.X; ++x)
                                {
                                    for (int y = field.Cells[i, j].Border.Start.Y; y <= field.Cells[i, j].Border.End.Y; ++y)
                                    {
                                        if ((field.Cells[x, y].Type != CellType.Mine) && (field.Cells[x, y].MinesAround == 0))
                                        {
                                            field.Cells[x, y].State = CellState.Opened;
                                        }
                                    }
                                }

                                Redraw();

                                if (field.Cells[i, j].Type == CellType.Mine)
                                {
                                    foreach (Cell cell in field.Cells)
                                        if (cell.Type == CellType.Mine)
                                            cell.State = CellState.Opened;

                                    Redraw();

                                    timer.Stop();

                                    game = false;

                                    MessageBox.Show("Вы проиграли");

                                    return;
                                }

                                CheckForVictory();

                                return;
                            }
                        }
                    }
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    for (int i = 0; i < Math.Sqrt(panels.Length); ++i)
                    {
                        for (int j = 0; j < Math.Sqrt(panels.Length); ++j)
                        {
                            if (panels[i, j] == sender as Panel)
                            {
                                if (field.Cells[i, j].State == CellState.Marked)
                                    field.Cells[i, j].State = CellState.Closed;
                                else
                                    field.Cells[i, j].State = CellState.Marked;

                                Redraw();

                                CheckForVictory();

                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}