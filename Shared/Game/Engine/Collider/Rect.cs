using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ECS;

public class Rect : ColliderShape
{
    public float Width { get; private set; }
    public float Height { get; private set; }
    public Vector2 Size => new(Width, Height);

    public Rect(Vector2 offset, Vector2 size)
    {
        Offset = offset;
        Width = size.X;
        Height = size.Y;
    }

    public override BoundingBox GetBoundingBox(Vector2 worldPosition)
    {
        return new(worldPosition, this);
    }
}


