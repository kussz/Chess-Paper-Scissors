﻿using GameObjects.Functional;
using GameObjects.Graphics.Models;
using System.Drawing;

namespace GameObjects;

public class Paper : Piece
{
    public Paper(int x, int y, bool color, bool generateGraphics) : base(color)
    {
        if(generateGraphics)
        {
            InitialPoints = Model.Paper.Points;
            VAO = Model.Paper.VAO;
            UpdatePosition(new Point(x, y));
        }
        else
            CellPosition = new Point(x, y);
    }
    public Paper(Point point, bool color, bool generateGraphics) : this(point.X, point.Y, color, generateGraphics) { }
    public override Point[] GetAvailableMoves()
    {
        List<Point> resultList = new List<Point>();
        int i = 0;
        do
        {
            i++;
            if (IsNotAllyAndInside(i, 0))
                resultList.Add(new Point(CellPosition.X + i, CellPosition.Y));
        } while (IsNotAllyAndInside(i, 0) && GameLogic.FindPiece(new Point(CellPosition.X + i, CellPosition.Y)) == null);
        i = 0;
        do
        {
            i--;
            if (IsNotAllyAndInside(i, 0))
                resultList.Add(new Point(CellPosition.X + i, CellPosition.Y));
        } while (IsNotAllyAndInside(i, 0) && GameLogic.FindPiece(new Point(CellPosition.X + i, CellPosition.Y)) == null);
        i = 0;
        do
        {
            i++;
            if (IsNotAllyAndInside(0, i))
                resultList.Add(new Point(CellPosition.X, CellPosition.Y + i));
        } while (IsNotAllyAndInside(0, i) && GameLogic.FindPiece(new Point(CellPosition.X, CellPosition.Y + i)) == null);
        i = 0;
        do
        {
            i--;
            if (IsNotAllyAndInside(0, i))
                resultList.Add(new Point(CellPosition.X, CellPosition.Y + i));
        } while (IsNotAllyAndInside(0, i) && GameLogic.FindPiece(new Point(CellPosition.X, CellPosition.Y + i)) == null);
        return resultList.ToArray();
    }
    public override int IsHigher(Piece piece)
    {
        if (piece is Scissor)
            return -1;
        if (piece is Paper)
            return 0;
        return 1;
    }
}
