namespace Saper
{
    partial class FormSettings
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
            this.trackBarHeight = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBarMineCount = new System.Windows.Forms.TrackBar();
            this.trackBarWidth = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMineCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarHeight
            // 
            this.trackBarHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarHeight.Location = new System.Drawing.Point(12, 25);
            this.trackBarHeight.Maximum = 30;
            this.trackBarHeight.Minimum = 10;
            this.trackBarHeight.Name = "trackBarHeight";
            this.trackBarHeight.Size = new System.Drawing.Size(425, 45);
            this.trackBarHeight.TabIndex = 0;
            this.trackBarHeight.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarHeight.Value = 10;
            this.trackBarHeight.Scroll += new System.EventHandler(this.trackBarHeight_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Высота: 10";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Количество мин: 10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Ширина: 10";
            // 
            // trackBarMineCount
            // 
            this.trackBarMineCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarMineCount.Location = new System.Drawing.Point(12, 153);
            this.trackBarMineCount.Maximum = 100;
            this.trackBarMineCount.Minimum = 3;
            this.trackBarMineCount.Name = "trackBarMineCount";
            this.trackBarMineCount.Size = new System.Drawing.Size(425, 45);
            this.trackBarMineCount.TabIndex = 4;
            this.trackBarMineCount.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarMineCount.Value = 10;
            this.trackBarMineCount.Scroll += new System.EventHandler(this.trackBarMineCount_Scroll);
            // 
            // trackBarWidth
            // 
            this.trackBarWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarWidth.Location = new System.Drawing.Point(12, 89);
            this.trackBarWidth.Maximum = 30;
            this.trackBarWidth.Minimum = 10;
            this.trackBarWidth.Name = "trackBarWidth";
            this.trackBarWidth.Size = new System.Drawing.Size(425, 45);
            this.trackBarWidth.TabIndex = 5;
            this.trackBarWidth.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarWidth.Value = 10;
            this.trackBarWidth.Scroll += new System.EventHandler(this.trackBarWidth_Scroll);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(12, 206);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 35);
            this.button1.TabIndex = 6;
            this.button1.Text = "ОК";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(312, 206);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 35);
            this.button2.TabIndex = 7;
            this.button2.Text = "Отменить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 253);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.trackBarWidth);
            this.Controls.Add(this.trackBarMineCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarHeight);
            this.Name = "FormSettings";
            this.Text = "Настройка сложности";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMineCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBarMineCount;
        private System.Windows.Forms.TrackBar trackBarWidth;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}