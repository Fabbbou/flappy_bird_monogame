﻿using flappyrogue_mg.Core.Collider;
using Microsoft.Xna.Framework;
using System;

public class Collides
{

    /// <summary>
    /// AABB collision detection and resolution
    /// 
    /// </summary>
    /// <param name="rDynamic"></param>
    /// <param name="rStatic"></param>
    /// <param name="gameTime"></param>
    /// <returns></returns>
    public static Collision CollideAndSolve(PhysicsObject rDynamic, PhysicsObject rStatic, GameTime gameTime)
    {
        CollisionSide side = CollidePostPhysics(rDynamic, rStatic);
        if (side != CollisionSide.None)
        {
            return new Collision(side);
        }
        return null;
    }

    /// <summary>
    /// Process the collision and position the collider's PhysicObject correctly.
    /// This mean you already processed the physics for this frame and know you want to pixelperfectly position the collider.
    /// 
    /// </summary>
    /// <param name="other">the other collider you are placing with</param>
    /// <returns>The collision side you collide </returns>
    private static CollisionSide CollidePostPhysics(PhysicsObject physicsObject, PhysicsObject other)
    {
        if (other == null)
        {
            return CollisionSide.None;
        }
        CollisionSide side = CheckIfCollision(physicsObject.Collider, other.Collider);
        if (side == CollisionSide.None)
        {
            physicsObject.ColorDebugCollision = Constants.DEFAULT_DEBUG_COLOR_GIZMOS;
            return side;
        }
        BoundingBox b = physicsObject.Collider.ColliderShape.GetBoundingBox(physicsObject.Position);
        BoundingBox otherB = other.Collider.ColliderShape.GetBoundingBox(other.Position);
        switch (side)
        {
            case CollisionSide.Left:
                physicsObject.Position.X = otherB.Position.X + otherB.Width;
                if (physicsObject.Velocity.X > 0)
                {
                    physicsObject.Velocity.X = 0;
                }
                physicsObject.ColorDebugCollision = Color.Red;
                break;
            case CollisionSide.Right:
                physicsObject.Position.X = otherB.Position.X - b.Width;
                if (physicsObject.Velocity.X < 0)
                {
                    physicsObject.Velocity.X = 0;
                }
                physicsObject.ColorDebugCollision = Color.Blue;
                break;
            case CollisionSide.Top:
                physicsObject.Position.Y = otherB.Position.Y + otherB.Height;
                if (physicsObject.Velocity.Y < 0)
                {
                    physicsObject.Velocity.Y = 0;
                }
                physicsObject.ColorDebugCollision = Color.Green;
                break;
            case CollisionSide.Bottom:
                physicsObject.Position.Y = otherB.Position.Y - b.Height;
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
    private static CollisionSide CheckIfCollision(Collider collider, Collider other)
    {
        if (!Intersects(collider.PhysicsObject, other.PhysicsObject))
        {
            return CollisionSide.None;
        }
        BoundingBox b = collider.ColliderShape.GetBoundingBox(collider.PhysicsObject.Position);
        BoundingBox otherB = other.ColliderShape.GetBoundingBox(other.PhysicsObject.Position);
        float overlapX = Math.Min(
            b.Right - otherB.Left,
            otherB.Right - b.Left
        );

        float overlapY = Math.Min(
            b.Bottom - otherB.Top,
            otherB.Bottom - b.Top
        );

        // The collision is on the X axis
        if (overlapX < overlapY)
        {
            if (b.Position.X < otherB.Position.X)
                return CollisionSide.Right;
            else
                return CollisionSide.Left;
        }
        else
        {
            // Collision on the Y axis
            if (b.Top < otherB.Top)
            {
                return CollisionSide.Bottom;
            }
            else
            {
                return CollisionSide.Top;
            }
        }
    }

    private static bool Intersects(PhysicsObject physicsObject, PhysicsObject other)
    {
        if (physicsObject.Collider.ColliderShape is Rect && other.Collider.ColliderShape is Rect)
        {
            return RectVsRect(physicsObject, other);
        }
        return false;
    }

    private static bool RectVsRect(PhysicsObject physicsObject, PhysicsObject other)
    {
        Rect rect = (Rect)physicsObject.Collider.ColliderShape;
        Rect otherR = (Rect)other.Collider.ColliderShape;
        return physicsObject.Position.X < other.Position.X + otherR.Width &&
                physicsObject.Position.X + rect.Width > other.Position.X &&
                physicsObject.Position.Y < other.Position.Y + otherR.Height &&
                physicsObject.Position.Y + rect.Height > other.Position.Y;
    }
}
