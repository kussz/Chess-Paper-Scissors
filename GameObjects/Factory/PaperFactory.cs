using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects.Factory;

internal class PaperFactory : PieceFactory
{
    public override Piece Create(int x, int y, bool color)
    {
        return new Paper(x, y, color);
    }
}
