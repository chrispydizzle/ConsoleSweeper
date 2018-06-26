namespace ConsoleSweeper.Main
{
    using System;

    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Tuple<int, int> size = new Tuple<int, int>(5, 5);
            const int danger = 5;
            Grid grid = new Grid();
            grid.Build(size);
            grid.MakeDangerous(danger);
            string coordinate;
            do
            {
                grid.Show();
                Console.Write("Pick a square: ");
                coordinate = Console.ReadLine();
            } while (grid.Reveal(coordinate));

            Console.WriteLine(grid.GameOver());
        }
    }
}