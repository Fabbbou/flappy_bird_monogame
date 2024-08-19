﻿using Microsoft.Xna.Framework;

//apply physics https://guide.handmadehero.org/code/day043/#1535
public class PhysicsObject
{
    public const float GRAVITY = 9.8f;
    public const float FrictionCoefficient = 0.1f;
    private Vector2 _position;
    public Collider Collider;
    public Vector2 Position
    {
        get => _position;
        private set
        {
            _position = value;
            if (Collider != null)
            {
                Collider.RelativePosition = value.ToPoint();
            }
        }
    }
    public Vector2 Velocity;
    public Vector2 Acceleration { get; private set; }

    public Vector2 Gravity { get; set; }


    public PhysicsObject(Vector2 initialPosition)
    {
        Position = initialPosition;
        Collider = null;
        Velocity = Vector2.Zero;
        Acceleration = Vector2.Zero;
        Gravity = new Vector2(0, GRAVITY);
    }

    public static PhysicsObject Rectangle(Vector2 initialPosition, int width, int height)
    {
        PhysicsObject physicsObject = new(initialPosition);
        RectangleCollider collider = new(physicsObject, Vector2.Zero.ToPoint(), width, height);
        physicsObject.Collider = collider;
        return physicsObject;
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
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Apply gravity
        ApplyForce(Gravity);
        // Apply friction
        Vector2 friction = -FrictionCoefficient * Velocity;
        ApplyForce(friction);
        // Update velocity
        Velocity += Acceleration * deltaTime;
        // Update position
        Position += Velocity * deltaTime + 0.5f * Acceleration * deltaTime * deltaTime;
        // Reset acceleration for the next frame
        Acceleration = Vector2.Zero;

        //Collider collider = ColliderRegistry.Instance.isAABBCollision(Collider);
        //if (collider != null)
        //{

        //}
    }
}
public enum ForceType
{
    Impulse,
    Continuous
}