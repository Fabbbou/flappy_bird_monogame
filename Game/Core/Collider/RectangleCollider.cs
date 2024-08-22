using flappyrogue_mg.Game.Core.Collider;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

public class RectangleCollider : Collider
{
    public float Width { get; }
    public float Height { get; }
    public Rect Rect => new(RelativePosition+PhysicsObject.Position, new(Width, Height));

    public override Vector2 Center => Rect.Center;

    public RectangleCollider(PhysicsObject physicsObject, Vector2 position, float width, float height) : base(physicsObject, position)
    {
        Width = width;
        Height = height;
    }

    public override void DrawDebug(SpriteBatch spriteBatch, Color color)
    {
        spriteBatch.DrawRectangle(Rect.Render, color, 2);
    }

    public override string ToString()
    {
        return $"Rectangle: {Rect}";
    }

}