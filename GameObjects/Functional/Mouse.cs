using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing;
using OpenTK.Mathematics; 

namespace GameObjects.Functional;

public class Mouse
{
    private static float Sensivity = 1.5f;

    private static float _xmin = 0.051f;
    private static float _ymin = 0.151f;
    private static float _xmax = 0.85f;
    private static float _ymax = 0.95f;
    private static bool _locked=true;
    static Point mouse;
    public static void Init(Vector2i resolustion)
    {
        mouse.X=resolustion.X/2; mouse.Y=resolustion.Y/2;
    }
    public static void SetPosition(MouseState o,int xSize, int ySize)
    {
        int x = (int)((o.X - o.PreviousX) * Sensivity);
        int y = (int)((o.Y - o.PreviousY) * Sensivity);
        mouse.X += x;
        mouse.Y += y;
        if(_locked)
        {
        mouse.X = Math.Max(Math.Min((int)(xSize * _xmax), mouse.X), (int)(xSize * _xmin));
        mouse.Y = Math.Max(Math.Min((int)(ySize * _ymax), mouse.Y), (int)(ySize * _ymin));
        }
    }
    private static Vector2 GetPosition()
    {
        return new Vector2(mouse.X, mouse.Y);
    }
    public static Vector2 GetNormalized(int xSize, int ySize)
    {
        float x = (GetPosition().X - xSize / 2) / xSize;
        float y = (GetPosition().Y - ySize / 2) / ySize;
        return new Vector2(x, y);
    }
}
