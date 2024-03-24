
using System.ComponentModel;
using System.Drawing;
using GameObjects;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Graphics
{
    public class Drawer
    {
        public static void Draw(ShaderProgram shaderProg, int xSize, int ySize,Matrix4 mvpMatrix)
        {
            int mvpMatrixLocation = shaderProg.UnifLocation("mvpMatrix");
            //shaderProg.SetUniform2("angle", new Vector2(angle, 0));
            GL.BindVertexArray(shaderProg.VAO.Index);
            Point point = Board.GetCellPosition(Mouse.GetNormalized(xSize, ySize));
            shaderProg.SetUniform2("u_CellPos", new Vector2(point.X, point.Y));
            shaderProg.SetUniform2("u_mouse", Mouse.GetNormalized(xSize, ySize));
            shaderProg.SetUniform2("u_resolution", new Vector2(xSize, ySize));
            GL.UniformMatrix4(mvpMatrixLocation, false, ref mvpMatrix);
            shaderProg.SetUniform2("u_resolution", new Vector2(xSize, ySize));
            shaderProg.SetUniform2("u_mouse", Mouse.GetNormalized(xSize, ySize));
            GL.DrawElements(PrimitiveType.Triangles, BoardDrawer.GetBorderIndexes().Length, DrawElementsType.UnsignedInt, 0);
            
        }
        public static void Draw(ShaderProgram shaderProg, Matrix4 mvpMatrix,int length)
        {
            int mvpMatrixLocation = shaderProg.UnifLocation("mvpMatrix");
            GL.BindVertexArray(shaderProg.VAO.Index);
            GL.UniformMatrix4(mvpMatrixLocation, false, ref mvpMatrix);
            GL.DrawElements(PrimitiveType.Triangles, length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
