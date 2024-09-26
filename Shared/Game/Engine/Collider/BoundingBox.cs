using Microsoft.Xna.Framework;

public class BoundingBox : Rect
{

    private Vector2 _worldPosition;
    public BoundingBox(Vector2 worldPosition, Rect rect) : base(rect.Offset, rect.Size) 
    {
        _worldPosition = worldPosition;
    }
    public Vector2 Position => _worldPosition + Offset;
    public float Left => Offset.X + _worldPosition.X;
    public float Right => Offset.X + _worldPosition.X + Width;
    public float Top => Offset.Y + _worldPosition.Y;
    public float Bottom => Offset.Y + _worldPosition.Y + Height;


}