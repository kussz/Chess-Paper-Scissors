﻿using GameObjects.Functional;
using GameObjects.Graphics.Models;
using System.Drawing;

namespace GameObjects;

public class Scissor : Piece
{
    public Scissor(int x, int y, bool color, bool generateGraphics) : base(color)
    {
        if(generateGraphics)
        {
            InitialPoints = Model.Scissor.Points;
            VAO = Model.Scissor.VAO;
            UpdatePosition(new Point(x, y));
        }
        else
            CellPosition = new Point(x, y);
    }
    public Scissor(Point point, bool color, bool generateGraphics) : this(point.X, point.Y, color, generateGraphics) { }
    public override Point[] GetAvailableMoves()
    {
        List<Point> resultList = new List<Point>();
        for (int j = -1; j <= 1; j += 2)
        {
            int i = 0;
            do
            {
                i += j;
                if (IsNotAllyAndInside(i, i))
                    resultList.Add(new Point(CellPosition.X + i, CellPosition.Y + i));
            } while (IsNotAllyAndInside(i, i) && GameLogic.FindPiece(new Point(CellPosition.X + i, CellPosition.Y + i)) == null);
        }
        for (int j = -1; j <= 1; j += 2)
        {
            int i = 0;
            do
            {
                i += j;
                if (IsNotAllyAndInside(i, -i))
                    resultList.Add(new Point(CellPosition.X + i, CellPosition.Y - i));
            } while (IsNotAllyAndInside(i, -i) && GameLogic.FindPiece(new Point(CellPosition.X + i, CellPosition.Y - i)) == null);
        }

        return resultList.ToArray();
    }
    public override int IsHigher(Piece piece)
    {
        if (piece is Rock)
            return -1;
        if (piece is Scissor)
            return 0;
        return 1;
    }
}
