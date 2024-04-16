
using OpenTK.Mathematics;
using System.Drawing;

namespace GameObjects
{
    public static class Board
    {
        public static char[,] State = new char[8, 8];
        static string arrangement =
        "psrskrsp" +
            "rpsprspr" +
            "        " +
            "        " +
            "        " +
            "        " +
            "RPSPRSPR" +
            "PSRSKRSP";
        public static List<Piece> pieceList;
        public static void Init()
        {
            pieceList = new List<Piece>();
            for (int j = 0; j < 8; j++)
                for (int i = 0; i < 8; i++)
                {
                    switch (arrangement[j * 8 + i])
                    {
                        case 'r':
                            pieceList.Add(new Rock(i, j, false));
                            State[i, j] = 'o';
                            break;
                        case 'p':
                            pieceList.Add(new Paper(i, j, false));
                            State[i, j] = 'o';
                            break;
                        case 's':
                            pieceList.Add(new Scissor(i, j, false));
                            State[i, j] = 'o';
                            break;
                        case 'k':
                            pieceList.Add(new King(i, j, false));
                            State[i, j] = 'o';
                            break;
                        case 'R':
                            pieceList.Add(new Rock(i, j, true));
                            State[i, j] = 'O';
                            break;
                        case 'P':
                            pieceList.Add(new Paper(i, j, true));
                            State[i, j] = 'O';
                            break;
                        case 'S':
                            pieceList.Add(new Scissor(i, j, true));
                            State[i, j] = 'O';
                            break;
                        case 'K':
                            pieceList.Add(new King(i, j, true));
                            State[i, j] = 'O';
                            break;
                        default:
                            State[i, j] = ' ';
                            break;

                    }
                }
        }
        static Board()
        {
            Init();
        }
        public static Point GetCellPosition(float x, float y)
        {
            int xRes = (int)((x + 0.45f) * 10);
            int yRes = (int)((y + 0.35f) * 10);
            return new Point(xRes, yRes);
        }
        

        public static Vector2 GetCellTopLeftPosition(Point point) 
        {
            float xRes = point.X / 5.0f-0.8f;
            float yRes = -point.Y / 5.0f +0.6f;
            return new Vector2(xRes, yRes);
        }

        public static Point GetCellPosition(Vector2 vector2)
        {
            return GetCellPosition(vector2.X, vector2.Y);
        }
    }

}
