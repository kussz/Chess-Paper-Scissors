using GameObjects;
using OpenTK.Mathematics;

namespace Graphics;

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
        _startEye = new Vector4(0, 0f, 0, 1f);
        _viewTarget = new Vector4(0, 0, 0, 1f);
        _cameraPositionDistanceFromOrigin = 2f;
        _viewYUp = new Vector4(0, 1f, 0, 1f);
        _fovY = (float)Math.PI / 3;
        _aspectRatio = 1;
        _nearDistance = 0.1f;
        _farDistance = 10f;
        _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(_fovY, _aspectRatio, _nearDistance, _farDistance);
    }
    public static Matrix4 CountMVPMatrix(int xSize,int ySize)
    {
        Vector4 eye = _startEye;
        eye.Z = _cameraPositionDistanceFromOrigin;
        Matrix4 viewMatrix =
            Matrix4.CreateRotationX((float)((Mouse.GetPosition().Y - ySize / 2) / ySize /2 - 0.1f)) *
            Matrix4.CreateRotationY((float)((Mouse.GetPosition().X - xSize / 2) / xSize /2)) *
            Matrix4.LookAt(eye.Xyz, _viewTarget.Xyz, _viewYUp.Xyz);
        return (_modelMatrix*viewMatrix *_projectionMatrix );
    }
}
