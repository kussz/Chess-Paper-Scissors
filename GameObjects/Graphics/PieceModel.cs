using GameObjects;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class PieceModel
    {
        public float[] Points;
        public Vector3[] Texture;
        public uint[] Indexes;
        public Vector2[] TextureCoords;
        public Point TextureResolution;
        public PieceModel(string filePath)
        {
            Points = Model.Make(filePath);
            Indexes = Model.GetIndexes();
            Texture = Model.GetTexture();
            TextureCoords = Model.GetTextureCoords();
            TextureResolution = Model.TextureResolution;
            VAO = new(this);
        }
        public VAO VAO { get; private set; }

    }
}



