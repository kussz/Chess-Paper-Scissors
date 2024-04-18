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
using GameObjects;
using GameObjects.Graphics;

namespace Graphics
{
    public class VAO
    {
        public int Index { get; private set; }
        private int _vboVC;
        private int _eboVC;
        private int _vboTex;
        private int _vboTexCoords;
        public VAO(float[] points, uint[] indexes, ShaderProgram shaderProg)
        {
            Index = Create(points, indexes, shaderProg);
        }
        public VAO()
        {
            Index = GL.GenVertexArray();
        }
        public VAO(PieceModel model) : this(model.Points, model.Indexes,model.Texture,model.TextureCoords,model.TextureResolution) { }
        public VAO(float[] points, uint[] indexes, Vector3[] textureData, Vector2[] textureCoords, Point size)
        {
            Index = GL.GenVertexArray();
            Update(points, indexes, textureData, textureCoords, size);
        }
        public int Create(float[] points, uint[] indexes, ShaderProgram shaderProg)
        {
            Index = GL.GenVertexArray();
            Update(points,indexes,shaderProg);
            return Index;
        }
        public void Bind(ShaderProgram shaderProg)
        {
            int VertexArray = shaderProg.AttribLocation("aPosition");
            int ColorArray = shaderProg.AttribLocation("aColor");
            GL.EnableVertexAttribArray(VertexArray);
            GL.EnableVertexAttribArray(ColorArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vboVC);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _eboVC);
            GL.VertexAttribPointer(VertexArray, 3, VertexAttribPointerType.Float, false, 7 * sizeof(float), 0);
            GL.VertexAttribPointer(ColorArray, 4, VertexAttribPointerType.Float, false, 7 * sizeof(float), 3 * sizeof(float));
            GL.BindVertexArray(0);
            GL.DisableVertexAttribArray(VertexArray);
            GL.DisableVertexAttribArray(ColorArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
        public void Update(float[] points)
        {
            VBO.Update(points,_vboVC);
        }
        public void Update(float[] points, uint[] indexes)
        {
            VBO.Update(points, _vboVC);
            EBO.Update(indexes, _eboVC);
        }
        public void Update(float[] points, uint[] indexes,ShaderProgram shaderProg)
        {
            GL.BindVertexArray(Index);
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
            GL.DisableVertexAttribArray(VertexArray);
            GL.DisableVertexAttribArray(ColorArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
        public void Update(float[] points, uint[] indexes, Vector3[] textureData, Vector2[] textureCoords, Point size)
        {
            GL.BindVertexArray(Index);
            _vboVC = VBO.Create(points);
            _eboVC = EBO.Create(indexes);
            int VertexArray = 14;
            int TextureArray = 15;
            GL.EnableVertexAttribArray(VertexArray);
            GL.EnableVertexAttribArray(TextureArray);


            _vboTex = TBO.CreateImage(textureData, size);

            _vboTexCoords = TBO.CreateCoords(textureCoords);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vboVC);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _eboVC);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vboTexCoords);
            GL.BindTexture(TextureTarget.Texture2D, _vboTex);
            GL.VertexAttribPointer(VertexArray, 3, VertexAttribPointerType.Float, false, 7 * sizeof(float), 0);
            GL.VertexAttribPointer(TextureArray, 2, VertexAttribPointerType.Float, true, 0, 0);
            GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, 0);
            GL.BindVertexArray(0);
            GL.DisableVertexAttribArray(VertexArray);
            GL.DisableVertexAttribArray(TextureArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
        public void Update(float[] points, ShaderProgram shaderProg)
        {
            GL.BindVertexArray(Index);
            _vboVC = VBO.Create(points);
            int VertexArray = shaderProg.AttribLocation("aPosition");
            int ColorArray = shaderProg.AttribLocation("aColor");
            GL.EnableVertexAttribArray(VertexArray);
            GL.EnableVertexAttribArray(ColorArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vboVC);
            GL.VertexAttribPointer(VertexArray, 3, VertexAttribPointerType.Float, false, 7 * sizeof(float), 0);
            GL.VertexAttribPointer(ColorArray, 4, VertexAttribPointerType.Float, false, 7 * sizeof(float), 3 * sizeof(float));
            GL.BindVertexArray(0);
            GL.DisableVertexAttribArray(VertexArray);
            GL.DisableVertexAttribArray(ColorArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
        public void DisposeBuffs()
        {
            GL.DeleteBuffer(_vboVC);
            GL.DeleteBuffer(_eboVC);
            GL.DeleteBuffer(Index);
        }
        public static void Delete()
        {
            
            GL.BindVertexArray(0);
        }
        
    }
}
