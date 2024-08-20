using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flappyrogue_mg.Game.Core.Collider
{
    public class Collides
    {
        public static bool RectVsRect(RectangleCollider a, RectangleCollider b)
        {
            return a.Rectangle.Intersects(b.Rectangle);
        }

        public static bool PointVsRect(Point a, RectangleCollider b)
        {
            return b.Rectangle.Contains(a);
        }

        /// <summary>
        /// A Ray Cast to a Rectangle. The Collision object is the FIRST collision point.
        /// This algorithm is inspired from a YT video from OneLoneCoder.
        /// https://www.youtube.com/watch?v=8JJ-4JgR7Dg
        /// https://github.com/OneLoneCoder/Javidx9/blob/master/PixelGameEngine/SmallerProjects/OneLoneCoder_PGE_Rectangles.cpp
        ///
        /// </summary>
        /// <param name="rayOrigin">origin point of the ray</param>
        /// <param name="rayDirection">the direction vecttor of the ray (not the point at the and on the ray)</param>
        /// <param name="rect">the rectangle to collide with</param>
        /// <returns>
        ///     A collision object if there is a collision between the rectangle and the ray
        ///     null if there is no collision.
        /// </returns>
        public static Collision RayVsRect(Vector2 rayOrigin, Vector2 rayDirection, RectangleCollider rect)
        {
            Rectangle target = rect.Rectangle; //the target is the rectangle origin i.e. the top left corner
            //nearTimeIntersectionPoint.X is a pourcentage of the ray direction on the x axis (between 0 and 1) for the nearest intersection point
            //farTimeIntersectionPoint.X is a pourcentage of the ray direction on the x axis (between 0 and 1) for the farthest intersection point
            Vector2 nearTimeIntersectionPoint = (target.Location.ToVector2() - rayOrigin) / rayDirection; //is known as tNear or Nx and Ny in the video
            Vector2 farTimeIntersectionPoint = (target.Location.ToVector2() + target.Size.ToVector2() - rayOrigin) / rayDirection; //is known as tFar is Fx and Fy in the video

            //swaping the values if the near is greater than the far
            // i.e. we make sure near is always the nearest intersection point (and far the farthest)
            if (nearTimeIntersectionPoint.X > farTimeIntersectionPoint.X)
            {
                float temp = nearTimeIntersectionPoint.X;
                nearTimeIntersectionPoint.X = farTimeIntersectionPoint.X;
                farTimeIntersectionPoint.X = temp;
            }
            if (nearTimeIntersectionPoint.Y > farTimeIntersectionPoint.Y)
            {
                float temp = nearTimeIntersectionPoint.Y;
                nearTimeIntersectionPoint.Y = farTimeIntersectionPoint.Y;
                farTimeIntersectionPoint.Y = temp;
            }

            //the rule from the video is
            // Nx < Fy && Ny < Fx
            if (nearTimeIntersectionPoint.X > farTimeIntersectionPoint.Y || nearTimeIntersectionPoint.Y > farTimeIntersectionPoint.X)
                return null;

            // Furthest 'time' is contact on opposite side of target
            float tHitFar = Math.Min(farTimeIntersectionPoint.X, farTimeIntersectionPoint.Y);
            // Reject if ray direction is pointing away from object
            if (tHitFar < 0)
                return null;

            // Closest 'time' will be the first contact
            float tHitNear = Math.Max(nearTimeIntersectionPoint.X, nearTimeIntersectionPoint.Y);
            // Means the ray is ending before the rectangle (1 is a pourcent of the ray direction)
            //i.e. the first hit on the rectangle will be after the end of the ray
            if (tHitNear > 1f)
                return null;

            //Computing the contact point
            Vector2 contactNormal;
            if (nearTimeIntersectionPoint.X > nearTimeIntersectionPoint.Y)
                contactNormal = rayDirection.X < 0 ? new Vector2(1, 0) : new Vector2(-1, 0);
            else if (nearTimeIntersectionPoint.X < nearTimeIntersectionPoint.Y)
                contactNormal = rayDirection.Y < 0 ? new Vector2(0, 1) : new Vector2(0, -1);
            else
                contactNormal = new Vector2(0, 0);

            //Computing the contact point
            Vector2 contactPoint = rayOrigin + tHitNear * rayDirection;

            return new Collision(contactPoint, contactNormal, tHitNear);
        }
    }
}

