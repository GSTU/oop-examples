namespace PingPongGame
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
            this.playground = new System.Windows.Forms.Panel();
            this.stick2 = new System.Windows.Forms.PictureBox();
            this.gameover_lbl = new System.Windows.Forms.Label();
            this.points_lbl = new System.Windows.Forms.Label();
            this.score_lbl = new System.Windows.Forms.Label();
            this.ball = new System.Windows.Forms.PictureBox();
            this.stick = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.playground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stick2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ball)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stick)).BeginInit();
            this.SuspendLayout();
            // 
            // playground
            // 
            this.playground.Controls.Add(this.stick2);
            this.playground.Controls.Add(this.gameover_lbl);
            this.playground.Controls.Add(this.points_lbl);
            this.playground.Controls.Add(this.score_lbl);
            this.playground.Controls.Add(this.ball);
            this.playground.Controls.Add(this.stick);
            this.playground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playground.Location = new System.Drawing.Point(0, 0);
            this.playground.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.playground.Name = "playground";
            this.playground.Size = new System.Drawing.Size(984, 534);
            this.playground.TabIndex = 0;
            // 
            // stick2
            // 
            this.stick2.BackColor = System.Drawing.Color.Black;
            this.stick2.Location = new System.Drawing.Point(392, 31);
            this.stick2.Name = "stick2";
            this.stick2.Size = new System.Drawing.Size(200, 20);
            this.stick2.TabIndex = 6;
            this.stick2.TabStop = false;
            // 
            // gameover_lbl
            // 
            this.gameover_lbl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gameover_lbl.AutoSize = true;
            this.gameover_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameover_lbl.Location = new System.Drawing.Point(350, 141);
            this.gameover_lbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.gameover_lbl.Name = "gameover_lbl";
            this.gameover_lbl.Size = new System.Drawing.Size(345, 220);
            this.gameover_lbl.TabIndex = 5;
            this.gameover_lbl.Text = "Game Over\r\n\r\nF1     - Restart\r\nEsc   - Exit";
            // 
            // points_lbl
            // 
            this.points_lbl.AutoSize = true;
            this.points_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.points_lbl.Location = new System.Drawing.Point(216, 44);
            this.points_lbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.points_lbl.Name = "points_lbl";
            this.points_lbl.Size = new System.Drawing.Size(52, 55);
            this.points_lbl.TabIndex = 4;
            this.points_lbl.Text = "0";
            this.points_lbl.Click += new System.EventHandler(this.point_lbl_Click);
            // 
            // score_lbl
            // 
            this.score_lbl.AutoSize = true;
            this.score_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.score_lbl.Location = new System.Drawing.Point(27, 44);
            this.score_lbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.score_lbl.Name = "score_lbl";
            this.score_lbl.Size = new System.Drawing.Size(169, 55);
            this.score_lbl.TabIndex = 3;
            this.score_lbl.Text = "Score:";
            // 
            // ball
            // 
            this.ball.BackColor = System.Drawing.Color.Red;
            this.ball.Location = new System.Drawing.Point(115, 102);
            this.ball.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ball.Name = "ball";
            this.ball.Size = new System.Drawing.Size(29, 30);
            this.ball.TabIndex = 2;
            this.ball.TabStop = false;
            // 
            // stick
            // 
            this.stick.BackColor = System.Drawing.Color.Black;
            this.stick.Location = new System.Drawing.Point(392, 482);
            this.stick.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.stick.Name = "stick";
            this.stick.Size = new System.Drawing.Size(200, 20);
            this.stick.TabIndex = 1;
            this.stick.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 534);
            this.Controls.Add(this.playground);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.playground.ResumeLayout(false);
            this.playground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stick2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ball)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stick)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel playground;
        private System.Windows.Forms.PictureBox ball;
        private System.Windows.Forms.PictureBox stick;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label points_lbl;
        private System.Windows.Forms.Label score_lbl;
        private System.Windows.Forms.Label gameover_lbl;
        private System.Windows.Forms.PictureBox stick2;
    }
}

