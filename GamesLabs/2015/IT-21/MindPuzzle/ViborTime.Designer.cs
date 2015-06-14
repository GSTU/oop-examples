namespace MindPuzzle
{
    partial class ViborTime
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
            this.RB2 = new System.Windows.Forms.RadioButton();
            this.RB1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.RB3 = new System.Windows.Forms.RadioButton();
            this.GO = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RB2
            // 
            this.RB2.AutoSize = true;
            this.RB2.Location = new System.Drawing.Point(62, 42);
            this.RB2.Name = "RB2";
            this.RB2.Size = new System.Drawing.Size(14, 13);
            this.RB2.TabIndex = 8;
            this.RB2.TabStop = true;
            this.RB2.UseVisualStyleBackColor = true;
            this.RB2.CheckedChanged += new System.EventHandler(this.RB2_CheckedChanged);
            // 
            // RB1
            // 
            this.RB1.AutoSize = true;
            this.RB1.Location = new System.Drawing.Point(62, 24);
            this.RB1.Name = "RB1";
            this.RB1.Size = new System.Drawing.Size(14, 13);
            this.RB1.TabIndex = 7;
            this.RB1.TabStop = true;
            this.RB1.UseVisualStyleBackColor = true;
            this.RB1.CheckedChanged += new System.EventHandler(this.RB1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Время для игры выбрал, ага?!";
            // 
            // RB3
            // 
            this.RB3.AutoSize = true;
            this.RB3.Location = new System.Drawing.Point(62, 60);
            this.RB3.Name = "RB3";
            this.RB3.Size = new System.Drawing.Size(14, 13);
            this.RB3.TabIndex = 9;
            this.RB3.TabStop = true;
            this.RB3.UseVisualStyleBackColor = true;
            this.RB3.CheckedChanged += new System.EventHandler(this.RB3_CheckedChanged);
            // 
            // GO
            // 
            this.GO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GO.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.GO.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GO.Location = new System.Drawing.Point(6, 79);
            this.GO.Name = "GO";
            this.GO.Size = new System.Drawing.Size(180, 23);
            this.GO.TabIndex = 10;
            this.GO.Text = "Начать игру!";
            this.GO.UseVisualStyleBackColor = true;
            // 
            // Vibor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(192, 107);
            this.ControlBox = false;
            this.Controls.Add(this.GO);
            this.Controls.Add(this.RB3);
            this.Controls.Add(this.RB2);
            this.Controls.Add(this.RB1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Vibor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vibor";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton RB2;
        private System.Windows.Forms.RadioButton RB1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton RB3;
        private System.Windows.Forms.Button GO;
    }
}