using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    public class Rock : Piece
    {
        public Rock(int x, int y, bool color) : base(x, y, color)
        {  }
        public Rock(Point point, bool color) : base(point,color)
        { }
        protected override void InitPoints()
        {
            float angle = 0;
            int faces = 6;
            float angleDelta=(float)Math.PI * 2/ faces;
            float[] circPoints = new float[faces*2+2];
            for(int  i = 0; i < circPoints.Length;i+=2)
            {
                circPoints[i] = (float)Math.Cos(angle)*_size;
                circPoints[i + 1] = (float)Math.Sin(angle) * _size;
                angle += angleDelta;
            }
            circPoints[faces * 2] = circPoints[0];
            circPoints[faces * 2+1] = circPoints[1];
            Points = new float[faces*14+7];
            for(int i=0;i<Points.Length-7;i+=14)
            {
                Points[i] = circPoints[i*2/14];
                Points[i+1] = circPoints[i*2/14+1];
                Points[i + 2] = 0f;
                Points[i + 3] = GetColor().X/2;
                Points[i + 4] = GetColor().Y/2;
                Points[i + 5] = GetColor().Z/2;
                Points[i + 6] = GetColor().W;
                Points[i + 7] = circPoints[i * 2 /14];
                Points[i + 8] = circPoints[i * 2 /14 + 1];
                Points[i + 9] = _height;
                Points[i + 10] = GetColor().X;
                Points[i + 11] = GetColor().Y;
                Points[i + 12] = GetColor().Z;
                Points[i + 13] = GetColor().W;
            }
            Points[Points.Length - 7] = 0;
            Points[Points.Length - 6] = 0;
            Points[Points.Length - 5] = _height;
            Points[Points.Length - 4] = GetColor().X;
            Points[Points.Length - 3] = GetColor().Y;
            Points[Points.Length - 2] = GetColor().Z;
            Points[Points.Length - 1] = 1;
            Indexes = new uint[faces*9];

            uint offset = 0;
            for(int j = 0;j< (faces*6);j+=6)
            {

                Indexes[j] = offset;
                Indexes[j + 1] = offset + 2;
                Indexes[j + 2] = offset + 1;
                Indexes[j + 3] = offset + 1;
                Indexes[j + 4] = offset + 2;
                Indexes[j + 5] = offset + 3;
                offset += 2;
            }
            Indexes[faces * 6 -1] = 1;
            Indexes[faces * 6 - 2] = 0;
            Indexes[faces * 6 - 5] = 0;
            offset = 1;
            for (int j = 0; j < (faces*3); j += 3)
            {

                Indexes[faces*6+j ] = offset + 2;
                Indexes[faces * 6 +j + 1] = (uint)faces*2;
                Indexes[faces * 6 +j + 2] = offset;
                offset += 2;
            }
            Indexes[Indexes.Length - 3] = 1;

        }
        public override int IsHigher(Piece piece)
        {
            if (piece is Paper)
                return -1;
            if (piece is Rock)
                return 0;
            return 1;
        }
        public override Point[] GetAvailableMoves()
        {
            List<Point> resultList = new List<Point>();
            for(int i = -1;i<=1;i++)
                for(int j = -1;j<=1;j++)
                    if (IsNotAllyAndInside(i,j))
                    {
                        if(i!=0 || j!=0)
                            resultList.Add(new Point(CellPosition.X + i, CellPosition.Y + j));
                    }
            return resultList.ToArray();
        }
    }
}
