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
        public override Point[] GetAvailableMoves()
        {
            List<Point> resultList = new List<Point>();
            int i = 0;
            do
            {
                i++;
                if (CheckCellAvailability(i, 0))
                    resultList.Add(new Point(CellPosition.X + i, CellPosition.Y));
            } while (CheckCellAvailability(i, 0) && Board.State[CellPosition.X + i, CellPosition.Y] == ' ');
            i = 0;
            do
            {
                i--;
                if (CheckCellAvailability(i, 0))
                    resultList.Add(new Point(CellPosition.X + i, CellPosition.Y));
            } while (CheckCellAvailability(i, 0) && Board.State[CellPosition.X + i, CellPosition.Y] == ' ');
            i = 0;
            do
            {
                i++;
                if (CheckCellAvailability(0, i))
                    resultList.Add(new Point(CellPosition.X, CellPosition.Y + i));
            } while (CheckCellAvailability(0, i) && Board.State[CellPosition.X, CellPosition.Y + i] == ' ');
            i = 0;
            do
            {
                i--;
                if (CheckCellAvailability(0, i))
                    resultList.Add(new Point(CellPosition.X, CellPosition.Y + i));
            } while (CheckCellAvailability(0, i) && Board.State[CellPosition.X, CellPosition.Y + i] == ' ');
            return resultList.ToArray();
        }
    }
}
