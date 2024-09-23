using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

public class Rect : ColliderShape
{
    public float Width { get; private set; }
    public float Height { get; private set; }
    public Vector2 Size => new(Width, Height);

    public Rect(Vector2 offset, Vector2 size)
    {
        Offset = offset;
        Width = size.X;
        Height = size.Y;
    }

    public override Rect GetBoundingBox()
    {
        return this;
    }

    public override bool Intersects(Vector2 position, Collider other)
    {
        //throw exception is other is not a rect
        if (other.ColliderShape is not Rect)
        {
            throw new System.Exception("The other collider is not a Rect.");
        }
        Rect otherR = (Rect)other.ColliderShape;
        return position.X < other.Position.X + otherR.Width &&
                position.X + Width > other.Position.X &&
                position.Y < other.Position.Y + otherR.Height &&
                position.Y + Height > other.Position.Y;
    }
}
