using Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    public class King : Piece
    {
        public King(int x, int y, bool color) : base(x, y, color)
        { }
        public King(Point point, bool color) : base(point, color)
        { }
        public override Point[] GetAvailableMoves()
        {
            List<Point> resultList = new List<Point>();
            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                    if (IsNotAllyAndInside(i, j))
                    {
                        if (i != 0 || j != 0)
                            resultList.Add(new Point(CellPosition.X + i, CellPosition.Y + j));
                    }
            return resultList.ToArray();
        }
        protected override void InitPoints()
        {
            Model.KingModel.Init(this);
        }
        public override int IsHigher(Piece piece)
        {
            if (piece is King)
                return 0;
            return 1;
        }
    }
}
