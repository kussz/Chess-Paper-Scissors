using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System.Drawing;
using OpenTK.Windowing.Common.Input;
using Graphics;
using GameObjects;
using System.Windows.Media.Imaging;
using OpenTK.Compute.OpenCL;
using System.Windows;
using System.IO;
using System.Reflection.PortableExecutable;
using GameObjects.Decorators;
using static System.Net.Mime.MediaTypeNames;

namespace Chess_Paper_Scissors
{
    class Game : GameWindow
    {
        #region variables
        int fps = 0;
        float delayTime = 0;
        private Matrix4 mvpMatrix;
        private System.Drawing.Point[] avalPts;
        private List<ShaderProgram> shaderProgs;
        private ShaderProgram tileProg;
        private ShaderProgram pieceProg;
        private ShaderProgram cursorProg;
        private Piece? selectedPiece;
        private MainWindow _window;
        private bool turn = true;
        #endregion
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, MainWindow window) : base(gameWindowSettings, nativeWindowSettings)
        {
            Console.WriteLine("Start");
            Console.WriteLine(GL.GetString(StringName.Version));
            Console.WriteLine(GL.GetString(StringName.Vendor));
            Console.WriteLine(GL.GetString(StringName.Extensions));
            Console.WriteLine(GL.GetString(StringName.Renderer));
            Console.WriteLine(GL.GetString(StringName.ShadingLanguageVersion));
            VSync = VSyncMode.Off;
            CursorState = CursorState.Grabbed;
            _window = window;
            Mouse.Init(Size);
            Board.Init();
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
            GL.ClearColor(0.15f, 0.05f, 0.05f, 1);
            GL.LineWidth(1);
            GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);
            GL.CullFace(CullFaceMode.Back);
            GL.BlendFunc(BlendingFactor.SrcAlpha,BlendingFactor.OneMinusSrcAlpha);
            avalPts = [];
            shaderProgs =
            [
                new ShaderProgram(@"data\Shader\Shader_base.vert", @"data\Shader\Shader_base1.frag"),
                new ShaderProgram(@"data\Shader\Shader_base.vert", @"data\Shader\Shader_board.frag")
            ];
            tileProg = new ShaderProgram(@"data\Shader\Objects.vert", @"data\Shader\Objects.frag");
            tileProg.VAO = new VAO(TileDrawer.Points, TileDrawer.Indexes);
            shaderProgs[0].VAO = new VAO(BoardVertices.GetVertices(), BoardVertices.GetBorderIndexes());
            shaderProgs[1].VAO = new VAO(BoardVertices.GetVertices(), BoardVertices.GetIndexes());
            pieceProg = new ShaderProgram("data\\Shader\\piece.vert", "data\\Shader\\piece.frag");
            cursorProg = new ShaderProgram("data\\Shader\\Cursor.vert", "data\\Shader\\Cursor.frag");
            cursorProg.VAO = new VAO(BoardVertices.GetVertices(),BoardVertices.GetCursorIndexes());
            Console.WriteLine("Loaded");
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            System.Drawing.Point point = Board.GetCellPosition(Mouse.GetNormalized(Size.X, Size.Y));
            Piece? foundPiece = FindPiece(point);
            if (foundPiece != null && foundPiece.Color==turn)
            {
                selectedPiece = foundPiece;
                avalPts = GetAvailablePoints(selectedPiece);
            }
            else if(selectedPiece != null)
            {
                if (Array.Exists(avalPts,element => element.X ==point.X&&element.Y==point.Y))
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
                firstPiece.SetPosition(secondPiece.CellPosition);
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
            MessageBoxResult result = MessageBox.Show(winnerString+"\nЖелаете сыграть ещё?", "Игра окончена!", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Restart();
            }
            else
            {
                Close();
                _window.Show();
            }
        }
        private void MovePiece(Piece piece, System.Drawing.Point point)
        {
            char pieceO = Board.State[piece.CellPosition.X, piece.CellPosition.Y];
            Board.State[piece.CellPosition.X, piece.CellPosition.Y] = ' ';
            piece.SetPosition(point);
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
            int aspect = Math.Min(this.Size.X, this.Size.Y);
            GL.Viewport(0, 0, aspect,aspect);
            OnRenderFrame(new FrameEventArgs());
            base.OnResize(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            mvpMatrix = View.CountMVPMatrix(this.Size.X,this.Size.Y);
            fps += 1;
            delayTime += (float)e.Time;
            if (delayTime >= 1)
            {
                Title = $"Chess-Paper-Scissors - {(int)(fps)}";
                fps = 0;
                delayTime = 0;

            }
            Mouse.SetPosition(MouseState,this.Size.X,this.Size.Y);
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
            Drawer.SetUnifs(Size.X, Size.Y, mvpMatrix);
            foreach (var prog in shaderProgs)
            {
                prog.ActivateProgram();
                prog.Draw();
                prog.DeactivateProgram();
            }
            pieceProg.ActivateProgram();
            foreach (Piece piece in Board.PieceList)
            {
                piece.Draw(pieceProg);
            }

            pieceProg.DeactivateProgram();
            tileProg.ActivateProgram();
            for(int i=0;i<avalPts.Length;i++)
            {
                TileDrawer.SetPts(avalPts[i]);
                tileProg.VAO.Update(TileDrawer.Points);
                tileProg.Draw();
            }
            tileProg.DeactivateProgram();
            GL.Disable(EnableCap.DepthTest);
            cursorProg.ActivateProgram();
            //cursorProg.VAO.Bind(cursorProg);
            cursorProg.Draw();
            cursorProg.DeactivateProgram();
            SwapBuffers();
            base.OnRenderFrame(e);
        }
        protected override void OnUnload()
        {
            //DeleteDisplayList(0);
            VAO.Delete();
            foreach (var prog in shaderProgs)
            {
                prog.DeleteProgram();
            }
            base.OnUnload();
            Dispose();
        }

        
        



        
    }
}
