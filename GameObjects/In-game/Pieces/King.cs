﻿using GameObjects.Graphics.Models;
using System.Drawing;

namespace GameObjects;

public class King : Piece
{
    public King(int x, int y, bool color, bool generateGraphics) : base(color)
    {
        if(generateGraphics)
        {
            InitialPoints = Model.King.Points;
            VAO = Model.King.VAO;
            UpdatePosition(new Point(x, y));
        }
        else
            CellPosition = new Point(x, y);
    }
    public King(Point point, bool color, bool generateGraphics) : this(point.X, point.Y, color, generateGraphics)
    { }
    public override Point[] GetAvailableMoves()
    {
        List<Point> resultList = new List<Point>();
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (IsNotAllyAndInside(i, j))
                {
                    if (i != 0 || j != 0)
                        resultList.Add(new Point(CellPosition.X + i, CellPosition.Y + j));
                }
        return resultList.ToArray();
    }
    public override int IsHigher(Piece piece)
    {
        if (piece is King)
            return 0;
        return 1;
    }
}
