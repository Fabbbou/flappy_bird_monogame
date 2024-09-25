using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class ColliderShape
{
    public Vector2 Offset { get; set; }
    public abstract BoundingBox GetBoundingBox(Vector2 worldPosition);
}