﻿using Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    public class Paper : Piece
    {
        public Paper(int x, int y, bool color) : base(x, y, color)
        { 
            Type = PieceType.Paper;
            Points = Model.Paper.Points;
            Indexes = Model.Paper.Indexes;
        }
        public Paper(Point point, bool color) : this(point.X, point.Y, color) { }
        public override Point[] GetAvailableMoves()
        {
            List<Point> resultList = new List<Point>();
            int i = 0;
            do
            {
                i++;
                if (IsNotAllyAndInside(i, 0))
                    resultList.Add(new Point(CellPosition.X + i, CellPosition.Y));
            } while (IsNotAllyAndInside(i, 0) && Board.State[CellPosition.X + i, CellPosition.Y] == ' ');
            i = 0;
            do
            {
                i--;
                if (IsNotAllyAndInside(i, 0))
                    resultList.Add(new Point(CellPosition.X + i, CellPosition.Y));
            } while (IsNotAllyAndInside(i, 0) && Board.State[CellPosition.X + i, CellPosition.Y] == ' ');
            i = 0;
            do
            {
                i++;
                if (IsNotAllyAndInside(0, i))
                    resultList.Add(new Point(CellPosition.X, CellPosition.Y + i));
            } while (IsNotAllyAndInside(0, i) && Board.State[CellPosition.X, CellPosition.Y + i] == ' ');
            i = 0;
            do
            {
                i--;
                if (IsNotAllyAndInside(0, i))
                    resultList.Add(new Point(CellPosition.X, CellPosition.Y + i));
            } while (IsNotAllyAndInside(0, i) && Board.State[CellPosition.X, CellPosition.Y + i] == ' ');
            return resultList.ToArray();
        }
        public override int IsHigher(Piece piece)
        {
            if (piece.Type == PieceType.Scissor)
                return -1;
            if (piece.Type == PieceType.Paper)
                return 0;
            return 1;
        }
    }
}