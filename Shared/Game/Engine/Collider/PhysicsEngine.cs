using flappyrogue_mg.Core.Collider;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class PhysicsEngine
{
    private static PhysicsEngine _instance;
    private readonly List<Collider> _colliders = new List<Collider>();
    //a hashmap-like private field called alreadyCollided
    private Dictionary<Collider, Collider> _alreadyCollided = new();

    public static PhysicsEngine Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PhysicsEngine();
            }
            return _instance;
        }
    }

    public void AddCollider(Collider collider)
    {
        _colliders.Add(collider);
    }
    public void RemoveCollider(Collider collider)
    {
        _colliders.Remove(collider);
    }

    /// <summary>
    /// Move and slide the physics object
    /// You should use this method at the end of the Update method of your object to make it work as expected
    /// This block is actually acting like a physics engine move_and_slide from Godot for example
    /// </summary>
    /// <param name="physicsObject"></param>
    /// <param name="gameTime"></param>
    /// <returns></returns>
    public List<Collision> MoveAndSlide(PhysicsObject physicsObject, GameTime gameTime)
    {
        // Update physics object
        physicsObject.Update(gameTime);
        List<Collision> collisions = new();
        if (physicsObject.Collider.ColliderType == ColliderType.Static)
        {
            return collisions;
        }
        // Check collision and solve it if physicsObject overlaps another collider
        foreach (Collider other in _colliders)
        {
            if (physicsObject.Collider != other)
            {
                if (_alreadyCollided.ContainsKey(physicsObject.Collider) && _alreadyCollided[physicsObject.Collider] == other)
                {
                    continue; //we dont process the same collision twice
                }
                Collision collision = Collides.CollideAndSolve(physicsObject.Collider, other, gameTime);
                if (collision != null)
                {
                    collisions.Add(collision);
                }
            }
        }
        return collisions;
    }

    /// <summary>
    /// 
    /// An abstraction of a simple update, the alreadyCollided behavior should be hidden inside the PhysicsEngine
    /// In order to keep the alreadyCollided hashmap clean, we need to call this method at the end of the Update method of your game
    /// </summary>
    /// <param name="gameTime"></param>
    public void Update(GameTime gameTime)
    {
        _alreadyCollided.Clear();
    }
}
