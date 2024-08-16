using Microsoft.Xna.Framework;

public class Collider
{
    private ColliderType _colliderType;
    public Rectangle Shape{  get; private set; }

    public Collider(Rectangle rectangle, ColliderType colliderType)
    {
        Shape = rectangle;
        _colliderType = colliderType;
    }

    public bool Intersects(Collider other)
    {
        return Shape.Intersects(other.Shape);
    }
}

public enum ColliderType
{
    Static, // Does not move, like walls or ground
    Physics // Moves, like the player or enemies
}