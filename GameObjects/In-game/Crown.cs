using GameObjects.Graphics.Builders;
using GameObjects.Graphics.Drawing;
using System.Drawing;

namespace GameObjects;

public class Crown : IDrawableDynamic
{
    public float[] InitialPoints { get; set; } = [];
    public float[] Points { get; set; }
    public Crown(Point cellPosition)
    {
        UpdatePosition(cellPosition);
    }
    public void UpdatePosition(Point cellPosition)
    {
        Points = PieceBuilder.GetCrownArray(cellPosition);
    }


}
