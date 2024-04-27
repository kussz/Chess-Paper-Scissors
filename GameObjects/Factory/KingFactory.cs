
namespace GameObjects.Factory;

internal class KingFactory : PieceFactory
{
    public override Piece Create(int x, int y, bool color)
    {
        return new King(x, y, color);
    }
}
