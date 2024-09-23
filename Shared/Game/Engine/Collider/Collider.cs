using flappyrogue_mg.Core.Collider;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

public class Collider
{
    public static readonly Color DEFAULT_DEBUG = Color.Yellow;
    private Color _colorDebugCollision = DEFAULT_DEBUG;
    public PhysicsObject PhysicsObject { get; private set; }
    public CollisionType CollisionType { get; private set; }
    public ColliderShape ColliderShape { get; private set; }
    public Vector2 Position => PhysicsObject.Position + ColliderShape.Offset;


    public Collider(PhysicsObject physicsObject, float width, float height, CollisionType colliderType)
    {
        PhysicsEngine.Instance.AddCollider(this);
        PhysicsObject = physicsObject;
        ColliderShape = new Rect(Vector2.Zero, new(width, height));
        CollisionType = colliderType;
    }

    ~Collider() => PhysicsEngine.Instance.RemoveCollider(this);

    /// <summary>
    ///     Check if the collider is colliding with another collider and return the side of the collision.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public CollisionSide CheckIfCollision(Collider other)
    {
        if (!ColliderShape.Intersects(Position, other))
        {
            return CollisionSide.None;
        }
        Rect b = ColliderShape.GetBoundingBox();
        Rect otherB = other.ColliderShape.GetBoundingBox();
        float overlapX = Math.Min(
            Position.X + b.Width - other.Position.X,
            other.Position.X + otherB.Width - Position.X
        );

        float overlapY = Math.Min(
            Position.Y + b.Height - other.Position.Y,
            other.Position.Y + otherB.Height - Position.Y
        );

        // The collision is on the X axis
        if (overlapX < overlapY)
        {
            if (Position.X < other.Position.X)
                return CollisionSide.Right;
            else
                return CollisionSide.Left;
        }
        else
        {
            // Collision on the Y axis
            if (Position.Y < other.Position.Y)
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
        Rect b = ColliderShape.GetBoundingBox();
        Rect otherB = other.ColliderShape.GetBoundingBox();
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
                PhysicsObject.Position.X = other.Position.X + otherB.Width;
                if (PhysicsObject.Velocity.X > 0)
                {
                    PhysicsObject.Velocity.X = 0;
                }
                _colorDebugCollision = Color.Blue;
                break;
            case CollisionSide.Right:
                PhysicsObject.Position.X = other.Position.X - b.Width;
                if (PhysicsObject.Velocity.X < 0)
                {
                    PhysicsObject.Velocity.X = 0;
                }
                _colorDebugCollision = Color.Blue;
                break;
            case CollisionSide.Top:
                PhysicsObject.Position.Y = other.Position.Y + otherB.Height;
                if(PhysicsObject.Velocity.Y < 0)
                {
                    PhysicsObject.Velocity.Y = 0;
                }
                _colorDebugCollision = Color.Green;
                break;
            case CollisionSide.Bottom:
                PhysicsObject.Position.Y = other.Position.Y - b.Height;
                if (PhysicsObject.Velocity.Y > 0)
                {
                    PhysicsObject.Velocity.Y = 0;
                }
                _colorDebugCollision = Color.Pink;
                break;
        }
        return side;
    }

    public void DebugDraw(SpriteBatch spriteBatch)
    {
        if(ColliderShape is Rect)
        {
            //square collider
            spriteBatch.DrawRectangle(Position, ((Rect)ColliderShape).Size, _colorDebugCollision, 1);
        }
        //else if (ColliderShape is Circle)
        //{
        //    //circle collider
        //    spriteBatch.DrawCircle(Position, ((Circle)ColliderShape).Radius, 16, _colorDebugCollision, 1);
        //}
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