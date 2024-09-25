using Microsoft.Xna.Framework;

public class BoundingBox : Rect
{

    public Vector2 WorldPosition { get; private set; }
    public BoundingBox(Vector2 worldPosition, Rect rect) : base(rect.Offset, rect.Size) { }
    public float Left => Offset.X + WorldPosition.X;
    public float Right => Offset.X + WorldPosition.X + Width;
    public float Top => Offset.Y + WorldPosition.Y;
    public float Bottom => Offset.Y + WorldPosition.Y + Height;
}