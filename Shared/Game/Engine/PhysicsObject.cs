﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

/// <summary>
/// Represents a physics object in the game world.
/// It has a position, velocity, acceleration, gravity, and friction.
/// Gravity is defaulted to 9.8f and friction is defaulted to 1f.
/// It can apply forces to itself and update its position based on the forces applied.
/// </summary>
public class PhysicsObject
{
    public const float GRAVITY = 9.8f;
    public const float FRICTION = 0f;
    public Collider Collider;
    public Vector2 Gravity;
    public Vector2 Position;
    public float X => Position.X;
    public float Y => Position.Y;
    public Vector2 Velocity;
    public Vector2 Acceleration;
    public Vector2 Friction;
    public string Label;
    public bool IsNotMoving => Velocity == Vector2.Zero && Acceleration == Vector2.Zero;
    public Color ColorDebugCollision = Constants.DEFAULT_DEBUG_COLOR_GIZMOS;

    public PhysicsObject(string label, float x, float y, float widthCollider, float heightCollider, CollisionType colliderType, Vector2 offsetCollider)
    {
        PhysicsDebug.Instance.AddObject(this);
        Label = label;
        Position = new(x, y);
        Collider = new(this, widthCollider, heightCollider, colliderType, offsetCollider);
        Velocity = Vector2.Zero;
        Acceleration = Vector2.Zero;
        Gravity = new Vector2(0, GRAVITY);
        Friction = new Vector2(FRICTION, FRICTION);
    }

    ~PhysicsObject()
    {
        PhysicsDebug.Instance.RemoveObject(this);
    }

    public void ApplyForce(Vector2 force)
    {
        ApplyForce(force, ForceType.Continuous);
    }

    public void ApplyForce(Vector2 force, ForceType forceType)
    {
        switch (forceType)
        {
            case ForceType.Impulse:
                Velocity += force;
                break;
            case ForceType.Continuous:
                Acceleration += force;
                break;
        }
    }

    public void Update(GameTime gameTime)
    {
        //if(Collider.CollisionType == CollisionType.Static) return;
        
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Apply gravity
        ApplyForce(Gravity);
        // Apply friction
        Vector2 friction = -Friction * Velocity;
        ApplyForce(friction);
        // Update velocity
        Velocity += Acceleration * deltaTime;
        // Update position
        Position += Velocity * deltaTime + 0.5f * Acceleration * deltaTime * deltaTime;
        // Reset acceleration for the next frame
        Acceleration = Vector2.Zero;
    }

    public void DebugDraw(SpriteBatch spriteBatch)
    {
        if (Collider.ColliderShape is Rect)
        {
            //square collider
            spriteBatch.DrawRectangle(Position, ((Rect)Collider.ColliderShape).Size, ColorDebugCollision, 1);
        }
    }

    public void Kill()
    {
        PhysicsDebug.Instance.RemoveObject(this);
        Collider.Kill();
    }
}
public enum ForceType
{
    Impulse,
    Continuous
}