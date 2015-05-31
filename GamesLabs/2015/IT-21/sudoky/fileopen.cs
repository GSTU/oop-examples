using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class fileopen
    {
        
        string[] mas = new string[81];
        string str = "TextFile.txt";//"File1.txt";
        public void readpazl(int i)
        {
            try
            {
                using (StreamReader sr = new StreamReader(str))
                {

                    //int i;
                    //int k = 0;
                    int j = 0;
                    //Random r1 = new Random();                  
                    //i = r1.Next(5);
                    //k = i;
                    string[] line = File.ReadAllLines(str);
                    string[] s = line[i].Split(' ');
                    foreach (string Mas in s)
                    {
                        mas[j] = Mas;
                        j++;
                    }
                    sr.Close();
                }
            }
            catch (Exception)
            { }
        }
        public string[] Mass
        {
            get { return mas; }
        }
    }
}
