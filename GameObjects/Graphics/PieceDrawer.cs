using GameObjects;
using GameObjects.Decorators;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public static class PieceDrawer
    {
        public static float[] GetVertexArray(this Piece piece)
        {
            float[] result = new float[piece.Points.Length];
            Vector2 position = GetPosition(piece.CellPosition);
            //Vector2 position = new Vector2(0, 0);
            for (int i = 0; i < piece.Points.Length; i += 7)
            {
                result[i] = piece.Points[i] + position.X;
                result[i + 1] = piece.Points[i + 1] + position.Y;
                result[i + 2] = piece.Points[i + 2];
                result[i + 3] = piece.Points[i + 3];
                result[i + 4] = piece.Points[i + 4];
                result[i + 5] = piece.Points[i + 5];
                result[i + 6] = piece.Points[i + 6];
            }
            return result;
        }
        public static float[] GetVertexArray(this Piece piece, float[] points)
        {
            float[] result = new float[points.Length];
            Vector2 position = GetPosition(piece.CellPosition);
            //Vector2 position = new Vector2(0, 0);
            for (int i = 0; i < points.Length; i += 7)
            {
                result[i] = points[i] + position.X;
                result[i + 1] = points[i + 1] + position.Y;
                result[i + 2] = points[i + 2];
                result[i + 3] = points[i + 3];
                result[i + 4] = points[i + 4];
                result[i + 5] = points[i + 5];
                result[i + 6] = points[i + 6];
            }
            return result;
        }
        public static float[] GetCrownArray(this Piece piece)
        {
            float[] points = Model.CrownModel.Points;
            float[] result = new float[points.Length];
            Vector2 position = GetPosition(piece.CellPosition);
            //Vector2 position = new Vector2(0, 0);
            for (int i = 0; i < result.Length; i += 7)
            {
                result[i] = points[i] + position.X;
                result[i + 1] = points[i + 1] + position.Y;
                result[i + 2] = points[i + 2];
                result[i + 3] = points[i + 3];
                result[i + 4] = points[i + 4];
                result[i + 5] = points[i + 5];
                result[i + 6] = points[i + 6];
            }
            return result;
        }

        private static Vector2 GetPosition(Point cellPos) 
        {
            Vector2 result = Board.GetCellTopLeftPosition(cellPos);
            result.X += 0.1f;
            result.Y += 0.1f;
            return result;
        }
    }
}
