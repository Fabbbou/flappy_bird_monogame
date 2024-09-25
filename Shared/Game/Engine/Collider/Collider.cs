using flappyrogue_mg.Core.Collider;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

public class Collider
{
    public  Color ColorDebugCollision = Constants.DEFAULT_DEBUG_COLOR_GIZMOS;
    public PhysicsObject PhysicsObject { get; private set; }
    public CollisionType CollisionType { get; private set; }
    public ColliderShape ColliderShape { get; private set; }
    public Vector2 Position => PhysicsObject.Position + ColliderShape.Offset;


    public Collider(PhysicsObject physicsObject, float width, float height, CollisionType colliderType, Vector2 offsetRelativePosition)
    {
        PhysicsEngine.Instance.AddCollider(this);
        PhysicsObject = physicsObject;
        ColliderShape = new Rect(offsetRelativePosition, new(width, height));
        CollisionType = colliderType;
    }

    ~Collider() => PhysicsEngine.Instance.RemoveCollider(this);

    public void DebugDraw(SpriteBatch spriteBatch)
    {
        if(ColliderShape is Rect)
        {
            //square collider
            spriteBatch.DrawRectangle(Position, ((Rect)ColliderShape).Size, ColorDebugCollision, 1);
        }
        //else if (ColliderShape is Circle)
        //{
        //    //circle collider
        //    spriteBatch.DrawCircle(Position, ((Circle)ColliderShape).Radius, 16, _colorDebugCollision, 1);
        //}
    }

    public void Kill()
    {
        PhysicsEngine.Instance.RemoveCollider(this);
    }
}
public enum CollisionSide
{
    None,
    Top,
    Bottom,
    Left,
    Right
}

public enum CollisionType
{
    Static,
    Moving
}