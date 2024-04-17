using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects.Factory;

internal class KingFactory : PieceFactory
{
    public override Piece Create(int x, int y, bool color)
    {
        return new King(x, y, color);
    }
}
