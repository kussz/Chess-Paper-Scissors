using OpenTK.Graphics.OpenGL;

namespace Graphics
{
    internal class EBO
    {
        internal static int Create(uint[] data)
        {
            int ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, data.Length * sizeof(uint), data, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            return ebo;
        }
        internal static void Update(uint[] data, int ebo)
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, data.Length * sizeof(uint), data, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }
    }
}
