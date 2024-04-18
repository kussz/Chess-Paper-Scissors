﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects.Decorators;

public class RockDecorator(Rock piece) : PieceDecorator(piece)
{
    
    public override Point[] GetAvailableMoves()
    {
        Point[] defaultPoints = _piece.GetAvailableMoves();
        List<Point> extendedPoints = new List<Point>();
        int dir = Color ? 1 : -1;
        for (int i=-1;i<=1;i++)
        {
            if (IsNotAllyAndInside(i, dir))
            {
                extendedPoints.Add(new(_piece.CellPosition.X + i, _piece.CellPosition.Y + dir));
            }
            if(IsNotAllyAndInside(i,0) && i!=0)
            {
                extendedPoints.Add(new(_piece.CellPosition.X + i, _piece.CellPosition.Y));
            }
        }
        for (int i = -2; i <= 2; i+=4)
        {
            if (IsNotAllyAndInside(i, 0))
            {
            extendedPoints.Add(new(_piece.CellPosition.X + i, _piece.CellPosition.Y));
            }
            if (IsNotAllyAndInside(0, i))
            {
                extendedPoints.Add(new(_piece.CellPosition.X, _piece.CellPosition.Y + i));
            }
        }
        

        return defaultPoints.Concat(extendedPoints).ToArray();
    }
    

}
