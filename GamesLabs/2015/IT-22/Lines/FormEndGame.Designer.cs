namespace linesk
{
    partial class FormEndGame
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
            this.labelTablo = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.labelPlaceGraphItem = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelTablo
            // 
            this.labelTablo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTablo.Location = new System.Drawing.Point(88, 23);
            this.labelTablo.Name = "labelTablo";
            this.labelTablo.Size = new System.Drawing.Size(243, 70);
            this.labelTablo.TabIndex = 0;
            this.labelTablo.Text = "Игра закончена!\r\nЖелаете начать новую игру?";
            this.labelTablo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(84, 113);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Да";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(181, 113);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Нет";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // labelPlaceGraphItem
            // 
            this.labelPlaceGraphItem.Location = new System.Drawing.Point(12, 23);
            this.labelPlaceGraphItem.Name = "labelPlaceGraphItem";
            this.labelPlaceGraphItem.Size = new System.Drawing.Size(70, 70);
            this.labelPlaceGraphItem.TabIndex = 3;
            this.labelPlaceGraphItem.Text = "label2";
            this.labelPlaceGraphItem.Paint += new System.Windows.Forms.PaintEventHandler(this.labelPlaceGraphItem_Paint);
            // 
            // FormEndGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 148);
            this.Controls.Add(this.labelPlaceGraphItem);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelTablo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEndGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Поздравляем!";
            this.Load += new System.EventHandler(this.FormEndGame_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTablo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelPlaceGraphItem;
    }
}