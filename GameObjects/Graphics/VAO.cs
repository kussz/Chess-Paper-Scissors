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
        public int TextureIndex {  get; private set; }
        private int _vboTexCoords;
        public int VertexLength {  get; private set; }
        public VAO(float[] points, uint[] indexes)
        {
            Index = Create(points, indexes);
        }
        public VAO()
        {
            Index = GL.GenVertexArray();
        }
        public VAO(PieceModel model) : this(model.Points, model.Indexes,model.Texture,model.TextureCoords,model.TextureResolution) { }
        public VAO(float[] points, uint[] indexes, Vector3[] textureData, Vector2[] textureCoords, Point size)
        {
            Index = GL.GenVertexArray();
            Create(points, indexes, textureData, textureCoords, size);
        }
        public int Create(float[] points, uint[] indexes)
        {
            Index = GL.GenVertexArray();
            VertexLength = points.Length;
            GL.BindVertexArray(Index);
            _vboVC = VBO.Create(points);
            _eboVC = EBO.Create(indexes);
            int VertexArray = 10;
            int ColorArray = 11;
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
            return Index;
        }
        public void Update(float[] points)
        {
            VertexLength = points.Length;
            VBO.Update(points,_vboVC);
        }
        public void Create(float[] points, uint[] indexes, Vector3[] textureData, Vector2[] textureCoords, Point size)
        {
            VertexLength = points.Length;
            GL.BindVertexArray(Index);
            _vboVC = VBO.Create(points);
            _eboVC = EBO.Create(indexes);
            int VertexArray = 10;
            int TextureArray = 11;
            TextureIndex = TBO.CreateImage(textureData, size);
            _vboTexCoords = TBO.CreateCoords(textureCoords);

            GL.EnableVertexAttribArray(VertexArray);
            GL.EnableVertexAttribArray(TextureArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vboVC);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _eboVC);
            GL.VertexAttribPointer(VertexArray, 3, VertexAttribPointerType.Float, false, 7 * sizeof(float), 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vboTexCoords);
            GL.BindBuffer(BufferTarget.TextureBuffer, TextureIndex);
            GL.VertexAttribPointer(TextureArray, 2, VertexAttribPointerType.Float, true, 0, 0);
            //GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, 0);
            GL.BindVertexArray(0);
            GL.DisableVertexAttribArray(VertexArray);
            GL.DisableVertexAttribArray(TextureArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
        public void Init(float[] points)
        {
            VertexLength = points.Length;
            GL.BindVertexArray(Index);
            _vboVC = VBO.Create(points);
            int VertexArray = 10;
            GL.EnableVertexAttribArray(VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vboVC);
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, _eboVC);
            GL.VertexAttribPointer(VertexArray, 3, VertexAttribPointerType.Float, false, 7 * sizeof(float), 0);
            GL.BindVertexArray(0);
            GL.DisableVertexAttribArray(VertexArray);
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
        public static VAO Get(IDrawable drawable)
        {
            switch (drawable.GetType().Name)
            {
                case "StrongRock":
                case "Rock":
                    return Model.RockModel.VAO;
                case "Paper":
                    return Model.PaperModel.VAO;
                case "Scissor":
                    return Model.ScissorModel.VAO;
                case "King":
                    return Model.KingModel.VAO;
                case "Crown":
                    return Model.CrownModel.VAO;
                default:
                    return new VAO();
            }
        }
        
    }
}
