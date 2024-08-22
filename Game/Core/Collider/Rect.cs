using Microsoft.Xna.Framework;

namespace flappyrogue_mg.Game.Core.Collider
{
    public class Rect
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Width { get; private set; }
        public float Height { get; private set; }

        public Vector2 Position => new(X, Y);
        public Vector2 Size => new(Width, Height);
        public Vector2 Center => new(X + Width / 2, Y + Height / 2);

        public Rectangle Render => new((int)X, (int)Y, (int)Width, (int)Height);

        public Rect(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Rect(Vector2 position, Vector2 size)
        {
            X = position.X;
            Y = position.Y;
            Width = size.X;
            Height = size.Y;
        }
        public bool Contains(Vector2 point)
        {
            return point.X >= X && point.X <= X + Width && point.Y >= Y && point.Y <= Y + Height;
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}, Width: {Width}, Height: {Height}";
        }
    }
}
