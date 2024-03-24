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
        public Scissor(int x, int y, bool color) : base(x, y, color) { }
        public Scissor(Point point, bool color) : base(point, color) { }
        protected override void InitPoints()
        {
            Points =
          [
           //bottom
           -0.03f, 0.03f,0.01f,GetColor().X/2,GetColor().Y/2,GetColor().Z/2,1,
            0.03f,0.03f,0.01f,GetColor().X/2,GetColor().Y/2,GetColor().Z/2,1,
            0.03f,-0.03f,0.01f,GetColor().X/2,GetColor().Y/2,GetColor().Z/2,1,
            -0.03f,-0.03f,0.01f,GetColor().X/2,GetColor().Y/2,GetColor().Z/2,1,
            //top
            -0.03f, 0.03f,0.06f,GetColor().X,GetColor().Y,GetColor().Z,1,
            0.03f,0.03f,0.06f,GetColor().X,GetColor().Y,GetColor().Z,1,
            0.03f,-0.03f,0.06f,GetColor().X,GetColor().Y,GetColor().Z,1,
            -0.03f,-0.03f,0.06f,GetColor().X,GetColor().Y,GetColor().Z,1];
            Indexes = [2,5,6,2,1,5,
                        0,4,5,0,5,1,
                        2,6,7,3,2,7,
                        0,3,7,0,7,4,
                        7,5,4,7,6,5];
        }
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
            if (piece is Rock)
                return -1;
            if (piece is Scissor)
                return 0;
            return 1;
        }
    }
}
