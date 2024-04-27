using System.Drawing;
using Assimp;

namespace Graphics
{
    public static class Model
    {
        private static uint[] _indexes;
        public static PieceModel King { get; private set; }
        public static PieceModel Rock { get; private set; }
        public static PieceModel Paper { get; private set; }
        public static PieceModel Scissor { get; private set; }
        public static PieceModel Crown { get; private set; }
        private static OpenTK.Mathematics.Vector2[] _texCoords;
        private static OpenTK.Mathematics.Vector3[] _textureData;
        public static Point TextureResolution { get; private set; }

        public static float[] Make(string modelPath)
        {
            AssimpContext importer = new AssimpContext();
            Scene scene = importer.ImportFile(modelPath,PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs | PostProcessSteps.MakeLeftHanded);
            if (scene != null && scene.HasMeshes)
            {

                Mesh mesh = scene.Meshes[0];
                float [] points = new float[mesh.Vertices.Count * 7];
                int j = 0;
                for (int i = 0; i < points.Length; i += 7)
                {
                    points[i] = mesh.Vertices[j].X/15;
                    points[i + 1] = mesh.Vertices[j].Z/15;
                    points[i + 2] = mesh.Vertices[j].Y/15;
                    j++;
                }
                scene.Materials[mesh.MaterialIndex].GetMaterialTexture(TextureType.Diffuse, 0, out TextureSlot textureSlot);
                Bitmap bitmap = (Bitmap)Image.FromFile(textureSlot.FilePath);
                _textureData = new OpenTK.Mathematics.Vector3[bitmap.Width*bitmap.Height];
                for (int y = 0, i=0; y < bitmap.Height; y++)
                    for (int x = 0; x < bitmap.Width; x++,i++)
                    {
                        Color p = bitmap.GetPixel(x, y);
                        _textureData[i] = new OpenTK.Mathematics.Vector3(p.R / 255f, p.G / 255f, p.B / 255f);
                    }
                TextureResolution = new(bitmap.Width, bitmap.Height);
                _texCoords = mesh.TextureCoordinateChannels[0].ConvertAll(vector => new OpenTK.Mathematics.Vector2(vector.X, vector.Y)).ToArray();
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
        public static OpenTK.Mathematics.Vector3[] GetTexture()
        {
            return _textureData;
        }
        public static OpenTK.Mathematics.Vector2[] GetTextureCoords()
        {
            return _texCoords;
        }
        static Model()
        {
            _indexes = [];
            _texCoords = [];
            _textureData = [];
            Scissor = new PieceModel("D:\\work\\Course 2\\Term 2\\Курсовая\\Программа\\Graphics\\data\\Model\\Scissor.obj");
            Crown = new PieceModel("D:\\work\\Course 2\\Term 2\\Курсовая\\Программа\\Graphics\\data\\Model\\Crown.obj");
            King = new PieceModel("D:\\work\\Course 2\\Term 2\\Курсовая\\Программа\\Graphics\\data\\Model\\King.obj");
            Rock = new PieceModel("D:\\work\\Course 2\\Term 2\\Курсовая\\Программа\\Graphics\\data\\Model\\Rock.obj");
            Paper = new PieceModel("D:\\work\\Course 2\\Term 2\\Курсовая\\Программа\\Graphics\\data\\Model\\Paper.obj");
        }

        
    }
}
