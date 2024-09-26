using flappyrogue_mg.Core.Collider;
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
        CollisionSide side = CollisionSide.None;
        if (rDynamic.Collider.Shape is Rect && rStatic.Collider.Shape is Rect)
        {
            side = RectCollides.CollidePostPhysicsRectVsRect(rDynamic, rStatic);
        }else if(rDynamic.Collider.Shape is Circl && rStatic.Collider.Shape is Circl)
        {
            side = CirclCollides.CollidePostPhysicsCirclVsCircl(rDynamic, rStatic);
        }else if (rDynamic.Collider.Shape is Circl && rStatic.Collider.Shape is Rect)
        {
            side = CollidePostPhysicsCirclVsRect(rStatic, rDynamic);
        }else if (rDynamic.Collider.Shape is Rect && rStatic.Collider.Shape is Circl)
        {
            side = CollidePostPhysicsCirclVsRect(rStatic, rDynamic);
        }
        return new Collision(side);
    }

    public static CollisionSide CollidePostPhysicsCirclVsRect(PhysicsObject physicsObject, PhysicsObject other)
    {
        CollisionSide side = CheckIfCollisionCirclVsRect(physicsObject, other);
        if (side == CollisionSide.None)
        {
            physicsObject.ColorDebugCollision = Constants.DEFAULT_DEBUG_COLOR_GIZMOS;
            return side;
        }
        Circl circle = (Circl)physicsObject.Collider.Shape;
        Rect rect = (Rect)other.Collider.Shape;

    }

    private static CollisionSide CheckIfCollisionCirclVsRect(PhysicsObject physicsObject, PhysicsObject other)
    {
        if(CircleVsRect(physicsObject, other))
        {
            return CollisionSide.Circle;
        }
        return CollisionSide.None;
    }

    private static bool CircleVsRect(PhysicsObject physicsObject, PhysicsObject other)
    {
        Circl circle = (Circl)physicsObject.Collider.Shape;
        Rect rect = (Rect)other.Collider.Shape;
        Vector2 circleDistance = new Vector2(Math.Abs(physicsObject.Position.X - other.Position.X), Math.Abs(physicsObject.Position.Y - other.Position.Y));
        if (circleDistance.X > (rect.Width * 0.5f + circle.Radius)) { return false; }
        if (circleDistance.Y > (rect.Height * 0.5f + circle.Radius)) { return false; }

        if (circleDistance.X <= (rect.Width * 0.5f)) { return true; }
        if (circleDistance.Y <= (rect.Height * 0.5f)) { return true; }

        float cornerDistance_sq = (circleDistance.X - rect.Width * 0.5f) * (circleDistance.X - rect.Width * 0.5f) +
                                  (circleDistance.Y - rect.Height * 0.5f) * (circleDistance.Y - rect.Height * 0.5f);

        return (cornerDistance_sq <= (circle.Radius * circle.Radius));
    }
}
