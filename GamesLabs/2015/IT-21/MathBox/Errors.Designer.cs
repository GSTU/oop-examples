namespace game
{
    partial class Errors
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
            this.CloseErrorWindows = new GhostButton();
            this.OK = new GhostButton();
            this.InfoError = new System.Windows.Forms.TextBox();
            this.LabelInfo = new System.Windows.Forms.Label();
            this.TitleSmall = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.Label();
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // Body
            // 
            this.Body.BorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Body.Colors = new Bloom[0];
            this.Body.Controls.Add(this.CloseErrorWindows);
            this.Body.Controls.Add(this.OK);
            this.Body.Controls.Add(this.InfoError);
            this.Body.Controls.Add(this.LabelInfo);
            this.Body.Controls.Add(this.TitleSmall);
            this.Body.Controls.Add(this.Title);
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
            this.Body.Size = new System.Drawing.Size(276, 245);
            this.Body.SmartBounds = true;
            this.Body.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Body.TabIndex = 0;
            this.Body.Text = "Ошибка!";
            this.Body.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Body.Transparent = false;
            // 
            // CloseErrorWindows
            // 
            this.CloseErrorWindows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseErrorWindows.Color = System.Drawing.Color.Empty;
            this.CloseErrorWindows.Colors = new Bloom[0];
            this.CloseErrorWindows.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseErrorWindows.Customization = "";
            this.CloseErrorWindows.EnableGlass = true;
            this.CloseErrorWindows.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CloseErrorWindows.Image = null;
            this.CloseErrorWindows.Location = new System.Drawing.Point(249, 4);
            this.CloseErrorWindows.Name = "CloseErrorWindows";
            this.CloseErrorWindows.NoRounding = false;
            this.CloseErrorWindows.Size = new System.Drawing.Size(18, 18);
            this.CloseErrorWindows.TabIndex = 13;
            this.CloseErrorWindows.Text = "X";
            this.CloseErrorWindows.Transparent = false;
            this.CloseErrorWindows.Click += new System.EventHandler(this.CloseWindow);
            // 
            // OK
            // 
            this.OK.BackColor = System.Drawing.Color.Red;
            this.OK.Color = System.Drawing.Color.Empty;
            this.OK.Colors = new Bloom[0];
            this.OK.Customization = "";
            this.OK.EnableGlass = true;
            this.OK.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OK.Image = null;
            this.OK.Location = new System.Drawing.Point(12, 196);
            this.OK.Name = "OK";
            this.OK.NoRounding = false;
            this.OK.Size = new System.Drawing.Size(253, 38);
            this.OK.TabIndex = 4;
            this.OK.Text = "Принять и закрыть ;)";
            this.OK.Transparent = false;
            this.OK.Click += new System.EventHandler(this.CloseWindow);
            // 
            // InfoError
            // 
            this.InfoError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoError.BackColor = System.Drawing.Color.Black;
            this.InfoError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InfoError.Cursor = System.Windows.Forms.Cursors.Default;
            this.InfoError.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InfoError.ForeColor = System.Drawing.Color.Gainsboro;
            this.InfoError.Location = new System.Drawing.Point(13, 106);
            this.InfoError.Multiline = true;
            this.InfoError.Name = "InfoError";
            this.InfoError.ReadOnly = true;
            this.InfoError.Size = new System.Drawing.Size(251, 86);
            this.InfoError.TabIndex = 3;
            this.InfoError.TabStop = false;
            // 
            // LabelInfo
            // 
            this.LabelInfo.AutoSize = true;
            this.LabelInfo.BackColor = System.Drawing.Color.Transparent;
            this.LabelInfo.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelInfo.ForeColor = System.Drawing.Color.Silver;
            this.LabelInfo.Location = new System.Drawing.Point(15, 91);
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Size = new System.Drawing.Size(157, 13);
            this.LabelInfo.TabIndex = 2;
            this.LabelInfo.Text = "Подробнее об ошибке:";
            // 
            // TitleSmall
            // 
            this.TitleSmall.AutoSize = true;
            this.TitleSmall.BackColor = System.Drawing.Color.Transparent;
            this.TitleSmall.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TitleSmall.ForeColor = System.Drawing.Color.Silver;
            this.TitleSmall.Location = new System.Drawing.Point(11, 51);
            this.TitleSmall.Name = "TitleSmall";
            this.TitleSmall.Size = new System.Drawing.Size(253, 24);
            this.TitleSmall.TabIndex = 1;
            this.TitleSmall.Text = "В ходе работы приложения возникла ошибка. \r\nПросим прощения за доставленные неудо" +
    "бства.";
            this.TitleSmall.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Title
            // 
            this.Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Title.AutoSize = true;
            this.Title.BackColor = System.Drawing.Color.Transparent;
            this.Title.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.Location = new System.Drawing.Point(10, 28);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(256, 23);
            this.Title.TabIndex = 0;
            this.Title.Text = "Приносим извинения!";
            // 
            // Errors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 245);
            this.Controls.Add(this.Body);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Errors";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Errors";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Body.ResumeLayout(false);
            this.Body.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GhostTheme Body;
        private System.Windows.Forms.Label LabelInfo;
        private System.Windows.Forms.Label TitleSmall;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.TextBox InfoError;
        private GhostButton OK;
        private GhostButton CloseErrorWindows;
    }
}