﻿using Microsoft.Xna.Framework;
using System;

public enum CollisionType
{
    AreaCastTrigger,
    Physics,
    None
}
public class Collides
{
    public static CollisionType CollideAndSolve(Collider collider, Collider other, GameTime gameTime)
    {
        if (!collider.CollidesWith(other)) return CollisionType.None;

        if (isTriggerCollision(other)) return CollisionType.AreaCastTrigger;

        if (collider is RectCollider rect1 && other is RectCollider rect2)
        {
            return ResolveCollision(rect1, rect2) ? CollisionType.Physics : CollisionType.None;
        }
        else if (collider is CirclCollider circl && other is RectCollider rect)
        {
            return ResolveCollision(circl, rect) ? CollisionType.Physics : CollisionType.None;
        }
        return CollisionType.None;
    }

    private static bool isTriggerCollision(Collider other)
    {
        if(other.CollisionType != ColliderType.AreaCastTrigger) return false;
        other.TriggerCollision();
        return true;

    }

    private static bool ResolveCollision(RectCollider rect1, RectCollider rect2)
    {
        // Calculate the intersection depth (overlap) between the two rectangles
        float overlapX = Math.Min(
            rect1.Right - rect2.Left,
            rect2.Right - rect1.Left
        );
        float overlapY = Math.Min(
            rect1.Bottom - rect2.Top,
            rect2.Bottom - rect1.Top
        );
        // Resolve the collision by moving rect1 out of rect2
        // The collision is on the X axis
        if (overlapX < overlapY)
            if (rect1.Left < rect2.Left)
            {
                //right
                rect1.Position = new Vector2(rect2.Left - rect1.Width, rect1.Position.Y);
                if (rect1.PhysicsObject.Velocity.X > 0)
                {
                    rect1.PhysicsObject.Velocity.X = 0;
                }
            }
            else
            {
                //left
                rect1.Position = new Vector2(rect2.Right, rect1.Position.Y);
                if (rect1.PhysicsObject.Velocity.X < 0)
                {
                    rect1.PhysicsObject.Velocity.X = 0;
                }
            }
        else
            if (rect1.Top < rect2.Top)
        {
            //bottom
            rect1.Position = new Vector2(rect1.Position.X, rect2.Top - rect1.Height);
            if (rect1.PhysicsObject.Velocity.Y > 0)
            {
                rect1.PhysicsObject.Velocity.Y = 0;
            }
        }
        else
        {
            //top
            rect1.Position = new Vector2(rect1.Position.X, rect2.Top + rect2.Height);
            if (rect1.PhysicsObject.Velocity.Y < 0)
            {
                rect1.PhysicsObject.Velocity.Y = 0;
            }
        }
        return true;
    }

    private static bool ResolveCollision(CirclCollider rect1, RectCollider rect2)
    {
        float overlapX = Math.Min(
            rect1.Right - rect2.Left,
            rect2.Right - rect1.Left
        );
        float overlapY = Math.Min(
            rect1.Bottom - rect2.Top,
            rect2.Bottom - rect1.Top
        );
        // Resolve the collision by moving rect1 out of rect2
        // The collision is on the X axis
        if (overlapX < overlapY)
            if (rect1.Left < rect2.Left)
            {
                //right
                rect1.Position = new Vector2(rect2.Left - rect1.Radius, rect1.Position.Y);
                if (rect1.PhysicsObject.Velocity.X > 0)
                {
                    rect1.PhysicsObject.Velocity.X = 0;
                }
            }
            else
            {
                //left
                rect1.Position = new Vector2(rect2.Right + rect1.Radius, rect1.Position.Y);
                if (rect1.PhysicsObject.Velocity.X < 0)
                {
                    rect1.PhysicsObject.Velocity.X = 0;
                }
            }
        else
            if (rect1.Top < rect2.Top)
        {
            //bottom
            rect1.Position = new Vector2(rect1.Position.X, rect2.Top - rect1.Radius);
            if (rect1.PhysicsObject.Velocity.Y > 0)
            {
                rect1.PhysicsObject.Velocity.Y = 0;
            }
        }
        else
        {
            //top
            rect1.Position = new Vector2(rect1.Position.X, rect2.Top + rect2.Height + rect1.Radius);
            if (rect1.PhysicsObject.Velocity.Y < 0)
            {
                rect1.PhysicsObject.Velocity.Y = 0;
            }
        }
        return true;
    }

    private static bool CirclVsRect(CirclCollider circl, RectCollider rect)
    {
        Vector2 closestPoint = new Vector2(
            MathHelper.Clamp(circl.Position.X, rect.Left, rect.Right),
            MathHelper.Clamp(circl.Position.Y, rect.Top, rect.Bottom)
        );
        float distance = Vector2.Distance(circl.Position, closestPoint);
        return distance < circl.Radius;
    }
}
