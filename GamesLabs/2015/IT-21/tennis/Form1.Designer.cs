namespace tennis
{
    partial class MainForm
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
            this.WorldFrame = new System.Windows.Forms.Panel();
            this.pb_Ball = new System.Windows.Forms.PictureBox();
            this.pb_Enemy = new System.Windows.Forms.PictureBox();
            this.pb_Player = new System.Windows.Forms.PictureBox();
            this.label_Time = new System.Windows.Forms.Label();
            this.label_Start = new System.Windows.Forms.Label();
            this.enemy_4 = new System.Windows.Forms.PictureBox();
            this.enemy_5 = new System.Windows.Forms.PictureBox();
            this.player_5 = new System.Windows.Forms.PictureBox();
            this.player_4 = new System.Windows.Forms.PictureBox();
            this.enemy_1 = new System.Windows.Forms.PictureBox();
            this.enemy_2 = new System.Windows.Forms.PictureBox();
            this.enemy_3 = new System.Windows.Forms.PictureBox();
            this.player_3 = new System.Windows.Forms.PictureBox();
            this.player_2 = new System.Windows.Forms.PictureBox();
            this.player_1 = new System.Windows.Forms.PictureBox();
            this.timer_Moveball = new System.Windows.Forms.Timer(this.components);
            this.timer_Enemy = new System.Windows.Forms.Timer(this.components);
            this.timer_Sec = new System.Windows.Forms.Timer(this.components);
            this.WorldFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Ball)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Enemy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemy_4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemy_5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player_5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player_4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemy_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemy_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemy_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player_1)).BeginInit();
            this.SuspendLayout();
            // 
            // WorldFrame
            // 
            this.WorldFrame.BackColor = System.Drawing.Color.White;
            this.WorldFrame.Controls.Add(this.pb_Ball);
            this.WorldFrame.Controls.Add(this.pb_Enemy);
            this.WorldFrame.Controls.Add(this.pb_Player);
            this.WorldFrame.Dock = System.Windows.Forms.DockStyle.Top;
            this.WorldFrame.Location = new System.Drawing.Point(0, 0);
            this.WorldFrame.Name = "WorldFrame";
            this.WorldFrame.Size = new System.Drawing.Size(428, 209);
            this.WorldFrame.TabIndex = 1;
            // 
            // pb_Ball
            // 
            this.pb_Ball.BackColor = System.Drawing.Color.Black;
            this.pb_Ball.Location = new System.Drawing.Point(208, 90);
            this.pb_Ball.Name = "pb_Ball";
            this.pb_Ball.Size = new System.Drawing.Size(14, 16);
            this.pb_Ball.TabIndex = 2;
            this.pb_Ball.TabStop = false;
            // 
            // pb_Enemy
            // 
            this.pb_Enemy.BackColor = System.Drawing.Color.Red;
            this.pb_Enemy.Location = new System.Drawing.Point(409, 67);
            this.pb_Enemy.Name = "pb_Enemy";
            this.pb_Enemy.Size = new System.Drawing.Size(15, 70);
            this.pb_Enemy.TabIndex = 1;
            this.pb_Enemy.TabStop = false;
            // 
            // pb_Player
            // 
            this.pb_Player.BackColor = System.Drawing.Color.Blue;
            this.pb_Player.Location = new System.Drawing.Point(3, 67);
            this.pb_Player.Name = "pb_Player";
            this.pb_Player.Size = new System.Drawing.Size(15, 70);
            this.pb_Player.TabIndex = 0;
            this.pb_Player.TabStop = false;
            // 
            // label_Time
            // 
            this.label_Time.AutoSize = true;
            this.label_Time.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Time.Location = new System.Drawing.Point(173, 222);
            this.label_Time.Name = "label_Time";
            this.label_Time.Size = new System.Drawing.Size(79, 13);
            this.label_Time.TabIndex = 27;
            this.label_Time.Text = "Time: 00: 00";
            this.label_Time.Visible = false;
            // 
            // label_Start
            // 
            this.label_Start.AutoSize = true;
            this.label_Start.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Start.Location = new System.Drawing.Point(147, 222);
            this.label_Start.Name = "label_Start";
            this.label_Start.Size = new System.Drawing.Size(127, 13);
            this.label_Start.TabIndex = 26;
            this.label_Start.Text = "Press Space to Start";
            // 
            // enemy_4
            // 
            this.enemy_4.BackColor = System.Drawing.Color.Silver;
            this.enemy_4.Location = new System.Drawing.Point(299, 220);
            this.enemy_4.Name = "enemy_4";
            this.enemy_4.Size = new System.Drawing.Size(15, 15);
            this.enemy_4.TabIndex = 25;
            this.enemy_4.TabStop = false;
            // 
            // enemy_5
            // 
            this.enemy_5.BackColor = System.Drawing.Color.Silver;
            this.enemy_5.Location = new System.Drawing.Point(278, 220);
            this.enemy_5.Name = "enemy_5";
            this.enemy_5.Size = new System.Drawing.Size(15, 15);
            this.enemy_5.TabIndex = 24;
            this.enemy_5.TabStop = false;
            // 
            // player_5
            // 
            this.player_5.BackColor = System.Drawing.Color.Silver;
            this.player_5.Location = new System.Drawing.Point(128, 220);
            this.player_5.Name = "player_5";
            this.player_5.Size = new System.Drawing.Size(15, 15);
            this.player_5.TabIndex = 23;
            this.player_5.TabStop = false;
            // 
            // player_4
            // 
            this.player_4.BackColor = System.Drawing.Color.Silver;
            this.player_4.Location = new System.Drawing.Point(107, 220);
            this.player_4.Name = "player_4";
            this.player_4.Size = new System.Drawing.Size(15, 15);
            this.player_4.TabIndex = 22;
            this.player_4.TabStop = false;
            // 
            // enemy_1
            // 
            this.enemy_1.BackColor = System.Drawing.Color.Silver;
            this.enemy_1.Location = new System.Drawing.Point(362, 220);
            this.enemy_1.Name = "enemy_1";
            this.enemy_1.Size = new System.Drawing.Size(15, 15);
            this.enemy_1.TabIndex = 21;
            this.enemy_1.TabStop = false;
            // 
            // enemy_2
            // 
            this.enemy_2.BackColor = System.Drawing.Color.Silver;
            this.enemy_2.Location = new System.Drawing.Point(341, 220);
            this.enemy_2.Name = "enemy_2";
            this.enemy_2.Size = new System.Drawing.Size(15, 15);
            this.enemy_2.TabIndex = 20;
            this.enemy_2.TabStop = false;
            // 
            // enemy_3
            // 
            this.enemy_3.BackColor = System.Drawing.Color.Silver;
            this.enemy_3.Location = new System.Drawing.Point(320, 220);
            this.enemy_3.Name = "enemy_3";
            this.enemy_3.Size = new System.Drawing.Size(15, 15);
            this.enemy_3.TabIndex = 19;
            this.enemy_3.TabStop = false;
            // 
            // player_3
            // 
            this.player_3.BackColor = System.Drawing.Color.Silver;
            this.player_3.Location = new System.Drawing.Point(86, 220);
            this.player_3.Name = "player_3";
            this.player_3.Size = new System.Drawing.Size(15, 15);
            this.player_3.TabIndex = 18;
            this.player_3.TabStop = false;
            // 
            // player_2
            // 
            this.player_2.BackColor = System.Drawing.Color.Silver;
            this.player_2.Location = new System.Drawing.Point(65, 220);
            this.player_2.Name = "player_2";
            this.player_2.Size = new System.Drawing.Size(15, 15);
            this.player_2.TabIndex = 17;
            this.player_2.TabStop = false;
            // 
            // player_1
            // 
            this.player_1.BackColor = System.Drawing.Color.Silver;
            this.player_1.Location = new System.Drawing.Point(44, 220);
            this.player_1.Name = "player_1";
            this.player_1.Size = new System.Drawing.Size(15, 15);
            this.player_1.TabIndex = 16;
            this.player_1.TabStop = false;
            // 
            // timer_Moveball
            // 
            this.timer_Moveball.Enabled = true;
            this.timer_Moveball.Interval = 1;
            this.timer_Moveball.Tick += new System.EventHandler(this.timer_Moveball_Tick);
            // 
            // timer_Enemy
            // 
            this.timer_Enemy.Enabled = true;
            this.timer_Enemy.Interval = 10;
            this.timer_Enemy.Tick += new System.EventHandler(this.timer_Enemy_Tick);
            // 
            // timer_Sec
            // 
            this.timer_Sec.Enabled = true;
            this.timer_Sec.Interval = 1000;
            this.timer_Sec.Tick += new System.EventHandler(this.timer_Sec_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 248);
            this.Controls.Add(this.label_Time);
            this.Controls.Add(this.label_Start);
            this.Controls.Add(this.enemy_4);
            this.Controls.Add(this.enemy_5);
            this.Controls.Add(this.player_5);
            this.Controls.Add(this.player_4);
            this.Controls.Add(this.enemy_1);
            this.Controls.Add(this.enemy_2);
            this.Controls.Add(this.enemy_3);
            this.Controls.Add(this.player_3);
            this.Controls.Add(this.player_2);
            this.Controls.Add(this.player_1);
            this.Controls.Add(this.WorldFrame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "Tenis";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.WorldFrame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Ball)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Enemy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemy_4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemy_5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player_5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player_4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemy_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemy_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemy_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player_1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel WorldFrame;
        private System.Windows.Forms.PictureBox pb_Ball;
        private System.Windows.Forms.PictureBox pb_Enemy;
        private System.Windows.Forms.PictureBox pb_Player;
        private System.Windows.Forms.Label label_Time;
        private System.Windows.Forms.Label label_Start;
        private System.Windows.Forms.PictureBox enemy_4;
        private System.Windows.Forms.PictureBox enemy_5;
        private System.Windows.Forms.PictureBox player_5;
        private System.Windows.Forms.PictureBox player_4;
        private System.Windows.Forms.PictureBox enemy_1;
        private System.Windows.Forms.PictureBox enemy_2;
        private System.Windows.Forms.PictureBox enemy_3;
        private System.Windows.Forms.PictureBox player_3;
        private System.Windows.Forms.PictureBox player_2;
        private System.Windows.Forms.PictureBox player_1;
        private System.Windows.Forms.Timer timer_Moveball;
        private System.Windows.Forms.Timer timer_Enemy;
        private System.Windows.Forms.Timer timer_Sec;
    }
}

