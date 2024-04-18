using System.ComponentModel.Design;
using System.Drawing;
using Graphics;
using OpenTK.Mathematics;

namespace GameObjects
{
    public abstract class Piece
    {
        public virtual VAO VAO {  get; set; }
        protected float _size = 0.06f;
        protected float _height = 0.09f;
        protected Piece() { }
        public Piece(int x, int y, bool color)
        {
            CellPosition = new Point(x, y);
            Color = color;
        }
        public Vector4 GetColor()
        {
            if (Color)
                return new Vector4(1, 0.5f, 0.5f, 1);
            return new Vector4(0.5f,0.5f, 1, 1);
        }
        public virtual PieceType Type { get; protected set; }
        public virtual bool Color { get; private set; }
        public virtual Point CellPosition { get; set; }
        public virtual float[] Points { get; set; }
        public virtual uint[] Indexes { get; set; }
        public virtual int TextureID {  get; set; }
        public virtual int TextureCoordsBufferID { get; set; }
        protected bool IsNotAllyAndInside(int i, int j)
        {
            Piece piece = Board.PieceList.Find(match => match.CellPosition.X == CellPosition.X + i && match.CellPosition.Y == CellPosition.Y + j);
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
