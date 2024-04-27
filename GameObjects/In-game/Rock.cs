using GameObjects.Decorators;
using Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    public class Rock : Piece
    {
        public Rock(int x, int y, bool color) : base(x, y, color)
        { 
            Type = PieceType.Rock;
            Points = Model.Rock.Points;
            Indexes = Model.Rock.Indexes;
        }
        public Rock(Point point, bool color) : this(point.X,point.Y,color)
        { }
        public override int IsHigher(Piece piece)
        {
            if (piece.Type == PieceType.Paper)
                return -1;
            if (piece.Type == PieceType.Rock)
                return 0;
            return 1;
        }
        public override Point[] GetAvailableMoves()
        {
            List<Point> resultList = new List<Point>();
            int dir = Color ? -1 : 1;
            for(int i = -1;i<=1;i++)
                if (IsNotAllyAndInside(i,dir))
                {
                    resultList.Add(new Point(CellPosition.X + i, CellPosition.Y + dir));
                }
            return resultList.ToArray();
        }
        public bool CheckAscension()
        {
            if (Color && CellPosition.Y == 0)
            {
                return true;
            }
            else if (!Color && CellPosition.Y == 7)
            {
                return true;
            }
            else
                return false;
        }
    }
}
