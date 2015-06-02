using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laba13
{
    class SetShips
    {
        //Горизонтальные варианты
        static List<int>[] hList = new List<int>[2];
        //Вертикальные варианты
        static List<int>[] vList = new List<int>[2];

        public static void Set(Player player)
        {
            hList[0] = new List<int>();//x
            hList[1] = new List<int>();//y
            vList[0] = new List<int>();//x
            vList[1] = new List<int>();//y

            //поле 12х12
            int time = 10;

            SetRandom4Ship(player, 41);
            System.Threading.Thread.Sleep(time);
            SetRandom3Ship(player, 31);
            System.Threading.Thread.Sleep(time);
            SetRandom3Ship(player, 32);
            System.Threading.Thread.Sleep(time);
            SetRandom2Ship(player, 21);
            System.Threading.Thread.Sleep(time);
            SetRandom2Ship(player, 22);
            System.Threading.Thread.Sleep(time);
            SetRandom2Ship(player, 23);
            System.Threading.Thread.Sleep(time);
            SetRandom1Ship(player, 11);
            System.Threading.Thread.Sleep(time);
            SetRandom1Ship(player, 12);
            System.Threading.Thread.Sleep(time);
            SetRandom1Ship(player, 13);
            System.Threading.Thread.Sleep(time);
            SetRandom1Ship(player, 14);
            System.Threading.Thread.Sleep(time);
        }

        //Получение места размещения 4 корабля
        public static void SetRandom4Ship(Player player, int number)
        {
            Random random = new Random();

            //Получение списка возможных вариантов размещения
            for (int i = 1; i < 11; i++)
                for (int j = 1; j < 8; j++)
                    if(player[i, j].Number == 0 && player[i, j + 1].Number == 0 && player[i, j + 2].Number == 0 &&
                        player[i, j + 3].Number == 0)
                    {
                        hList[0].Add(i);
                        hList[1].Add(j);
                    }
            for (int j = 1; j < 11; j++)
                for (int i = 1; i < 8; i++)
                    if (player[i, j].Number == 0 && player[i + 1, j].Number == 0 && player[i + 2, j].Number == 0 &&
                        player[i + 3, j].Number == 0)
                    {
                        vList[0].Add(i);
                        vList[1].Add(j);
                    }

            //Выбор горизонтального или вертикального размещения
            switch (random.Next(0, 2))
            {
                case 0:
                    {
                        int num = random.Next(0, hList[0].Count);
                        int i = hList[0][num];
                        int j = hList[1][num];

                        for (int k = j; k < j + 4; k++)
                            player[i, k].Number = number;

                        SetMarker(i, j, 0, player, 5);

                        break;
                    }
                case 1:
                    {
                        int num = random.Next(0, vList[0].Count);
                        int i = vList[0][num];
                        int j = vList[1][num];

                        for (int k = i; k < i + 4; k++)
                            player[k, j].Number = number;

                        SetMarker(i, j, 1, player, 5);

                        break;
                    }
            }

            //Очистка вариантов
            hList[0].Clear();
            hList[1].Clear();
            vList[0].Clear();
            vList[1].Clear();
        }

        //Получения места размещения 3 корабля
        public static void SetRandom3Ship(Player player, int number)
        {
            Random random = new Random();

            //Получение списка возможных вариантов размещения
            for (int i = 1; i < 11; i++)
                for (int j = 1; j < 9; j++)
                    if (player[i, j].Number == 0 && player[i, j + 1].Number == 0 && player[i, j + 2].Number == 0)
                    {
                        hList[0].Add(i);
                        hList[1].Add(j);
                    }
            for (int j = 1; j < 11; j++)
                for (int i = 1; i < 9; i++)
                    if (player[i, j].Number == 0 && player[i + 1, j].Number == 0 && player[i + 2, j].Number == 0)
                    {
                        vList[0].Add(i);
                        vList[1].Add(j);
                    }

            //выбор горизонтального или вертикального размещения
            switch (random.Next(0, 2))
            {
                case 0:
                    {
                        int num = random.Next(0, hList[0].Count);
                        int i = hList[0][num];
                        int j = hList[1][num];

                        for (int k = j; k < j + 3; k++)
                            player[i, k].Number = number;

                        SetMarker(i, j, 0, player, 4);

                        break;
                    }
                case 1:
                    {
                        int num = random.Next(0, vList[0].Count);
                        int i = vList[0][num];
                        int j = vList[1][num];

                        for (int k = i; k < i + 3; k++)
                            player[k, j].Number = number;

                        SetMarker(i, j, 1, player, 4);

                        break;
                    }
            }

            //Очистка списков вариантов
            hList[0].Clear();
            hList[1].Clear();
            vList[0].Clear();
            vList[1].Clear();
        }

        ////Получения места размещения 2 корабля
        public static void SetRandom2Ship(Player player, int number)
        {
            Random random = new Random();

            //Получение списка возможных вариантов размещения
            for (int i = 1; i < 11; i++)
                for (int j = 1; j < 10; j++)
                    if (player[i, j].Number == 0 && player[i, j + 1].Number == 0)
                    {
                        hList[0].Add(i);
                        hList[1].Add(j);
                    }
            for (int j = 1; j < 11; j++)
                for (int i = 1; i < 10; i++)
                    if (player[i, j].Number == 0 && player[i + 1, j].Number == 0)
                    {
                        vList[0].Add(i);
                        vList[1].Add(j);
                    }

            //Выбор горизонтального или вертикального размещения
            switch (random.Next(0, 2))
            {
                case 0:
                    {
                        int num = random.Next(0, hList[0].Count);
                        int i = hList[0][num];
                        int j = hList[1][num];

                        for (int k = j; k < j + 2; k++)
                            player[i, k].Number = number;

                        SetMarker(i, j, 0, player, 3);

                        break;
                    }
                case 1:
                    {
                        int num = random.Next(0, vList[0].Count);
                        int i = vList[0][num];
                        int j = vList[1][num];

                        for (int k = i; k < i + 2; k++)
                            player[k, j].Number = number;

                        SetMarker(i, j, 1, player, 3);

                        break;
                    }
            }

            //Очистка списков вариантов
            hList[0].Clear();
            hList[1].Clear();
            vList[0].Clear();
            vList[1].Clear();
        }

        //Получение места размещения 1 корабля
        public static void SetRandom1Ship(Player player, int number)
        {
            Random random = new Random();

            //Получение списка возможных вариантов размещения
            for (int i = 1; i < 11; i++)
                for (int j = 1; j < 11; j++)
                    if (player[i, j].Number == 0)
                    {
                        hList[0].Add(i);
                        hList[1].Add(j);
                    }
            for (int j = 1; j < 11; j++)
                for (int i = 1; i < 11; i++)
                    if (player[i, j].Number == 0)
                    {
                        vList[0].Add(i);
                        vList[1].Add(j);
                    }

            //Выбор горизонтального или вертикального размещения
            switch (random.Next(0, 2))
            {
                case 0:
                    {
                        int num = random.Next(0, hList[0].Count);
                        int i = hList[0][num];
                        int j = hList[1][num];

                        for (int k = j; k < j + 1; k++)
                            player[i, k].Number = number;

                        SetMarker(i, j, 0, player, 2);

                        break;
                    }
                case 1:
                    {
                        int num = random.Next(0, vList[0].Count);
                        int i = vList[0][num];
                        int j = vList[1][num];

                        for (int k = i; k < i + 1; k++)
                            player[k, j].Number = number;

                        SetMarker(i, j, 1, player, 2);

                        break;
                    }
            }

            //Очистка списков вариантов
            hList[0].Clear();
            hList[1].Clear();
            vList[0].Clear();
            vList[1].Clear();
        }

        //Метка ячеек вокруг корабля
        public static void SetMarker(int i, int j, int t, Player player, int paluba)
        {
            if (t == 0) //горизонтальная
            {
                for (int k = j - 1; k < j + paluba; k++)
                {
                    if (player[i - 1, k].Number != -1)
                        player[i - 1, k].Number = 2;
                    if (player[i + 1, k].Number != -1)
                        player[i + 1, k].Number = 2;
                }
                for (int k = j - 1; k < j + paluba; k += paluba)
                    if (player[i, k].Number != -1)
                        player[i, k].Number = 2;
            }
            else //вертикальная
            {
                for (int k = i - 1; k < i + paluba; k++)
                {
                    if (player[k, j - 1].Number != -1)
                        player[k, j - 1].Number = 2;
                    if (player[k, j + 1].Number != -1)
                        player[k, j + 1].Number = 2;
                }
                for (int k = i - 1; k < i + paluba; k += paluba)
                    if (player[k, j].Number != -1)
                        player[k, j].Number = 2;
            }
        }
    }
}
