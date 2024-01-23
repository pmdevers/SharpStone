using System.Diagnostics;
using System.Numerics;

namespace SharpStone.Graphics;
public class OrthographicCamera 
    : Camera
{
    private Vector3 _position = Vector3.Zero;
    private float _rotation = 0f;
    private Matrix4x4 _projectionMatrix;
    private Matrix4x4 _viewMatrix;

    public OrthographicCamera(float left, float right, float bottom, float top)
    {
        _projectionMatrix = Matrix4x4.CreateOrthographicOffCenter(left, right, bottom, top, -1f, 1f);
        _viewMatrix = Matrix4x4.Identity;
        ProjectionView = _projectionMatrix * _viewMatrix;
    }

    public OrthographicCamera(float width, float height)
    {
        _projectionMatrix = Matrix4x4.CreateOrthographic(width, height, -1f, 1f);
        _viewMatrix = Matrix4x4.Identity;
        ProjectionView = _projectionMatrix * _viewMatrix;
    }

    public Vector3 Position
    {
        get => _position;
        set
        {
            _position = value;
            RecalculateViewMatix();
        }
    }
    public float Rotation
    {
        get => _rotation; set
        {
            _rotation = value;
            RecalculateViewMatix();
        }
    }
    public override Matrix4x4 ProjectionView { get; set; }
    public void RecalculateViewMatix()
    {
        var transform = Matrix4x4.CreateTranslation(_position)
            * Matrix4x4.CreateRotationZ(_rotation);

        Matrix4x4.Invert(transform, out var inverted);
        _viewMatrix = inverted;

        ProjectionView = _projectionMatrix * _viewMatrix;
    }

    
}
