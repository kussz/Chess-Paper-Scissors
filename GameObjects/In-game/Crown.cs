using Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    public class Crown : IDrawable
    {
        public float[] InitialPoints { get; set; }
        public float[] Points { get; set; }
        public Crown(Point cellPosition) 
        { 
            Points = PieceVertices.GetCrownArray(cellPosition);
        }
        public void UpdatePosition(Point cellPosition)
        {
            Points = PieceVertices.GetCrownArray(cellPosition);
        }
        

    }
}
