using Microsoft.Xna.Framework;
using System;


public class RectCollider : Collider
{
    public float Width { get; private set; }
    public float Height { get; private set; }
    public RectCollider(PhysicsObject physicsObject, ColliderType collisionType, float width, float height) : base(physicsObject, collisionType)
    {
        Width = width;
        Height = height;
    }
    public Vector2 Size => new(Width, Height);

    public override float Left => Position.X;
    public override float Right => Position.X + Width;
    public override float Top => Position.Y;
    public override float Bottom => Position.Y + Height;

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



