using flappyrogue_mg.Core.Collider;
using Microsoft.Xna.Framework;

public class Collider
{
    public PhysicsObject PhysicsObject { get; private set; }
    public CollisionType CollisionType { get; private set; }
    public ColliderShape ColliderShape { get; private set; }

    public Collider(PhysicsObject physicsObject, float width, float height, CollisionType colliderType, Vector2 offsetRelativePosition)
    {
        PhysicsEngine.Instance.AddCollider(this);
        PhysicsObject = physicsObject;
        ColliderShape = new Rect(offsetRelativePosition, new(width, height));
        CollisionType = colliderType;
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