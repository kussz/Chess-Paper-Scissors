using System.Drawing;
using GameObjects.Graphics.GraphicsObjects;

namespace GameObjects.Decorators;

public abstract class StrongPieceDecorator : Piece
{
    protected Piece _piece;
    public StrongPieceDecorator(Piece piece, bool generateGraphics)
    {
        _piece = piece;
        if (generateGraphics)
            Crown = new Crown(_piece.CellPosition);
    }
    public Crown? Crown { get; set; }
    public override VAO VAO { get { return _piece.VAO; } set { _piece.VAO = value; } }
    public override float[] Points { get { return _piece.Points; } set { _piece.Points = value; } }
    public override float[] InitialPoints { get { return _piece.InitialPoints; } }
    public override bool Color { get { return _piece.Color; } }
    public override Point CellPosition { get { return _piece.CellPosition; } set { _piece.CellPosition = value; } }
    public override int IsHigher(Piece piece)
    {
        return _piece.IsHigher(piece);
    }
    public override void UpdatePosition(Point cellPosition)
    {
        base.UpdatePosition(cellPosition);
        Crown?.UpdatePosition(cellPosition);
    }
}
