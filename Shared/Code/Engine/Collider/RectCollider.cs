using Microsoft.Xna.Framework;
using System;


public class RectCollider : Collider
{
    public float Width { get; private set; }
    public float Height { get; private set; }
    public RectCollider(PhysicsObject physicsObject, CollisionType collisionType, float width, float height) : base(physicsObject, collisionType)
    {
        Width = width;
        Height = height;
    }
    public Vector2 Size => new(Width, Height);

    public float Left => Position.X;
    public float Right => Position.X + Width;
    public float Top => Position.Y;
    public float Bottom => Position.Y + Height;
    public Vector2 TopLeft => new(Position.X, Position.Y);
    public Vector2 TopRight => new(Position.X + Width, Position.Y);
    public Vector2 BottomLeft => new(Position.X, Position.Y + Height);
    public Vector2 BottomRight => new(Position.X + Width, Position.Y + Height);
    public Vector2[] Corners => new[] { TopLeft, TopRight, BottomRight, BottomLeft };
    public override bool CollidesWith(Collider other)
    {
        if (other is RectCollider otherRect)
        {
            return RectVsRect(otherRect);
        }
        else if (other is CirclCollider otherCircl)
        {
            return otherCircl.CollidesWith(this);
        }
        return false;
    }

    private bool RectVsRect(RectCollider otherRect)
    {
        return Left < otherRect.Right &&
               Right > otherRect.Left &&
               Top < otherRect.Bottom &&
               Bottom > otherRect.Top;
    }
}



