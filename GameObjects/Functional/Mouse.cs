﻿using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing;
using OpenTK.Mathematics; 

namespace GameObjects
{
    public class Mouse
    {
        private static bool _locked=true;
        static Point mouse;
        public static void Init(Vector2i resolustion)
        {
            mouse.X=resolustion.X/2; mouse.Y=resolustion.Y/2;
        }
        public static void SetPosition(MouseState o,int xSize, int ySize)
        {
            int x = (int)((o.X - o.PreviousX) * WS.Sensivity);
            int y = (int)((o.Y - o.PreviousY) * WS.Sensivity);
            mouse.X += x;
            mouse.Y += y;
            if(_locked)
            {
            mouse.X = Math.Max(Math.Min((int)(xSize * WS.Xmax), mouse.X), (int)(xSize * WS.Xmin));
            mouse.Y = Math.Max(Math.Min((int)(ySize * WS.Ymax), mouse.Y), (int)(ySize * WS.Ymin));
            }
        }
        public static Vector2 GetPosition()
        {
            return new Vector2(mouse.X, mouse.Y);
        }
        public static Vector2 GetNormalized(int xSize, int ySize)
        {
            float x = (GetPosition().X - xSize / 2) / xSize;
            float y = (GetPosition().Y - ySize / 2) / ySize;
            return new Vector2(x, y);
        }
    }
}