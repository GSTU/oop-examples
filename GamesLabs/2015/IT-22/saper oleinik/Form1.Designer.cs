using System.Windows.Forms;

namespace Saper
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (exitFromMenu)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
            else
            {
                DialogResult res = MessageBox.Show("", "Завершить игру?", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    if (disposing && (components != null))
                    {
                        components.Dispose();
                    }
                    base.Dispose(disposing);
                }
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.дополнительноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новаяИграToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.новичокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.любительToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.профессионалToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкаСложностиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проПрограммуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.дополнительноToolStripMenuItem,
            this.c});
            this.menuStrip1.Location = new System.Drawing.Point(0, 171);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(249, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // дополнительноToolStripMenuItem
            // 
            this.дополнительноToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новаяИграToolStripMenuItem,
            this.toolStripSeparator2,
            this.новичокToolStripMenuItem,
            this.любительToolStripMenuItem,
            this.профессионалToolStripMenuItem,
            this.настройкаСложностиToolStripMenuItem,
            this.toolStripSeparator1,
            this.выходToolStripMenuItem});
            this.дополнительноToolStripMenuItem.Name = "дополнительноToolStripMenuItem";
            this.дополнительноToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.дополнительноToolStripMenuItem.Text = "Игра";
            // 
            // новаяИграToolStripMenuItem
            // 
            this.новаяИграToolStripMenuItem.Name = "новаяИграToolStripMenuItem";
            this.новаяИграToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.новаяИграToolStripMenuItem.Text = "Новая игра    ctrl+N";
            this.новаяИграToolStripMenuItem.Click += new System.EventHandler(this.новаяИграToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(194, 6);
            // 
            // новичокToolStripMenuItem
            // 
            this.новичокToolStripMenuItem.Checked = true;
            this.новичокToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.новичокToolStripMenuItem.Name = "новичокToolStripMenuItem";
            this.новичокToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.новичокToolStripMenuItem.Text = "Новичок";
            this.новичокToolStripMenuItem.Click += new System.EventHandler(this.новичокToolStripMenuItem_Click);
            // 
            // любительToolStripMenuItem
            // 
            this.любительToolStripMenuItem.Name = "любительToolStripMenuItem";
            this.любительToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.любительToolStripMenuItem.Text = "Любитель";
            this.любительToolStripMenuItem.Click += new System.EventHandler(this.любительToolStripMenuItem_Click);
            // 
            // профессионалToolStripMenuItem
            // 
            this.профессионалToolStripMenuItem.Name = "профессионалToolStripMenuItem";
            this.профессионалToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.профессионалToolStripMenuItem.Text = "Профессионал";
            this.профессионалToolStripMenuItem.Click += new System.EventHandler(this.профессионалToolStripMenuItem_Click);
            // 
            // настройкаСложностиToolStripMenuItem
            // 
            this.настройкаСложностиToolStripMenuItem.Name = "настройкаСложностиToolStripMenuItem";
            this.настройкаСложностиToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.настройкаСложностиToolStripMenuItem.Text = "Настройка сложности";
            this.настройкаСложностиToolStripMenuItem.Click += new System.EventHandler(this.настройкаСложностиToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(194, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.выходToolStripMenuItem.Text = "Выход         Alt+F4";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // c
            // 
            this.c.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справкаToolStripMenuItem,
            this.проПрограммуToolStripMenuItem});
            this.c.Name = "c";
            this.c.Size = new System.Drawing.Size(65, 20);
            this.c.Text = "Справка";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.справкаToolStripMenuItem.Text = "Справка";
            this.справкаToolStripMenuItem.Click += new System.EventHandler(this.справкаToolStripMenuItem_Click);
            // 
            // проПрограммуToolStripMenuItem
            // 
            this.проПрограммуToolStripMenuItem.Name = "проПрограммуToolStripMenuItem";
            this.проПрограммуToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.проПрограммуToolStripMenuItem.Text = "Про программу";
            this.проПрограммуToolStripMenuItem.Click += new System.EventHandler(this.проПрограммуToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 195);
            this.Controls.Add(this.menuStrip1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сапер";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem дополнительноToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c;
        private System.Windows.Forms.ToolStripMenuItem новаяИграToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem новичокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem любительToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem профессионалToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкаСложностиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem проПрограммуToolStripMenuItem;



    }
}

