using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mine_Sweeper
{
    public enum CellType
    {
        Empty,
        Mine
    }
    public enum CellState
    {
        Closed,
        Opened,
        Marked
    }
    public enum MineState
    {
        Hidden,
        Sweeped
    }

    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Location(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
    public class Border
    {
        public Location Start { get; private set; }
        public Location End   { get; private set; }

        public Border(Cell cell)
        {
            this.Start = new Location(cell.Location.X - 1, cell.Location.Y - 1);
            this.End   = new Location(cell.Location.X + 1, cell.Location.Y + 1);
        }
    }

    public class Cell
    {
        private Field Parent;

        public Location  Location    { get; private set; }
        public Border    Border      { get; private set; }
        public CellState State       { get; set; }
        public CellType  Type        { get; set; }
        public int       MinesAround { get; set; }

        public Cell(Field parent, Location location, CellType type)
        {
            this.Parent      = parent;
            this.Location    = location;
            this.Border      = new Border(this);
            this.Type        = type;
            this.MinesAround = 0;
            this.State       = CellState.Closed;

            this.CutBorder();
        }

        public void CutBorder()
        {
            if (this.Location.X == 0)
                this.Border.Start.X = 0;
            else if (this.Location.X == Parent.Size - 1)
                this.Border.End.X = Parent.Size - 1;

            if (this.Location.Y == 0)
                this.Border.Start.Y = 0;
            else if (this.Location.Y == Parent.Size - 1)
                this.Border.End.Y = Parent.Size - 1;
        }
    }


    public class Field
    {
        public int Size       { get; private set; }
        public int MinesCount { get; private set; }

        public Cell[,] Cells  { get; private set; }

        public Field(int size, int minescount)
        {
            this.Size       = size;
            this.MinesCount = minescount;
        }

        public void Reset()
        {
            this.Cells = new Cell[this.Size, this.Size];

            for (int i = 0; i < this.Size; ++i)
                for (int j = 0; j < this.Size; ++j)
                    this.Cells[i, j] = new Cell(this, new Location(i, j), CellType.Empty);
        }
        public void PlantMines()
        {
            Reset();

            int    planted = 0, 
                   i, j;
            Random random  = new Random();

            while(planted < this.MinesCount)
            {
                i = random.Next(0, this.Size);
                j = random.Next(0, this.Size);

                if ((random.Next(0, 2) == 1) && (this.Cells[i, j].Type == CellType.Empty))
                {
                    this.Cells[i, j].Type = CellType.Mine;

                   ++planted;
                }
            }
        }
        public void CountMines()
        {
            foreach (Cell cell in this.Cells)
                for (int i = cell.Border.Start.X; i <= cell.Border.End.X; ++i)
                    for (int j = cell.Border.Start.Y; j <= cell.Border.End.Y; ++j)
                        if (((cell.Location.X != i) || (cell.Location.Y != j)) && (this.Cells[i, j].Type == CellType.Mine))
                            ++cell.MinesAround;
        }
    }
}
