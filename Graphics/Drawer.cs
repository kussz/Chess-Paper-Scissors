
using System.Drawing;
using GameObjects;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Graphics
{
    public class Drawer
    {
        public static void Draw(int vaoInd, ShaderProgram shaderProg, int xSize, int ySize)
        {
            shaderProg.ActivateProgram();
            //shaderProg.SetUniform2("angle", new Vector2(angle, 0));
            shaderProg.SetUniform2("u_resolution", new Vector2(xSize, ySize));
            shaderProg.SetUniform2("u_mouse", Mouse.GetNormalized(xSize,ySize));
            Point point = Board.GetCellPosition(Mouse.GetNormalized(xSize, ySize));
            shaderProg.SetUniform2("u_CellPos", new Vector2(point.X, point.Y));
            GL.BindVertexArray(vaoInd);
            GL.DrawElements(PrimitiveType.Triangles, Board.GetBorderIndexes().Length, DrawElementsType.UnsignedInt, 0);
            shaderProg.DeactivateProgram();
        }
    }
}
