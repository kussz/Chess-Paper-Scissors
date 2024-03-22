
using OpenTK.Mathematics;
using System.Drawing;

namespace GameObjects
{
    public static class Board
    {
        public static char[,] State = new char[8, 8];
        static Board()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    State[i, j] = ' ';
            State[7, 6] = 'R';
        }
        public static Point GetCellPosition(float x, float y)
        {
            int xRes = (int)((x + 0.45f) * 10);
            int yRes = (int)((y + 0.35f) * 10);
            return new Point(xRes, yRes);
        }
        public static Point GetCellPosition(Vector2 vector2)
        {
            return GetCellPosition(vector2.X, vector2.Y);
        }

        public static Vector2 GetCellTopLeftPosition(Point point) 
        {
            float xRes = point.X / 5.0f-0.8f;
            float yRes = -point.Y / 5.0f +0.6f;
            return new Vector2(xRes, yRes);
        }
    }

}
