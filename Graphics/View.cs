using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GameObjects;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Graphics
{
    public class View
    {
        private static Matrix4 _modelMatrix;
        private static Vector4 _startEye;
        private static Vector4 _viewTarget;
        private static float _cameraPositionDistanceFromOrigin;
        private static Vector4 _viewYUp;
        private static Matrix4 _projectionMatrix;
        private static float _fovY;
        private static float _aspectRatio;
        private static float _nearDistance;
        private static float _farDistance;
        static View()
        {
            _modelMatrix = Matrix4.Identity;
            _startEye = new Vector4(0, 0, 0, 1f);
            _viewTarget = new Vector4(0, 0, 0, 1f);
            _cameraPositionDistanceFromOrigin = 5f;
            _viewYUp = new Vector4(0, 1f, 0, 1f);
            _fovY = (float)Math.PI / 3;
            _aspectRatio = 1;
            _nearDistance = 0.1f;
            _farDistance = 100f;
            _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(_fovY, _aspectRatio, _nearDistance, _farDistance);
        }
        public static Matrix4 CountMVPMatrix(int xSize,int ySize)
        {
            Vector4 eye = _startEye;
            eye.Z = _cameraPositionDistanceFromOrigin;
            Matrix4 viewMatrix =
                Matrix4.CreateRotationX((float)(Mouse.GetPosition().Y / ySize * 5)) *
                Matrix4.CreateRotationY((float)(Mouse.GetPosition().X / xSize * 5)) *
                Matrix4.LookAt(eye.X, eye.Y, eye.Z,
                _viewTarget.X, _viewTarget.Y, _viewTarget.Z,
                _viewYUp.X, _viewYUp.Y, _viewYUp.Z);
            return( _modelMatrix * viewMatrix * _projectionMatrix);
        }
    }
}
