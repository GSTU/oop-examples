using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1 {
    static class Program {
        public static Form1 GlobalForm;
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GlobalForm=new Form1();
            Application.Run(GlobalForm);
        }
    }
}
