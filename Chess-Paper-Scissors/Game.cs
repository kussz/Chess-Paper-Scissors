using GameObjects;
using GameObjects.Decorators;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Windows;
using GameObjects.Graphics.Builders;
using GameObjects.Graphics.GraphicsObjects;
using GameObjects.Graphics.Drawing;
using GameObjects.Functional;
using GameObjects.Graphics.Models;

namespace Chess_Paper_Scissors;

class Game : GameWindow
{
    #region variables
    int fps = 0;
    float delayTime = 0;
    private Matrix4 mvpMatrix;
    private System.Drawing.Point[] avalPts;
    private ShaderProgram borderProg;
    private ShaderProgram boardProg;
    private ShaderProgram tileProg;
    private ShaderProgram pieceProg;
    private ShaderProgram cursorProg;
    private Piece? selectedPiece;
    private MainWindow _window;
    private bool turn = true;
    private TileBuilder tileBuilder;
    private BoardBuilder boardBuilder;
    #endregion
    public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, MainWindow window) : base(gameWindowSettings, nativeWindowSettings)
    {
        VSync = VSyncMode.Off;
        CursorState = CursorState.Grabbed;
        _window = window;
    }
    private static NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
    {
        Size = new Vector2i(1200, 1200),
        Location = new Vector2i(0, 0),
        WindowBorder = WindowBorder.Resizable,
        WindowState = OpenTK.Windowing.Common.WindowState.Normal,
        Title = "Chess-Paper-Scissors",
        Flags = ContextFlags.Default,
        Profile = ContextProfile.Compatability,
        APIVersion = new Version(4, 6),
        NumberOfSamples = 0,
    };

    public static NativeWindowSettings NWSettings()
    { return nativeWindowSettings; }

    protected override void OnLoad()
    {
        base.OnLoad();
        //Model.LoadModels();
        Mouse.Init(Size);
        Board.Init();
        boardBuilder = new BoardBuilder();
        tileBuilder = new TileBuilder(0.18f);
        GL.ClearColor(0.15f, 0.05f, 0.05f, 1);
        GL.LineWidth(1);
        GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
        GL.Enable(EnableCap.CullFace);
        GL.Enable(EnableCap.Blend);
        GL.Enable(EnableCap.Texture2D);
        GL.CullFace(CullFaceMode.Back);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        avalPts = [];
        borderProg = new ShaderProgram(@"data\Shader\Shader_base.vert", @"data\Shader\Shader_base1.frag");
        boardProg = new ShaderProgram(@"data\Shader\Shader_base.vert", @"data\Shader\Shader_board.frag");
        tileProg = new ShaderProgram(@"data\Shader\Objects.vert", @"data\Shader\Objects.frag");
        pieceProg = new ShaderProgram(@"data\Shader\piece.vert", @"data\Shader\piece.frag");
        cursorProg = new ShaderProgram(@"data\Shader\Cursor.vert", @"data\Shader\Cursor.frag");
        Drawer.SetResolution(Size);
        Console.WriteLine("Loaded");
    }
    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
        base.OnMouseDown(e);
        System.Drawing.Point point = Board.GetCellPosition(Mouse.GetNormalized(Size.X, Size.Y));
        Piece? foundPiece = FindPiece(point);
        if (foundPiece != null && foundPiece.Color == turn)
        {
            selectedPiece = foundPiece;
            avalPts = GetAvailablePoints(selectedPiece);
        }
        else if (selectedPiece != null)
        {
            if (Array.Exists(avalPts, element => element.X == point.X && element.Y == point.Y))
            {
                turn = !turn;
                if (foundPiece == null)
                {
                    MovePiece(selectedPiece, point);
                }
                else
                {
                    Impact(selectedPiece, foundPiece);
                }
                if (selectedPiece is Rock rock && rock.CheckAscension())
                {
                    Board.PieceList[Board.PieceList.FindIndex(match => match.CellPosition.X == point.X && match.CellPosition.Y == point.Y)] = new StrongRock(rock);
                }
                avalPts = [];
                SetBackgroundColor(turn);
            }
        }


    }
    private System.Drawing.Point[] GetAvailablePoints(Piece? piece)
    {
        System.Drawing.Point[] points;
        if (piece != null && piece.Color == turn)
        {
            points = piece.GetAvailableMoves();
        }
        else
            points = [];
        return points;
    }
    private Piece? FindPiece(System.Drawing.Point point)
    {
        return Board.PieceList.Find(match => match.CellPosition.X == point.X && match.CellPosition.Y == point.Y);
    }
    private void Impact(Piece firstPiece, Piece secondPiece)
    {
        if (firstPiece.IsHigher(secondPiece) == 1)
        {
            if (secondPiece is King)
            {
                End($"Победил {(secondPiece.Color ? "синий" : "красный")} игрок!");
            }
            char pieceO = Board.State[firstPiece.CellPosition.X, firstPiece.CellPosition.Y];
            Board.State[firstPiece.CellPosition.X, firstPiece.CellPosition.Y] = ' ';
            firstPiece.UpdatePosition(secondPiece.CellPosition);
            Board.State[secondPiece.CellPosition.X, secondPiece.CellPosition.Y] = pieceO;
            Board.PieceList.Remove(secondPiece);
        }
        else if (firstPiece.IsHigher(secondPiece) == -1)
        {
            Board.State[firstPiece.CellPosition.X, firstPiece.CellPosition.Y] = ' ';
            Board.PieceList.Remove(firstPiece);
        }
    }
    private void Restart()
    {
        turn = true;
        Mouse.Init(Size);
        Board.Init();
    }
    private void End(string winnerString)
    {
        MessageBoxResult result = MessageBox.Show(winnerString + "\nЖелаете сыграть ещё?", "Игра окончена!", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            Restart();
        }
        else
        {
            Close();
            _window.Close();
        }
    }
    private void MovePiece(Piece piece, System.Drawing.Point point)
    {
        char pieceO = Board.State[piece.CellPosition.X, piece.CellPosition.Y];
        Board.State[piece.CellPosition.X, piece.CellPosition.Y] = ' ';
        piece.UpdatePosition(point);
        Board.State[piece.CellPosition.X, piece.CellPosition.Y] = pieceO;
    }
    private void SetBackgroundColor(bool turn)
    {
        if (turn)
            GL.ClearColor(0.15f, 0.05f, 0.05f, 1);
        else
            GL.ClearColor(0.05f, 0.05f, 0.15f, 1);
    }
    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        int aspect = Math.Min(this.Size.X, this.Size.Y);
        GL.Viewport(0, 0, aspect, aspect);
        OnRenderFrame(new FrameEventArgs());
        Drawer.SetResolution(Size);
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        mvpMatrix = View.CountMVPMatrix(this.Size.X, this.Size.Y);
        fps += 1;
        delayTime += (float)e.Time;
        if (delayTime >= 1)
        {
            Title = $"Chess-Paper-Scissors - {(int)(fps)}";
            fps = 0;
            delayTime = 0;

        }
        Mouse.SetPosition(MouseState, this.Size.X, this.Size.Y);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        var key = KeyboardState;
        if (key.IsKeyDown(Keys.Escape))
        {
            MessageBoxResult result = MessageBox.Show("Игра будет завершена ничьей.", "Окончить игру?", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                End("Ничья!");
            }
        }
    }
    protected override void OnRenderFrame(FrameEventArgs e)
    {
        GL.Enable(EnableCap.DepthTest);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        borderProg.ActivateProgram();
        boardBuilder.Border.Draw(mvpMatrix);
        boardProg.ActivateProgram();
        boardBuilder.Board.Draw(mvpMatrix);
        pieceProg.ActivateProgram();
        foreach (Piece piece in Board.PieceList)
        {
            piece.Draw(mvpMatrix);
        }
        pieceProg.DeactivateProgram();
        tileProg.ActivateProgram();
        for (int i = 0; i < avalPts.Length; i++)
        {
            tileBuilder.SetPts(avalPts[i], avalPts[i] == Board.GetCellPosition(Mouse.GetNormalized(Size.X, Size.Y)));
            tileBuilder.Tile.Draw(mvpMatrix);
        }
        tileProg.DeactivateProgram();
        GL.Disable(EnableCap.DepthTest);
        cursorProg.ActivateProgram();
        boardBuilder.Cursor.Draw(mvpMatrix);
        cursorProg.DeactivateProgram();
        SwapBuffers();
        base.OnRenderFrame(e);
    }
    protected override void OnUnload()
    {
        base.OnUnload();
        cursorProg.DeleteProgram();
        tileProg.DeleteProgram();
        boardProg.DeleteProgram();
        borderProg.DeleteProgram();
        pieceProg.DeleteProgram();
        GL.DeleteVertexArrays(9, [Model.Crown.VAO.Index,
                                Model.King.VAO.Index,
                                Model.Rock.VAO.Index,
                                Model.Paper.VAO.Index,
                                Model.Scissor.VAO.Index,
                                tileBuilder.Tile.VAO.Index,
                                boardBuilder.Board.VAO.Index,
                                boardBuilder.Border.VAO.Index,
                                boardBuilder.Cursor.VAO.Index]);

        Dispose();
    }
}
