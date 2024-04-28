using OpenTK.Graphics.OpenGL;

namespace Graphics
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
        internal void Update(uint[] data)
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, Index);
            GL.BufferData(BufferTarget.ElementArrayBuffer, data.Length * sizeof(uint), data, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }
    }
}
