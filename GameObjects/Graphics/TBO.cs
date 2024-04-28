using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Drawing;

namespace GameObjects.Graphics;

internal class TBO
{
    internal int TextureIndex { get; set; }
    internal int CoordsIndex { get; set; }
    internal TBO(Vector3[] image, Point size, Vector2[] coords)
    {
        TextureIndex = CreateImage(image, size);
        CoordsIndex = CreateCoords(coords);
    }
    internal static int CreateImage(Vector3[] data, Point size)
    {
        int vboTex = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, vboTex);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb32f, size.X, size.Y, 0, PixelFormat.Rgb, PixelType.Float, data);
        GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)TextureMagFilter.Linear });
        GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)TextureMinFilter.Linear });
        GL.BindTexture(TextureTarget.Texture2D, 0);
        return vboTex;
    }
    internal static int CreateCoords(Vector2[] data)
    {
        int vboTex = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, vboTex);
        GL.BufferData(BufferTarget.ArrayBuffer, Vector2.SizeInBytes * data.Length, data, BufferUsageHint.DynamicDraw);
        return vboTex;
    }
    internal static void Update(float[] data, int vbo)
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }
}
