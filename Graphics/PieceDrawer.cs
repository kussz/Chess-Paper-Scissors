using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public static class PieceDrawer
    {
        public static void Draw(this Piece piece)
        {

        }
        private static float[] GetPointsPosition(this Piece piece)
        {
            float[] result = new float[piece.Points.Length];
            Board.GetCellTopLeftPosition(piece.Position);
            for (int i = 0; i < piece.Points.Length / 3; i += 3)
            {
                result[i] = piece.Points[i] + piece.Position.X;
                result[i + 1] = piece.Points[i] + piece.Position.Y;
                result[i + 2] = piece.Points[i];
            }
            return result;
        }
    }
}
