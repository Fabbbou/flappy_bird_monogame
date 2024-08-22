using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flappyrogue_mg.Core.Collider
{
    public class Collision
    {
        public CollisionSide CollisionSide { get; private set; }

        public Collision(CollisionSide collisionSide)
        {
            CollisionSide = collisionSide;
        }
    }
}
