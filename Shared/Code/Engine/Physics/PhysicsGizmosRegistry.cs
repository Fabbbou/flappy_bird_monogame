
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

/// <summary>
/// PhysicsDebug is a singleton class that allows you to debug the physics of your game.
/// Just call it during your Draw method, and set the debug mode to true to see the colliders of your physics objects.
/// </summary>
public class PhysicsGizmosRegistry
{
    private bool _isDebugging = false;
    private List<PhysicsObject> _objects = [];
    private PhysicsGizmosRegistry(){}

    private static PhysicsGizmosRegistry _instance;
    public static PhysicsGizmosRegistry Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PhysicsGizmosRegistry();
            }
            return _instance;
        }
    }

    public void DrawGizmos(bool isDebugging)
    {
        _isDebugging = isDebugging;
    }

    //add a physics object to the list of objects to debug
    public void AddObject(PhysicsObject physicsObject)
    {
        foreach (PhysicsObject obj in _objects)
        {
            if (obj == physicsObject)
            {
                throw new Exception("Physics object already exists in the list of objects to debug.");
            }
            if (obj.Label == physicsObject.Label)
            {
                throw new Exception("Physics object with the same label already exists in the list of objects to debug.");
            }
        }
        _objects.Add(physicsObject);
    }

    //remove a physics object from the list of objects to debug
    public void RemoveObject(PhysicsObject physicsObject)
    {
        if (_isDebugging) return;
        _objects.Remove(physicsObject);
    }

    //draw the debug information of all physics objects
    public void Draw(SpriteBatch spriteBatch)
    {
        if (!_isDebugging) return;
        foreach (PhysicsObject physicsObject in _objects)
        {
            //draw the collider of the physics object
            physicsObject.DebugDraw(spriteBatch);
        }
    }
}