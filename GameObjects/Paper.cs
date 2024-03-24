using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    public class Paper : Piece
    {
        public Paper(int x, int y, bool color) : base(x, y, color) { }
        public Paper(Point point, bool color) : base(point, color) { }
        protected override void InitPoints()
        {
            _height = 0.01f;
            Points =
          [
           //bottom
           -_size, _size,0,GetColor().X/2,GetColor().Y/2,GetColor().Z/2,1,
            _size,_size,0,GetColor().X/2,GetColor().Y/2,GetColor().Z/2,1,
            _size,-_size,0,GetColor().X/2,GetColor().Y/2,GetColor().Z/2,1,
            -_size,-_size,0,GetColor().X/2,GetColor().Y/2,GetColor().Z/2,1,
            //top
            -_size, _size,_height,GetColor().X,GetColor().Y,GetColor().Z,1,
            _size,_size,_height,GetColor().X,GetColor().Y,GetColor().Z,1,
            _size,-_size,_height,GetColor().X,GetColor().Y,GetColor().Z,1,
            -_size,-_size,_height,GetColor().X,GetColor().Y,GetColor().Z,1];
            Indexes = [2,5,6,2,1,5,
                        0,4,5,0,5,1,
                        2,6,7,3,2,7,
                        0,3,7,0,7,4,
                        7,5,4,7,6,5];
        }
        public override Point[] GetAvailableMoves()
        {
            List<Point> resultList = new List<Point>();
            int i = 0;
            do
            {
                i++;
                if (IsNotAllyAndInside(i, 0))
                    resultList.Add(new Point(CellPosition.X + i, CellPosition.Y));
            } while (IsNotAllyAndInside(i, 0) && Board.State[CellPosition.X + i, CellPosition.Y] == ' ');
            i = 0;
            do
            {
                i--;
                if (IsNotAllyAndInside(i, 0))
                    resultList.Add(new Point(CellPosition.X + i, CellPosition.Y));
            } while (IsNotAllyAndInside(i, 0) && Board.State[CellPosition.X + i, CellPosition.Y] == ' ');
            i = 0;
            do
            {
                i++;
                if (IsNotAllyAndInside(0, i))
                    resultList.Add(new Point(CellPosition.X, CellPosition.Y + i));
            } while (IsNotAllyAndInside(0, i) && Board.State[CellPosition.X, CellPosition.Y + i] == ' ');
            i = 0;
            do
            {
                i--;
                if (IsNotAllyAndInside(0, i))
                    resultList.Add(new Point(CellPosition.X, CellPosition.Y + i));
            } while (IsNotAllyAndInside(0, i) && Board.State[CellPosition.X, CellPosition.Y + i] == ' ');
            return resultList.ToArray();
        }
        public override int IsHigher(Piece piece)
        {
            if (piece is Scissor)
                return -1;
            if (piece is Paper)
                return 0;
            return 1;
        }
    }
}
