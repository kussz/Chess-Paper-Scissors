﻿using GameObjects;
using OpenTK.Mathematics;
using System.Drawing;

namespace Graphics
{
    public static class PieceVertices
    {
        public static float[] GetVertexArray(this Piece piece, float[] points)
        {
            float[] result = new float[points.Length];
            Vector2 position = GetPosition(piece.CellPosition);
            //Vector2 position = new Vector2(0, 0);
            for (int i = 0; i < points.Length; i += 3)
            {
                result[i] = points[i] + position.X;
                result[i + 1] = points[i + 1] + position.Y;
                result[i + 2] = points[i + 2];
            }
            return result;
        }
        public static float[] GetCrownArray(this Piece piece)
        {
            float[] points = Model.Crown.Points;
            float[] result = new float[points.Length];
            Vector2 position = GetPosition(piece.CellPosition);
            //Vector2 position = new Vector2(0, 0);
            for (int i = 0; i < result.Length; i += 3)
            {
                result[i] = points[i] + position.X;
                result[i + 1] = points[i + 1] + position.Y;
                result[i + 2] = points[i + 2];
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
