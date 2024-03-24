using GameObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenTK.Mathematics;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public static class TileDrawer
    {
        private static float _tileSize = 0.2f;
        private static float _height = 0.001f;
        public static float[] Points {  get; private set; }
        private static Vector3 _color;
        public static uint[] Indexes { get; set; }
        static TileDrawer()
        {
            _color = new Vector3();
            Indexes = [0, 1, 2, 0, 2, 3];
            Points = new float[28];
        }
        public static void SetPts(Point point)
        {
            if (Board.State[point.X,point.Y] == ' ')
            {
                _color.X = 0;
                _color.Y = 1;
                _color.Z = 1;
            }
            else
            {
                _color.X = 1;
                _color.Y = 0;
                _color.Z = 0;
            }
            Vector2 unnormed  = Board.GetCellTopLeftPosition(point);
            Points[0] = unnormed.X+0.025f;
            Points[1] = unnormed.Y+0.025f;
            Points[2] = _height;
            Points[3] = _color.X;
            Points[4] = _color.Y;
            Points[5] = _color.Z;
            Points[6] = 0.3f;
            Points[7] = unnormed.X + _tileSize-0.025f;
            Points[8] = unnormed.Y+0.025f;
            Points[9] = _height;
            Points[10] = _color.X;
            Points[11] = _color.Y;
            Points[12] = _color.Z;
            Points[13] = 0.3f;
            Points[14] = unnormed.X + _tileSize- 0.025f;
            Points[15] = unnormed.Y + _tileSize- 0.025f;
            Points[16] = _height;
            Points[17] = _color.X;
            Points[18] = _color.Y;
            Points[19] = _color.Z;
            Points[20] = 0.3f;
            Points[21] = unnormed.X+ 0.025f;
            Points[22] = unnormed.Y + _tileSize- 0.025f;
            Points[23] = _height;
            Points[24] = _color.X;
            Points[25] = _color.Y;
            Points[26] = _color.Z;
            Points[27] = 0.3f;
        }
    }
}
