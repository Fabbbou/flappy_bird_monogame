using Microsoft.Xna.Framework;
using System;

public class CirclCollides
{

    public static CollisionSide CollidePostPhysicsCirclVsCircl(PhysicsObject physicsObject, PhysicsObject other)
    {
        Circl circle = (Circl)physicsObject.Collider.Shape;
        Circl otherCircle = (Circl)other.Collider.Shape;

        Vector2 distance = other.Position - physicsObject.Position;
        float radii = circle.Radius + otherCircle.Radius;
        float radiiSquared = radii * radii;

        if (distance.LengthSquared() < radiiSquared)
        {
            return CollisionSide.Circle;
        }
        return CollisionSide.None;
    }
}