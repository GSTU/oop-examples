using System;
using System.Xml;

/**
 * Здесь показан пример работы с XML, в котором загружаются конфигурации к
 * игре. Обратите внимание, в .NET есть специальные средства для работы с
 * XML конфигурациями, поэтому не следует понимать показанный подход как 
 * совершенно верный, в данном случае это просто простоая демонстрация.
 * 
 * Внимание: 
 * Для примера использован XML без вложенных узлов, в лабораторной работе
 * у вас будет хотябы один вложенный узел
 */
namespace UsingXML
{
    class Program
    {
        static void Main(string[] args)
        {
            XMLWithDOM xmlWorker = new XMLWithDOM("Config.xml");
            Config config = xmlWorker.GetConfig();

            Console.WriteLine("Cofig from XML:");
            Console.WriteLine(config);
            Console.ReadKey();
        }
    }

    class XMLWithDOM
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

        private const String NODE_MESSAGE = "message";
        private const String NODE_VICTORY_CONDITION = "victoryCondition";
        private const String NODE_X = "x";
        private const String NODE_Y = "y";
        private const String ROOT = "config";

        public Config GetConfig()
        {
            XmlElement root = document[ROOT];
            String message = root[NODE_MESSAGE].InnerText;
            int x = int.Parse(root[NODE_X].InnerText);
            int y = int.Parse(root[NODE_Y].InnerText);
            int victoryCondition = int.Parse(root[NODE_VICTORY_CONDITION].InnerText);

            FieldSize fieldSize = new FieldSize(x, y);
            Config config = new Config(fieldSize, victoryCondition, message);

            return config;
        }
    }

    class FieldSize
    {
        public int X { get; set; }
        public int Y { get; set; }

        public FieldSize(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    class Config
    {
        public FieldSize FieldSize { get; set; }
        public int VictoryCondition { get; set; }
        public String Message { get; set; }

        public Config(FieldSize fieldSize, int victoryCondition, String message)
        {
            FieldSize = fieldSize;
            VictoryCondition = victoryCondition;
            Message = message;
        }

        public override string ToString()
        {
            String viewTemplate = "Field Size: \n\tx:{0}\n\ty:{1}\nMessage: {2}\nVictoryCondition: {3}\n";
            return String.Format(viewTemplate, FieldSize.X, FieldSize.Y, Message, VictoryCondition);
        }
    }
}
