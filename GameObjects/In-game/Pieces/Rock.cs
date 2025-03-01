﻿using GameObjects.Graphics.Models;
using System.Drawing;

namespace GameObjects;

public class Rock : Piece
{
    public Rock(int x, int y, bool color, bool generateGraphics) : base(color)
    {
        if(generateGraphics)
        {
            InitialPoints = Model.Rock.Points;
            VAO = Model.Rock.VAO;
            UpdatePosition(new Point(x, y));
        }
        else
            CellPosition = new Point(x, y);
    }
    public Rock(Point point, bool color, bool generateGraphics) : this(point.X, point.Y, color, generateGraphics)
    { }
    public override int IsHigher(Piece piece)
    {
        if (piece is Paper)
            return -1;
        if (piece is Rock)
            return 0;
        return 1;
    }
    public override Point[] GetAvailableMoves()
    {
        List<Point> resultList = new List<Point>();
        int dir = Color ? -1 : 1;
        for (int i = -1; i <= 1; i++)
            if (IsNotAllyAndInside(i, dir))
            {
                resultList.Add(new Point(CellPosition.X + i, CellPosition.Y + dir));
            }
        return resultList.ToArray();
    }
    public bool CheckAscension()
    {
        if (Color && CellPosition.Y == 0)
        {
            return true;
        }
        else if (!Color && CellPosition.Y == 7)
        {
            return true;
        }
        else
            return false;
    }
}
