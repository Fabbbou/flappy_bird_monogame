using flappyrogue_mg.Core.Collider;
using Microsoft.Xna.Framework;

public class Collides
{
    public const float APPROX = 0.01f;
    public const int DIGIT_ROUND = 2;

    public static Collision Check(Collider rDynamic, Collider rStatic, GameTime gameTime)
    {
        CollisionSide side = rDynamic.CheckIfCollision(rStatic);
        if (side != CollisionSide.None)
        {
            return new Collision(side);
        }
        return null;
    }

    /// <summary>
    /// AABB collision detection and resolution
    /// 
    /// </summary>
    /// <param name="rDynamic"></param>
    /// <param name="rStatic"></param>
    /// <param name="gameTime"></param>
    /// <returns></returns>
    public static Collision CollideAndSolve(Collider rDynamic, Collider rStatic, GameTime gameTime)
    {
        CollisionSide side = rDynamic.CollidePostPhysics(rStatic);
        if (side != CollisionSide.None)
        {
            return new Collision(side);
        }
        return null;
    }
}
