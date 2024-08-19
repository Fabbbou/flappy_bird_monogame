using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

public abstract class Collider
{
    public PhysicsObject PhysicsObject { get; private set; }
    public Point RelativePosition;

    public Collider(PhysicsObject physicsObject, Point position)
    {
        PhysicsObject = physicsObject;
        RelativePosition = position;
    }

    //public abstract bool Intersects(Collider other);

    public virtual void DrawDebug(SpriteBatch spriteBatch, Color color)
    {
        spriteBatch.DrawPoint(PhysicsObject.Position, color, 5);
    }
}
