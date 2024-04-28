using GameObjects.Graphics.Builders;
using GameObjects.Graphics.Drawing;
using GameObjects.Graphics.GraphicsObjects;
using GameObjects.Graphics.Models;
using System.Drawing;

namespace GameObjects;

public class Crown : IDrawableDynamic
{
    public float[] InitialPoints { get; set; } = [];
    public VAO VAO {  get; set; }
    public float[] Points { get; set; }
    public Crown(Point cellPosition)
    {
        UpdatePosition(cellPosition);
        VAO = Model.Crown.VAO;
    }
    public void UpdatePosition(Point cellPosition)
    {
        Points = PieceBuilder.GetCrownArray(cellPosition);
    }


}
