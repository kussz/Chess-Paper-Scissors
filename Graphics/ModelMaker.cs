using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assimp;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

namespace Graphics
{
    public class Model
    {
        private static float[] points;
        private static uint[] indexes;
        public static float[] Make(string path)
        {
            AssimpContext importer = new AssimpContext();
            Scene scene = importer.ImportFile(path,PostProcessSteps.Triangulate);
            if (scene != null && scene.HasMeshes)
            {
                points = new float[scene.Meshes[0].Vertices.Count * 7];
                int j = 0;
                for (int i = 0; i < points.Length; i += 7)
                {
                    points[i] = scene.Meshes[0].Vertices[j].X/3;
                    points[i + 1] = scene.Meshes[0].Vertices[j].Z/3;
                    points[i + 2] = scene.Meshes[0].Vertices[j].Y/3;
                    points[i + 3] = 0.5f;
                    points[i + 4] = 0.3f;
                    points[i + 5] = 0.5f;
                    points[i + 6] = 1;
                    j++;
                }
                indexes = new uint[scene.Meshes[0].GetIndices().Length];
                int k = 0;
                for(int i = indexes.Length-1;i>=0;i--)
                {
                    indexes[k] = (uint)scene.Meshes[0].GetIndices()[i];
                    k++;
                }
                return points;
            }
            else
                return [];
        }

        public static uint[] GetIndexes()
        {
            return indexes;
        }
    }
}
