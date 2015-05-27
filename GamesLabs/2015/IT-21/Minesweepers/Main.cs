using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace Minesweepers
{
    public partial class Main : Form
    {
        Mines Game;
        Graphics gph;
        Log appLog = new Log();
        string NameXml = "settings.xml";
        bool Valid;

        public Main()
        {
            InitializeComponent();
            if (!File.Exists(NameXml))
            {
                NameXml = null;
                Game = new Mines(0);
            }
            else
                Game = new Mines(0, NameXml);
            gph = panel1.CreateGraphics();
            appLog.Write("[START]");
            UpdateForm();
        }

        private void UpdateForm()
        {
            this.ClientSize = new Size(Game.CountWeight * Game.Weight + 25, Game.CountHeight * Game.Height + 60);
            panel1.ClientSize = new Size(Game.CountWeight * (Game.Weight + 3), Game.CountHeight * (Game.Height + 3));
            mineTextBox.Location = new Point(2, Game.CountHeight * Game.Height + 5);
            timeTextBox.Location = new Point(Game.CountWeight * Game.Weight - 100, Game.CountHeight * Game.Height+ 5);
            timeTextBox.Font = new Font("Tahoma", 16);
            mineTextBox.Font = new Font("Tahoma", 16);
            timeTextBox.Text = "00:00:00";
            mineTextBox.Text = (Game.CountOfMines - Game.CountOfFlags).ToString();
            ShowField(gph);
        }

        private void ShowField(Graphics g)
        {
            g.DrawLine(Pens.Black, 10, 10, 100, 100);
            for (int i = 0; i < Game.CountHeight; i++)
            {
                for (int j = 0; j < Game.CountWeight; j++)
                {
                    Game.PaintCell(gph, i, j);
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //UpdateForm();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ShowField(gph);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (Game.Status == 2) return;
                if (Game.Status == 0)
                {
                    Game.Time = DateTime.Now;
                    timer.Start();
                    Game.Status = 1;
                }

                int row = (int)(e.Y / Game.Height),
                    col = (int)(e.X / Game.Weight);
                int x = (col - 1) * Game.Weight + 1,
                    y = (row - 1) * Game.Height + 1;

                if (e.Button == MouseButtons.Left)
                {
                    if (Game.GameField[row, col].Active == 0)
                    {
                        switch (Game.GameField[row, col].Type)
                        {
                            case 2:
                                Game.Status = 2;
                                Game.Lose(gph);
                                timer.Stop();
                                MessageBox.Show("Вы проиграли!");
                                break;
                            case 1:
                                Game.GameField[row, col].Active = 1;
                                Game.PaintCell(gph, row, col);
                                break;
                            case 0:
                                Game.OpenNull(row, col, gph);
                                break;
                        }
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    switch (Game.GameField[row, col].Active)
                    {
                        case 0:
                            Game.CountOfFlags++;
                            if (Game.GameField[row, col].Type == 2)
                                Game.CountOfFindMines++;
                            Game.GameField[row, col].Active = 2;
                            break;
                        case 2:
                            Game.CountOfFlags--;
                            if (Game.GameField[row, col].Type == 2)
                                Game.CountOfFindMines--;
                            Game.GameField[row, col].Active = 3;
                            break;
                        case 3:
                            Game.GameField[row, col].Active = 0;
                            break;
                    }
                    Game.PaintCell(gph, row, col);
                }
                if (Game.CountOfFlags == Game.CountOfMines && Game.CountOfFindMines == Game.CountOfMines && Game.CountOfActiv)
                {
                    Game.Status = 3;
                    timer.Stop();
                    MessageBox.Show("Вы выйграли");
                }
                mineTextBox.Text = (Game.CountOfMines - Game.CountOfFlags).ToString();
            }
            catch (Exception ex) { appLog.Write(ex.Message); }
        }

        private void экспертToolStripMenuItem_Click(object sender, EventArgs e)
        {
            новичокToolStripMenuItem.Checked = false;
            профессионалToolStripMenuItem.Checked = false;
            экспертToolStripMenuItem.Checked = true;
            if (NameXml == null) Game = new Mines(1);
            else
                Game = new Mines(1, NameXml);
            gph.Clear(Color.White);
            timer.Stop();
            UpdateForm();
        }

        private void профессионалToolStripMenuItem_Click(object sender, EventArgs e)
        {
            новичокToolStripMenuItem.Checked = false;
            экспертToolStripMenuItem.Checked = false;
            профессионалToolStripMenuItem.Checked = true;
            if (NameXml == null) Game = new Mines(2);
            else
                Game = new Mines(2, NameXml);
            gph.Clear(Color.White);
            timer.Stop();
            UpdateForm();
        }

        private void новичокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            новичокToolStripMenuItem.Checked = true;
            профессионалToolStripMenuItem.Checked = false;
            экспертToolStripMenuItem.Checked = false;
            if (NameXml == null) Game = new Mines(0);
            else
                Game = new Mines(0, NameXml);
            gph.Clear(Color.White);
            timer.Stop();
            UpdateForm();
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NameXml != null)
            {
                if (новичокToolStripMenuItem.Checked) Game = new Mines(0, NameXml);
                else
                    if (профессионалToolStripMenuItem.Checked) Game = new Mines(2, NameXml);
                    else if (экспертToolStripMenuItem.Checked) Game = new Mines(1, NameXml);
            }
            else
            {
                if (новичокToolStripMenuItem.Checked) Game = new Mines(0);
                else
                    if (профессионалToolStripMenuItem.Checked) Game = new Mines(2);
                    else if (экспертToolStripMenuItem.Checked) Game = new Mines(1);
            }
            gph.Clear(Color.White);
            ShowField(gph);
            UpdateForm();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timeTextBox.Text = (DateTime.Now - Game.Time).ToString();
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Valid = true;
                    XmlReaderSettings gameSettings = new XmlReaderSettings();
                    gameSettings.Schemas.Add(null, "table.xsd");
                    gameSettings.ValidationType = ValidationType.Schema;
                    gameSettings.ValidationEventHandler += new ValidationEventHandler(gameSettingsValidationEventHandler);
                    XmlReader games = XmlReader.Create(openFileDialog1.FileName, gameSettings);
                    while (games.Read()) { }
                    if (!Valid)
                    {
                        NameXml = null;
                        Game = new Mines(0);
                        gph.Clear(Color.White);
                        timer.Stop();
                        throw new Exception();
                    }
                    else
                    {
                        NameXml = openFileDialog1.FileName;
                        Game = new Mines(0, NameXml);
                        gph.Clear(Color.White);
                        timer.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выбран неверный файл");
                appLog.Write(ex.Message);
            }
            finally
            {
                UpdateForm();
            }
        }

        private void gameSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            try
            {
                if(e.Severity == XmlSeverityType.Warning || e.Severity == XmlSeverityType.Error) throw new Exception();
            }
            catch (Exception ex)
            {
                Valid = false;
                appLog.Write(ex.Message);
            }
        }
    }
}
