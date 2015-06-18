// Bring the needed packages into the global scope
using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
//using CheckersCtrl;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Schema;

/// <sumary>
///  The form that holds the Checkers control.
/// </sumary>
class Checkers : Form
{


    // Reference to the checkers control
    private BoardView m_view;

    // Menu options for setting the game level
    // Needed because we need to set/unset the checkmarks
    private MenuItem language;
    /// <sumary> 
    ///  Creates a from with a checkers game control
    /// </sumary>
    /// 


    public Checkers()
    {


        this.readXml();
        // Set the window title
        Text = "Шашки";
        //Names name_window = new Names();
        //name_window.ShowDialog();
        // Set the window size
        ClientSize = new Size(Checkers.BoardSize,Checkers.BoardSize);

        // Create the menu
        MainMenu menu = new MainMenu();
        MenuItem item = new MenuItem("&Файл");
        menu.MenuItems.Add(item);

        // Add the menu entries to the "File" menu    
        item.MenuItems.Add(new MenuItem("&Новая игра", new EventHandler(OnNewGame)));
        item.MenuItems.Add(new MenuItem("&Открыть...", new EventHandler(OnOpen)));
        item.MenuItems.Add(new MenuItem("&Сохранить...", new EventHandler(OnSave)));
        item.MenuItems.Add(new MenuItem("&Выход", new EventHandler(OnExit)));


        // Create a new Menu
        item = new MenuItem("&Помощь");
        menu.MenuItems.Add(item);

        // Add the menu entries to the "Help" menu
        item.MenuItems.Add(new MenuItem("&О проекте", new EventHandler(OnAbout)));

        // Attach the menu to the window
        Menu = menu;

        // Add the checkers control to the form
        m_view = new BoardView(this);
        m_view.Location = new Point(0, 0);
        m_view.Size = ClientSize;
        Controls.Add(m_view);
    }


    /// <sumary> 	
    // Handler for the "New Game" option
    /// </sumary>
    private void OnNewGame(object sender, EventArgs ev)
    {
        // Save the current dificulty level
        int level = m_view.depth;
        m_view.newGame();
        m_view.depth = level;
    }

    /// <sumary> 
    // Handler for the "Open" option
    /// </sumary>
    private void OnOpen(object sender, EventArgs ev)
    {

        OpenFileDialog openDlg = new OpenFileDialog();

        openDlg.InitialDirectory = Directory.GetCurrentDirectory();
        openDlg.Filter = "Checker files (*.sav)|*.sav|All files (*.*)|*.*";
        openDlg.FilterIndex = 1;
        openDlg.RestoreDirectory = true;

        if (openDlg.ShowDialog() == DialogResult.OK)
        {
            FileStream kl = new FileStream(openDlg.FileName, FileMode.Open);

            if ((kl != null))
            {
                m_view.loadBoard(kl);
                kl.Close();
            }
        }
    }

    /// <sumary> 
    /// Handler for the "Save" option
    /// </sumary>
    private void OnSave(object sender, EventArgs ev)
    {

        SaveFileDialog saveDlg = new SaveFileDialog();

        saveDlg.InitialDirectory = Directory.GetCurrentDirectory();
        saveDlg.Filter = "Checker files (*.sav)|*.sav|All files (*.*)|*.*";
        saveDlg.FilterIndex = 1;
        saveDlg.RestoreDirectory = true;

        if (saveDlg.ShowDialog() == DialogResult.OK)
        {
            FileStream fs1 = new FileStream(saveDlg.FileName, FileMode.Create);

            StreamWriter myStream = new StreamWriter(fs1);
            if ((myStream != null))
            {
                m_view.saveBoard(fs1);
                myStream.Close();
            }
        }
    }

    /// <sumary> 
    /// Handler for the "Exit" option
    /// </sumary>
    private void OnExit(object sender, EventArgs ev)
    {
        Close();
    }

    /// <sumary> 	
    // Handler for the "Easy" option
    /// </sumary>
    private void OnEasyOpt(object sender, EventArgs ev)
    {
        m_view.depth = 1;

    }

