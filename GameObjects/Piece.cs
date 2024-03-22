using System.Drawing;
namespace GameObjects
{
    public abstract class Piece
    {
        public Piece() { }
        public Piece(int x, int y, bool color)
        {
            Position = new Point(x, y);
            Color = color;
        }
        public Piece(Point point, bool color) : this(point.X,point.Y,color)
        { }
        public bool Color { get; private set; }
        public Point Position { get; set; }
        public Point CellPosition { get; set; }
        public float[] Points { get; private set; }
        public uint[] Indexes { get; private set; }
    protected bool CheckCellAvailability(int i, int j)
    {
        if (CellPosition.X + i >= 0 && CellPosition.X + i < 8 && CellPosition.Y + j >= 0 && CellPosition.Y + j < 8)
            return true;
        return false;
    }
    public abstract Point[] GetAvailableMoves();
    }
}
