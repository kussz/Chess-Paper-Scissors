
using System.Drawing;
using GameObjects;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Graphics
{
    public class Drawer
    {
        public static void Draw(int vaoInd, ShaderProgram shaderProg)
        {
            shaderProg.ActivateProgram();
            //shaderProg.SetUniform2("angle", new Vector2(angle, 0));
            GL.BindVertexArray(vaoInd);
            GL.DrawElements(PrimitiveType.Triangles, Board.GetBorderIndexes().Length, DrawElementsType.UnsignedInt, 0);
            shaderProg.DeactivateProgram();
        }
        public static void TranspUniforms(ShaderProgram shaderProg, int xSize, int ySize)
        {
            shaderProg.SetUniform2("u_CellPos", new Vector2(Board.GetCellPosition(Mouse.GetPosition()).X, Board.GetCellPosition(Mouse.GetPosition()).Y));
            shaderProg.SetUniform2("u_mouse", new Vector2(Mouse.GetPosition().X, Mouse.GetPosition().Y));
            shaderProg.SetUniform2("u_resolution", new Vector2(xSize,ySize));
        }
    }
}
