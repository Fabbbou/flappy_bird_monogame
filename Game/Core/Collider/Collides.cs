using flappyrogue_mg.Game.Core.Collider;
using Microsoft.Xna.Framework;
using System;

public class Collides
{
    public const float APPROX = 0.01f;
    public const int DIGIT_ROUND = 2;

    public static Collision AABB(Collider rDynamic, Collider rStatic, GameTime gameTime)
    {
        CollisionSide side = rDynamic.HandleCollision(rStatic);
        if (side != CollisionSide.None)
        {
            return new Collision(side);
        }
        return null;
    }

    public static float Round(float value)
    {
        //return a round of value by DIGIT_ROUND
        return (float)Math.Round(value, DIGIT_ROUND);
    }
    public static Vector2 Round(Vector2 vector)
    {
        return new Vector2(Collides.Round(vector.X), Collides.Round(vector.Y));
    }
}
