using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Drawing;

namespace battle
{
    public class XMLWithDOM
    {
        private XmlDocument document;

        public XMLWithDOM(String pathToXMLFile)
        {
            try
            {
                if (!(System.IO.File.Exists(pathToXMLFile)))
                    throw new System.IO.FileNotFoundException();

                document = new XmlDocument();

                using (XmlReader reader = XmlReader.Create(pathToXMLFile))
                {
                    document.Load(reader);
                }
            }
            catch { throw; }
        }

        public Config GetConfig()
        {
            
            XmlElement root = document["config"];

            return new Config(new GameConfig(int.Parse(root["player"].FirstChild.InnerText), int.Parse(root["player"].LastChild.InnerText)),
                              new GameConfig(int.Parse(root["ai"].FirstChild.InnerText), int.Parse(root["ai"].LastChild.InnerText)), Color.FromName(root["bgcolor"].LastChild.InnerText));
        }
    }
}
