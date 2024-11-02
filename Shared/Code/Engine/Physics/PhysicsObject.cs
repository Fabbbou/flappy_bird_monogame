using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

/// <summary>
/// Represents a physics object in the game world.
/// It has a position, velocity, acceleration, gravity, and friction.
/// Gravity is defaulted to 9.8f and friction is defaulted to 1f.
/// It can apply forces to itself and update its position based on the forces applied.
/// </summary>
public class PhysicsObject : Gizmo
{
    public const float GRAVITY = 9.8f;
    public const float FRICTION = 0f;
    public readonly GraphicalUiElement GraphicalUiElement; //nullable
    private readonly GumGizmo _gumGizmo;
    private Collider _collider;
    public Collider Collider
    {
        get => _collider;
        //protecting collider from being set multiple times
        set
        {
            if (_collider != null)
            {
                throw new Exception("Collider already attached to this PhysicsObject");
            }
            _collider = value;
            GizmosRegistry.Instance.Add(this);
            PhysicsEngine.Instance.AddCollider(this);
        }
    }
    public Vector2 Gravity;
    private Vector2 _position;
    public Vector2 Position
    {
        get => _position;
        set
        {
            _position = value;
            if (GraphicalUiElement != null && Collider.CollisionType != ColliderType.Static)
            {
                GraphicalUiElement.X = value.X;
                GraphicalUiElement.Y = value.Y;
            }
        }
    }

    public Vector2 Velocity;
    public Vector2 Acceleration;
    public Vector2 Friction;
    public bool IsNotMoving => Velocity == Vector2.Zero && Acceleration == Vector2.Zero;

    private string _label;
    public string Label => _label;

    public PhysicsObject(string label, float x, float y, ColliderType colliderType, GraphicalUiElement graphicalUiElement = null, GumGizmo gumGizmo = null)
    {
        _label = label;
        Position = new(x, y);
        Velocity = Vector2.Zero;
        Acceleration = Vector2.Zero;
        Gravity = new Vector2(0, GRAVITY);
        Friction = new Vector2(FRICTION, FRICTION);
        GraphicalUiElement = graphicalUiElement;
        _gumGizmo = gumGizmo;
    }

    ~PhysicsObject()
    {
        Kill();
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
        if (ColliderType.Static != Collider.CollisionType)
        {
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
        _gumGizmo?.Update();
    }

    public void DrawGizmo(SpriteBatch spriteBatch)
    {
        if(_gumGizmo == null)
        {
            if (Collider is RectCollider rect)
            {
                spriteBatch.DrawRectangle(rect.Position, rect.Size, Constants.COLOR_DEFAULT_DEBUG_GIZMOS, layerDepth: Constants.LAYER_DEPTH_DEBUG);
            }else if (Collider is CirclCollider circl)
            {
                spriteBatch.DrawCircle(circl.Position, circl.Radius, 16, Constants.COLOR_DEFAULT_DEBUG_GIZMOS, layerDepth: Constants.LAYER_DEPTH_DEBUG);
            }
        }
        _gumGizmo?.Update();
    }

    public void Kill()
    {
        _gumGizmo?.Deactivate();
        GizmosRegistry.Instance.RemoveObject(this);
        PhysicsEngine.Instance.RemoveCollider(this);
    }

    public override string ToString()
    {
        // all fields are public, so we can access them directly
        return $"PhysicsObject {Label} at {Position} with velocity {Velocity}";
    }
}
public enum ForceType
{
    Impulse,
    Continuous
}