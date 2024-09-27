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
    public Vector2 Center => new(Position.X + Width * 0.5f, Position.Y + Height * 0.5f);

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
        return Position.X < otherRect.Position.X + otherRect.Width &&
               Position.X + Width > otherRect.Position.X &&
               Position.Y < otherRect.Position.Y + Height &&
               Position.Y + Height > otherRect.Position.Y;
    }
}



