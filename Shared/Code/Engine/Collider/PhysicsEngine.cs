using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class PhysicsEngine
{
    private static PhysicsEngine _instance;
    private readonly List<PhysicsObject> _physicsObjects = new List<PhysicsObject>();
    //a hashmap-like private field called alreadyCollided
    private Dictionary<PhysicsObject, PhysicsObject> _alreadyCollided = new();

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

    public void AddCollider(PhysicsObject physicsObject)
    {
        _physicsObjects.Add(physicsObject);
    }

    public void RemoveCollider(PhysicsObject physicsObject)
    {
        _physicsObjects.Remove(physicsObject);
    }

    /// <summary>
    /// Move and slide the physics object
    /// You should use this method at the end of the Update method of your object to make it work as expected
    /// This block is actually acting like a physics engine move_and_slide from Godot for example
    /// </summary>
    /// <param name="physicsObject"></param>
    /// <param name="gameTime"></param>
    /// <returns>All the PhysicsObject we collided with</returns>
    public List<PhysicsObject> MoveAndSlide(PhysicsObject physicsObject, GameTime gameTime)
    {
        // Update physics object
        physicsObject.Update(gameTime);
        List<PhysicsObject> collisions = new();
        if (physicsObject.Collider.CollisionType == CollisionType.Static) // Static objects dont collides with anything
        {
            return collisions;
        }
        // Check collision and solve it if physicsObject overlaps another collider
        foreach (PhysicsObject otherPhysicsObject in _physicsObjects)
        {
            if (physicsObject != otherPhysicsObject)
            {
                if (_alreadyCollided.ContainsKey(physicsObject) && _alreadyCollided[physicsObject] == otherPhysicsObject)
                {
                    continue; //we dont process the same collision twice
                }
                if (Collides.CollideAndSolve(physicsObject.Collider, otherPhysicsObject.Collider, gameTime))
                {
                    collisions.Add(otherPhysicsObject);
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
