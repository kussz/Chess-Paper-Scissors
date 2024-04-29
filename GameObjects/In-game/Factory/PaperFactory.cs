namespace GameObjects.Factory;

internal class PaperFactory : PieceFactory
{
    public override Piece Create(int x, int y, bool color)
    {
        return new Paper(x, y, color);
    }
}
