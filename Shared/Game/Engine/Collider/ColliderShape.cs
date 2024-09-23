using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class ColliderShape
{
    public Vector2 Offset { get; set; }
    public abstract bool Intersects(Vector2 position, Collider other);
    public abstract Rect GetBoundingBox();
}