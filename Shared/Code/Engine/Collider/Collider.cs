using Microsoft.Xna.Framework;

public abstract class Collider
{
    public PhysicsObject PhysicsObject { get; private set; }
    public CollisionType CollisionType { get; private set; }

    public Vector2 Position
    {
        get => PhysicsObject.Position;
        set => PhysicsObject.Position = value;
    }

    protected Collider(PhysicsObject physicsObject, CollisionType collisionType)
    {
        PhysicsObject = physicsObject;
        CollisionType = collisionType;
    }

    public abstract float Left { get; }
    public abstract float Right { get; }
    public abstract float Top { get; }
    public abstract float Bottom { get; }

    public abstract bool CollidesWith(Collider other);

    public void StopHorizontal()
    {
        PhysicsObject.Velocity = new Vector2(0, PhysicsObject.Velocity.Y);
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