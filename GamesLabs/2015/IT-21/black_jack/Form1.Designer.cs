namespace black_jack
{
    partial class frm_BJ
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BJ));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbl_HScore = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_CScore = new System.Windows.Forms.Label();
            this.cmb_Stand = new System.Windows.Forms.Button();
            this.cmb_BetMax = new System.Windows.Forms.Button();
            this.cmb_BetMinus = new System.Windows.Forms.Button();
            this.cmb_BetPlus = new System.Windows.Forms.Button();
            this.lbl_Money = new System.Windows.Forms.Label();
            this.lbl_Bet = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_ok_name = new System.Windows.Forms.Button();
            this.OFD = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(29, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // lbl_HScore
            // 
            this.lbl_HScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_HScore.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_HScore.Location = new System.Drawing.Point(26, 203);
            this.lbl_HScore.Name = "lbl_HScore";
            this.lbl_HScore.Size = new System.Drawing.Size(77, 24);
            this.lbl_HScore.TabIndex = 4;
            this.lbl_HScore.Text = "Вы";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(644, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newGameToolStripMenuItem.Text = "Новая игра";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.quitToolStripMenuItem.Text = "Выход";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // lbl_CScore
            // 
            this.lbl_CScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_CScore.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_CScore.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_CScore.Location = new System.Drawing.Point(26, 227);
            this.lbl_CScore.Name = "lbl_CScore";
            this.lbl_CScore.Size = new System.Drawing.Size(77, 24);
            this.lbl_CScore.TabIndex = 6;
            this.lbl_CScore.Text = "Бот";
            // 
            // cmb_Stand
            // 
            this.cmb_Stand.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.cmb_Stand.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmb_Stand.Location = new System.Drawing.Point(26, 367);
            this.cmb_Stand.Name = "cmb_Stand";
            this.cmb_Stand.Size = new System.Drawing.Size(77, 26);
            this.cmb_Stand.TabIndex = 3;
            this.cmb_Stand.Text = "Стоп";
            this.cmb_Stand.UseVisualStyleBackColor = true;
            this.cmb_Stand.Click += new System.EventHandler(this.cmb_Stand_Click);
            // 
            // cmb_BetMax
            // 
            this.cmb_BetMax.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.cmb_BetMax.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmb_BetMax.Location = new System.Drawing.Point(26, 334);
            this.cmb_BetMax.Name = "cmb_BetMax";
            this.cmb_BetMax.Size = new System.Drawing.Size(77, 26);
            this.cmb_BetMax.TabIndex = 7;
            this.cmb_BetMax.Text = "Макс";
            this.cmb_BetMax.UseVisualStyleBackColor = true;
            this.cmb_BetMax.Click += new System.EventHandler(this.cmb_BetMax_Click);
            // 
            // cmb_BetMinus
            // 
            this.cmb_BetMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmb_BetMinus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmb_BetMinus.Location = new System.Drawing.Point(26, 302);
            this.cmb_BetMinus.Name = "cmb_BetMinus";
            this.cmb_BetMinus.Size = new System.Drawing.Size(30, 26);
            this.cmb_BetMinus.TabIndex = 8;
            this.cmb_BetMinus.Text = "-";
            this.cmb_BetMinus.UseVisualStyleBackColor = true;
            this.cmb_BetMinus.Click += new System.EventHandler(this.cmb_BetMinus_Click);
            // 
            // cmb_BetPlus
            // 
            this.cmb_BetPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmb_BetPlus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmb_BetPlus.Location = new System.Drawing.Point(73, 302);
            this.cmb_BetPlus.Name = "cmb_BetPlus";
            this.cmb_BetPlus.Size = new System.Drawing.Size(30, 26);
            this.cmb_BetPlus.TabIndex = 10;
            this.cmb_BetPlus.Text = "+";
            this.cmb_BetPlus.UseVisualStyleBackColor = true;
            this.cmb_BetPlus.Click += new System.EventHandler(this.cmb_BetPlus_Click);
            // 
            // lbl_Money
            // 
            this.lbl_Money.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Money.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_Money.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_Money.Location = new System.Drawing.Point(26, 251);
            this.lbl_Money.Name = "lbl_Money";
            this.lbl_Money.Size = new System.Drawing.Size(77, 24);
            this.lbl_Money.TabIndex = 11;
            this.lbl_Money.Text = "Деньги";
            // 
            // lbl_Bet
            // 
            this.lbl_Bet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Bet.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_Bet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_Bet.Location = new System.Drawing.Point(26, 275);
            this.lbl_Bet.Name = "lbl_Bet";
            this.lbl_Bet.Size = new System.Drawing.Size(77, 24);
            this.lbl_Bet.TabIndex = 12;
            this.lbl_Bet.Text = "Поднять";
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(467, 2);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(127, 20);
            this.txt_name.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(318, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 18);
            this.label1.TabIndex = 14;
            this.label1.Text = "Введите имя ";
            // 
            // cmb_ok_name
            // 
            this.cmb_ok_name.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmb_ok_name.Location = new System.Drawing.Point(600, 0);
            this.cmb_ok_name.Name = "cmb_ok_name";
            this.cmb_ok_name.Size = new System.Drawing.Size(44, 24);
            this.cmb_ok_name.TabIndex = 15;
            this.cmb_ok_name.Text = "OK";
            this.cmb_ok_name.UseVisualStyleBackColor = true;
            this.cmb_ok_name.Click += new System.EventHandler(this.cmb_ok_name_Click);
            // 
            // OFD
            // 
            this.OFD.FileName = "openFileDialog1";
            // 
            // frm_BJ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::black_jack.Properties.Resources.Background_Green_Frame;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(644, 467);
            this.Controls.Add(this.cmb_ok_name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.lbl_Bet);
            this.Controls.Add(this.lbl_Money);
            this.Controls.Add(this.cmb_BetPlus);
            this.Controls.Add(this.cmb_BetMinus);
            this.Controls.Add(this.cmb_BetMax);
            this.Controls.Add(this.lbl_CScore);
            this.Controls.Add(this.lbl_HScore);
            this.Controls.Add(this.cmb_Stand);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_BJ";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blackjack";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbl_HScore;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Label lbl_CScore;
        private System.Windows.Forms.Button cmb_Stand;
        private System.Windows.Forms.Button cmb_BetMax;
        private System.Windows.Forms.Button cmb_BetMinus;
        private System.Windows.Forms.Button cmb_BetPlus;
        private System.Windows.Forms.Label lbl_Money;
        private System.Windows.Forms.Label lbl_Bet;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmb_ok_name;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OFD;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
    }
}

