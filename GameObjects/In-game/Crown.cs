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
        public Point CellPosition { get; set; }
        public float[] Points { get; set; }
        public VAO VAO { get; set; }
        public Crown(Piece piece) 
        { 
            VAO = new VAO(Model.CrownModel);
            Points = piece.GetVertexArray(Model.CrownModel.Points);
            CellPosition = piece.CellPosition;
            VAO.Init(Points);
        }
        

    }
}
