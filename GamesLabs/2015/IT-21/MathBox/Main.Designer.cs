namespace game
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.stream = new System.Windows.Forms.Timer(this.components);
            this.Theme = new GhostTheme();
            this.ghostButton2 = new GhostButton();
            this.ghostButton1 = new GhostButton();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.DrapesPanel = new System.Windows.Forms.Panel();
            this.GamePanel = new System.Windows.Forms.Panel();
            this.ButtonTren = new System.Windows.Forms.Panel();
            this.LabelTren = new System.Windows.Forms.Label();
            this.ButtonInfo = new System.Windows.Forms.Panel();
            this.LabelInfo = new System.Windows.Forms.Label();
            this.x5 = new GhostGroupBox();
            this.expert = new GhostGroupBox();
            this.x6 = new GhostGroupBox();
            this.x10 = new GhostGroupBox();
            this.x8 = new GhostGroupBox();
            this.close = new GhostButton();
            this.DopPanel = new System.Windows.Forms.Panel();
            this.CurtainButtonPanel = new System.Windows.Forms.Panel();
            this.CurtainButtonText = new System.Windows.Forms.Label();
            this.StartButton = new GhostButton();
            this.RecordTable = new GhostGroupBox();
            this.RecordTime = new System.Windows.Forms.Label();
            this.RecordPoints = new System.Windows.Forms.Button();
            this.TimeTable = new GhostGroupBox();
            this.TimeLable = new System.Windows.Forms.Label();
            this.BalanceTable = new GhostGroupBox();
            this.Balance = new System.Windows.Forms.Button();
            this.NumberTable = new GhostGroupBox();
            this.SearchGenSum = new System.Windows.Forms.Button();
            this.turn = new GhostButton();
            this.Theme.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.DrapesPanel.SuspendLayout();
            this.ButtonTren.SuspendLayout();
            this.ButtonInfo.SuspendLayout();
            this.DopPanel.SuspendLayout();
            this.CurtainButtonPanel.SuspendLayout();
            this.RecordTable.SuspendLayout();
            this.TimeTable.SuspendLayout();
            this.BalanceTable.SuspendLayout();
            this.NumberTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // stream
            // 
            this.stream.Enabled = true;
            this.stream.Tick += new System.EventHandler(this.stream_Tick);
            // 
            // Theme
            // 
            this.Theme.BorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Theme.Colors = new Bloom[0];
            this.Theme.Controls.Add(this.ghostButton2);
            this.Theme.Controls.Add(this.ghostButton1);
            this.Theme.Controls.Add(this.MainPanel);
            this.Theme.Controls.Add(this.close);
            this.Theme.Controls.Add(this.DopPanel);
            this.Theme.Controls.Add(this.turn);
            this.Theme.Customization = "";
            this.Theme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Theme.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Theme.Image = ((System.Drawing.Image)(resources.GetObject("Theme.Image")));
            this.Theme.Location = new System.Drawing.Point(0, 0);
            this.Theme.Movable = true;
            this.Theme.Name = "Theme";
            this.Theme.NoRounding = false;
            this.Theme.ShowIcon = false;
            this.Theme.Sizable = true;
            this.Theme.Size = new System.Drawing.Size(497, 370);
            this.Theme.SmartBounds = true;
            this.Theme.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Theme.TabIndex = 0;
            this.Theme.Text = "-+ MathBox +-";
            this.Theme.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Theme.Transparent = false;
            // 
            // ghostButton2
            // 
            this.ghostButton2.Color = System.Drawing.Color.Empty;
            this.ghostButton2.Colors = new Bloom[0];
            this.ghostButton2.Customization = "";
            this.ghostButton2.EnableGlass = true;
            this.ghostButton2.Font = new System.Drawing.Font("Verdana", 8F);
            this.ghostButton2.Image = null;
            this.ghostButton2.Location = new System.Drawing.Point(0, 0);
            this.ghostButton2.Name = "ghostButton2";
            this.ghostButton2.NoRounding = false;
            this.ghostButton2.Size = new System.Drawing.Size(0, 0);
            this.ghostButton2.TabIndex = 0;
            this.ghostButton2.Transparent = false;
            // 
            // ghostButton1
            // 
            this.ghostButton1.Color = System.Drawing.Color.Empty;
            this.ghostButton1.Colors = new Bloom[0];
            this.ghostButton1.Customization = "";
            this.ghostButton1.EnableGlass = true;
            this.ghostButton1.Font = new System.Drawing.Font("Verdana", 8F);
            this.ghostButton1.Image = null;
            this.ghostButton1.Location = new System.Drawing.Point(0, 0);
            this.ghostButton1.Name = "ghostButton1";
            this.ghostButton1.NoRounding = false;
            this.ghostButton1.Size = new System.Drawing.Size(0, 0);
            this.ghostButton1.TabIndex = 1;
            this.ghostButton1.Transparent = false;
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.BackColor = System.Drawing.Color.Transparent;
            this.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainPanel.Controls.Add(this.DrapesPanel);
            this.MainPanel.ForeColor = System.Drawing.Color.Transparent;
            this.MainPanel.Location = new System.Drawing.Point(10, 28);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(332, 332);
            this.MainPanel.TabIndex = 8;
            // 
            // DrapesPanel
            // 
            this.DrapesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DrapesPanel.Controls.Add(this.GamePanel);
            this.DrapesPanel.Controls.Add(this.ButtonTren);
            this.DrapesPanel.Controls.Add(this.ButtonInfo);
            this.DrapesPanel.Controls.Add(this.x5);
            this.DrapesPanel.Controls.Add(this.expert);
            this.DrapesPanel.Controls.Add(this.x6);
            this.DrapesPanel.Controls.Add(this.x10);
            this.DrapesPanel.Controls.Add(this.x8);
            this.DrapesPanel.Location = new System.Drawing.Point(-1, -1);
            this.DrapesPanel.Name = "DrapesPanel";
            this.DrapesPanel.Size = new System.Drawing.Size(332, 332);
            this.DrapesPanel.TabIndex = 13;
            // 
            // GamePanel
            // 
            this.GamePanel.Location = new System.Drawing.Point(0, 0);
            this.GamePanel.Name = "GamePanel";
            this.GamePanel.Size = new System.Drawing.Size(332, 332);
            this.GamePanel.TabIndex = 14;
            this.GamePanel.Visible = false;
            // 
            // ButtonTren
            // 
            this.ButtonTren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonTren.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ButtonTren.Controls.Add(this.LabelTren);
            this.ButtonTren.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonTren.Location = new System.Drawing.Point(168, 7);
            this.ButtonTren.Name = "ButtonTren";
            this.ButtonTren.Size = new System.Drawing.Size(158, 25);
            this.ButtonTren.TabIndex = 6;
            this.ButtonTren.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Training);
            // 
            // LabelTren
            // 
            this.LabelTren.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelTren.AutoSize = true;
            this.LabelTren.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelTren.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.LabelTren.Location = new System.Drawing.Point(39, 5);
            this.LabelTren.Name = "LabelTren";
            this.LabelTren.Size = new System.Drawing.Size(86, 13);
            this.LabelTren.TabIndex = 0;
            this.LabelTren.Text = "Тренировка";
            this.LabelTren.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LabelTren.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Training);
            // 
            // ButtonInfo
            // 
            this.ButtonInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ButtonInfo.Controls.Add(this.LabelInfo);
            this.ButtonInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonInfo.Location = new System.Drawing.Point(6, 7);
            this.ButtonInfo.Name = "ButtonInfo";
            this.ButtonInfo.Size = new System.Drawing.Size(158, 25);
            this.ButtonInfo.TabIndex = 5;
            this.ButtonInfo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OpenInfoWindows);
            // 
            // LabelInfo
            // 
            this.LabelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelInfo.AutoSize = true;
            this.LabelInfo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelInfo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.LabelInfo.Location = new System.Drawing.Point(38, 5);
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Size = new System.Drawing.Size(91, 13);
            this.LabelInfo.TabIndex = 0;
            this.LabelInfo.Text = "Информация";
            this.LabelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LabelInfo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OpenInfoWindows);
            // 
            // x5
            // 
            this.x5.BackColor = System.Drawing.Color.LightGray;
            this.x5.Colors = new Bloom[0];
            this.x5.Customization = "";
            this.x5.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.x5.Image = null;
            this.x5.Location = new System.Drawing.Point(5, 35);
            this.x5.Name = "x5";
            this.x5.NoRounding = false;
            this.x5.Size = new System.Drawing.Size(160, 111);
            this.x5.TabIndex = 0;
            this.x5.Text = "             5x5";
            this.x5.Transparent = false;
            // 
            // expert
            // 
            this.expert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expert.BackColor = System.Drawing.Color.LightGray;
            this.expert.Colors = new Bloom[0];
            this.expert.Customization = "";
            this.expert.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.expert.Image = null;
            this.expert.Location = new System.Drawing.Point(5, 261);
            this.expert.Name = "expert";
            this.expert.NoRounding = false;
            this.expert.Size = new System.Drawing.Size(322, 66);
            this.expert.TabIndex = 4;
            this.expert.Text = "                                ЭКСПЕРТ";
            this.expert.Transparent = false;
            // 
            // x6
            // 
            this.x6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.x6.BackColor = System.Drawing.Color.LightGray;
            this.x6.Colors = new Bloom[0];
            this.x6.Customization = "";
            this.x6.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.x6.Image = null;
            this.x6.Location = new System.Drawing.Point(167, 35);
            this.x6.Name = "x6";
            this.x6.NoRounding = false;
            this.x6.Size = new System.Drawing.Size(160, 111);
            this.x6.TabIndex = 1;
            this.x6.Text = "             6x6";
            this.x6.Transparent = false;
            // 
            // x10
            // 
            this.x10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.x10.BackColor = System.Drawing.Color.LightGray;
            this.x10.Colors = new Bloom[0];
            this.x10.Customization = "";
            this.x10.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.x10.Image = null;
            this.x10.Location = new System.Drawing.Point(167, 148);
            this.x10.Name = "x10";
            this.x10.NoRounding = false;
            this.x10.Size = new System.Drawing.Size(160, 111);
            this.x10.TabIndex = 3;
            this.x10.Text = "           10x10";
            this.x10.Transparent = false;
            // 
            // x8
            // 
            this.x8.BackColor = System.Drawing.Color.LightGray;
            this.x8.Colors = new Bloom[0];
            this.x8.Customization = "";
            this.x8.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.x8.Image = null;
            this.x8.Location = new System.Drawing.Point(5, 148);
            this.x8.Name = "x8";
            this.x8.NoRounding = false;
            this.x8.Size = new System.Drawing.Size(160, 111);
            this.x8.TabIndex = 2;
            this.x8.Text = "             8x8";
            this.x8.Transparent = false;
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.close.Color = System.Drawing.Color.Empty;
            this.close.Colors = new Bloom[0];
            this.close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close.Customization = "";
            this.close.EnableGlass = true;
            this.close.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.close.Image = null;
            this.close.Location = new System.Drawing.Point(467, 2);
            this.close.Name = "close";
            this.close.NoRounding = false;
            this.close.Size = new System.Drawing.Size(21, 21);
            this.close.TabIndex = 12;
            this.close.Text = "x";
            this.close.Transparent = false;
            this.close.Click += new System.EventHandler(this.CloseProgram);
            // 
            // DopPanel
            // 
            this.DopPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DopPanel.BackColor = System.Drawing.Color.Transparent;
            this.DopPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DopPanel.Controls.Add(this.CurtainButtonPanel);
            this.DopPanel.Controls.Add(this.StartButton);
            this.DopPanel.Controls.Add(this.RecordTable);
            this.DopPanel.Controls.Add(this.TimeTable);
            this.DopPanel.Controls.Add(this.BalanceTable);
            this.DopPanel.Controls.Add(this.NumberTable);
            this.DopPanel.ForeColor = System.Drawing.Color.Transparent;
            this.DopPanel.Location = new System.Drawing.Point(345, 28);
            this.DopPanel.Name = "DopPanel";
            this.DopPanel.Size = new System.Drawing.Size(143, 332);
            this.DopPanel.TabIndex = 9;
            // 
            // CurtainButtonPanel
            // 
            this.CurtainButtonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CurtainButtonPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurtainButtonPanel.Controls.Add(this.CurtainButtonText);
            this.CurtainButtonPanel.Cursor = System.Windows.Forms.Cursors.No;
            this.CurtainButtonPanel.Location = new System.Drawing.Point(3, 280);
            this.CurtainButtonPanel.Name = "CurtainButtonPanel";
            this.CurtainButtonPanel.Size = new System.Drawing.Size(135, 47);
            this.CurtainButtonPanel.TabIndex = 4;
            // 
            // CurtainButtonText
            // 
            this.CurtainButtonText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CurtainButtonText.AutoSize = true;
            this.CurtainButtonText.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CurtainButtonText.ForeColor = System.Drawing.Color.LightGray;
            this.CurtainButtonText.Location = new System.Drawing.Point(5, 4);
            this.CurtainButtonText.Name = "CurtainButtonText";
            this.CurtainButtonText.Size = new System.Drawing.Size(125, 36);
            this.CurtainButtonText.TabIndex = 0;
            this.CurtainButtonText.Text = "Перед началом игры, \r\nпожалуйста, выберите\r\nуровень её сложности";
            this.CurtainButtonText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StartButton
            // 
            this.StartButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StartButton.Color = System.Drawing.Color.Transparent;
            this.StartButton.Colors = new Bloom[0];
            this.StartButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StartButton.Customization = "";
            this.StartButton.EnableGlass = true;
            this.StartButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StartButton.Image = null;
            this.StartButton.Location = new System.Drawing.Point(2, 279);
            this.StartButton.Name = "StartButton";
            this.StartButton.NoRounding = false;
            this.StartButton.Size = new System.Drawing.Size(137, 49);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Начать игру";
            this.StartButton.Transparent = false;
            this.StartButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StartButton_MouseDown);
            this.StartButton.MouseEnter += new System.EventHandler(this.StartButton_MouseEnter);
            this.StartButton.MouseLeave += new System.EventHandler(this.StartButton_MouseLeave);
            this.StartButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Restart);
            // 
            // RecordTable
            // 
            this.RecordTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RecordTable.BackColor = System.Drawing.Color.LightGray;
            this.RecordTable.Colors = new Bloom[0];
            this.RecordTable.Controls.Add(this.RecordTime);
            this.RecordTable.Controls.Add(this.RecordPoints);
            this.RecordTable.Customization = "";
            this.RecordTable.Font = new System.Drawing.Font("Verdana", 8F);
            this.RecordTable.Image = null;
            this.RecordTable.Location = new System.Drawing.Point(2, 196);
            this.RecordTable.Name = "RecordTable";
            this.RecordTable.NoRounding = false;
            this.RecordTable.Size = new System.Drawing.Size(137, 82);
            this.RecordTable.TabIndex = 3;
            this.RecordTable.Text = "Рекорды:";
            this.RecordTable.Transparent = false;
            // 
            // RecordTime
            // 
            this.RecordTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RecordTime.AutoSize = true;
            this.RecordTime.BackColor = System.Drawing.Color.Transparent;
            this.RecordTime.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RecordTime.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RecordTime.ForeColor = System.Drawing.Color.Gray;
            this.RecordTime.Location = new System.Drawing.Point(14, 51);
            this.RecordTime.Name = "RecordTime";
            this.RecordTime.Size = new System.Drawing.Size(110, 25);
            this.RecordTime.TabIndex = 2;
            this.RecordTime.Text = "00:00:00";
            // 
            // RecordPoints
            // 
            this.RecordPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RecordPoints.BackColor = System.Drawing.Color.Transparent;
            this.RecordPoints.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.RecordPoints.FlatAppearance.BorderSize = 0;
            this.RecordPoints.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.RecordPoints.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.RecordPoints.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RecordPoints.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RecordPoints.ForeColor = System.Drawing.Color.Gray;
            this.RecordPoints.Location = new System.Drawing.Point(3, 23);
            this.RecordPoints.Margin = new System.Windows.Forms.Padding(1);
            this.RecordPoints.Name = "RecordPoints";
            this.RecordPoints.Size = new System.Drawing.Size(133, 32);
            this.RecordPoints.TabIndex = 2;
            this.RecordPoints.Text = "000";
            this.RecordPoints.UseVisualStyleBackColor = false;
            // 
            // TimeTable
            // 
            this.TimeTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeTable.BackColor = System.Drawing.Color.LightGray;
            this.TimeTable.Colors = new Bloom[0];
            this.TimeTable.Controls.Add(this.TimeLable);
            this.TimeTable.Customization = "";
            this.TimeTable.Font = new System.Drawing.Font("Verdana", 8F);
            this.TimeTable.Image = null;
            this.TimeTable.Location = new System.Drawing.Point(2, 143);
            this.TimeTable.Name = "TimeTable";
            this.TimeTable.NoRounding = false;
            this.TimeTable.Size = new System.Drawing.Size(137, 51);
            this.TimeTable.TabIndex = 2;
            this.TimeTable.Text = "Осталось времени:";
            this.TimeTable.Transparent = false;
            // 
            // TimeLable
            // 
            this.TimeLable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeLable.AutoSize = true;
            this.TimeLable.BackColor = System.Drawing.Color.Transparent;
            this.TimeLable.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TimeLable.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TimeLable.ForeColor = System.Drawing.Color.Gray;
            this.TimeLable.Location = new System.Drawing.Point(14, 23);
            this.TimeLable.Name = "TimeLable";
            this.TimeLable.Size = new System.Drawing.Size(110, 25);
            this.TimeLable.TabIndex = 2;
            this.TimeLable.Text = "00:00:00";
            // 
            // BalanceTable
            // 
            this.BalanceTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.BalanceTable.BackColor = System.Drawing.Color.LightGray;
            this.BalanceTable.Colors = new Bloom[0];
            this.BalanceTable.Controls.Add(this.Balance);
            this.BalanceTable.Customization = "";
            this.BalanceTable.Font = new System.Drawing.Font("Verdana", 8F);
            this.BalanceTable.Image = null;
            this.BalanceTable.Location = new System.Drawing.Point(2, 82);
            this.BalanceTable.Name = "BalanceTable";
            this.BalanceTable.NoRounding = false;
            this.BalanceTable.Size = new System.Drawing.Size(137, 59);
            this.BalanceTable.TabIndex = 1;
            this.BalanceTable.Text = "Ваш счёт составляет:";
            this.BalanceTable.Transparent = false;
            // 
            // Balance
            // 
            this.Balance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Balance.BackColor = System.Drawing.Color.Transparent;
            this.Balance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Balance.FlatAppearance.BorderSize = 0;
            this.Balance.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Balance.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Balance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Balance.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Balance.ForeColor = System.Drawing.Color.Gray;
            this.Balance.Location = new System.Drawing.Point(1, 22);
            this.Balance.Margin = new System.Windows.Forms.Padding(1);
            this.Balance.Name = "Balance";
            this.Balance.Size = new System.Drawing.Size(135, 36);
            this.Balance.TabIndex = 2;
            this.Balance.Text = "0";
            this.Balance.UseVisualStyleBackColor = false;
            // 
            // NumberTable
            // 
            this.NumberTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NumberTable.BackColor = System.Drawing.Color.LightGray;
            this.NumberTable.Colors = new Bloom[0];
            this.NumberTable.Controls.Add(this.SearchGenSum);
            this.NumberTable.Customization = "";
            this.NumberTable.Font = new System.Drawing.Font("Verdana", 8F);
            this.NumberTable.Image = null;
            this.NumberTable.Location = new System.Drawing.Point(2, 3);
            this.NumberTable.Name = "NumberTable";
            this.NumberTable.NoRounding = false;
            this.NumberTable.Size = new System.Drawing.Size(137, 77);
            this.NumberTable.TabIndex = 0;
            this.NumberTable.Text = "Необходимое число:";
            this.NumberTable.Transparent = false;
            // 
            // SearchGenSum
            // 
            this.SearchGenSum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchGenSum.BackColor = System.Drawing.Color.Transparent;
            this.SearchGenSum.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.SearchGenSum.FlatAppearance.BorderSize = 0;
            this.SearchGenSum.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.SearchGenSum.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.SearchGenSum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchGenSum.Font = new System.Drawing.Font("Verdana", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SearchGenSum.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.SearchGenSum.Location = new System.Drawing.Point(2, 23);
            this.SearchGenSum.Margin = new System.Windows.Forms.Padding(1);
            this.SearchGenSum.Name = "SearchGenSum";
            this.SearchGenSum.Size = new System.Drawing.Size(133, 52);
            this.SearchGenSum.TabIndex = 1;
            this.SearchGenSum.Text = "?";
            this.SearchGenSum.UseVisualStyleBackColor = false;
            // 
            // turn
            // 
            this.turn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.turn.Color = System.Drawing.Color.Empty;
            this.turn.Colors = new Bloom[0];
            this.turn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.turn.Customization = "";
            this.turn.EnableGlass = true;
            this.turn.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.turn.Image = null;
            this.turn.Location = new System.Drawing.Point(444, 2);
            this.turn.Name = "turn";
            this.turn.NoRounding = false;
            this.turn.Size = new System.Drawing.Size(21, 21);
            this.turn.TabIndex = 7;
            this.turn.Text = "_";
            this.turn.Transparent = false;
            this.turn.Click += new System.EventHandler(this.TurnProgram);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(497, 370);
            this.Controls.Add(this.Theme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MathBox";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Theme.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            this.DrapesPanel.ResumeLayout(false);
            this.ButtonTren.ResumeLayout(false);
            this.ButtonTren.PerformLayout();
            this.ButtonInfo.ResumeLayout(false);
            this.ButtonInfo.PerformLayout();
            this.DopPanel.ResumeLayout(false);
            this.CurtainButtonPanel.ResumeLayout(false);
            this.CurtainButtonPanel.PerformLayout();
            this.RecordTable.ResumeLayout(false);
            this.RecordTable.PerformLayout();
            this.TimeTable.ResumeLayout(false);
            this.TimeTable.PerformLayout();
            this.BalanceTable.ResumeLayout(false);
            this.NumberTable.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GhostButton turn;
        private GhostTheme Theme;
        private System.Windows.Forms.Panel DopPanel;
        private GhostGroupBox RecordTable;
        private GhostGroupBox TimeTable;
        private System.Windows.Forms.Label TimeLable;
        private GhostGroupBox BalanceTable;
        private GhostGroupBox NumberTable;
        private System.Windows.Forms.Timer timer;
        private GhostButton close;
        private System.Windows.Forms.Timer stream;
        private GhostButton StartButton;
        public System.Windows.Forms.Panel MainPanel;
        public System.Windows.Forms.Panel CurtainButtonPanel;
        private System.Windows.Forms.Label CurtainButtonText;
        public System.Windows.Forms.Panel ButtonTren;
        private System.Windows.Forms.Label LabelTren;
        public System.Windows.Forms.Panel ButtonInfo;
        private System.Windows.Forms.Label LabelInfo;
        internal GhostGroupBox x10;
        internal GhostGroupBox x8;
        internal GhostGroupBox x6;
        internal GhostGroupBox x5;
        internal GhostGroupBox expert;
        private System.Windows.Forms.Button SearchGenSum;
        internal System.Windows.Forms.Button Balance;
        private GhostButton ghostButton2;
        private GhostButton ghostButton1;
        internal System.Windows.Forms.Panel DrapesPanel;
        internal System.Windows.Forms.Panel GamePanel;
        internal System.Windows.Forms.Label RecordTime;
        internal System.Windows.Forms.Button RecordPoints;

    }
}

