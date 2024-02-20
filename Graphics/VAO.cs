using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System.Drawing;
using OpenTK.Windowing.Common.Input;
using System;

namespace Graphics
{
    public class VAO
    {
        public int Index { get; }
        public VAO(float[] points, uint[] indexes, ShaderProgram shaderProg)
        {
            Index = Create(points,indexes,shaderProg);
        }
        public static int Create(float[] points, uint[] indexes, ShaderProgram shaderProg)
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);
            int vboVC = VBO.Create(points);

            int ebo = EBO.Create(indexes);
            int VertexArray = shaderProg.AttribLocation("aPosition");
            int ColorArray = shaderProg.AttribLocation("aColor");
            GL.EnableVertexAttribArray(VertexArray);
            GL.EnableVertexAttribArray(ColorArray);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVC);
            GL.VertexAttribPointer(VertexArray, 3, VertexAttribPointerType.Float, false, 7 * sizeof(float), 0);
            GL.VertexAttribPointer(ColorArray, 4, VertexAttribPointerType.Float, false, 7 * sizeof(float), 3 * sizeof(float));
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DisableVertexAttribArray(VertexArray);
            GL.DisableVertexAttribArray(ColorArray);
            return vao;
        }

        public static void Delete()
        {
            GL.BindVertexArray(0);
        }
        
    }
}
