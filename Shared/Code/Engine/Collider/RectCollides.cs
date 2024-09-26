using Microsoft.Xna.Framework;
using System;

public class RectCollides
{
    /// <summary>
    /// Process the collision and position the collider's PhysicObject correctly.
    /// This mean you already processed the physics for this frame and know you want to pixelperfectly position the collider.
    /// 
    /// </summary>
    /// <param name="other">the other collider you are placing with</param>
    /// <returns>The collision side you collide </returns>
    public static CollisionSide CollidePostPhysicsRectVsRect(PhysicsObject physicsObject, PhysicsObject other)
    {
        if (other == null)
        {
            return CollisionSide.None;
        }
        CollisionSide side = CheckIfCollisionRectVsRect(physicsObject, other);
        if (side == CollisionSide.None)
        {
            physicsObject.ColorDebugCollision = Constants.DEFAULT_DEBUG_COLOR_GIZMOS;
            return side;
        }
        Rect rect = (Rect)physicsObject.Collider.Shape;
        Rect otherR = (Rect)other.Collider.Shape;
        switch (side)
        {
            case CollisionSide.Left:
                physicsObject.Position.X = other.Position.X + otherR.Width;
                if (physicsObject.Velocity.X > 0)
                {
                    physicsObject.Velocity.X = 0;
                }
                physicsObject.ColorDebugCollision = Color.Blue;
                break;
            case CollisionSide.Right:
                physicsObject.Position.X = other.Position.X - rect.Width;
                if (physicsObject.Velocity.X < 0)
                {
                    physicsObject.Velocity.X = 0;
                }
                physicsObject.ColorDebugCollision = Color.Blue;
                break;
            case CollisionSide.Top:
                physicsObject.Position.Y = other.Position.Y + otherR.Height;
                if (physicsObject.Velocity.Y < 0)
                {
                    physicsObject.Velocity.Y = 0;
                }
                physicsObject.ColorDebugCollision = Color.Green;
                break;
            case CollisionSide.Bottom:
                physicsObject.Position.Y = other.Position.Y - rect.Height;
                if (physicsObject.Velocity.Y > 0)
                {
                    physicsObject.Velocity.Y = 0;
                }
                physicsObject.ColorDebugCollision = Color.Pink;
                break;
        }
        return side;
    }

    /// <summary>
    ///     Check if the collider is colliding with another collider and return the side of the collision.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    private static CollisionSide CheckIfCollisionRectVsRect(PhysicsObject physicsObject, PhysicsObject other)
    {
        if (!RectVsRect(physicsObject, other))
        {
            return CollisionSide.None;
        }
        Rect rect = (Rect)physicsObject.Collider.Shape;
        Rect otherR = (Rect)other.Collider.Shape;
        float overlapX = Math.Min(
            physicsObject.Position.X + rect.Width - other.Position.X,
            other.Position.X + otherR.Width - physicsObject.Position.X
        );

        float overlapY = Math.Min(
            physicsObject.Position.Y + rect.Height - other.Position.Y,
            other.Position.Y + otherR.Height - physicsObject.Position.Y
        );

        // The collision is on the X axis
        if (overlapX < overlapY)
        {
            if (physicsObject.Position.X < other.Position.X)
                return CollisionSide.Right;
            else
                return CollisionSide.Left;
        }
        else
        {
            // Collision on the Y axis
            if (physicsObject.Position.Y < other.Position.Y)
                return CollisionSide.Bottom;
            else
                return CollisionSide.Top;
        }
    }

    private static bool RectVsRect(PhysicsObject physicsObject, PhysicsObject other)
    {
        Rect rect = (Rect)physicsObject.Collider.Shape;
        Rect otherR = (Rect)other.Collider.Shape;
        return physicsObject.Position.X < other.Position.X + otherR.Width &&
               physicsObject.Position.X + rect.Width > other.Position.X &&
               physicsObject.Position.Y < other.Position.Y + otherR.Height &&
               physicsObject.Position.Y + rect.Height > other.Position.Y;
    }
}