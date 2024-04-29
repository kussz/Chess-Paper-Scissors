using GameObjects.Graphics.GraphicsObjects;
using OpenTK.Mathematics;
using System.Drawing;

namespace GameObjects.Graphics.Models;

public class ConcreteModel
{
    public float[] Points;
    public Vector3[] Texture;
    public uint[] Indexes;
    public Vector2[] TextureCoords;
    public Point TextureResolution;
    public ConcreteModel(float[] points, uint[] indexes, Vector3[] textureData, Vector2[] textureCoords, Point textureResolution)
    {
        Points = points;
        Indexes = indexes;
        Texture = textureData;
        TextureCoords = textureCoords;
        TextureResolution = textureResolution;
        VAO = new(points, indexes, textureData, textureCoords, textureResolution);
    }
    public VAO VAO { get; private set; }

}



