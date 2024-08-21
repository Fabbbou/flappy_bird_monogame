using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flappyrogue_mg.Game.Core.Collider
{
    public class Collision
    {
        public RayVsRectCollision RayVsRectCollision { get; private set; }
        public RectangleCollider RectangleCollider { get; private set; }

        public Collision(RayVsRectCollision rayVsRectCollision, RectangleCollider rectangleCollider)
        {
            RayVsRectCollision = rayVsRectCollision;
            RectangleCollider = rectangleCollider;
        }

        public override string ToString() {
            return $"RayVsRectCollision: {RayVsRectCollision}, RectangleCollider: {RectangleCollider}";
        }   
    }
}
