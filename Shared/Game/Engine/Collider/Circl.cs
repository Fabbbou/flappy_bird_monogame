using Microsoft.Xna.Framework;

public class Circl : ColliderShape
{
    public float Radius { get; private set; }
    public Circl(Vector2 offset, float radius)
    {
        Offset = offset;
        Radius = radius;
    }
    public override BoundingBox GetBoundingBox(Vector2 worldPosition)
    {
        return new BoundingBox(worldPosition, new Rect(Offset - new Vector2(Radius), new Vector2(Radius * 2)));
    }
}