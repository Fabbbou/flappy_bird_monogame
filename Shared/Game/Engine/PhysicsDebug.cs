
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

/// <summary>
/// PhysicsDebug is a singleton class that allows you to debug the physics of your game.
/// Just call it during your Draw method, and set the debug mode to true to see the colliders of your physics objects.
/// </summary>
public class PhysicsDebug
{
    private bool _isDebugging = false;
    private List<PhysicsObject> _objects = [];
    private PhysicsDebug(){}

    private static PhysicsDebug _instance;
    public static PhysicsDebug Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PhysicsDebug();
            }
            return _instance;
        }
    }

    public void SetDebug(bool isDebugging)
    {
        _isDebugging = isDebugging;
    }

    //add a physics object to the list of objects to debug
    public void AddObject(PhysicsObject physicsObject)
    {
        _objects.Add(physicsObject);
    }

    //remove a physics object from the list of objects to debug
    public void RemoveObject(PhysicsObject physicsObject)
    {
        _objects.Remove(physicsObject);
    }

    //draw the debug information of all physics objects
    public void Draw(SpriteBatch spriteBatch)
    {
        if (!_isDebugging)
        {
            return;
        }
        foreach (PhysicsObject physicsObject in _objects)
        {
            //draw the collider of the physics object
            physicsObject.Collider.DebugDraw(spriteBatch);
        }
    }
}