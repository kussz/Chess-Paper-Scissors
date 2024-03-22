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
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1);
            GL.LineWidth(1);
            GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha,BlendingFactor.OneMinusSrcAlpha);
            avalPts = [];
            shaderProgs =
            [
                new ShaderProgram(@"data\Shader\Shader_base.vert", @"data\Shader\Shader_base1.frag"),
                new ShaderProgram(@"data\Shader\Shader_base.vert", @"data\Shader\Shader_board.frag")
            ];
            tileProg = new ShaderProgram(@"data\Shader\pathSquare.vert", @"data\Shader\Shader_base1.frag");
            tileProg.VAO = new VAO(TileDrawer.Points, TileDrawer.Indexes, tileProg);
            shaderProgs[0].VAO = new VAO(BoardDrawer.GetVertices(), BoardDrawer.GetBorderIndexes(), shaderProgs[0]);
            shaderProgs[1].VAO = new VAO(BoardDrawer.GetVertices(), BoardDrawer.GetIndexes(), shaderProgs[1]);
            Console.WriteLine("Loaded");
            
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            System.Drawing.Point point = Board.GetCellPosition(Mouse.GetNormalized(Size.X, Size.Y));
            Rock rock = new Rock(point);
            avalPts = rock.GetAvailableMoves();
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
            var key = KeyboardState;
            if (key.IsKeyDown(Keys.Escape))
            {
                Console.WriteLine("Closed (Esc)");
                Close();
            }
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            
            //Drawer.TranspUniforms(boardProgram, Size.X,Size.Y);
            //Drawer.TranspUniforms(borderProgram, Size.X, Size.Y);
            foreach(var prog in shaderProgs)
            {
                prog.ActivateProgram();
                Drawer.Draw(prog,Size.X,Size.Y,mvpMatrix);
                prog.DeactivateProgram();
            }
            tileProg.ActivateProgram();
            for(int i=0;i<avalPts.Length;i++)
            {
                TileDrawer.SetPts(avalPts[i]);
                tileProg.VAO.Update(TileDrawer.Points,TileDrawer.Indexes, tileProg);
                Drawer.Draw(tileProg, mvpMatrix);
                tileProg.VAO.DisposeBuffs();
                
            }
            tileProg.DeactivateProgram();
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
