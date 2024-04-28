using Graphics;
using System.Drawing;

namespace GameObjects;

public class Crown : IDrawable
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
