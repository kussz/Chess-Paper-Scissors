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
using System.Windows.Media.Imaging;

namespace Chess_Paper_Scissors
{
    class Game : GameWindow
    {
        #region variables
        ShaderProgram borderProgram;
        ShaderProgram boardProgram;
        VAO vaoBoard;
        VAO vaoBorder;
        int fps = 0;
        float delayTime = 0;
        Point mouse = new Point();
        #endregion
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            Console.WriteLine("Start");
            Console.WriteLine(GL.GetString(StringName.Version));
            Console.WriteLine(GL.GetString(StringName.Vendor));
            Console.WriteLine(GL.GetString(StringName.Extensions));
            Console.WriteLine(GL.GetString(StringName.Renderer));
            Console.WriteLine(GL.GetString(StringName.ShadingLanguageVersion));
            VSync = VSyncMode.On;
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
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1);
            GL.LineWidth(1);
            GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
            GL.PolygonMode(MaterialFace.Back, PolygonMode.Point);
            borderProgram = new ShaderProgram(@"data\Shader\Shader_base.vert", @"data\Shader\Shader_base1.frag");
            boardProgram = new ShaderProgram(@"data\Shader\Shader_base.vert", @"data\Shader\Shader_board.frag");
            vaoBorder = new VAO(Board.GetVertices(), Board.GetBorderIndexes(), borderProgram);
            vaoBoard = new VAO(Board.GetVertices(), Board.GetIndexes(), boardProgram);
            Console.WriteLine("Loaded");
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

            fps += 1;
            delayTime += (float)e.Time;
            if (delayTime >= 1)
            {
                Title = $"Chess-Paper-Scissors - {(int)(fps)}";
                fps = 0;
                delayTime = 0;

            }
            SetMousePosition();
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
            Draw(vaoBoard.Index, boardProgram);
            Draw(vaoBorder.Index, borderProgram);
            SwapBuffers();
            base.OnRenderFrame(e);
        }
        protected override void OnUnload()
        {
            //DeleteDisplayList(0);
            VAO.Delete();
            borderProgram.DeleteProgram();
            boardProgram.DeleteProgram();
            base.OnUnload();
        }

        void SetMousePosition()
        {
            int x = (int)((MouseState.X - MouseState.PreviousX) * SV.Sensivity);
            int y = (int)((MouseState.Y - MouseState.PreviousY) * SV.Sensivity);
            mouse.X += x;
            mouse.Y += y;
            mouse.X = Math.Max(Math.Min((int)(this.Size.X * SV.Xmax), mouse.X), (int)(this.Size.X * SV.Xmin));
            mouse.Y = Math.Max(Math.Min((int)(this.Size.Y * SV.Ymax), mouse.Y), (int)(this.Size.Y * SV.Ymin));
        }
        Vector2 GetMousePosition()
        {
            return new Vector2(mouse.X, mouse.Y);
        }
        Vector2 GetMouseNormalized()
        {
            float x = (GetMousePosition().X - this.Size.X / 2) / this.Size.X;
            float y = (GetMousePosition().Y - this.Size.Y / 2) / this.Size.Y;
            return new Vector2(x, y);
        }
        Point GetCellPosition()
        {
            int x = (int)((GetMouseNormalized().X + 0.45f) * 10);
            int y = (int)((GetMouseNormalized().Y + 0.35f) * 10);
            return new Point(x, y);
        }


        public void Draw(int vaoInd, ShaderProgram shaderProg)
        {
            shaderProg.ActivateProgram();
            //shaderProg.SetUniform2("angle", new Vector2(angle, 0));
            shaderProg.SetUniformDouble("u_time", delayTime);
            shaderProg.SetUniform2("u_resolution", new Vector2(this.Size.X, this.Size.Y));
            shaderProg.SetUniform2("u_mouse", GetMouseNormalized());
            shaderProg.SetUniform2("u_CellPos", new Vector2(GetCellPosition().X, GetCellPosition().Y));
            GL.BindVertexArray(vaoInd);
            GL.DrawElements(PrimitiveType.Triangles, Board.GetBorderIndexes().Length, DrawElementsType.UnsignedInt, 0);
            shaderProg.DeactivateProgram();
        }
    }
}
