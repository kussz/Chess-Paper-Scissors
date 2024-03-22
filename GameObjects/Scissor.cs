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
        public override Point[] GetAvailableMoves()
        {
            List<Point> resultList = new List<Point>();
            for (int j = -1; j <= 1; j += 2)
            {
                int i = 0;
                do
                {
                    i+=j;
                    if (CheckCellAvailability(i, i))
                        resultList.Add(new Point(CellPosition.X + i, CellPosition.Y + i));
                } while (CheckCellAvailability(i, i) && Board.State[CellPosition.X + i, CellPosition.Y + i] == ' ');
            }
            for (int j = -1; j <= 1; j += 2)
            {
                int i = 0;
                do
                {
                    i+=j;
                    if (CheckCellAvailability(i, -i))
                        resultList.Add(new Point(CellPosition.X + i, CellPosition.Y - i));
                } while (CheckCellAvailability(i, -i) && Board.State[CellPosition.X + i, CellPosition.Y - i] == ' ');
            }

            return resultList.ToArray();
        }
    }
}
