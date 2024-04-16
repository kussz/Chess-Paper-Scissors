using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class PieceModel
    {
        public float[] Points;
        public int TextureID;
        public uint[] Indexes;
        public int TextureBufferID;
        public PieceModel(string filePath)
        {
            Points = Model.Make(filePath);
            Indexes = Model.GetIndexes();
            TextureID = Model.GetTexID();
            TextureBufferID = Model.GetTexBufferID();
        }
        public void Init(Piece piece)
        {
            piece.Points = Points;
            piece.Indexes = Indexes;
            piece.TextureID = TextureID;
            piece.TextureCoordsBufferID = TextureBufferID;
        }

    }
}



