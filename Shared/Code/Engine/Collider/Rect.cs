using Microsoft.Xna.Framework;


public class Rect : Shape
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



