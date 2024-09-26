public class Collider
{
    public PhysicsObject PhysicsObject { get; private set; }
    public CollisionType CollisionType { get; private set; }
    public Shape Shape { get; private set; }

    public Collider(PhysicsObject physicsObject, Shape shape, CollisionType collisionType)
    {
        PhysicsEngine.Instance.AddCollider(this);
        Shape = shape;
        PhysicsObject = physicsObject;
        CollisionType = collisionType;
    }
    ~Collider() => PhysicsEngine.Instance.RemoveCollider(this);

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
    Right,
    Circle
}

public enum CollisionType
{
    Static,
    Moving
}