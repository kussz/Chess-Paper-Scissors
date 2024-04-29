namespace GameObjects.Factory;

internal abstract class PieceFactory
{
    public abstract Piece Create(int x, int y, bool color, bool generateGraphics);
}