
using System.ComponentModel;
using System.Drawing;
using GameObjects;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Graphics
{
    public static class Drawer
    {
        public static void Draw(this ShaderProgram shaderProg, int xSize, int ySize,Matrix4 mvpMatrix)
        {
            int mvpMatrixLocation = shaderProg.UnifLocation("mvpMatrix");
            //shaderProg.SetUniform2("angle", new Vector2(angle, 0));
            GL.BindVertexArray(shaderProg.VAO.Index);
            Point point = Board.GetCellPosition(Mouse.GetNormalized(xSize, ySize));
            shaderProg.SetUniform2("u_CellPos", new Vector2(point.X, point.Y));
            shaderProg.SetUniform2("u_mouse", Mouse.GetNormalized(xSize, ySize));
            shaderProg.SetUniform2("u_resolution", new Vector2(xSize, ySize));
            GL.UniformMatrix4(mvpMatrixLocation, false, ref mvpMatrix);
            GL.DrawElements(PrimitiveType.Triangles, BoardDrawer.GetBorderIndexes().Length, DrawElementsType.UnsignedInt, 0);
            
        }
        public static void Draw(this ShaderProgram shaderProg, Matrix4 mvpMatrix,int length)
        {
            int mvpMatrixLocation = shaderProg.UnifLocation("mvpMatrix");
            GL.BindVertexArray(shaderProg.VAO.Index);
            GL.UniformMatrix4(mvpMatrixLocation, false, ref mvpMatrix);
            GL.DrawElements(PrimitiveType.Triangles, length, DrawElementsType.UnsignedInt, 0);
        }
        public static void Draw(this ShaderProgram shaderProg, Matrix4 mvpMatrix, Piece piece)
        {
            int mvpMatrixLocation = shaderProg.UnifLocation("mvpMatrix");
            int colorLocation = shaderProg.UnifLocation("pcColor");
            GL.Uniform1(colorLocation,Convert.ToSingle(piece.Color));
            GL.UniformMatrix4(mvpMatrixLocation, false, ref mvpMatrix);

            


            GL.BindBuffer(BufferTarget.ArrayBuffer, piece.TextureBufferID);
            int texLoc = shaderProg.AttribLocation("inTextureCoordinate");
            GL.EnableVertexAttribArray(texLoc);
            GL.VertexAttribPointer(texLoc, 2, VertexAttribPointerType.Float, false, 0, piece.TextureBufferID);

            GL.BindTexture(TextureTarget.Texture2D, piece.TextureID);
            GL.BindVertexArray(shaderProg.VAO.Index);
            GL.DrawElements(PrimitiveType.Triangles, piece.Indexes.Length, DrawElementsType.UnsignedInt, 0);
        }


    }
}
