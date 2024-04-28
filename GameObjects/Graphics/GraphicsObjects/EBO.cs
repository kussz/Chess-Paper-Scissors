using OpenTK.Graphics.OpenGL;

namespace GameObjects.Graphics.GraphicsObjects
{
    internal class EBO
    {
        internal int Index { get; set; }
        internal EBO(uint[] data)
        {
            Index = Create(data);
        }
        internal static int Create(uint[] data)
        {
            int ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, data.Length * sizeof(uint), data, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            return ebo;
        }
    }
}
