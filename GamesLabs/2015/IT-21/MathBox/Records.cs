using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace game
{
    public class Records
    {
        
        private int[] RPOINTS { get; set; }
        private string[] RTIME { get; set; }

        public void ReadRecord()
        {
            /*string NF = (Environment.GetFolderPath(Environment.SpecialFolder.Resources) + @"\r.txt");
            DirectoryInfo dir = new DirectoryInfo("C:\\Game");
            if (!dir.Exists)
                dir.Create();
            
        
            if (!File.Exists("C:\\Game\\records.ini")) File.Create("C:\\Game\\records.ini");
            FileStream file = File.OpenRead("C:\\Game\\records.ini");
           /* string name = @"C:\\Game\\records.ini";
            FileStream stream = new FileStream(name, FileMode.Create);*/
            
            //StreamReader file = new StreamReader("C:\\Game\\records.ini");
            /*string s = "";
            string[] param = new string[2];
            int i=0;
            while(s=file!=null)
            {
                param = s.Split(';');
                RPOINTS[i] = Convert.ToInt32(param[0]);
                RTIME[i] = param[1];
                i++;
            }
            file.Close();*/
        }
        
        public void WriteRecord()
        {
            StreamWriter file = new StreamWriter(@"C://Game//records.ini");
            int i = 0;
            while (i<3)
            {
                file.WriteLine(RPOINTS[i].ToString() + ";" + RTIME[i] + ";");
                i++;
            }
            file.Close();
        }

        public string GetRecPoints(int level)
        {
            string points = "";
            switch(level)
            {
                case 1: { /*points = RPOINTS[level - 1].ToString();*/ }
                    break;
                default:
                    break;
            }
            return points;
        }

        public string GetRecTime(int level)
        {
            string time = "";
            switch (level)
            {
                case 1: { /*time = RTIME[level - 1].ToString();*/ }
                    break;
                default:
                    break;
            }
            return time;
        }

    }
}
