namespace game
{
    partial class NextLevel
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
            this.Body = new GhostTheme();
            this.CloseWindow = new GhostButton();
            this.OK = new System.Windows.Forms.Button();
            this.Panel = new GhostGroupBox();
            this.LabelNameLevel = new System.Windows.Forms.Label();
            this.LabelTime = new System.Windows.Forms.Label();
            this.LabelBalance = new System.Windows.Forms.Label();
            this.Time = new System.Windows.Forms.TextBox();
            this.Balance = new System.Windows.Forms.TextBox();
            this.NameLevel = new System.Windows.Forms.TextBox();
            this.close = new GhostButton();
            this.Body.SuspendLayout();
            this.Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Body
            // 
            this.Body.BorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Body.Colors = new Bloom[0];
            this.Body.Controls.Add(this.CloseWindow);
            this.Body.Controls.Add(this.OK);
            this.Body.Controls.Add(this.Panel);
            this.Body.Customization = "";
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Body.Image = null;
            this.Body.Location = new System.Drawing.Point(0, 0);
            this.Body.Movable = true;
            this.Body.Name = "Body";
            this.Body.NoRounding = false;
            this.Body.ShowIcon = false;
            this.Body.Sizable = true;
            this.Body.Size = new System.Drawing.Size(328, 194);
            this.Body.SmartBounds = true;
            this.Body.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Body.TabIndex = 0;
            this.Body.Text = "Уровень пройден! :)";
            this.Body.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Body.Transparent = false;
            // 
            // CloseWindow
            // 
            this.CloseWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseWindow.Color = System.Drawing.Color.Empty;
            this.CloseWindow.Colors = new Bloom[0];
            this.CloseWindow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseWindow.Customization = "";
            this.CloseWindow.EnableGlass = true;
            this.CloseWindow.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CloseWindow.Image = null;
            this.CloseWindow.Location = new System.Drawing.Point(298, 2);
            this.CloseWindow.Name = "CloseWindow";
            this.CloseWindow.NoRounding = false;
            this.CloseWindow.Size = new System.Drawing.Size(21, 21);
            this.CloseWindow.TabIndex = 17;
            this.CloseWindow.Text = "x";
            this.CloseWindow.Transparent = false;
            this.CloseWindow.Click += new System.EventHandler(this.CloseWindow_Click);
            // 
            // OK
            // 
            this.OK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OK.BackColor = System.Drawing.SystemColors.InfoText;
            this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.OK.FlatAppearance.BorderSize = 2;
            this.OK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.OK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OK.ForeColor = System.Drawing.Color.LightGray;
            this.OK.Location = new System.Drawing.Point(12, 140);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(304, 42);
            this.OK.TabIndex = 16;
            this.OK.Text = "Следующий уровень";
            this.OK.UseVisualStyleBackColor = false;
            // 
            // Panel
            // 
            this.Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel.Colors = new Bloom[0];
            this.Panel.Controls.Add(this.LabelNameLevel);
            this.Panel.Controls.Add(this.LabelTime);
            this.Panel.Controls.Add(this.LabelBalance);
            this.Panel.Controls.Add(this.Time);
            this.Panel.Controls.Add(this.Balance);
            this.Panel.Controls.Add(this.NameLevel);
            this.Panel.Customization = "";
            this.Panel.Font = new System.Drawing.Font("Verdana", 8F);
            this.Panel.Image = null;
            this.Panel.Location = new System.Drawing.Point(12, 30);
            this.Panel.Name = "Panel";
            this.Panel.NoRounding = false;
            this.Panel.Size = new System.Drawing.Size(304, 104);
            this.Panel.TabIndex = 15;
            this.Panel.Text = "       Информация по пройденному уровню:";
            this.Panel.Transparent = false;
            // 
            // LabelNameLevel
            // 
            this.LabelNameLevel.AutoSize = true;
            this.LabelNameLevel.BackColor = System.Drawing.Color.Black;
            this.LabelNameLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LabelNameLevel.Location = new System.Drawing.Point(11, 32);
            this.LabelNameLevel.Name = "LabelNameLevel";
            this.LabelNameLevel.Size = new System.Drawing.Size(62, 13);
            this.LabelNameLevel.TabIndex = 5;
            this.LabelNameLevel.Text = "Уровень:";
            // 
            // LabelTime
            // 
            this.LabelTime.AutoSize = true;
            this.LabelTime.BackColor = System.Drawing.Color.Black;
            this.LabelTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LabelTime.Location = new System.Drawing.Point(11, 80);
            this.LabelTime.Name = "LabelTime";
            this.LabelTime.Size = new System.Drawing.Size(126, 13);
            this.LabelTime.TabIndex = 4;
            this.LabelTime.Text = "Затрачено времени:";
            // 
            // LabelBalance
            // 
            this.LabelBalance.AutoSize = true;
            this.LabelBalance.BackColor = System.Drawing.Color.Black;
            this.LabelBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LabelBalance.Location = new System.Drawing.Point(11, 56);
            this.LabelBalance.Name = "LabelBalance";
            this.LabelBalance.Size = new System.Drawing.Size(101, 13);
            this.LabelBalance.TabIndex = 3;
            this.LabelBalance.Text = "Набрано очков:";
            // 
            // Time
            // 
            this.Time.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Time.BackColor = System.Drawing.SystemColors.InfoText;
            this.Time.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Time.Cursor = System.Windows.Forms.Cursors.Default;
            this.Time.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Time.ForeColor = System.Drawing.Color.White;
            this.Time.Location = new System.Drawing.Point(6, 75);
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.Size = new System.Drawing.Size(292, 23);
            this.Time.TabIndex = 2;
            this.Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Balance
            // 
            this.Balance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Balance.BackColor = System.Drawing.SystemColors.InfoText;
            this.Balance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Balance.Cursor = System.Windows.Forms.Cursors.Default;
            this.Balance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Balance.ForeColor = System.Drawing.Color.White;
            this.Balance.Location = new System.Drawing.Point(6, 51);
            this.Balance.Name = "Balance";
            this.Balance.ReadOnly = true;
            this.Balance.Size = new System.Drawing.Size(292, 23);
            this.Balance.TabIndex = 1;
            this.Balance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // NameLevel
            // 
            this.NameLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NameLevel.BackColor = System.Drawing.SystemColors.InfoText;
            this.NameLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NameLevel.Cursor = System.Windows.Forms.Cursors.Default;
            this.NameLevel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameLevel.ForeColor = System.Drawing.Color.White;
            this.NameLevel.Location = new System.Drawing.Point(6, 27);
            this.NameLevel.Name = "NameLevel";
            this.NameLevel.ReadOnly = true;
            this.NameLevel.Size = new System.Drawing.Size(292, 23);
            this.NameLevel.TabIndex = 0;
            this.NameLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // close
            // 
            this.close.Color = System.Drawing.Color.Empty;
            this.close.Colors = new Bloom[0];
            this.close.Customization = "";
            this.close.EnableGlass = true;
            this.close.Font = new System.Drawing.Font("Verdana", 8F);
            this.close.Image = null;
            this.close.Location = new System.Drawing.Point(0, 0);
            this.close.Name = "close";
            this.close.NoRounding = false;
            this.close.Size = new System.Drawing.Size(0, 0);
            this.close.TabIndex = 0;
            this.close.Transparent = false;
            // 
            // NextLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 194);
            this.Controls.Add(this.Body);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NextLevel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Уровень пройден! :)";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Body.ResumeLayout(false);
            this.Panel.ResumeLayout(false);
            this.Panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GhostTheme Body;
        private GhostGroupBox Panel;
        private System.Windows.Forms.TextBox Time;
        private System.Windows.Forms.TextBox Balance;
        private System.Windows.Forms.TextBox NameLevel;
        private System.Windows.Forms.Label LabelNameLevel;
        private System.Windows.Forms.Label LabelTime;
        private System.Windows.Forms.Label LabelBalance;
        private System.Windows.Forms.Button OK;
        private GhostButton close;
        private GhostButton CloseWindow;

    }
}