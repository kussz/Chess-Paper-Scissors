
namespace GameObjects.Graphics.Builders;

public class BoardBuilder
{
    public StaticDrawableDataContainer Board { get; set; } = new(_points, _boardIndexes);
    public StaticDrawableDataContainer Border { get; set; } = new(_points, _borderIndexes);
    public StaticDrawableDataContainer Cursor { get; set; } = new(_points, _cursorIndexes);

    private static float[] _points = new float[]
    {
        -0.9f,0.9f,0.07f,   0.9f,0.8f,0.8f,1,
        -0.8f,0.8f,0.07f,   0.9f,0.8f,0.8f,1,
        0.9f,0.9f,0.07f,    0.9f,0.8f,0.8f,1,
        0.8f,0.8f,0.07f,    0.9f,0.8f,0.8f,1,
        0.9f,-0.9f,0.07f,   0.9f,0.8f,0.8f,1,
        0.8f,-0.8f,0.07f,   0.9f,0.8f,0.8f,1,
        -0.9f,-0.9f,0.07f,  0.9f,0.8f,0.8f,1,
        -0.8f,-0.8f,0.07f,  0.9f,0.8f,0.8f,1,
        //8
        -0.8f,0.8f,0,      0f,0f,0f,1,
        0.8f,0.8f,0,       0f,0f,0f,1,
        0.8f,-0.8f,0,      0f,0f,0f,1,
        -0.8f,-0.8f,0,     0f,0f,0f,1,
        //12
        -0.9f,0.9f,0,      0.9f,0.8f,0.8f,1,
        0.9f,0.9f,0,       0.9f,0.8f,0.8f,1,
        0.9f,-0.9f,0,      0.9f,0.8f,0.8f,1,
        -0.9f,-0.9f,0,     0.9f,0.8f,0.8f,1,
    };
    private static uint[] _borderIndexes = new uint[]
    {
        8,9,1,
        1,9,3,
        9,10,3,
        3,10,5,
        10,11,5,
        11,7,5,
        11,8,7,
        8,1,7,

        13,12,0,
        0,2,13,
        14,13,2,
        14,2,4,
        15,14,4,
        15,4,6,
        12,15,6,
        12,6,0,

        0,1,2,
        2,1,3,
        2,3,4,
        4,3,5,
        4,5,6,
        6,5,7,
        6,7,0,
        7,1,0,
    };
    private static uint[] _boardIndexes = new uint[]
    {
        9,8,10,
        8,11,10
    };
    private static uint[] _cursorIndexes = new uint[]
    {
        13,12,14,
        12,15,14
    };
    public static float[] GetVertices()
    {
        return _points;
    }
    public static uint[] GetBorderIndexes()
    {
        return _borderIndexes;
    }
    public static uint[] GetCursorIndexes()
    {
        return _cursorIndexes;
    }
    public static uint[] GetIndexes()
    {
        return _boardIndexes;
    }
}
