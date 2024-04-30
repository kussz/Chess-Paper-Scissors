using GameObjects.Functional;
using GameObjects.Graphics.Builders;
using GameObjects.Graphics.Drawing;
using GameObjects.Graphics.GraphicsObjects;
using OpenTK.Mathematics;
using System.Drawing;

namespace GameObjects;

public abstract class Piece : IDrawableDynamic
{
    protected Piece() : this(false) { }
    public virtual VAO VAO { get; set; }
    public virtual bool Color { get; private set; }
    public virtual Point CellPosition { get; set; }
    public virtual float[] InitialPoints { get; set; } = [];
    public virtual float[] Points { get; set; } = [];
    public Piece(bool color)
    {
        Color = color;
    }
    protected bool IsNotAllyAndInside(int i, int j)
    {
        if (CellPosition.X + i >= 0 && CellPosition.X + i < 8 && CellPosition.Y + j >= 0 && CellPosition.Y + j < 8)
        {
            Piece? piece = GameLogic.FindPiece(new Point(CellPosition.X + i, CellPosition.Y + j));
            if (piece == null || (piece.Color != this.Color && IsHigher(piece) != 0))
                return true;
        }
        return false;
    }
    public abstract Point[] GetAvailableMoves();
    public abstract int IsHigher(Piece piece);
    public virtual void UpdatePosition(Point cellPosition)
    {
        Points = this.GetVertexArray(cellPosition);
        CellPosition = cellPosition;
    }
}
