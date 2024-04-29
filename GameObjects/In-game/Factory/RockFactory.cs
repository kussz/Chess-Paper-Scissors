using System;

namespace GameObjects.Factory
{
    internal class RockFactory : PieceFactory
    {
        public override Piece Create(int x, int y, bool color, bool generateGraphics)
        {
            return new Rock(x, y, color, generateGraphics);
        }
    }
}
