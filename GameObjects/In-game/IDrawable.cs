using Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    public interface IDrawable
    {
        public float[] InitialPoints { get; set; }
        public float[] Points { get; set; }
        public void UpdatePosition(Point cellPosition);
    }
}
