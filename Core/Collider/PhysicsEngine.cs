using flappyrogue_mg.Core.Collider;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class PhysicsEngine
{
    private static PhysicsEngine _instance;
    private readonly List<Collider> colliders = new List<Collider>();

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
        colliders.Add(collider);
    }
    public void RemoveCollider(Collider collider)
    {
        colliders.Remove(collider);
    }

    /// <summary>
    /// Move and slide the physics object
    /// You should use this method at the end of the Update method of your object to make it work as expected
    /// This block is actually acting like a physics engine move_and_slide from Godot for example
    /// </summary>
    /// <param name="physicsObject"></param>
    /// <param name="gameTime"></param>
    /// <returns></returns>
    public Collision MoveAndSlide(PhysicsObject physicsObject, GameTime gameTime)
    {
        // Update physics object
        physicsObject.Update(gameTime);
        // Check collision and solve it if physicsObject overlaps another collider
        foreach (Collider other in colliders)
        {
            if (physicsObject.Collider != other)
            {
                return Collides.CollideAndSolve(physicsObject.Collider, other, gameTime);    
            }
        }
        return null;
    }

    public Collision CheckCollision(Collider collider, GameTime gameTime)
    {
        foreach (Collider other in colliders)
        {
            if (collider == other)
            {
                continue; // Skip self
            }
            return Collides.CollideAndSolve(collider, other, gameTime);
        }
        return null;
    }
}