using System;
using System.Numerics;

public class PhysicsObjectFactory

{
    public static PhysicsObject Rect(string label, float x, float y, ColliderType collisionType, float width, float height)
    {
        var physicsObject = new PhysicsObject(label, x, y, collisionType);
        var rect = new RectCollider(physicsObject, collisionType, width, height);
        physicsObject.Collider = rect;
        return physicsObject;
    }

    public static PhysicsObject Circl(string label, float x, float y, ColliderType collisionType, float radius)
    {
        var physicsObject = new PhysicsObject(label, x, y, collisionType);
        var rect = new CirclCollider(physicsObject, collisionType, radius);
        physicsObject.Collider = rect;
        return physicsObject;
    }

    public static PhysicsObject AreaRectTriggerOnce(string label, float x, float y, ColliderType collisionType, float width, float height, Action onTrigger)
    {
        var physicsObject = new PhysicsObject(label, x, y, collisionType)
        {
            Gravity = Vector2.Zero,
        };
        var rect = new RectCollider(physicsObject, collisionType, width, height)
        {
            OnCollisionAction = onTrigger
        };
        physicsObject.Collider = rect;
        return physicsObject;
    }
}   