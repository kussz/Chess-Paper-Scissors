﻿using System.Drawing;

namespace GameObjects.Decorators;

public abstract class StrongPieceDecorator : Piece
{
    protected Piece _piece;
    public StrongPieceDecorator(Piece piece)
    {
        _piece = piece;
        Crown = new Crown(_piece.CellPosition);
    }
    public Crown Crown { get; set; }
    public override float[] Points { get { return _piece.Points; } set { _piece.Points = value; } }
    public override float[] InitialPoints { get { return _piece.InitialPoints; } }
    public override bool Color { get { return _piece.Color; } }
    public override Point CellPosition { get { return _piece.CellPosition; } set { _piece.CellPosition = value; } }
    public override PieceType Type { get { return _piece.Type; } }
    public override int IsHigher(Piece piece)
    {
        return _piece.IsHigher(piece);
    }
    public override void UpdatePosition(Point cellPosition)
    {
        base.UpdatePosition(cellPosition);
        Crown.UpdatePosition(cellPosition);
    }
}
