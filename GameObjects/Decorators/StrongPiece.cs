
using Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects.Decorators
{
    public abstract class StrongPiece : Piece
    {
        protected Piece _piece;
        public StrongPiece(Piece piece)
        {
            _piece = piece;
            Crown = new Crown(_piece);
        }
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
