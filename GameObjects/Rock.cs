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
        public override Point[] GetAvailableMoves()
        {
            List<Point> resultList = new List<Point>();
            for(int i = -1;i<=1;i++)
                for(int j = -1;j<=1;j++)
                    if (CheckCellAvailability(i,j))
                    {
                        resultList.Add(new Point(CellPosition.X + i, CellPosition.Y + j));
                    }
            return resultList.ToArray();
        }
    }
}
