using Microsoft.Xna.Framework;

public class Collision
{
    public Vector2 Normal { get; private set; }
    public Vector2 Position { get; private set; }
    public float THitNear { get; private set; }

    public Collision(Vector2 position, Vector2 collisionNormal, float tHitNear)
    {
        Position = position;
        Normal = collisionNormal;
        THitNear = tHitNear;
    }
}
