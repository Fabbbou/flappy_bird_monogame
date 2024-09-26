public class Collider
{
    public PhysicsObject PhysicsObject { get; private set; }
    public CollisionType CollisionType { get; private set; }
    public Rect Rect { get; private set; }

    public Collider(PhysicsObject physicsObject, float width, float height, CollisionType collisionType)
    {
        PhysicsEngine.Instance.AddCollider(this);
        Rect = new Rect(width, height);
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
    Right
}

public enum CollisionType
{
    Static,
    Moving
}