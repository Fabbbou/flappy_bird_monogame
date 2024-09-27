using Microsoft.Xna.Framework;
using System;

public class Collides
{
    public static bool CollideAndSolve(Collider cDynamic, Collider cStatic, GameTime gameTime)
    {
        if (cDynamic is RectCollider rect1 && cStatic is RectCollider rect2)
        {
            return ResolveCollision(rect1, rect2);
        }
        else if (cDynamic is CirclCollider circl && cStatic is RectCollider rect)
        {
            return ResolveCollision(circl, rect);
        }
        return false;
    }


    private static bool ResolveCollision(RectCollider rect1, RectCollider rect2)
    {

        if(!rect1.CollidesWith(rect2)) return false;
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
        {
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
        }
        else
        {
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
        }
        return true;

        //Rect rect = (Rect)physicsObject.Collider.Shape;
        //Rect otherR = (Rect)other.Collider.Shape;
        //float overlapX = Math.Min(
        //    physicsObject.Position.X + rect.Width - other.Position.X,
        //    other.Position.X + otherR.Width - physicsObject.Position.X
        //);
        //float overlapY = Math.Min(
        //    physicsObject.Position.Y + rect.Height - other.Position.Y,
        //    other.Position.Y + otherR.Height - physicsObject.Position.Y
        //);

        //// The collision is on the X axis
        //if (overlapX < overlapY)
        //{
        //    if (physicsObject.Position.X < other.Position.X)
        //        return CollisionSide.Right;
        //    else
        //        return CollisionSide.Left;
        //}
        //else
        //{
        //    // Collision on the Y axis
        //    if (physicsObject.Position.Y < other.Position.Y)
        //        return CollisionSide.Bottom;
        //    else
        //        return CollisionSide.Top;
        //}

        //switch (side)
        //{
        //    case CollisionSide.Left:
        //        physicsObject.Position.X = other.Position.X + otherR.Width;
        //        if (physicsObject.Velocity.X > 0)
        //        {
        //            physicsObject.Velocity.X = 0;
        //        }
        //        physicsObject.ColorDebugCollision = Color.Blue;
        //        break;
        //    case CollisionSide.Right:
        //        physicsObject.Position.X = other.Position.X - rect.Width;
        //        if (physicsObject.Velocity.X < 0)
        //        {
        //            physicsObject.Velocity.X = 0;
        //        }
        //        physicsObject.ColorDebugCollision = Color.Blue;
        //        break;
        //    case CollisionSide.Top:
        //        physicsObject.Position.Y = other.Position.Y + otherR.Height;
        //        if (physicsObject.Velocity.Y < 0)
        //        {
        //            physicsObject.Velocity.Y = 0;
        //        }
        //        physicsObject.ColorDebugCollision = Color.Green;
        //        break;
        //    case CollisionSide.Bottom:
        //        physicsObject.Position.Y = other.Position.Y - rect.Height;
        //        if (physicsObject.Velocity.Y > 0)
        //        {
        //            physicsObject.Velocity.Y = 0;
        //        }
        //        physicsObject.ColorDebugCollision = Color.Pink;
        //        break;
        //}
    }

    private static bool ResolveCollision(CirclCollider circl, RectCollider rect)
    {
        if (!CirclVsRect(circl, rect)) return false;
        Vector2 closestPoint = new Vector2(
            MathHelper.Clamp(circl.Position.X, rect.Left, rect.Right),
            MathHelper.Clamp(circl.Position.Y, rect.Top, rect.Bottom)
        );

        Vector2 direction = circl.Position - closestPoint;
        direction.Normalize();

        circl.Position = closestPoint + direction * circl.Radius;
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

    ///// <summary> 
    ///// AABB collision detection and resolution
    ///// 
    ///// </summary>
    ///// <param name="rDynamic"></param>
    ///// <param name="rStatic"></param>
    ///// <param name="gameTime"></param>
    ///// <returns></returns>
    //public static Collision CollideAndSolve(PhysicsObject rDynamic, PhysicsObject rStatic, GameTime gameTime)
    //{
    //    CollisionSide side = CollisionSide.None;
    //    if (rDynamic.Collider.Shape is RectCollider && rStatic.Collider.Shape is RectCollider)
    //    {
    //        side = RectCollides.CollidePostPhysicsRectVsRect(rDynamic, rStatic);
    //    }else if(rDynamic.Collider.Shape is CirclCollider && rStatic.Collider.Shape is CirclCollider)
    //    {
    //        side = CirclCollides.CollidePostPhysicsCirclVsCircl(rDynamic, rStatic);
    //    }else if (rDynamic.Collider.Shape is CirclCollider && rStatic.Collider.Shape is RectCollider)
    //    {
    //        side = CollidePostPhysicsCirclVsRect(rStatic, rDynamic);
    //    }else if (rDynamic.Collider.Shape is RectCollider && rStatic.Collider.Shape is CirclCollider)
    //    {
    //        side = CollidePostPhysicsCirclVsRect(rStatic, rDynamic);
    //    }
    //    return new Collision(side);
    //}

    //public static CollisionSide CollidePostPhysicsCirclVsRect(PhysicsObject physicsObject, PhysicsObject other)
    //{
    //    CollisionSide side = CheckIfCollisionCirclVsRect(physicsObject, other);
    //    if (side == CollisionSide.None)
    //    {
    //        physicsObject.ColorDebugCollision = Constants.DEFAULT_DEBUG_COLOR_GIZMOS;
    //        return side;
    //    }
    //    CirclCollider circle = (CirclCollider)physicsObject.Collider.Shape;
    //    RectCollider rect = (RectCollider)other.Collider.Shape;
    //    // Calculate the position of the circle relative to the rectangle to make it next to it when it collides
    //    Vector2 circleDistance = new Vector2(Math.Abs(physicsObject.Position.X - other.Position.X), Math.Abs(physicsObject.Position.Y - other.Position.Y));
    //}

    //private static CollisionSide CheckIfCollisionCirclVsRect(PhysicsObject physicsObject, PhysicsObject other)
    //{
    //    if(CircleVsRect(physicsObject, other))
    //    {
    //        return CollisionSide.Circle;
    //    }
    //    return CollisionSide.None;
    //}

    //private static bool CircleVsRect(PhysicsObject physicsObject, PhysicsObject other)
    //{
    //    CirclCollider circle = (CirclCollider)physicsObject.Collider.Shape;
    //    RectCollider rect = (RectCollider)other.Collider.Shape;
    //    Vector2 circleDistance = new Vector2(Math.Abs(physicsObject.Position.X - other.Position.X), Math.Abs(physicsObject.Position.Y - other.Position.Y));
    //    if (circleDistance.X > (rect.Width * 0.5f + circle.Radius)) { return false; }
    //    if (circleDistance.Y > (rect.Height * 0.5f + circle.Radius)) { return false; }

    //    if (circleDistance.X <= (rect.Width * 0.5f)) { return true; }
    //    if (circleDistance.Y <= (rect.Height * 0.5f)) { return true; }

    //    float cornerDistance_sq = (circleDistance.X - rect.Width * 0.5f) * (circleDistance.X - rect.Width * 0.5f) +
    //                              (circleDistance.Y - rect.Height * 0.5f) * (circleDistance.Y - rect.Height * 0.5f);

    //    return (cornerDistance_sq <= (circle.Radius * circle.Radius));
    //}
}
