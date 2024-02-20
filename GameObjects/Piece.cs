namespace GameObjects
{
        public abstract class Shape
        {
            public Point3 Position { get; set; }
            private float[] _Points;
            private uint[] _Indexes;
            private float[] GetPointsPosition()
            {
                float[] result = new float[_Points.Length];
                for (int i = 0; i < _Points.Length / 3; i += 3)
                {
                    result[i] = _Points[i] + Position.X;
                    result[i + 1] = _Points[i] + Position.Y;
                    result[i + 2] = _Points[i] + Position.Z;
                }
                return result;
            }
            public void Draw()
            {
                
            }
        }
        public struct Point3
        {
            public float X;
            public float Y;
            public float Z;
        }
}
