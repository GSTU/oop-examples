namespace Minesweepers
{
    partial class Main
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.новаяИграToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.уровеньToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новичокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспертToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.профессионалToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timeTextBox = new System.Windows.Forms.TextBox();
            this.mineTextBox = new System.Windows.Forms.TextBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.изменитьДокументXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новаяИграToolStripMenuItem,
            this.уровеньToolStripMenuItem,
            this.изменитьДокументXMLToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(820, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // новаяИграToolStripMenuItem
            // 
            this.новаяИграToolStripMenuItem.Name = "новаяИграToolStripMenuItem";
            this.новаяИграToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.новаяИграToolStripMenuItem.Text = "Новая игра";
            this.новаяИграToolStripMenuItem.Click += new System.EventHandler(this.новаяИграToolStripMenuItem_Click);
            // 
            // уровеньToolStripMenuItem
            // 
            this.уровеньToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новичокToolStripMenuItem,
            this.экспертToolStripMenuItem,
            this.профессионалToolStripMenuItem});
            this.уровеньToolStripMenuItem.Name = "уровеньToolStripMenuItem";
            this.уровеньToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.уровеньToolStripMenuItem.Text = "Уровень";
            // 
            // новичокToolStripMenuItem
            // 
            this.новичокToolStripMenuItem.Checked = true;
            this.новичокToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.новичокToolStripMenuItem.Name = "новичокToolStripMenuItem";
            this.новичокToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.новичокToolStripMenuItem.Text = "Новичок";
            this.новичокToolStripMenuItem.Click += new System.EventHandler(this.новичокToolStripMenuItem_Click);
            // 
            // экспертToolStripMenuItem
            // 
            this.экспертToolStripMenuItem.Name = "экспертToolStripMenuItem";
            this.экспертToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.экспертToolStripMenuItem.Text = "Эксперт";
            this.экспертToolStripMenuItem.Click += new System.EventHandler(this.экспертToolStripMenuItem_Click);
            // 
            // профессионалToolStripMenuItem
            // 
            this.профессионалToolStripMenuItem.Name = "профессионалToolStripMenuItem";
            this.профессионалToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.профессионалToolStripMenuItem.Text = "Профессионал";
            this.профессионалToolStripMenuItem.Click += new System.EventHandler(this.профессионалToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.timeTextBox);
            this.panel1.Controls.Add(this.mineTextBox);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1301, 700);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // timeTextBox
            // 
            this.timeTextBox.Enabled = false;
            this.timeTextBox.Location = new System.Drawing.Point(146, 273);
            this.timeTextBox.Name = "timeTextBox";
            this.timeTextBox.Size = new System.Drawing.Size(85, 20);
            this.timeTextBox.TabIndex = 1;
            // 
            // mineTextBox
            // 
            this.mineTextBox.Enabled = false;
            this.mineTextBox.Location = new System.Drawing.Point(22, 273);
            this.mineTextBox.Name = "mineTextBox";
            this.mineTextBox.Size = new System.Drawing.Size(85, 20);
            this.mineTextBox.TabIndex = 0;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // изменитьДокументXMLToolStripMenuItem
            // 
            this.изменитьДокументXMLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.изменитьToolStripMenuItem});
            this.изменитьДокументXMLToolStripMenuItem.Name = "изменитьДокументXMLToolStripMenuItem";
            this.изменитьДокументXMLToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.изменитьДокументXMLToolStripMenuItem.Text = "XML";
            // 
            // изменитьToolStripMenuItem
            // 
            this.изменитьToolStripMenuItem.Name = "изменитьToolStripMenuItem";
            this.изменитьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.изменитьToolStripMenuItem.Text = "Изменить";
            this.изменитьToolStripMenuItem.Click += new System.EventHandler(this.изменитьToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 332);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Сапер";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem новаяИграToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem уровеньToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новичокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem экспертToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem профессионалToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TextBox timeTextBox;
        private System.Windows.Forms.TextBox mineTextBox;
        private System.Windows.Forms.ToolStripMenuItem изменитьДокументXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

