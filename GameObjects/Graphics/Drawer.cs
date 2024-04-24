
using System.ComponentModel;
using System.Drawing;
using GameObjects;
using GameObjects.Decorators;
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
        public static void Draw(this Piece drawable, Matrix4 mvpMatrix, ShaderProgram shaderProg)
        {
            int mvpMatrixLocation = shaderProg.UnifLocation("mvpMatrix");
            VAO VAO;
            GL.UniformMatrix4(mvpMatrixLocation, false, ref mvpMatrix);
            if (drawable is Piece piece)
            {
                int colorLocation = shaderProg.UnifLocation("pcColor");
                GL.Uniform1(colorLocation,Convert.ToSingle(piece.Color));
                if(piece is StrongPiece stPiece && stPiece.Crown!=null)
                {
                    VAO = VAO.Get(stPiece.Crown);
                    VAO.Update(stPiece.GetCrownArray());
                    GL.BindTexture(TextureTarget.Texture2D, VAO.TextureIndex);
                    GL.BindVertexArray(VAO.Index);
                    GL.DrawElements(PrimitiveType.Triangles, VAO.VertexLength, DrawElementsType.UnsignedInt, 0);
                }
            }
            VAO = VAO.Get(drawable);
            VAO.Update(drawable.GetVertexArray(drawable.Points));
            GL.BindTexture(TextureTarget.Texture2D, VAO.TextureIndex);
            GL.BindVertexArray(VAO.Index);
            GL.DrawElements(PrimitiveType.Triangles, VAO.VertexLength, DrawElementsType.UnsignedInt, 0);
        }


    }
}
