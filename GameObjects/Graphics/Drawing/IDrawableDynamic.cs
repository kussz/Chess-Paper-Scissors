using System.Drawing;

namespace GameObjects.Graphics.Drawing;

public interface IDrawableDynamic : IDrawable
{
    public float[] InitialPoints { get; set; }
    public float[] Points { get; set; }
    public void UpdatePosition(Point cellPosition);
}
