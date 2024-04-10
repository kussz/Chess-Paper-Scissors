using OpenTK.Graphics.OpenGL;


namespace Graphics
{
    internal class VBO
    {
        internal static int Create(float[] data)
        {
            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            return vbo;
        }
    }
}
