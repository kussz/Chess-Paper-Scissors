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
        private Piece selectedPiece;
        private bool turn = true;
        private float[] model = Model.Make("D:\\work\\Course 2\\Term 2\\Курсовая\\Программа\\Graphics\\data\\Model\\figure.obj");
        #endregion
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            Console.WriteLine("Start");
            Console.WriteLine(GL.GetString(StringName.Version));
            Console.WriteLine(GL.GetString(StringName.Vendor));
            Console.WriteLine(GL.GetString(StringName.Extensions));
            Console.WriteLine(GL.GetString(StringName.Renderer));
            Console.WriteLine(GL.GetString(StringName.ShadingLanguageVersion));
            VSync = VSyncMode.Off;
            CursorState = CursorState.Grabbed;
        }
        private static NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
        {
            Size = new Vector2i(1000, 1000),
            Location = new Vector2i(0, 0),
            WindowBorder = WindowBorder.Resizable,
            WindowState = WindowState.Normal,
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

            
            GL.Enable(EnableCap.DepthTest);
            //GL.DepthFunc(DepthFunction.Lequal);
            

            GL.CullFace(CullFaceMode.Back);
            GL.BlendFunc(BlendingFactor.SrcAlpha,BlendingFactor.OneMinusSrcAlpha);
            avalPts = [];
            shaderProgs =
            [
                new ShaderProgram(@"data\Shader\Shader_base.vert", @"data\Shader\Shader_base1.frag"),
                new ShaderProgram(@"data\Shader\Shader_base.vert", @"data\Shader\Shader_board.frag")
            ];
            tileProg = new ShaderProgram(@"data\Shader\Objects.vert", @"data\Shader\Objects.frag");
            tileProg.VAO = new VAO(TileDrawer.Points, TileDrawer.Indexes, tileProg);
            shaderProgs[0].VAO = new VAO(BoardDrawer.GetVertices(), BoardDrawer.GetBorderIndexes(), shaderProgs[0]);
            shaderProgs[1].VAO = new VAO(BoardDrawer.GetVertices(), BoardDrawer.GetIndexes(), shaderProgs[1]);
            Console.WriteLine("Loaded");
            
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Point point = Board.GetCellPosition(Mouse.GetNormalized(Size.X, Size.Y));
            if(selectedPiece != null)
            {
                if (Array.Exists(avalPts,element => element.X ==point.X&&element.Y==point.Y))
                {
                    turn = !turn;
                    Piece found = Board.pieceList.Find(match => match.CellPosition.X == point.X && match.CellPosition.Y == point.Y);
                    if (found != null)
                    {
                        if (selectedPiece.IsHigher(found) == 1)
                        {
                            Board.pieceList.Remove(found);
                            char pieceO = Board.State[selectedPiece.CellPosition.X, selectedPiece.CellPosition.Y];
                            Board.State[selectedPiece.CellPosition.X, selectedPiece.CellPosition.Y] = ' ';
                            selectedPiece.SetPosition(point);
                            Board.State[found.CellPosition.X, found.CellPosition.Y] = pieceO;
                        }
                        else if (selectedPiece.IsHigher(found) == -1)
                        {
                            Board.State[selectedPiece.CellPosition.X, selectedPiece.CellPosition.Y] = ' ';
                            Board.pieceList.Remove(selectedPiece);
                            point = new Point(-1, -1);
                        }
                    }
                    else
                    {
                        char pieceO = Board.State[selectedPiece.CellPosition.X, selectedPiece.CellPosition.Y];
                        Board.State[selectedPiece.CellPosition.X, selectedPiece.CellPosition.Y] = ' ';
                        selectedPiece.SetPosition(point);
                        Board.State[selectedPiece.CellPosition.X, selectedPiece.CellPosition.Y] = pieceO;
                    }
                    if (turn)
                        GL.ClearColor(0.15f, 0.05f, 0.05f, 1);
                    else
                        GL.ClearColor(0.05f, 0.05f, 0.15f, 1);
                }
            }
            selectedPiece = Board.pieceList.Find(match => match.CellPosition.X == point.X && match.CellPosition.Y == point.Y);
            if (selectedPiece != null && selectedPiece.Color == turn)
            {
                avalPts = selectedPiece.GetAvailableMoves();
            }
            else
                avalPts = [];
        }
        
        protected override void OnResize(ResizeEventArgs e)
        {
            int aspect = Math.Max(this.Size.X, this.Size.Y);
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
                Console.WriteLine("Closed (Esc)");
                Close();
            }
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.DepthMask(true);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            foreach (var prog in shaderProgs)
            {
                prog.ActivateProgram();
                Drawer.Draw(prog,Size.X,Size.Y,mvpMatrix);
                prog.DeactivateProgram();
            }
            tileProg.ActivateProgram();
            foreach (Piece piece in Board.pieceList)
            {
                tileProg.VAO.Update(piece.GetVertexArray(), piece.Indexes, tileProg);
                Drawer.Draw(tileProg, mvpMatrix, piece.Indexes.Length);
                tileProg.VAO.DisposeBuffs();

            }
            for(int i=0;i<avalPts.Length;i++)
            {
                TileDrawer.SetPts(avalPts[i]);
                tileProg.VAO.Update(TileDrawer.Points,TileDrawer.Indexes, tileProg);
                Drawer.Draw(tileProg, mvpMatrix, TileDrawer.Indexes.Length);
                tileProg.VAO.DisposeBuffs();
                
            }
            tileProg.VAO.Update(model, Model.GetIndexes(), tileProg);
            Drawer.Draw(tileProg, mvpMatrix, model.Length);
            tileProg.VAO.DisposeBuffs();
            tileProg.DeactivateProgram();
            GL.DepthMask(false);
            


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
        }

        
        



        
    }
}
