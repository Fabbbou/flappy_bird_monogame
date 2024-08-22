using flappyrogue_mg.Game.Core.Collider;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

public class Collider
{
    public PhysicsObject PhysicsObject { get; private set; }
    public float X => PhysicsObject.Position.X;
    public float Y => PhysicsObject.Position.Y;
    public Vector2 Position => PhysicsObject.Position;
    public float Width { get; }
    public float Height { get; }
    public Rect Rect => new(Position, new(Width, Height));
    public  Vector2 Center => Rect.Center;

    public Vector2 Size => Rect.Size;

    public Collider(PhysicsObject physicsObject, float width, float height)
    {
        PhysicsObject = physicsObject;
        ColliderRegistry.Instance.Register(this);
        Width = width;
        Height = height;
    }

    public  void DrawDebug(SpriteBatch spriteBatch, Color color)
    {
        spriteBatch.DrawRectangle(Rect.Render, color, 2);
    }

    public CollisionSide GetCollisionSide(Collider other)
    {
        if (!Rect.Intersects(other.Rect))
            return CollisionSide.None;

        float overlapX = Math.Min(
            Position.X + Width - other.Position.X,
            other.Position.X + other.Width - X
        );

        float overlapY = Math.Min(
            Y + Height - other.Y,
            other.Y + other.Height - Y
        );

        if (overlapX < overlapY)
        {
            // Collision on the X axis
            if (X < other.X)
                return CollisionSide.Right;
            else
                return CollisionSide.Left;
        }
        else
        {
            // Collision on the Y axis
            if (Y < other.Y)
                return CollisionSide.Bottom;
            else
                return CollisionSide.Top;
        }
    }

    public CollisionSide HandleCollision(Collider other)
    {
        CollisionSide side = GetCollisionSide(other);
        if (side == CollisionSide.None)
            return side;

        switch (side)
        {
            case CollisionSide.Left:
                PhysicsObject.Position.X = other.X + other.Width;
                PhysicsObject.Velocity.X = 0;
                break;
            case CollisionSide.Right:
                PhysicsObject.Position.X = other.X - Width;
                PhysicsObject.Velocity.X = 0;
                break;
            case CollisionSide.Top:
                PhysicsObject.Position.Y = other.Y + other.Height;
                PhysicsObject.Velocity.Y = 0;
                break;
            case CollisionSide.Bottom:
                PhysicsObject.Position.Y = other.Y - Height;
                PhysicsObject.Velocity.Y = 0;
                break;
        }
        return side;
    }
}
public enum CollisionSide
{
    None,
    Top,
    Bottom,
    Left,
    Right
}