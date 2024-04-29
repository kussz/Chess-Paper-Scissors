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
    public static Point[] GetAvailablePoints(Piece? piece)
    {
        System.Drawing.Point[] points;
        if (piece != null && piece.Color == Turn)
        {
            points = piece.GetAvailableMoves();
        }
        else
            points = [];
        return points;
    }
    public static bool ImpactSelectedPieceWith(Piece secondPiece)
    {
        if(SelectedPiece != null)
        {
            if (SelectedPiece.IsHigher(secondPiece) == 1)
            {
                char pieceO = Board.State[SelectedPiece.CellPosition.X, SelectedPiece.CellPosition.Y];
                Board.State[SelectedPiece.CellPosition.X, SelectedPiece.CellPosition.Y] = ' ';
                SelectedPiece.UpdatePosition(secondPiece.CellPosition);
                Board.State[secondPiece.CellPosition.X, secondPiece.CellPosition.Y] = pieceO;
                Board.PieceList.Remove(secondPiece);
                if (secondPiece is King)
                {
                    return true;
                }
            }
            else if (SelectedPiece.IsHigher(secondPiece) == -1)
            {
                Board.State[SelectedPiece.CellPosition.X, SelectedPiece.CellPosition.Y] = ' ';
                Board.PieceList.Remove(SelectedPiece);
            }
        }
        return false;
    }
    public static Piece? FindPiece(System.Drawing.Point point)
    {
        return Board.PieceList.Find(match => match.CellPosition.X == point.X && match.CellPosition.Y == point.Y);
    }
    public static void MoveSelectedPieceTo(System.Drawing.Point point)
    {
        if(SelectedPiece!=null)
        {
            char pieceO = Board.State[SelectedPiece.CellPosition.X, SelectedPiece.CellPosition.Y];
            Board.State[SelectedPiece.CellPosition.X, SelectedPiece.CellPosition.Y] = ' ';
            SelectedPiece.UpdatePosition(point);
            Board.State[SelectedPiece.CellPosition.X, SelectedPiece.CellPosition.Y] = pieceO;
        }
    }
    public static Point GetSelectedCell(int xSize, int ySize)
    {
        return Board.GetCellPosition(Mouse.GetNormalized(xSize, ySize));
    }
    public static void AscendRock(Rock rock)
    {
        Board.PieceList[Board.PieceList.FindIndex(match => match.CellPosition == rock.CellPosition)] = new StrongRock(rock);
    }
}
