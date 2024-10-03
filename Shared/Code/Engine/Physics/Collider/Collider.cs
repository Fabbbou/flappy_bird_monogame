using Microsoft.Xna.Framework;
using System;

public abstract class Collider
{
    public PhysicsObject PhysicsObject { get; private set; }
    public ColliderType CollisionType { get; private set; }

    public Action _onCollisionAction;
    private bool _isTriggered = false;
    // Define an Action field to store the function
    public Action OnCollisionAction
    {
        get => _onCollisionAction;
        set
        {
            if (CollisionType != ColliderType.AreaCastTrigger)
            {
                throw new Exception("You can't set an OnCollisionAction on another collider than AreaCastTrigger collider");
            }
            _onCollisionAction = value;
        }
    }

    // Method to trigger the action
    public void TriggerCollision()
    {
        if (_isTriggered) return;
        if (CollisionType != ColliderType.AreaCastTrigger)
        {
            throw new Exception("You can't trigger a collision on another collider than AreaCastTrigger collider");
        }
        OnCollisionAction?.Invoke();
        _isTriggered = true;
    }

    public Vector2 Position
    {
        get => PhysicsObject.Position;
        set => PhysicsObject.Position = value;
    }

    protected Collider(PhysicsObject physicsObject, ColliderType collisionType)
    {
        PhysicsObject = physicsObject;
        CollisionType = collisionType;
    }

    public abstract float Left { get; }
    public abstract float Right { get; }
    public abstract float Top { get; }
    public abstract float Bottom { get; }

    public abstract bool CollidesWith(Collider other);

    public void StopHorizontal()
    {
        PhysicsObject.Velocity = new Vector2(0, PhysicsObject.Velocity.Y);
    }
}

public enum ColliderType
{
    Static,
    Moving,
    AreaCastTrigger
}