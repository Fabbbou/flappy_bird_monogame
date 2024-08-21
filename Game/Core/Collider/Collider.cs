using flappyrogue_mg.Game.Core.Collider;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System.Collections.Generic;

public abstract class Collider
{
    public PhysicsObject PhysicsObject { get; private set; }
    public Point RelativePosition;
    List<Collider> Colliders = new();

    public Collider(PhysicsObject physicsObject, Point position)
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
