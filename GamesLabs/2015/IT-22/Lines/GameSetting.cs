
using System;
using System.Drawing;

namespace linesk
{
    ///////////////////////////////////////////////////////////////////////////
    //    Модуль настроек приложения и глобальных переменных.
    ///////////////////////////////////////////////////////////////////////////


    // Глобальные данные приложения.
    public class Global
    {
        public const int AmountColorBalls = 3; // количество цветов графэлеменов
        public const int NumGraphItems = 100;  // количество графэлементов
        public const int NumPlayers = 6;       // количество запоминаемых рекордсменов
    }


    // Для хранения и записи настроек в двоичный файл.
    // Обязательно устанавливаем атрибут [Serializable]
    [Serializable]
    class GameSetting
    {
        public GameSetting()
        {
            // Инициализация игроков рекордсменов.
            for (int i = 0; i < DRH.Length; i++)
            {
                DRH[i] = new DataRecordsman();
            }

            // Цвета графэлементов по умолчанию.
            GraphItems = new Color[] { Color.DeepPink, Color.DeepSkyBlue, Color.Gold };
        }

        // Массив хранения данных рекордсменов.
        public DataRecordsman[] DRH = new DataRecordsman[Global.NumPlayers];
        
        // Массив хранения цвета графэлементов.
        public Color[] GraphItems = null;

        // Режим работы приложения.
        public bool FullScreen = false;

        // Размер и положение окна приложения.
        public Rectangle Bounds;

        // Хранения типа графэлемента(шар, прямоугольник, ромб).
        public GraphItem.TypeGraphItem CurrentGraphItem;
    }

    // Класс данных игроков-рекордсменов, подготовлен 
    // для сериализации (сохранения в файл).
    [Serializable]
    public class DataRecordsman
    {
        public int Score = 0; // очки
        public string Name = null; // имя игрока
        public bool CurrentPlayer = false; // действующий игрок
    }

}
