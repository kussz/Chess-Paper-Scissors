using Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    public class Scissor : Piece
    {
        public Scissor(int x, int y, bool color) : base(x, y, color) 
        { 
            Type = PieceType.Scissor;
            Points = Model.Scissor.Points;
            Indexes = Model.Scissor.Indexes;
        }
        public Scissor(Point point, bool color) : this(point.X,point.Y, color) { }
        public override Point[] GetAvailableMoves()
        {
            List<Point> resultList = new List<Point>();
            for (int j = -1; j <= 1; j += 2)
            {
                int i = 0;
                do
                {
                    i+=j;
                    if (IsNotAllyAndInside(i, i))
                        resultList.Add(new Point(CellPosition.X + i, CellPosition.Y + i));
                } while (IsNotAllyAndInside(i, i) && Board.State[CellPosition.X + i, CellPosition.Y + i] == ' ');
            }
            for (int j = -1; j <= 1; j += 2)
            {
                int i = 0;
                do
                {
                    i+=j;
                    if (IsNotAllyAndInside(i, -i))
                        resultList.Add(new Point(CellPosition.X + i, CellPosition.Y - i));
                } while (IsNotAllyAndInside(i, -i) && Board.State[CellPosition.X + i, CellPosition.Y - i] == ' ');
            }

            return resultList.ToArray();
        }
        public override int IsHigher(Piece piece)
        {
            if (piece.Type == PieceType.Rock)
                return -1;
            if (piece.Type == PieceType.Scissor)
                return 0;
            return 1;
        }
    }
}
