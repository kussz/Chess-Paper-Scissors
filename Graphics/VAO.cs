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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Graphics
{
    public class VAO
    {
        public int Index { get; }
        private int _vboVC;
        private int _eboVC;
        private int _vao;
        public VAO(float[] points, uint[] indexes, ShaderProgram shaderProg)
        {
            Index = Create(points,indexes,shaderProg);
        }
        public int Create(float[] points, uint[] indexes, ShaderProgram shaderProg)
        {
            _vao = GL.GenVertexArray();
            Update(points,indexes,shaderProg);
            return _vao;
        }
        public void Update(float[] points, uint[] indexes,ShaderProgram shaderProg)
        {
            GL.BindVertexArray(_vao);
            _vboVC = VBO.Create(points);
            _eboVC = EBO.Create(indexes);
            int VertexArray = shaderProg.AttribLocation("aPosition");
            int ColorArray = shaderProg.AttribLocation("aColor");
            GL.EnableVertexAttribArray(VertexArray);
            GL.EnableVertexAttribArray(ColorArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vboVC);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _eboVC);
            GL.VertexAttribPointer(VertexArray, 3, VertexAttribPointerType.Float, false, 7 * sizeof(float), 0);
            GL.VertexAttribPointer(ColorArray, 4, VertexAttribPointerType.Float, false, 7 * sizeof(float), 3 * sizeof(float));
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DisableVertexAttribArray(VertexArray);
            GL.DisableVertexAttribArray(ColorArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
        public void DisposeBuffs()
        {
            GL.DeleteBuffer(_vboVC);
            GL.DeleteBuffer(_eboVC);
            GL.DeleteBuffer(_vao);
        }
        public static void Delete()
        {
            
            GL.BindVertexArray(0);
        }
        
    }
}
