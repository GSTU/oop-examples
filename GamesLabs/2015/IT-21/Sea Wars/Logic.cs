using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab13
{
    class MyPoint
    {
        public int X, Y;
    }
    class Coordinates
    {
        public MyPoint Start;
        public MyPoint End;

        public Coordinates()
        {
            this.Start = new MyPoint();
            this.End = new MyPoint();
        }
    }
    class Border
    {
        public MyPoint Start;
        public MyPoint End;

        public Border()
        {
            this.Start = new MyPoint();
            this.End = new MyPoint();
        }
    }

    class Ship
    {
        public int Length;
        public int Orientation;
        public Border Border;
        public Coordinates Coordinates;

        public Ship()
        {
            this.Coordinates = new Coordinates();
            this.Border = new Border();
        }

        public void BorderArea()
        {
            this.Border.Start.X = this.Coordinates.Start.X - 1;
            this.Border.Start.Y = this.Coordinates.Start.Y - 1;

            this.Border.End.X = this.Coordinates.End.X + 1;
            this.Border.End.Y = this.Coordinates.End.Y + 1;

            if (this.Coordinates.Start.X == 0)
                this.Border.Start.X = 0;
            else if (this.Coordinates.End.X == 9)
                this.Border.End.X = 9;

            if (this.Coordinates.Start.Y == 0)
                this.Border.Start.Y = 0;
            else if (this.Coordinates.End.Y == 9)
                this.Border.End.Y = 9;
        }
        public void ResetPosition(int o, int x, int y)
        {
            this.Orientation = o;

            switch (this.Orientation)
            {
                case 0:
                    this.Coordinates.Start.X = x;
                    this.Coordinates.Start.Y = y;

                    this.Coordinates.End.X = this.Coordinates.Start.X + this.Length - 1;
                    this.Coordinates.End.Y = this.Coordinates.Start.Y;

                    break;
                case 1:
                    this.Coordinates.Start.X = y;
                    this.Coordinates.Start.Y = x;

                    this.Coordinates.End.X = this.Coordinates.Start.X;
                    this.Coordinates.End.Y = this.Coordinates.Start.Y + this.Length - 1;

                    break;
            }

            this.BorderArea();
        }
    }
    class Field
    {
        public int[,] Matrix;
        public List<Ship> Ships;
        private Random Random;
        private bool HasShip;
        private int Killed;
        private int Size;

        public Field(int randomSeed, int size)
        {
            this.Matrix = new int[size, size];
            this.Ships = new List<Ship>();
            this.Random = new Random(randomSeed);
            this.Size = size;

            this.Clear();
        }

        public void Clear()
        {
            for (int i = 0; i < Size; ++i)
                for (int j = 0; j < Size; ++j)
                    this.Matrix[i, j] = 0;

            this.Ships.Clear();
        }
        public void SetupShips()
        {
            for (int i = 0; i < Size; ++i)
                this.Ships.Add(new Ship());

            Ships[0].Length = 4;

            for (int i = 1; i < 3; ++i)
                Ships[i].Length = 3;

            for (int i = 3; i < 6; ++i)
                Ships[i].Length = 2;

            for (int i = 6; i < 10; ++i)
                Ships[i].Length = 1;

            for (int i = 0; i < 10; ++i)
                Ships[i].ResetPosition(Random.Next(0, 2), Random.Next(0, 10 - Ships[i].Length + 1), Random.Next(0, 10));

        }
        public void AddShips()
        {
            SetupShips();

            for (int n = 0; n < 10; ++n)
            {
                HasShip = false;

                for (int i = Ships[n].Border.Start.X; i <= Ships[n].Border.End.X; ++i)
                    for (int j = Ships[n].Border.Start.Y; j <= Ships[n].Border.End.Y; ++j)
                        if (Matrix[i, j] == 1)
                            HasShip = true;

                if (!HasShip)
                {
                    for (int i = Ships[n].Coordinates.Start.X; i <= Ships[n].Coordinates.End.X; ++i)
                        for (int j = Ships[n].Coordinates.Start.Y; j <= Ships[n].Coordinates.End.Y; ++j)
                            Matrix[i, j] = 1;
                }
                else
                {
                    Ships[n].ResetPosition(Random.Next(0, 2), Random.Next(0, 10 - Ships[n].Length + 1), Random.Next(0, 10));

                    --n;
                }
            }
        }
        public void Action(int x, int y)
        {
            if (this.Matrix[x, y] == 1)
                this.Matrix[x, y] = 2;

            foreach (Ship s in this.Ships)
            {
                this.Killed = 0;

                for (int i = s.Coordinates.Start.X; i <= s.Coordinates.End.X; ++i)
                    for (int j = s.Coordinates.Start.Y; j <= s.Coordinates.End.Y; ++j)
                        if (this.Matrix[i, j] == 2)
                            ++this.Killed;

                if (this.Killed == s.Length)
                {
                    for (int i = s.Border.Start.X; i <= s.Border.End.X; ++i)
                        for (int j = s.Border.Start.Y; j <= s.Border.End.Y; ++j)
                            this.Matrix[i, j] = 4;

                    for (int i = s.Coordinates.Start.X; i <= s.Coordinates.End.X; ++i)
                        for (int j = s.Coordinates.Start.Y; j <= s.Coordinates.End.Y; ++j)
                            this.Matrix[i, j] = 3;
                }
            }
        }
    }
}