using flappyrogue_mg.Core.Collider;
using Microsoft.Xna.Framework;

public class Collides
{

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
