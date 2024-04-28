using OpenTK.Mathematics;
using System.Drawing;

namespace GameObjects.Graphics.Builders;

public class TileBuilder
{
    public StaticDrawableDataContainer Tile { get; set; }
    private float _cellSize = 0.2f;
    private float _tileSize = 0.2f;
    public float[] InitialPoints { get; set; }
    private Vector3 _color;
    public uint[] Indexes { get; set; }
    public TileBuilder(float tileSize)
    {
        _tileSize = tileSize;
        _color = new Vector3();
        Indexes = [0, 1, 2, 0, 2, 3];
        InitialPoints = new float[28];
        Tile = new(InitialPoints, Indexes);

    }
    public void SetPts(Point point, bool selected)
    {
        float height;
        if (Board.State[point.X, point.Y] == ' ')
        {
            _color.X = 0;
            _color.Y = 1;
            _color.Z = 1;
        }
        else
        {
            _color.X = 1;
            _color.Y = 0;
            _color.Z = 0;
        }
        if (selected)
        {
            height = 0.02f;
            _color.X = _color.X / 2;
            _color.Y = _color.Y / 2;
            _color.Z = _color.Z / 2;
        }
        else
        {
            height = 0.001f;
        }
        Vector2 unnormed = Board.GetCellTopLeftPosition(point);
        InitialPoints[0] = unnormed.X + _tileSize;
        InitialPoints[1] = unnormed.Y + _tileSize;
        InitialPoints[2] = height;
        InitialPoints[3] = _color.X;
        InitialPoints[4] = _color.Y;
        InitialPoints[5] = _color.Z;
        InitialPoints[6] = 0.3f;
        InitialPoints[7] = unnormed.X + _cellSize - _tileSize;
        InitialPoints[8] = unnormed.Y + _tileSize;
        InitialPoints[9] = height;
        InitialPoints[10] = _color.X;
        InitialPoints[11] = _color.Y;
        InitialPoints[12] = _color.Z;
        InitialPoints[13] = 0.3f;
        InitialPoints[14] = unnormed.X + _cellSize - _tileSize;
        InitialPoints[15] = unnormed.Y + _cellSize - _tileSize;
        InitialPoints[16] = height;
        InitialPoints[17] = _color.X;
        InitialPoints[18] = _color.Y;
        InitialPoints[19] = _color.Z;
        InitialPoints[20] = 0.3f;
        InitialPoints[21] = unnormed.X + _tileSize;
        InitialPoints[22] = unnormed.Y + _cellSize - _tileSize;
        InitialPoints[23] = height;
        InitialPoints[24] = _color.X;
        InitialPoints[25] = _color.Y;
        InitialPoints[26] = _color.Z;
        InitialPoints[27] = 0.3f;
        Tile.VAO.Update(InitialPoints);
    }
}
