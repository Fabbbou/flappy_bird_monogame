using Microsoft.Xna.Framework;

public class CirclCollider : Collider
{
    public float Radius { get; private set; }

    public override float Left => Position.X - Radius;

    public override float Right => Position.X + Radius ;

    public override float Top => Position.Y - Radius;

    public override float Bottom => Position.Y + Radius;

    public CirclCollider(PhysicsObject physicsObject, ColliderType collisionType, float radius) : base(physicsObject, collisionType)
    {
        Radius = radius;
    }

    public override bool CollidesWith(Collider other)
    {
        if (other is CirclCollider otherCircl)
        {
            return CirclVsCircl(otherCircl);
        }
        else if (other is RectCollider otherRect)
        {
            Vector2 closestPoint = new Vector2(
                MathHelper.Clamp(Position.X, otherRect.Left, otherRect.Right),
                MathHelper.Clamp(Position.Y, otherRect.Top, otherRect.Bottom)
            );
            float distance = Vector2.Distance(Position, closestPoint);
            return distance < Radius;
        }
        return false;
    }

    private bool CirclVsCircl(CirclCollider otherCircl)
    {
        float distance = Vector2.Distance(Position, otherCircl.Position);
        return distance < Radius + otherCircl.Radius;
    }
}