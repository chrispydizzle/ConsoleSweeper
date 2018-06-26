namespace ConsoleSweeper.Main
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    internal class GridSquare
    {
        internal SquareType SquareType { get; set; }
        public List<GridSquare> Neighbors { get; set; }

        internal void Draw(Action<string> drawAction)
        {
            string output = "\"";

            switch (this.SquareType)
            {
                case SquareType.SafeShown:
                    output = $"{this.Neighbors.Count(n => (n.SquareType == SquareType.DangerousHidden || n.SquareType == SquareType.DangerousShow )).ToString()}";
                    break;
                case SquareType.DangerousShow:
                    output = string.Format(output, "*");
                    break;
                default:
                    break;
            }

            drawAction(output);
        }
    }
}