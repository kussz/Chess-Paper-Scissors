using GameObjects.Graphics.Models;
using System.Drawing;

namespace GameObjects;

public class Paper : Piece
{
    public Paper(int x, int y, bool color) : base(x, y, color)
    {
        InitialPoints = Model.Paper.Points;
        UpdatePosition(new Point(x, y));
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
        if (piece is Scissor)
            return -1;
        if (piece is Paper)
            return 0;
        return 1;
    }
}
