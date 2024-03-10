
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
    }
}
