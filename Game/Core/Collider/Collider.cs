using flappyrogue_mg.Game.Core.Collider;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System.Collections.Generic;

public abstract class Collider
{
    public PhysicsObject PhysicsObject { get; private set; }
    public Vector2 RelativePosition;
    List<Collider> Colliders = new();

    public Collider(PhysicsObject physicsObject, Vector2 position)
    {
        PhysicsObject = physicsObject;
        RelativePosition = position;
        ColliderRegistry.Instance.Register(this);
    }

    public virtual void DrawDebug(SpriteBatch spriteBatch, Color color)
    {
        spriteBatch.DrawPoint(PhysicsObject.Position, color, 5);
    }
}
