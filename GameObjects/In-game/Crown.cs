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
        public float[] Points { get; set; }
        public VAO VAO { get; set; }
        public Crown(Piece piece) 
        { 
            VAO = new VAO(Model.Crown);
            Points = piece.GetVertexArray(Model.Crown.Points);
            VAO.Init(Points);
        }
        

    }
}
