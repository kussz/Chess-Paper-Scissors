
namespace GameObjects.Factory;

internal class ScissorFactory : PieceFactory
{
    public override Piece Create(int x, int y, bool color)
    {
        return new Scissor(x, y, color);
    }
}
