﻿using GameObjects.Graphics.GraphicsObjects;
using OpenTK.Mathematics;
using System.Drawing;

namespace GameObjects.Graphics.Models;

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


