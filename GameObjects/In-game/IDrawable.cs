using System.Drawing;

namespace GameObjects;

public interface IDrawable
{
    public float[] InitialPoints { get; set; }
    public float[] Points { get; set; }
    public void UpdatePosition(Point cellPosition);
}
