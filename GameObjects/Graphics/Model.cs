using System;
using System.Drawing;

using System.Numerics;
using Assimp;
using Assimp.Unmanaged;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Graphics
{
    public static class Model
    {
        private static uint[] _indexes;
        private static int _textureID;
        private static int _texCoordsBufferID;
        public static PieceModel KingModel { get; private set; }
        public static PieceModel RockModel { get; private set; }
        public static PieceModel PaperModel { get; private set; }
        public static PieceModel ScissorModel { get; private set; }

        public static float[] Make(string modelPath)
        {
            AssimpContext importer = new AssimpContext();
            Scene scene = importer.ImportFile(modelPath,PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs | PostProcessSteps.MakeLeftHanded);
            if (scene != null && scene.HasMeshes)
            {

                Mesh mesh = scene.Meshes[0];
                float []points = new float[mesh.Vertices.Count * 7];
                int j = 0;
                for (int i = 0; i < points.Length; i += 7)
                {
                    points[i] = mesh.Vertices[j].X/15;
                    points[i + 1] = mesh.Vertices[j].Z/15;
                    points[i + 2] = mesh.Vertices[j].Y/15;
                    points[i + 3] = 1;
                    points[i + 4] = 0;
                    points[i + 5] = 1;
                    points[i + 6] = 1;
                    j++;
                }
                scene.Materials[mesh.MaterialIndex].GetMaterialTexture(TextureType.Diffuse, 0, out TextureSlot textureSlot);
                Bitmap bitmap = (Bitmap)Image.FromFile(textureSlot.FilePath);
                OpenTK.Mathematics.Vector3[] textureData = new OpenTK.Mathematics.Vector3[bitmap.Width*bitmap.Height];
                for (int y = 0, i=0; y < bitmap.Height; y++)
                    for (int x = 0; x < bitmap.Width; x++,i++)
                    {
                        Color p = bitmap.GetPixel(x, y);
                        textureData[i] = new OpenTK.Mathematics.Vector3(p.R / 255f, p.G / 255f, p.B / 255f);
                    }




                _textureID = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, _textureID);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb32f, bitmap.Width, bitmap.Height, 0, PixelFormat.Rgb, PixelType.Float, textureData);
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)TextureMagFilter.Linear });
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)TextureMinFilter.Linear });

               // GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, 0);



                OpenTK.Mathematics.Vector2[] _texCoords = mesh.TextureCoordinateChannels[0].ConvertAll(vector => new OpenTK.Mathematics.Vector2(vector.X, vector.Y)).ToArray();
                _texCoordsBufferID = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, _texCoordsBufferID);
                GL.BufferData(BufferTarget.ArrayBuffer, OpenTK.Mathematics.Vector2.SizeInBytes * _texCoords.Length, _texCoords, BufferUsageHint.DynamicDraw);
                
                
                _indexes = scene.Meshes[0].GetUnsignedIndices();
                return points;
            }
            else
            {
                _indexes = [];
                return [];
            }
        }

        public static uint[] GetIndexes()
        {
            return _indexes;
        }

        public static int GetTexID()
        {
            return _textureID;
        }
        public static int GetTexBufferID()
        {
            return _texCoordsBufferID;
        }
        static Model()
        {
            KingModel = new PieceModel("D:\\work\\Course 2\\Term 2\\Курсовая\\Программа\\Graphics\\data\\Model\\King.obj");
            RockModel = new PieceModel("D:\\work\\Course 2\\Term 2\\Курсовая\\Программа\\Graphics\\data\\Model\\Rock.obj");
            PaperModel = new PieceModel("D:\\work\\Course 2\\Term 2\\Курсовая\\Программа\\Graphics\\data\\Model\\Paper.obj");
            ScissorModel = new PieceModel("D:\\work\\Course 2\\Term 2\\Курсовая\\Программа\\Graphics\\data\\Model\\Scissor.obj");
        }

        
    }
}
