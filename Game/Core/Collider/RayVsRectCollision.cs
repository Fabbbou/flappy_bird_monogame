using flappyrogue_mg.Game.Core.Collider;
using Microsoft.Xna.Framework;

public class RayVsRectCollision
{
    public Vector2 Normal { get; private set; }
    public Vector2 Position { get; private set; }
    public float THitNear { get; private set; }

    public Vector2 RayOrigin { get; private set;}
    public Vector2 RayDirection { get; private set;}
    public Rect Target { get; private set; }

    public RayVsRectCollision(Vector2 rayOrigin, Vector2 rayDirection, Rect target, Vector2 normal, Vector2 position, float tHitNear)
    {
        RayOrigin = rayOrigin;
        RayDirection = rayDirection;
        Target = target;
        Normal = normal;
        Position = position;
        THitNear = tHitNear;
    }

    //ToString() with names of all fields
    public override string ToString()
    {
        return $"RayOrigin: {RayOrigin}, RayDirection: {RayDirection.ToPoint()}, Target: {Target}, Normal: {Normal}, Position: {Position}, THitNear: {THitNear}";
    }
}
