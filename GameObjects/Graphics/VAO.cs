using GameObjects;
using GameObjects.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Drawing;

namespace Graphics;

public class VAO
{
    public int Index { get; private set; }
    public int TextureIndex { get { return _TBO!.TextureIndex; } private set { _TBO!.TextureIndex = value; } }
    private VBO? _VBO = null;
    private EBO? _EBO = null;
    private TBO? _TBO = null;
    public int VertexLength { get; private set; }
    public VAO(float[] points, uint[] indexes)
    {
        Index = Create(points, indexes);
    }
    public VAO()
    {
        Index = GL.GenVertexArray();
    }
    public VAO(PieceModel model) : this(model.Points, model.Indexes, model.Texture, model.TextureCoords, model.TextureResolution) { }
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
        _VBO = new VBO(points);
        _EBO = new EBO(indexes);
        int VertexArray = 10;
        int ColorArray = 11;
        GL.EnableVertexAttribArray(VertexArray);
        GL.EnableVertexAttribArray(ColorArray);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _VBO.Index);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _EBO.Index);
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
        _VBO?.Update(points);
    }
    public void Create(float[] points, uint[] indexes, Vector3[] textureData, Vector2[] textureCoords, Point size)
    {
        VertexLength = points.Length;
        GL.BindVertexArray(Index);
        _VBO = new VBO(points);
        _EBO = new EBO(indexes);
        int VertexArray = 10;
        int TextureArray = 11;
        _TBO = new TBO(textureData, size, textureCoords);
        GL.EnableVertexAttribArray(VertexArray);
        GL.EnableVertexAttribArray(TextureArray);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _VBO.Index);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _EBO.Index);
        GL.VertexAttribPointer(VertexArray, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _TBO.CoordsIndex);
        GL.BindBuffer(BufferTarget.TextureBuffer, _TBO.TextureIndex);
        GL.VertexAttribPointer(TextureArray, 2, VertexAttribPointerType.Float, true, 0, 0);
        //GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, 0);
        GL.BindVertexArray(0);
        GL.DisableVertexAttribArray(VertexArray);
        GL.DisableVertexAttribArray(TextureArray);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }

    public static void Delete()
    {

        GL.BindVertexArray(0);
    }
    public static VAO? Get(IDrawable drawable)
    {
        switch (drawable.GetType().Name)
        {
            case "StrongRock":
            case "Rock":
                return Model.Rock.VAO;
            case "Paper":
                return Model.Paper.VAO;
            case "Scissor":
                return Model.Scissor.VAO;
            case "King":
                return Model.King.VAO;
            case "Crown":
                return Model.Crown.VAO;
            default:
                return null;
        }
    }

}
