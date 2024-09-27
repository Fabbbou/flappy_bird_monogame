public class PhysicsObjectFactory

{
    public static PhysicsObject Rect(string label, float x, float y, CollisionType collisionType, float width, float height)
    {
        var physicsObject = new PhysicsObject(label, x, y, collisionType);
        var rect = new RectCollider(physicsObject, collisionType, width, height);
        physicsObject.Collider = rect;
        return physicsObject;
    }

    public static PhysicsObject Circl(string label, float x, float y, CollisionType collisionType, float radius)
    {
        var physicsObject = new PhysicsObject(label, x, y, collisionType);
        var rect = new CirclCollider(physicsObject, collisionType, radius);
        physicsObject.Collider = rect;
        return physicsObject;
    }
}   