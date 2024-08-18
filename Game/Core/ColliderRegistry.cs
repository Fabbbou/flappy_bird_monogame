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
    public bool CheckCollision(Collider collider)
    {
        foreach (Collider c in colliders)
        {
            if (collider.Intersects(c))
            {
                return true;
            }
        }
        return false;
    }
}
