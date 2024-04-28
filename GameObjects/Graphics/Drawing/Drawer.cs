using GameObjects;
using GameObjects.Decorators;
using GameObjects.Graphics.GraphicsObjects;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Drawing;

namespace GameObjects.Graphics.Drawing;

public static class Drawer
{
    private static int _xSize;
    private static int _ySize;
    public static void SetResolution(Vector2i size)
    {
        _xSize = size.X;
        _ySize = size.Y;
    }
    public static void Draw(this ShaderProgram drawable, Matrix4 mvpMatrix)
    {
        Point point = Board.GetCellPosition(Mouse.GetNormalized(_xSize, _ySize));
        GL.UniformMatrix4(0, false, ref mvpMatrix);
        GL.Uniform2(2, new Vector2(point.X, point.Y));
        GL.Uniform2(1, Mouse.GetNormalized(_xSize, _ySize));
        GL.BindVertexArray(drawable.VAO.Index);
        GL.DrawElements(PrimitiveType.Triangles, drawable.VAO.VertexLength, DrawElementsType.UnsignedInt, 0);
    }
    public static void Draw(this IDrawableStatic drawable, Matrix4 mvpMatrix)
    {
        Point point = Board.GetCellPosition(Mouse.GetNormalized(_xSize, _ySize));
        GL.UniformMatrix4(0, false, ref mvpMatrix);
        GL.Uniform2(2, new Vector2(point.X, point.Y));
        GL.Uniform2(1, Mouse.GetNormalized(_xSize, _ySize));
        GL.BindVertexArray(drawable.VAO.Index);
        GL.DrawElements(PrimitiveType.Triangles, drawable.VAO.VertexLength, DrawElementsType.UnsignedInt, 0);
    }
    public static void Draw(this IDrawableDynamic drawable, Matrix4 mvpMatrix)
    {
        Point point = Board.GetCellPosition(Mouse.GetNormalized(_xSize, _ySize));
        GL.UniformMatrix4(0, false, ref mvpMatrix);
        GL.Uniform2(2, new Vector2(point.X, point.Y));
        GL.Uniform2(1, Mouse.GetNormalized(_xSize, _ySize));
        VAO VAO;
        if (drawable is Piece piece)
        {
            GL.Uniform1(3, Convert.ToSingle(piece.Color));
            if (piece is StrongPieceDecorator stPiece && stPiece.Crown != null)
            {
                VAO = VAO.Get(stPiece.Crown)!;
                VAO.Update(stPiece.Crown.Points);
                GL.BindTexture(TextureTarget.Texture2D, VAO.TextureIndex);
                GL.BindVertexArray(VAO.Index);
                GL.DrawElements(PrimitiveType.Triangles, VAO.VertexLength, DrawElementsType.UnsignedInt, 0);
            }
        }
        VAO = VAO.Get(drawable)!;
        VAO.Update(drawable.Points);
        GL.BindTexture(TextureTarget.Texture2D, VAO.TextureIndex);
        GL.BindVertexArray(VAO.Index);
        GL.DrawElements(PrimitiveType.Triangles, VAO.VertexLength, DrawElementsType.UnsignedInt, 0);
    }


}
