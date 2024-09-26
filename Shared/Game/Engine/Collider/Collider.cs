using flappyrogue_mg.Core.Collider;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Collider
{
    public PhysicsObject PhysicsObject { get; private set; }
    public CollisionType CollisionType { get; private set; }
    public float X => PhysicsObject.Position.X;
    public float Y => PhysicsObject.Position.Y;
    public Vector2 Position => PhysicsObject.Position;
    public float Width { get; }
    public float Height { get; }
    public Rect Rect => new(Width, Height);

    public static readonly Color DEFAULT_DEBUG = Color.Yellow;
    private Color _colorDebugCollision = DEFAULT_DEBUG;

    public Collider(PhysicsObject physicsObject, float width, float height, CollisionType collisionType)
    {
        PhysicsEngine.Instance.AddCollider(this);
        Width = width;
        Height = height;
        PhysicsObject = physicsObject;
        CollisionType = collisionType;
    }
    ~Collider() => PhysicsEngine.Instance.RemoveCollider(this);

    public bool Intersects(Collider other)
    {
        return X < other.X + other.Width &&
               X + Width > other.X &&
               Y < other.Y + other.Height &&
               Y + Height > other.Y;
    }

    /// <summary>
    ///     Check if the collider is colliding with another collider and return the side of the collision.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public CollisionSide CheckIfCollision(Collider other)
    {
        if (!Intersects(other))
        {
            return CollisionSide.None;
        }
        float overlapX = Math.Min(
            Position.X + Width - other.Position.X,
            other.Position.X + other.Width - X
        );

        float overlapY = Math.Min(
            Y + Height - other.Y,
            other.Y + other.Height - Y
        );

        // The collision is on the X axis
        if (overlapX < overlapY)
        {
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

    /// <summary>
    /// Process the collision and position the collider's PhysicObject correctly.
    /// This mean you already processed the physics for this frame and know you want to pixelperfectly position the collider.
    /// 
    /// </summary>
    /// <param name="other">the other collider you are placing with</param>
    /// <returns>The collision side you collide </returns>
    public CollisionSide CollidePostPhysics(Collider other)
    {
        if (other == null)
        {
            return CollisionSide.None;
        }
        CollisionSide side = CheckIfCollision(other);
        if (side == CollisionSide.None)
        {
            _colorDebugCollision = DEFAULT_DEBUG;
            return side;
        }

        switch (side)
        {
            case CollisionSide.Left:
                PhysicsObject.Position.X = other.X + other.Width;
                if (PhysicsObject.Velocity.X > 0)
                {
                    PhysicsObject.Velocity.X = 0;
                }
                _colorDebugCollision = Color.Blue;
                break;
            case CollisionSide.Right:
                PhysicsObject.Position.X = other.X - Width;
                if (PhysicsObject.Velocity.X < 0)
                {
                    PhysicsObject.Velocity.X = 0;
                }
                _colorDebugCollision = Color.Blue;
                break;
            case CollisionSide.Top:
                PhysicsObject.Position.Y = other.Y + other.Height;
                if(PhysicsObject.Velocity.Y < 0)
                {
                    PhysicsObject.Velocity.Y = 0;
                }
                _colorDebugCollision = Color.Green;
                break;
            case CollisionSide.Bottom:
                PhysicsObject.Position.Y = other.Y - Height;
                if (PhysicsObject.Velocity.Y > 0)
                {
                    PhysicsObject.Velocity.Y = 0;
                }
                _colorDebugCollision = Color.Pink;
                break;
        }
        return side;
    }

    public void Kill()
    {
        PhysicsEngine.Instance.RemoveCollider(this);
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

public enum CollisionType
{
    Static,
    Moving
}