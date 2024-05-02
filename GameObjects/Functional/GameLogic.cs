using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using GameObjects.Decorators;

namespace GameObjects.Functional;

public static class GameLogic
{
    public static bool Turn { get; set; } = true;
    public static Piece? SelectedPiece { get; set; } = null;
    public static Point[] AvalPts { get; set; } = [];
    public static void Restart()
    {
        Turn = true;
        GL.ClearColor(0.15f, 0.05f, 0.05f, 1);
    }
    public static void SwitchTurn()
    {
        Turn = !Turn;
        if (Turn)
            GL.ClearColor(0.15f, 0.05f, 0.05f, 1);
        else
            GL.ClearColor(0.05f, 0.05f, 0.15f, 1);

    }
    public static void SetAvalPtsForSelectedPiece()
    {
        System.Drawing.Point[] points;
        if (SelectedPiece != null && SelectedPiece.Color == Turn)
        {
            points = SelectedPiece.GetAvailableMoves();
        }
        else
            points = [];
        AvalPts = points;
    }
    public static bool ImpactSelectedPieceWith(Piece secondPiece)
    {
        if(SelectedPiece != null)
        {
            if (SelectedPiece.IsHigher(secondPiece) == 1)
            {
                SelectedPiece.UpdatePosition(secondPiece.CellPosition);
                Board.PieceList.Remove(secondPiece);
                if (secondPiece is King)
                {
                    return true;
                }
            }
            else
            {
                Board.PieceList.Remove(SelectedPiece);
            }
        }
        return false;
    }
    public static Piece? FindPiece(Point point)
    {
        return Board.PieceList.Find(match => match.CellPosition.X == point.X && match.CellPosition.Y == point.Y);
    }
    public static void MoveSelectedPieceTo(Point point)
    {
        if(SelectedPiece!=null)
        {
            SelectedPiece.UpdatePosition(point);
        }
    }
    public static Point GetSelectedCell(int xSize, int ySize)
    {
        return Board.GetCellPosition(Mouse.GetNormalized(xSize, ySize));
    }
    public static void AscendRock(Rock rock, bool generateGraphics)
    {
        Board.PieceList[Board.PieceList.FindIndex(match => match.CellPosition == rock.CellPosition)] = new StrongRock(rock,generateGraphics);
    }
}
