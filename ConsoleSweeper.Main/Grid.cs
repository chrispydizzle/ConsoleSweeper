namespace ConsoleSweeper.Main
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Grid
    {
        private GridSquare[,] grid;

        public void Build(Tuple<int, int> size)
        {
            this.grid = new GridSquare[size.Item1, size.Item2];

            for (int i = 0; i < size.Item1; i++)
            {
                for (int j = 0; j < size.Item2; j++)
                {
                    GridSquare newSquare = this.grid[i, j] = new GridSquare();
                    newSquare.Neighbors = this.LinkToNeighbors(i, j);
                }
            }
        }

        private List<GridSquare> LinkToNeighbors(int y, int x)
        {
            List<GridSquare> neighbors = new List<GridSquare>();
            for (int yTrav = y - 1; yTrav <= y + 1 && y < this.grid.GetLength(0); yTrav++)
            {
                if (yTrav < 0) continue;

                for (int xTrav = x - 1; xTrav <= x + 1 && x < this.grid.GetLength(1); x++)
                {
                    if (xTrav < 0 || x == xTrav && y == yTrav) continue;

                    neighbors.Add(this.grid[yTrav, xTrav]);
                }
            }

            return neighbors;
        }

        public void MakeDangerous(int danger)
        {
            int counter = 0;
            Random r = new Random();
            while (counter < danger)
            {
                GridSquare dSquare;
                if (this.MakeDangerous(r, out dSquare)) continue;

                counter++;
            }
        }

        private bool MakeDangerous(Random r, out GridSquare dSquare)
        {
            int dangery = r.Next(this.grid.GetLength(0));
            int dangerx = r.Next(this.grid.GetLength(1));

            dSquare = this.grid[dangery, dangerx];
            if (dSquare.SquareType == SquareType.DangerousHidden) return true;

            dSquare.SquareType = SquareType.DangerousHidden;
            return false;
        }

        private void WarnSiblings(GridSquare[,] gridSquares, GridSquare dSquare)
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            this.WriteHeader();

            for (int i = 0; i < this.grid.GetLength(0); i++)
            {
                this.DrawRow(i);
            }
        }

        private void DrawRow(int i)
        {
            Console.Write($"|{i.ToString()}|".PadLeft(4));
            for (int j = 0; j < this.grid.GetLength(1); j++)
            {
                this.grid[i, j].Draw(a => Console.Write($"|{a}|"));
            }

            Console.WriteLine();
        }

        private void WriteHeader()
        {
            Console.Write(">".PadLeft(4));
            int[] headerRow = Enumerable.Range(0, this.grid.GetLength(0)).ToArray();
            foreach (int column in headerRow)
            {
                Console.Write($"|{column.ToString()}|");
            }

            Console.WriteLine();
        }

        public bool Reveal(string coordinate)
        {
            int[] coords = coordinate.Split(' ').Cast<int>().ToArray();
            GridSquare gridSquare = this.grid[coords[0], coords[1]];
            if (gridSquare.SquareType == SquareType.DangerousHidden)
            {
                gridSquare.SquareType = SquareType.DangerousShow;
                return false;
            }

            gridSquare.SquareType = SquareType.SafeShown;
            return true;
        }

        public bool GameOver()
        {
            this.Show();
            Console.WriteLine("Game OVer");
            Console.Write("Play Again?");

            return Console.ReadKey().Key == ConsoleKey.Y;
        }
    }
}