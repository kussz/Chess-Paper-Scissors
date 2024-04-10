using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObjects;

namespace Graphics
{
    public static class KingModel
    {
        public static float[] Points { get; private set; }
        public static uint[] Indexes { get; private set; }
        static KingModel()
        {
            Points = Model.Make("D:\\work\\Course 2\\Term 2\\Курсовая\\Программа\\Graphics\\data\\Model\\King.obj");
            Indexes = Model.GetIndexes();
        }
        public static void Init(this King piece)
        {
            piece.Points = Points;
            piece.Indexes = Indexes;
        }
    }
}
