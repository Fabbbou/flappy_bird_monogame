public class Collision
{
    public PhysicsObject PhysicsObject { get; set; }
    public CollisionType CollisionType { get; set; }

    public Collision(PhysicsObject physicsObject, CollisionType collisionType)
    {
        PhysicsObject = physicsObject;
        CollisionType = collisionType;
    }
}