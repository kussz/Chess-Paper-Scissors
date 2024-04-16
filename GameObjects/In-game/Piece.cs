using System.ComponentModel.Design;
using System.Drawing;
using OpenTK.Mathematics;

namespace GameObjects
{
    public abstract class Piece
    {
        protected float _size = 0.06f;
        protected float _height = 0.09f;
        protected abstract void InitPoints();
        public Piece(int x, int y, bool color)
        {
            CellPosition = new Point(x, y);
            Color = color;
            InitPoints();
        }
        public Vector4 GetColor()
        {
            if (Color)
                return new Vector4(1, 0.5f, 0.5f, 1);
            return new Vector4(0.5f,0.5f, 1, 1);
        }
        public Piece(Point point, bool color) : this(point.X,point.Y,color)
        { }
        public bool Color { get; private set; }
        public Point CellPosition { get; set; }
        public float[] Points { get; set; }
        public uint[] Indexes { get; set; }
        public int TextureID {  get; set; }
        public int TextureCoordsBufferID { get; set; }
        protected bool IsNotAllyAndInside(int i, int j)
        {
            Piece piece = Board.pieceList.Find(match => match.CellPosition.X == CellPosition.X + i && match.CellPosition.Y == CellPosition.Y + j);
            if (CellPosition.X + i >= 0 && CellPosition.X + i < 8 && CellPosition.Y + j >= 0 && CellPosition.Y + j < 8)
            {
                if (piece == null)
                    return true;
                else if (piece.Color != this.Color && IsHigher(piece) != 0)
                    return true;
            }
            return false;
        }
        public abstract Point[] GetAvailableMoves();
        public abstract int IsHigher(Piece piece);
        public void SetPosition(Point point)
        {
            CellPosition = point;
        }
    }
}
