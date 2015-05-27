namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.играToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новаяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.правилаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.интеллектИИToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.слабыйToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.сильныйToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.timer2_GameMetod = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Segoe Script", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(10, 200);
            this.label1.MaximumSize = new System.Drawing.Size(0, 100);
            this.label1.MinimumSize = new System.Drawing.Size(100, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 25);
            this.label1.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.играToolStripMenuItem,
            this.настройкиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(250, 31);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.menuStrip1_MouseDown);
            this.menuStrip1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.menuStrip1_MouseMove);
            this.menuStrip1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.menuStrip1_MouseUp);
            // 
            // играToolStripMenuItem
            // 
            this.играToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новаяToolStripMenuItem,
            this.правилаToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.играToolStripMenuItem.Font = new System.Drawing.Font("Segoe Print", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.играToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.играToolStripMenuItem.Name = "играToolStripMenuItem";
            this.играToolStripMenuItem.Size = new System.Drawing.Size(56, 27);
            this.играToolStripMenuItem.Text = "Игра";
            // 
            // новаяToolStripMenuItem
            // 
            this.новаяToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.новаяToolStripMenuItem.Name = "новаяToolStripMenuItem";
            this.новаяToolStripMenuItem.Size = new System.Drawing.Size(158, 28);
            this.новаяToolStripMenuItem.Text = "Новая Игра";
            this.новаяToolStripMenuItem.Click += new System.EventHandler(this.новаяToolStripMenuItem_Click);
            // 
            // правилаToolStripMenuItem
            // 
            this.правилаToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.правилаToolStripMenuItem.Name = "правилаToolStripMenuItem";
            this.правилаToolStripMenuItem.Size = new System.Drawing.Size(158, 28);
            this.правилаToolStripMenuItem.Text = "Правила";
            this.правилаToolStripMenuItem.Click += new System.EventHandler(this.правилаToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(158, 28);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click_1);
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3,
            this.toolStripTextBox2,
            this.toolStripSeparator4,
            this.ходToolStripMenuItem,
            this.интеллектИИToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripTextBox1,
            this.toolStripSeparator2,
            this.слабыйToolStripMenuItem1,
            this.сильныйToolStripMenuItem1});
            this.настройкиToolStripMenuItem.Font = new System.Drawing.Font("Segoe Script", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.настройкиToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(105, 27);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(159, 6);
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.toolStripTextBox2.ForeColor = System.Drawing.Color.Blue;
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.ReadOnly = true;
            this.toolStripTextBox2.Size = new System.Drawing.Size(100, 16);
            this.toolStripTextBox2.Text = "Первый ход";
            this.toolStripTextBox2.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(159, 6);
            // 
            // ходToolStripMenuItem
            // 
            this.ходToolStripMenuItem.Checked = true;
            this.ходToolStripMenuItem.CheckOnClick = true;
            this.ходToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ходToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.ходToolStripMenuItem.Name = "ходToolStripMenuItem";
            this.ходToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
            this.ходToolStripMenuItem.Text = "Человек";
            this.ходToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ходToolStripMenuItem_CheckedChanged);
            this.ходToolStripMenuItem.Click += new System.EventHandler(this.ходToolStripMenuItem_CheckedChanged);
            // 
            // интеллектИИToolStripMenuItem
            // 
            this.интеллектИИToolStripMenuItem.CheckOnClick = true;
            this.интеллектИИToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.интеллектИИToolStripMenuItem.Name = "интеллектИИToolStripMenuItem";
            this.интеллектИИToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
            this.интеллектИИToolStripMenuItem.Text = "Компьютер";
            this.интеллектИИToolStripMenuItem.CheckedChanged += new System.EventHandler(this.интеллектИИToolStripMenuItem_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(159, 6);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.ForeColor = System.Drawing.Color.Blue;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.ReadOnly = true;
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox1.Tag = "";
            this.toolStripTextBox1.Text = "Интеллект";
            this.toolStripTextBox1.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(159, 6);
            // 
            // слабыйToolStripMenuItem1
            // 
            this.слабыйToolStripMenuItem1.Checked = true;
            this.слабыйToolStripMenuItem1.CheckOnClick = true;
            this.слабыйToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.слабыйToolStripMenuItem1.ForeColor = System.Drawing.Color.Blue;
            this.слабыйToolStripMenuItem1.Name = "слабыйToolStripMenuItem1";
            this.слабыйToolStripMenuItem1.Size = new System.Drawing.Size(162, 24);
            this.слабыйToolStripMenuItem1.Text = "Слабый";
            this.слабыйToolStripMenuItem1.CheckedChanged += new System.EventHandler(this.слабыйToolStripMenuItem1_CheckedChanged);
            // 
            // сильныйToolStripMenuItem1
            // 
            this.сильныйToolStripMenuItem1.CheckOnClick = true;
            this.сильныйToolStripMenuItem1.ForeColor = System.Drawing.Color.Blue;
            this.сильныйToolStripMenuItem1.Name = "сильныйToolStripMenuItem1";
            this.сильныйToolStripMenuItem1.Size = new System.Drawing.Size(162, 24);
            this.сильныйToolStripMenuItem1.Text = "Сильный";
            this.сильныйToolStripMenuItem1.CheckedChanged += new System.EventHandler(this.сильныйToolStripMenuItem1_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(225, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(205, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // timer2_GameMetod
            // 
            this.timer2_GameMetod.Enabled = true;
            this.timer2_GameMetod.Interval = 20;
            this.timer2_GameMetod.Tick += new System.EventHandler(this.timer2_GameMetod_Tick);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe Script", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(25, 230);
            this.label2.MaximumSize = new System.Drawing.Size(0, 200);
            this.label2.MinimumSize = new System.Drawing.Size(200, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // timer2
            // 
            this.timer2.Interval = 50;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Interval = 50;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 260);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown_1);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem играToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новаяToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Timer timer2_GameMetod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem интеллектИИToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem слабыйToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem сильныйToolStripMenuItem1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.ToolStripMenuItem правилаToolStripMenuItem;
    }
}

