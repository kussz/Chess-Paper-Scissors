
using Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects.Decorators
{
    public abstract class PieceDecorator : Piece
    {
        protected Piece _piece;
        public PieceDecorator(Piece piece)
        {
            _piece = piece;
            var extPts = Model.CrownModel.Points;
            var extIndexes = Model.CrownModel.Indexes;
            uint max = Indexes.Max();
            for (int i = 0; i < extIndexes.Length; i++)
            {
                extIndexes[i] = extIndexes[i] + max;
            }
            Points = Points.Concat(extPts).ToArray();
            Indexes = Indexes.Concat(extIndexes).ToArray();

            VAO.Update(Points,Indexes);
        }
        public override VAO VAO {  get { return _piece.VAO; } set { _piece.VAO = value; } }
        public override float[] Points { get { return _piece.Points; } set { _piece.Points = value;  } }
        public override bool Color { get { return _piece.Color; } }
        public override Point CellPosition { get { return _piece.CellPosition; } set { _piece.CellPosition = value; } }
        public override uint[] Indexes { get { return _piece.Indexes; } set { _piece.Indexes = value; } }
        public override int TextureID { get { return _piece.TextureID; } set { _piece.TextureID = value; } }
        public override int TextureCoordsBufferID { get { return _piece.TextureCoordsBufferID; } set { _piece.TextureCoordsBufferID = value; } }
        public override PieceType Type { get { return _piece.Type; } }
        public override int IsHigher(Piece piece)
        {
            return _piece.IsHigher(piece);
        }

    }
}
