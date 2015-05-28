namespace game
{
    partial class Info
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Info));
            this.Body = new GhostTheme();
            this.Title = new System.Windows.Forms.Label();
            this.InfoText = new System.Windows.Forms.TextBox();
            this.OK = new GhostButton();
            this.CloseWindow = new GhostButton();
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // Body
            // 
            this.Body.BorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Body.Colors = new Bloom[0];
            this.Body.Controls.Add(this.Title);
            this.Body.Controls.Add(this.InfoText);
            this.Body.Controls.Add(this.OK);
            this.Body.Controls.Add(this.CloseWindow);
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
            this.Body.Size = new System.Drawing.Size(475, 343);
            this.Body.SmartBounds = true;
            this.Body.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Body.TabIndex = 0;
            this.Body.Text = "Информация по игре";
            this.Body.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Body.Transparent = false;
            // 
            // Title
            // 
            this.Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Title.AutoSize = true;
            this.Title.BackColor = System.Drawing.Color.Transparent;
            this.Title.Font = new System.Drawing.Font("Verdana", 12.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.Location = new System.Drawing.Point(26, 32);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(421, 20);
            this.Title.TabIndex = 21;
            this.Title.Text = "Инструкции, правила и особенности игры";
            // 
            // InfoText
            // 
            this.InfoText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoText.BackColor = System.Drawing.Color.Black;
            this.InfoText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InfoText.Cursor = System.Windows.Forms.Cursors.Default;
            this.InfoText.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InfoText.ForeColor = System.Drawing.Color.LightGray;
            this.InfoText.Location = new System.Drawing.Point(13, 56);
            this.InfoText.Multiline = true;
            this.InfoText.Name = "InfoText";
            this.InfoText.ReadOnly = true;
            this.InfoText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.InfoText.Size = new System.Drawing.Size(450, 217);
            this.InfoText.TabIndex = 20;
            this.InfoText.Text = resources.GetString("InfoText.Text");
            this.InfoText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // OK
            // 
            this.OK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OK.BackColor = System.Drawing.Color.Green;
            this.OK.Color = System.Drawing.Color.Empty;
            this.OK.Colors = new Bloom[0];
            this.OK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OK.Customization = "";
            this.OK.EnableGlass = true;
            this.OK.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OK.Image = null;
            this.OK.Location = new System.Drawing.Point(12, 279);
            this.OK.Name = "OK";
            this.OK.NoRounding = false;
            this.OK.Size = new System.Drawing.Size(452, 52);
            this.OK.TabIndex = 19;
            this.OK.Text = "Закрыть";
            this.OK.Transparent = false;
            this.OK.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CloseThisWindow);
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
            this.CloseWindow.Location = new System.Drawing.Point(445, 2);
            this.CloseWindow.Name = "CloseWindow";
            this.CloseWindow.NoRounding = false;
            this.CloseWindow.Size = new System.Drawing.Size(21, 21);
            this.CloseWindow.TabIndex = 18;
            this.CloseWindow.Text = "x";
            this.CloseWindow.Transparent = false;
            this.CloseWindow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CloseThisWindow);
            // 
            // Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 343);
            this.ControlBox = false;
            this.Controls.Add(this.Body);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Info";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Info";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Body.ResumeLayout(false);
            this.Body.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GhostTheme Body;
        private GhostButton CloseWindow;
        private GhostButton OK;
        private System.Windows.Forms.TextBox InfoText;
        private System.Windows.Forms.Label Title;
    }
}