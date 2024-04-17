namespace GameObjects.Factory
{
    abstract class PieceFactory
    {
        public abstract Piece Create(int x, int y, bool color);
    }
}