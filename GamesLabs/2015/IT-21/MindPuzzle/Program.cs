using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MindPuzzle
{
    static class Program
    {
        /// <summary>
        /// Точка входа в программу
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MindPuzzle());
        }
    }
}
