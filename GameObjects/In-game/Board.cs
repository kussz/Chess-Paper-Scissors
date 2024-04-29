
using GameObjects.Factory;
using OpenTK.Mathematics;
using System.Drawing;


namespace GameObjects;

public static class Board
{
    private static List<PieceFactory> _factories;
    public static char[,] State;
    private static string arrangement =
        "psrskrsp" +
        "rpsprspr" +
        "        " +
        "        " +
        "        " +
        "        " +
        "RPSPRSPR" +
        "PSRSKRSP";
    public static List<Piece> PieceList;
    static Board()
    {
        State = new char[8, 8];
        PieceList = new List<Piece>();
        _factories = new List<PieceFactory>() { new KingFactory(), new RockFactory(), new PaperFactory(), new ScissorFactory() };
    }
    public static void Init()
    {
        PieceList.Clear();
        State = new char[8, 8];
        int[,] pieceMap = GetArrangementMap();

        for (int j = 0; j < 8; j++)
            for (int i = 0; i < 8; i++)
            {
                if (pieceMap[i, j] != -1)
                    PieceList.Add(_factories[pieceMap[i, j] % _factories.Count].Create(i, j, Convert.ToBoolean(pieceMap[i, j] / _factories.Count),true));
            }
    }
    private static int[,] GetArrangementMap()
    {
        int[,] mapped = new int[8, 8];
        for (int j = 0; j < 8; j++)
            for (int i = 0; i < 8; i++)
            {
                switch (arrangement[j * 8 + i])
                {
                    case 'r':
                        mapped[i, j] = 1;
                        break;
                    case 'p':
                        mapped[i, j] = 2;
                        break;
                    case 's':
                        mapped[i, j] = 3;
                        break;
                    case 'k':
                        mapped[i, j] = 0;
                        break;
                    case 'R':
                        mapped[i, j] = 5;
                        break;
                    case 'P':
                        mapped[i, j] = 6;
                        break;
                    case 'S':
                        mapped[i, j] = 7;
                        break;
                    case 'K':
                        mapped[i, j] = 4;
                        break;
                    default:
                        mapped[i, j] = -1;
                        State[i, j] = ' ';
                        break;

                }
                if (mapped[i, j] >= 4)
                    State[i, j] = 'O';
                else if (mapped[i, j] != -1)
                    State[i, j] = 'o';
            }
        return mapped;
    }

    private static Point GetCellPosition(float x, float y)
    {
        int xRes = (int)((x + 0.45f) * 10);
        int yRes = (int)((y + 0.35f) * 10);
        return new Point(xRes, yRes);
    }


    public static Vector2 GetCellTopLeftPosition(Point point)
    {
        float xRes = point.X / 5.0f - 0.8f;
        float yRes = -point.Y / 5.0f + 0.6f;
        return new Vector2(xRes, yRes);
    }

    public static Point GetCellPosition(Vector2 vector2)
    {
        return GetCellPosition(vector2.X, vector2.Y);
    }
}
