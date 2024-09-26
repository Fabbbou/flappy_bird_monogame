using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ECS;

public class Rect
{
    public float Width { get; private set; }
    public float Height { get; private set; }
    public Vector2 Size => new(Width, Height);
    public Rect(float width, float height)
    {
        Width = width;
        Height = height;
    }
}