    /// <sumary> 	
    // Handler for the "Medium" option
    /// </sumary>
    private void OnMediumOpt(object sender, EventArgs ev)
    {
        m_view.depth = 3;

    }

    /// <sumary> 	
    // Handler for the "Hard" option
    /// </sumary>
    private void OnHardOpt(object sender, EventArgs ev)
    {
        m_view.depth = 6;

    }


    /// </sumary>
    private void OnAbout(object sender, EventArgs ev)
    {

        AboutBox about = new AboutBox();
        about.ShowDialog();
        about = null; // Help the GC
    }

    /// <sumary> 
    /// Processes the window resizing
    /// </sumary>
    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        if (m_view != null)
        {
            m_view.Size = ClientSize;
            m_view.Invalidate();
        }
    }

    /// <sumary> 
    /// Program entry point
    /// </sumary>
    public static int BoardSize = 300;
    public static int wR = 255;
    public static int wG = 255;
    public static int wB = 255;

    public static int bR = 32;
    public static int bG = 32;
    public static int bB = 32;


    [STAThread]
    public static void Main(String[] args)
    {
        Debug.Listeners.Add(new TextWriterTraceListener(System.Console.Out));
        Application.Run(new Checkers());
    }

    private void InitializeComponent()
    {
        this.SuspendLayout();
        // 
        // Checkers
        // 
        this.ClientSize = new System.Drawing.Size(284, 262);
        this.Name = "Checkers";
        this.Tag = "";
        this.ResumeLayout(false);

    }
    private static bool stop = false;
    private void readXml()
    {
        try
        {
            XmlReaderSettings xmls = new XmlReaderSettings();
            xmls.Schemas.Add(null, "Schema.xsd");
            xmls.ValidationType = ValidationType.Schema;
            xmls.ValidationEventHandler += new ValidationEventHandler(this.xmlHandler);
            xmls.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            xmls.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            xmls.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            XmlReader settings = XmlReader.Create("settings.xml", xmls);
            string currentNode = "";
            string currentElement = "";
            string currentValue = "";
            while (settings.Read())
            {
                if (settings.NodeType == XmlNodeType.Element)
                {
                    if (settings.Name != "settings")
                    {
                        if (currentNode == "")
                        {
                            currentNode = settings.Name;
                        }
                        else
                        {
                            currentElement = settings.Name;
                        }
                    }
                }
                if (settings.NodeType == XmlNodeType.Text)
                {
                    currentValue = settings.Value;
                }
                if (settings.NodeType == XmlNodeType.EndElement)
                {
                    if (!Checkers.stop)
                    {
                        if (currentNode == "White")
                        {
                            if (currentElement == "R")
                            {
                                Checkers.wR = Convert.ToInt32(currentValue);
                            }
                            else if (currentElement == "G")
                            {
                                Checkers.wG = Convert.ToInt32(currentValue);
                            }
                            else if (currentElement == "B")
                            {
                                Checkers.wB = Convert.ToInt32(currentValue);
                                currentNode = "";
                            }
                        }
                        else if (currentNode == "Black")
                        {
                            if (currentElement == "R")
                            {
                                Checkers.bR = Convert.ToInt32(currentValue);
                            }
                            else if (currentElement == "G")
                            {
                                Checkers.bG = Convert.ToInt32(currentValue);
                            }
                            else if (currentElement == "B")
                            {
                                Checkers.bB = Convert.ToInt32(currentValue);
                                currentNode = "";
                            }
                        }
                        else if (currentElement == "Size"||currentNode=="Size")
                        {
                            Checkers.BoardSize = Convert.ToInt32(currentValue);
                            currentElement = "";
                            currentNode = "";
                        }
                    }
                    else
                    {
                        throw new Exception("Ошибка валидации XSD");
                    }
                    currentElement = "";
                    currentValue = "";
                }
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.ToString());
        }
    }
    private void xmlHandler(object sender, ValidationEventArgs e)
    {
        MessageBox.Show(e.Message);
        Checkers.stop = true;
    }
}




