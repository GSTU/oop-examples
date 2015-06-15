using System;
using System.Drawing;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
namespace Sudoku
{
    public class SudokuMainForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Button btnAnswer;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn6;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn7;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn8;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn9;
        private System.Windows.Forms.ComboBox cboGameLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.IContainer components;
        public static bool isValid;
        public static string font;
        public static int size;
        public static Color color;
        public SudokuMainForm()
        {
            InitializeComponent();
            cboGameLevel.SelectedIndex = 0;
            InitialiseDataGrid("numberset");
            NewGame(0);

        }

        private void CurrentSet_ColumnChanging(object sender, System.Data.DataColumnChangeEventArgs e)
        {

            try
            {

                int rowPos = dataGrid1.CurrentCell.RowNumber;
                string currentNumber = e.ProposedValue as string;

                int number = Int32.Parse(currentNumber);
                if ((number < 1) || (number > 9))
                {
                    string errorMessage = "Number should be between 1 and 9";
                    e.Row.SetColumnError(e.Column, errorMessage);
                }
                else
                {
                    int col = e.Column.Ordinal;
                    bool answerChanged = _newGame.CheckForAnswerChange(rowPos, col, number);

                    if (answerChanged)
                    {
                        e.ProposedValue = e.Row[e.Column];

                    }
                    else if (_newGame.CheckForDuplicate(rowPos, col, number))
                    {
                        e.Row.SetColumnError(e.Column, "Number is Duplicate");
                        e.Row.AcceptChanges();


                    }
                    else
                    {
                        
                        DataColumn[] columns = e.Row.GetColumnsInError();
                        if (columns.Length == 1)
                        {
                            if (e.Column.Ordinal == columns[0].Ordinal)
                            {
                                e.Row.ClearErrors();

                            }
                        }
                        else
                        {
                            foreach (DataColumn column in columns)
                            {
                                if (e.Column.Ordinal == column.Ordinal)
                                {
                                    e.Row.SetColumnError(e.Column, "");
                                }
                            }
                        }

                        bool answerComplete = IsSolutionComplete();
                    }

                }
            }
            catch (Exception ex)
            {

                e.Row.SetColumnError(e.Column, "Enter valid Number between 1 & 9");

            }
        }
        private bool IsSolutionComplete()
        {

            foreach (DataRow row in _currentSet.Tables["numberset"].Rows)
            {

                foreach (DataColumn currentColumn in _currentSet.Tables["numberset"].Columns)
                {

                    string currentValue = row[currentColumn] as string;
                    if (currentValue == "")
                        return false;
                    int currentNumber = Int32.Parse(currentValue);


                    if (currentNumber == 0)
                    {
                        return false;
                    }

                }
            }
            return true;
        }
        private void NewGame(int index)
        {
            GameLevel[] levels = { GameLevel.SIMPLE, GameLevel.MEDIUM, GameLevel.COMPLEX };


            if (index > -1)
            {
                _newGame.GenerateGame(levels[index]);
                dataGrid1.Visible = false;
                _currentSet = _newGame.GameSet;
                _currentSet.Tables["numberset"].DefaultView.AllowNew = false;
                if (_currentSet != null)
                {
                    _currentSet.Tables["numberset"].ColumnChanging += new DataColumnChangeEventHandler(this.CurrentSet_ColumnChanging);

                }
                dataGrid1.DataSource = _currentSet.Tables["numberset"];

                dataGrid1.Visible = true;

            }


        }
        static void Main()
        {
            Application.Run(new SudokuMainForm());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.cboGameLevel = new System.Windows.Forms.ComboBox();
            this.btnAnswer = new System.Windows.Forms.Button();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn7 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn8 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn9 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Level:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboGameLevel
            // 
            this.cboGameLevel.Items.AddRange(new object[] {
            "Simple",
            "Medium",
            "Complex"});
            this.cboGameLevel.Location = new System.Drawing.Point(72, 16);
            this.cboGameLevel.Name = "cboGameLevel";
            this.cboGameLevel.Size = new System.Drawing.Size(96, 21);
            this.cboGameLevel.TabIndex = 3;
            this.cboGameLevel.Text = "Simple";
            // 
            // btnAnswer
            // 
            this.btnAnswer.Location = new System.Drawing.Point(80, 80);
            this.btnAnswer.Name = "btnAnswer";
            this.btnAnswer.Size = new System.Drawing.Size(88, 24);
            this.btnAnswer.TabIndex = 2;
            this.btnAnswer.Text = "Show Answer";
            this.btnAnswer.Click += new System.EventHandler(this.btnAnswer_Click);
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(80, 48);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(88, 24);
            this.btnNewGame.TabIndex = 0;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGrid1.CaptionVisible = false;
            this.dataGrid1.ColumnHeadersVisible = false;
            this.dataGrid1.DataMember = "";
            this.dataGrid1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid1.GridLineColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(184, 16);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.PreferredColumnWidth = 50;
            this.dataGrid1.PreferredRowHeight = 50;
            this.dataGrid1.RowHeadersVisible = false;
            this.dataGrid1.RowHeaderWidth = 30;
            this.dataGrid1.Size = new System.Drawing.Size(270, 270);
            this.dataGrid1.TabIndex = 0;
            this.dataGrid1.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.dataGrid1.Paint += new System.Windows.Forms.PaintEventHandler(this.DataGrid1_Paint);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.AlternatingBackColor = System.Drawing.Color.LightGray;
            this.dataGridTableStyle1.ColumnHeadersVisible = false;
            this.dataGridTableStyle1.DataGrid = this.dataGrid1;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2,
            this.dataGridTextBoxColumn3,
            this.dataGridTextBoxColumn4,
            this.dataGridTextBoxColumn5,
            this.dataGridTextBoxColumn6,
            this.dataGridTextBoxColumn7,
            this.dataGridTextBoxColumn8,
            this.dataGridTextBoxColumn9});
            this.dataGridTableStyle1.GridLineColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.PreferredColumnWidth = 30;
            this.dataGridTableStyle1.PreferredRowHeight = 30;
            this.dataGridTableStyle1.RowHeadersVisible = false;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.MappingName = "col0";
            this.dataGridTextBoxColumn1.Width = 30;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.MappingName = "col1";
            this.dataGridTextBoxColumn2.Width = 30;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.MappingName = "col2";
            this.dataGridTextBoxColumn3.Width = 30;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.MappingName = "col3";
            this.dataGridTextBoxColumn4.Width = 30;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.MappingName = "col4";
            this.dataGridTextBoxColumn5.Width = 30;
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.MappingName = "col5";
            this.dataGridTextBoxColumn6.Width = 30;
            // 
            // dataGridTextBoxColumn7
            // 
            this.dataGridTextBoxColumn7.Format = "";
            this.dataGridTextBoxColumn7.FormatInfo = null;
            this.dataGridTextBoxColumn7.MappingName = "col6";
            this.dataGridTextBoxColumn7.Width = 30;
            // 
            // dataGridTextBoxColumn8
            // 
            this.dataGridTextBoxColumn8.Format = "";
            this.dataGridTextBoxColumn8.FormatInfo = null;
            this.dataGridTextBoxColumn8.MappingName = "col7";
            this.dataGridTextBoxColumn8.Width = 30;
            // 
            // dataGridTextBoxColumn9
            // 
            this.dataGridTextBoxColumn9.Format = "";
            this.dataGridTextBoxColumn9.FormatInfo = null;
            this.dataGridTextBoxColumn9.MappingName = "col8";
            this.dataGridTextBoxColumn9.Width = 30;
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // SudokuMainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(496, 334);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.btnAnswer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboGameLevel);
            this.KeyPreview = true;
            this.Name = "SudokuMainForm";
            this.Text = "Play Sudoku";
            this.Load += new System.EventHandler(this.SudokuMainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        private void btnNewGame_Click(object sender, System.EventArgs e)
        {
            int index = cboGameLevel.SelectedIndex;
            NewGame(index);


        }

        private void InitialiseDataGrid(string mappingTable)
        {

            DataGridTableStyle tableStyle = new DataGridTableStyle();
            tableStyle.PreferredColumnWidth = PREFERRED_COLUMN_WIDTH;

            tableStyle.PreferredRowHeight = PREFERRED_ROW_HEIGHT;
            tableStyle.BackColor = Color.Lavender;
            tableStyle.AlternatingBackColor = Color.Ivory;
            tableStyle.ColumnHeadersVisible = false;
            tableStyle.RowHeadersVisible = false;
            tableStyle.GridLineColor = Color.Blue;
            tableStyle.MappingName = mappingTable;

            tableStyle.ForeColor = Color.Black;
            for (int i = 0; i < 9; i++)
            {
                DataGridSpinnerColumn columnID = new DataGridSpinnerColumn();
                columnID.MappingName = "col" + i.ToString().TrimEnd();
                columnID.HeaderText = columnID.MappingName;
                columnID.SpinnerWidth = SPINNER_BUTTON_WIDTH;
                columnID.ScrollMaximum = MAX_ROWS;
                columnID.Game = _newGame;
                columnID.ScrollMinimum = 1;
                columnID.ScrollSmallChange = 1;
                columnID.TextBox.TextAlign = HorizontalAlignment.Right;

                tableStyle.GridColumnStyles.Add(columnID);
               			

            }
            dataGrid1.PreferredColumnWidth = PREFERRED_COLUMN_WIDTH;
            dataGrid1.PreferredRowHeight = PREFERRED_ROW_HEIGHT;
            dataGrid1.Size = new Size(PREFERRED_COLUMN_WIDTH * MAX_COLS, PREFERRED_ROW_HEIGHT * MAX_ROWS);
            dataGrid1.TableStyles.Add(tableStyle);



        }
        Sudoku _newGame = new Sudoku();
        private void btnAnswer_Click(object sender, System.EventArgs e)
        {
            _showAnswer = false;
            ShowAnswer();
            timer1.Enabled = true;


        }
        private void ShowAnswer()
        {

            DataSet tempSet = new DataSet();
            dataGrid1.Visible = false;
            _currentSet.Tables["answerset"].DefaultView.AllowNew = false;


            dataGrid1.DataSource = _currentSet.Tables["answerset"];
            dataGrid1.TableStyles.Clear();
            InitialiseDataGrid("answerset");

            dataGrid1.Visible = true;

        }
        private void ShowProblem()
        {
            dataGrid1.Visible = false;
            dataGrid1.DataSource = _currentSet.Tables["numberset"];
            dataGrid1.TableStyles.Clear();
            InitialiseDataGrid("numberset");

            dataGrid1.Visible = true;


        }

        DataSet _currentSet;
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            if (_showAnswer)
            {
                ShowAnswer();
                _showAnswer = false;

            }
            else
            {
                ShowProblem();
                _showAnswer = true;
                timer1.Enabled = false;

            }
        }

        private bool _showAnswer = true;


        private void DataGrid1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {



            Point currentPoint = new Point(0, 0);
            Size size = new Size(PREFERRED_COLUMN_WIDTH * 3, PREFERRED_ROW_HEIGHT * 3);
            Pen myPen = new Pen(Color.Red, 3);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    currentPoint.X = i * PREFERRED_ROW_HEIGHT * 3;
                    currentPoint.Y = j * PREFERRED_ROW_HEIGHT * 3;
                    Rectangle rect = new Rectangle(currentPoint, size);
                    e.Graphics.DrawRectangle(myPen, rect);

                }
            }



        }

        private const int PREFERRED_COLUMN_WIDTH = 30;
        private const int PREFERRED_ROW_HEIGHT = 30;
        private const int SPINNER_BUTTON_WIDTH = 10;

        private const int MAX_ROWS = 9;
        private const int MAX_COLS = 9;
        
        private bool InitialiseGame(DataSet ds)
        {
            bool initSuccess = _newGame.InitiliseExistingGame(ds);

            if (initSuccess)
            {
                dataGrid1.Visible = false;
                _currentSet = ds;
                _currentSet.Tables["numberset"].DefaultView.AllowNew = false;
                if (_currentSet != null)
                {
                    _currentSet.Tables["numberset"].ColumnChanging += new DataColumnChangeEventHandler(this.CurrentSet_ColumnChanging);

                }
                dataGrid1.DataSource = _currentSet.Tables["numberset"];
                dataGrid1.TableStyles.Clear();
                InitialiseDataGrid("numberset");

                dataGrid1.Visible = true;
            }
            return initSuccess;

        }

        private void SudokuMainForm_Load(object sender, EventArgs e)
        {
           try
            {
                XmlReaderSettings XMLSettings = new XmlReaderSettings();
                XMLSettings.Schemas.Add(null, "XML.xsd");
                XMLSettings.ValidationType = ValidationType.Schema;
                XmlReader XMLReader = XmlReader.Create("XML.xml", XMLSettings);
                isValid = true;
                while (XMLReader.Read()) { }
                if (isValid == true)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("XML.xml");
                    size = Convert.ToInt32(doc["settings"]["size"].InnerText);
                    font = doc["settings"]["font"].InnerText;
                    color = Color.FromName(doc["settings"]["color"].InnerText);
                    this.BackColor = color;
                    cboGameLevel.Font = new Font(font, size);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Настройки не были установлены, загружены стандартные параметры ", "Ошибка ", MessageBoxButtons.OK);
            }
            
        }
    }
}
