namespace MindPuzzle
{
    partial class MindPuzzle
    {
        /// <summary>
        /// 
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Очищает ранее использованные ресурсы
        /// </summary>
        /// <param name="disposing"></param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MindPuzzle));
            this.PanelMenu = new System.Windows.Forms.Panel();
            this.T1 = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.T2 = new System.Windows.Forms.Button();
            this.T3 = new System.Windows.Forms.Button();
            this.PanelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelMenu
            // 
            this.PanelMenu.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.PanelMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelMenu.Controls.Add(this.T3);
            this.PanelMenu.Controls.Add(this.T2);
            this.PanelMenu.Controls.Add(this.T1);
            this.PanelMenu.Controls.Add(this.Title);
            this.PanelMenu.Location = new System.Drawing.Point(12, 64);
            this.PanelMenu.Name = "PanelMenu";
            this.PanelMenu.Size = new System.Drawing.Size(218, 110);
            this.PanelMenu.TabIndex = 0;
            // 
            // T1
            // 
            this.T1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.T1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.T1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.T1.Location = new System.Drawing.Point(7, 29);
            this.T1.Name = "T1";
            this.T1.Size = new System.Drawing.Size(202, 23);
            this.T1.TabIndex = 4;
            this.T1.UseVisualStyleBackColor = false;
            this.T1.Click += new System.EventHandler(this.T1_Click);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Title.Location = new System.Drawing.Point(12, 4);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(192, 20);
            this.Title.TabIndex = 0;
            this.Title.Text = "Таймер выбрал, ага?!";
            // 
            // T2
            // 
            this.T2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.T2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.T2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.T2.Location = new System.Drawing.Point(7, 54);
            this.T2.Name = "T2";
            this.T2.Size = new System.Drawing.Size(202, 23);
            this.T2.TabIndex = 5;
            this.T2.UseVisualStyleBackColor = false;
            this.T2.Click += new System.EventHandler(this.T2_Click);
            // 
            // T3
            // 
            this.T3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.T3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.T3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.T3.Location = new System.Drawing.Point(7, 79);
            this.T3.Name = "T3";
            this.T3.Size = new System.Drawing.Size(202, 23);
            this.T3.TabIndex = 6;
            this.T3.UseVisualStyleBackColor = false;
            this.T3.Click += new System.EventHandler(this.T3_Click);
            // 
            // MindPuzzle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 251);
            this.Controls.Add(this.PanelMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MindPuzzle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MindPuzzle";
            this.Load += new System.EventHandler(this.MindPuzzle_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Key_Down);
            this.PanelMenu.ResumeLayout(false);
            this.PanelMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelMenu;
        private System.Windows.Forms.Button T1;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button T3;
        private System.Windows.Forms.Button T2;


    }
}

