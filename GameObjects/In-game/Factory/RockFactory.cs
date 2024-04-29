using System;

namespace GameObjects.Factory
{
    internal class RockFactory : PieceFactory
    {
        public override Piece Create(int x, int y, bool color)
        {
            return new Rock(x, y, color);
        }
    }
}
