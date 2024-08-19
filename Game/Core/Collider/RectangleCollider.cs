using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

public class RectangleCollider : Collider
{
    public int Width { get; }
    public int Height { get; }

    public Rectangle Rectangle => new(RelativePosition+PhysicsObject.Position.ToPoint(), new(Width, Height));

    public RectangleCollider(PhysicsObject physicsObject, Point position, int width, int height) : base(physicsObject, position)
    {
        Width = width;
        Height = height;
    }

    public override void DrawDebug(SpriteBatch spriteBatch, Color color)
    {
        spriteBatch.DrawRectangle(Rectangle, color, 5);
    }
}