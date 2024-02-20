
using OpenTK.Mathematics;
using System.Drawing;

namespace GameObjects
{
    public static class Board
    {
        private static float[] _points = new float[]
        {
            -0.9f,0.9f,0.07f,   0.9f,0.8f,0.8f,1,
            -0.8f,0.8f,0.07f,   0.9f,0.8f,0.8f,1,
            0.9f,0.9f,0.07f,    0.9f,0.8f,0.8f,1,
            0.8f,0.8f,0.07f,    0.9f,0.8f,0.8f,1,
            0.9f,-0.9f,0.07f,   0.9f,0.8f,0.8f,1,
            0.8f,-0.8f,0.07f,   0.9f,0.8f,0.8f,1,
            -0.9f,-0.9f,0.07f,  0.9f,0.8f,0.8f,1,
            -0.8f,-0.8f,0.07f,  0.9f,0.8f,0.8f,1,
            //8
            -0.8f,0.8f,0,      0f,0f,0f,1,
            0.8f,0.8f,0,       0f,0f,0f,1,
            0.8f,-0.8f,0,      0f,0f,0f,1,
            -0.8f,-0.8f,0,     0f,0f,0f,1,
            //12
            -0.9f,0.9f,0,      0.9f,0.8f,0.8f,1,
            0.9f,0.9f,0,       0.9f,0.8f,0.8f,1,
            0.9f,-0.9f,0,      0.9f,0.8f,0.8f,1,
            -0.9f,-0.9f,0,     0.9f,0.8f,0.8f,1,
        };
        private static uint[] _borderIndexes = new uint[]
        {
            8,9,1,
            1,9,3,
            9,10,3,
            3,10,5,
            10,11,5,
            11,7,5,
            11,8,7,
            8,1,7,

            13,12,0,
            0,2,13,
            14,13,2,
            14,2,4,
            15,14,4,
            15,4,6,
            12,15,6,
            12,6,0,

            0,1,2,
            2,1,3,
            2,3,4,
            4,3,5,
            4,5,6,
            6,5,7,
            6,7,0,
            7,1,0,
        };
        private static uint[] _boardIndexes = new uint[]
        {
            9,8,10,
            8,11,10
        };
        public static float[] GetVertices()
        {
            return _points;
        }
        public static uint[] GetBorderIndexes()
        {
            return _borderIndexes;
        }
        public static uint[] GetIndexes()
        {
            return _boardIndexes;
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
    }

}
