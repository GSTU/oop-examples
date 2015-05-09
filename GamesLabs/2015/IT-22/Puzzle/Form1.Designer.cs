namespace Puzzle
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.загрузитьКартинкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.перемешатьКартинкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.восстановитьКартинкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкаСложностиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.загрузитьКартинкуToolStripMenuItem,
            this.перемешатьКартинкуToolStripMenuItem,
            this.восстановитьКартинкуToolStripMenuItem,
            this.настройкаСложностиToolStripMenuItem,
            this.помощьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // загрузитьКартинкуToolStripMenuItem
            // 
            this.загрузитьКартинкуToolStripMenuItem.Name = "загрузитьКартинкуToolStripMenuItem";
            this.загрузитьКартинкуToolStripMenuItem.Size = new System.Drawing.Size(134, 20);
            this.загрузитьКартинкуToolStripMenuItem.Text = "Загрузить XML-файл";
            this.загрузитьКартинкуToolStripMenuItem.Click += new System.EventHandler(this.загрузитьКартинкуToolStripMenuItem_Click);
            // 
            // перемешатьКартинкуToolStripMenuItem
            // 
            this.перемешатьКартинкуToolStripMenuItem.Enabled = false;
            this.перемешатьКартинкуToolStripMenuItem.Name = "перемешатьКартинкуToolStripMenuItem";
            this.перемешатьКартинкуToolStripMenuItem.Size = new System.Drawing.Size(143, 20);
            this.перемешатьКартинкуToolStripMenuItem.Text = "Перемешать картинку";
            this.перемешатьКартинкуToolStripMenuItem.Click += new System.EventHandler(this.перемешатьКартинкуToolStripMenuItem_Click);
            // 
            // восстановитьКартинкуToolStripMenuItem
            // 
            this.восстановитьКартинкуToolStripMenuItem.Enabled = false;
            this.восстановитьКартинкуToolStripMenuItem.Name = "восстановитьКартинкуToolStripMenuItem";
            this.восстановитьКартинкуToolStripMenuItem.Size = new System.Drawing.Size(147, 20);
            this.восстановитьКартинкуToolStripMenuItem.Text = "Восстановить картинку";
            this.восстановитьКартинкуToolStripMenuItem.Click += new System.EventHandler(this.восстановитьКартинкуToolStripMenuItem_Click);
            // 
            // настройкаСложностиToolStripMenuItem
            // 
            this.настройкаСложностиToolStripMenuItem.Enabled = false;
            this.настройкаСложностиToolStripMenuItem.Name = "настройкаСложностиToolStripMenuItem";
            this.настройкаСложностиToolStripMenuItem.Size = new System.Drawing.Size(142, 20);
            this.настройкаСложностиToolStripMenuItem.Text = "Настройка сложности";
            this.настройкаСложностиToolStripMenuItem.Click += new System.EventHandler(this.настройкаСложностиToolStripMenuItem_Click);
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.Enabled = false;
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.помощьToolStripMenuItem.Text = "Помощь";
            this.помощьToolStripMenuItem.Click += new System.EventHandler(this.помощьToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 600);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 630);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(370, 630);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(800, 669);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Puzzle";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem загрузитьКартинкуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem перемешатьКартинкуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem восстановитьКартинкуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкаСложностиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
    }
}

