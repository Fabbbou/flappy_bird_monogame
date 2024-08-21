using flappyrogue_mg.Game.Core.Collider;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class ColliderRegistry
{
    private static ColliderRegistry _instance;
    private readonly List<Collider> colliders = new List<Collider>();

    public static ColliderRegistry Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ColliderRegistry();
            }
            return _instance;
        }
    }

    public void Register(Collider collider)
    {
        colliders.Add(collider);
    }
    public void Unregister(Collider collider)
    {
        colliders.Remove(collider);
    }

    public Collision isColliding(Collider collider, GameTime gameTime)
    {
        foreach (Collider other in colliders)
        {
            if (collider == other)
            {
                continue; // Skip self
            }
            if (collider is RectangleCollider rectangleCollider && other is RectangleCollider otherRectangleCollider)
            {
                return Collides.DynamicRectVsRect(rectangleCollider, otherRectangleCollider, gameTime);
            }
        }
        return null;
    }
}
