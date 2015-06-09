namespace battle
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.ProtectHead = new System.Windows.Forms.Button();
            this.ProtectBody = new System.Windows.Forms.Button();
            this.ProtectLegs = new System.Windows.Forms.Button();
            this.AttackHead = new System.Windows.Forms.Button();
            this.AttackBody = new System.Windows.Forms.Button();
            this.AttackLegs = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.HealthPlayer = new System.Windows.Forms.Label();
            this.HealthEnemy = new System.Windows.Forms.Label();
            this.MakeStep = new System.Windows.Forms.Button();
            this.newGame_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(78)))), ((int)(((byte)(84)))));
            this.pictureBox1.Location = new System.Drawing.Point(12, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(162, 224);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(78)))), ((int)(((byte)(84)))));
            this.pictureBox2.Location = new System.Drawing.Point(512, 51);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(162, 224);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // ProtectHead
            // 
            this.ProtectHead.BackColor = System.Drawing.Color.CadetBlue;
            this.ProtectHead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProtectHead.Location = new System.Drawing.Point(180, 48);
            this.ProtectHead.Name = "ProtectHead";
            this.ProtectHead.Size = new System.Drawing.Size(75, 23);
            this.ProtectHead.TabIndex = 1;
            this.ProtectHead.Text = "Голова";
            this.ProtectHead.UseVisualStyleBackColor = false;
            this.ProtectHead.Click += new System.EventHandler(this.ProtectHead_Click);
            // 
            // ProtectBody
            // 
            this.ProtectBody.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProtectBody.Location = new System.Drawing.Point(180, 77);
            this.ProtectBody.Name = "ProtectBody";
            this.ProtectBody.Size = new System.Drawing.Size(75, 23);
            this.ProtectBody.TabIndex = 2;
            this.ProtectBody.Text = "Тело";
            this.ProtectBody.UseVisualStyleBackColor = true;
            this.ProtectBody.Click += new System.EventHandler(this.ProtectBody_Click);
            // 
            // ProtectLegs
            // 
            this.ProtectLegs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProtectLegs.Location = new System.Drawing.Point(180, 106);
            this.ProtectLegs.Name = "ProtectLegs";
            this.ProtectLegs.Size = new System.Drawing.Size(75, 23);
            this.ProtectLegs.TabIndex = 3;
            this.ProtectLegs.Text = "Ноги";
            this.ProtectLegs.UseVisualStyleBackColor = true;
            this.ProtectLegs.Click += new System.EventHandler(this.ProtectLegs_Click);
            // 
            // AttackHead
            // 
            this.AttackHead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AttackHead.Location = new System.Drawing.Point(261, 48);
            this.AttackHead.Name = "AttackHead";
            this.AttackHead.Size = new System.Drawing.Size(75, 23);
            this.AttackHead.TabIndex = 1;
            this.AttackHead.Text = "Голова";
            this.AttackHead.UseVisualStyleBackColor = true;
            this.AttackHead.Click += new System.EventHandler(this.AttackHead_Click);
            // 
            // AttackBody
            // 
            this.AttackBody.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AttackBody.Location = new System.Drawing.Point(261, 77);
            this.AttackBody.Name = "AttackBody";
            this.AttackBody.Size = new System.Drawing.Size(75, 23);
            this.AttackBody.TabIndex = 2;
            this.AttackBody.Text = "Тело";
            this.AttackBody.UseVisualStyleBackColor = true;
            this.AttackBody.Click += new System.EventHandler(this.AttackBody_Click);
            // 
            // AttackLegs
            // 
            this.AttackLegs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AttackLegs.Location = new System.Drawing.Point(261, 106);
            this.AttackLegs.Name = "AttackLegs";
            this.AttackLegs.Size = new System.Drawing.Size(75, 23);
            this.AttackLegs.TabIndex = 3;
            this.AttackLegs.Text = "Ноги";
            this.AttackLegs.UseVisualStyleBackColor = true;
            this.AttackLegs.Click += new System.EventHandler(this.AttackLegs_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(78)))), ((int)(((byte)(84)))));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox1.ForeColor = System.Drawing.Color.White;
            this.richTextBox1.Location = new System.Drawing.Point(180, 139);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(326, 276);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(195, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Защита";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(276, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Атака";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Вы";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(562, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Противник";
            // 
            // HealthPlayer
            // 
            this.HealthPlayer.AutoSize = true;
            this.HealthPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HealthPlayer.Location = new System.Drawing.Point(68, 289);
            this.HealthPlayer.Name = "HealthPlayer";
            this.HealthPlayer.Size = new System.Drawing.Size(50, 20);
            this.HealthPlayer.TabIndex = 7;
            this.HealthPlayer.Text = "100%";
            // 
            // HealthEnemy
            // 
            this.HealthEnemy.AutoSize = true;
            this.HealthEnemy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HealthEnemy.Location = new System.Drawing.Point(574, 289);
            this.HealthEnemy.Name = "HealthEnemy";
            this.HealthEnemy.Size = new System.Drawing.Size(50, 20);
            this.HealthEnemy.TabIndex = 7;
            this.HealthEnemy.Text = "100%";
            // 
            // MakeStep
            // 
            this.MakeStep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MakeStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MakeStep.Location = new System.Drawing.Point(342, 48);
            this.MakeStep.Name = "MakeStep";
            this.MakeStep.Size = new System.Drawing.Size(164, 81);
            this.MakeStep.TabIndex = 8;
            this.MakeStep.Text = "Вперед!";
            this.MakeStep.UseVisualStyleBackColor = true;
            this.MakeStep.Click += new System.EventHandler(this.MakeStep_Click);
            // 
            // newGame_btn
            // 
            this.newGame_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newGame_btn.Location = new System.Drawing.Point(22, 343);
            this.newGame_btn.Name = "newGame_btn";
            this.newGame_btn.Size = new System.Drawing.Size(75, 23);
            this.newGame_btn.TabIndex = 9;
            this.newGame_btn.Text = "Новая игра";
            this.newGame_btn.UseVisualStyleBackColor = true;
            this.newGame_btn.Click += new System.EventHandler(this.newGame_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(686, 427);
            this.Controls.Add(this.newGame_btn);
            this.Controls.Add(this.MakeStep);
            this.Controls.Add(this.HealthEnemy);
            this.Controls.Add(this.HealthPlayer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.AttackLegs);
            this.Controls.Add(this.ProtectLegs);
            this.Controls.Add(this.AttackBody);
            this.Controls.Add(this.ProtectBody);
            this.Controls.Add(this.AttackHead);
            this.Controls.Add(this.ProtectHead);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "The Elder Scrolls VI";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button ProtectHead;
        private System.Windows.Forms.Button ProtectBody;
        private System.Windows.Forms.Button ProtectLegs;
        private System.Windows.Forms.Button AttackHead;
        private System.Windows.Forms.Button AttackBody;
        private System.Windows.Forms.Button AttackLegs;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label HealthPlayer;
        private System.Windows.Forms.Label HealthEnemy;
        private System.Windows.Forms.Button MakeStep;
        private System.Windows.Forms.Button newGame_btn;
    }
}

