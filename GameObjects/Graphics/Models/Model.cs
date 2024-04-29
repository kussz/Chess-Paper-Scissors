using Assimp;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace GameObjects.Graphics.Models;

public static class Model
{
    public static ConcreteModel King { get; private set; }
    public static ConcreteModel Rock { get; private set; }
    public static ConcreteModel Paper { get; private set; }
    public static ConcreteModel Scissor { get; private set; }
    public static ConcreteModel Crown { get; private set; }

    public static ConcreteModel Make(string modelPath)
    {
        try
        {
            AssimpContext importer = new AssimpContext();
            Scene scene = importer.ImportFile(modelPath, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs | PostProcessSteps.MakeLeftHanded);

            Mesh mesh = scene.Meshes[0];
            float[] points = new float[mesh.Vertices.Count * 3];
            int j = 0;
            for (int i = 0; i < points.Length; i += 3)
            {
                points[i] = mesh.Vertices[j].X / 15;
                points[i + 1] = mesh.Vertices[j].Z / 15;
                points[i + 2] = mesh.Vertices[j].Y / 15;
                j++;
            }
            scene.Materials[mesh.MaterialIndex].GetMaterialTexture(TextureType.Diffuse, 0, out TextureSlot textureSlot);
            OpenTK.Mathematics.Vector3[] textureData;
            Point textureResolution;
            OpenTK.Mathematics.Vector2[] texCoords;
            try
            {

                Bitmap bitmap = (Bitmap)Image.FromFile(textureSlot.FilePath);
                textureData = new OpenTK.Mathematics.Vector3[bitmap.Width * bitmap.Height];
                for (int y = 0, i = 0; y < bitmap.Height; y++)
                    for (int x = 0; x < bitmap.Width; x++, i++)
                    {
                        Color p = bitmap.GetPixel(x, y);
                        textureData[i] = new OpenTK.Mathematics.Vector3(p.R / 255f, p.G / 255f, p.B / 255f);
                    }
                textureResolution = new(bitmap.Width, bitmap.Height);
                texCoords = mesh.TextureCoordinateChannels[0].ConvertAll(vector => new OpenTK.Mathematics.Vector2(vector.X, vector.Y)).ToArray();
            }
            catch
            {
                textureData = [];
                texCoords = [];
                textureResolution = new(0, 0);
            }

            uint[] indexes = scene.Meshes[0].GetUnsignedIndices();
            return new ConcreteModel(points,indexes,textureData,texCoords,textureResolution);
        }
        catch
        {
            return new ConcreteModel([-0.05f,-0.05f,0.05f, 0.05f, -0.05f, 0.05f, -0.05f, 0.05f, 0.05f, 0.05f, 0.05f, 0.05f],[2,0,1,1,3,2],[],[],new Point(0,0));
        }
    }
    static Model()
    {
        Scissor = Make("D:\\work\\Course 2\\Term 2\\Курсовая\\Программа\\GameObjects\\Graphics\\data\\Model\\Scissor.obj");
        Crown = Make("D:\\work\\Course 2\\Term 2\\Курсовая\\Программа\\GameObjects\\Graphics\\data\\Model\\Crown.obj");
        King = Make("D:\\work\\Course 2\\Term 2\\Курсовая\\Программа\\GameObjects\\Graphics\\data\\Model\\King.obj");
        Rock = Make("D:\\work\\Course 2\\Term 2\\Курсовая\\Программа\\GameObjects\\Graphics\\data\\Model\\Rock.obj");
        Paper = Make("D:\\work\\Course 2\\Term 2\\Курсовая\\Программа\\GameObjects\\Graphics\\data\\Model\\Paper.obj");
    }


}
