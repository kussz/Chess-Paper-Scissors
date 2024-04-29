
namespace GameObjects.Factory;

internal class KingFactory : PieceFactory
{
    public override Piece Create(int x, int y, bool color, bool generateGraphics)
    {
        return new King(x, y, color, generateGraphics);
    }
}
