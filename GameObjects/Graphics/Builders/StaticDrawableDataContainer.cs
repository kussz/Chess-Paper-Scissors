using GameObjects.Graphics.Drawing;
using GameObjects.Graphics.GraphicsObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects.Graphics.Builders
{
    public class StaticDrawableDataContainer : IDrawableStatic
    {
        public float[] InitialPoints { get; set; }
        public VAO VAO { get; set; }
        public StaticDrawableDataContainer(float[] points, uint[] indexes)
        {
            VAO = new VAO(points, indexes);
        }
    }
}
