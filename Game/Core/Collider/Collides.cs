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
        /// 
        /// 
        /// https://www.youtube.com/watch?v=8JJ-4JgR7Dg
        /// https://github.com/OneLoneCoder/Javidx9/blob/master/PixelGameEngine/SmallerProjects/OneLoneCoder_PGE_Rectangles.cpp
        /// </summary>
        /// <param name="rayOrigin"></param>
        /// <param name="rayDirection"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Collision RayVsRect(Vector2 rayOrigin, Vector2 rayDirection, RectangleCollider rect)
        {
            Rectangle target = rect.Rectangle;
            Vector2 tNear = (target.Location.ToVector2() - rayOrigin) / rayDirection;
            Vector2 tFar = (target.Location.ToVector2() + target.Size.ToVector2() - rayOrigin) / rayDirection;

            if(tNear.X > tFar.X)
            {
                float temp = tNear.X;
                tNear.X = tFar.X;
                tFar.X = temp;
            }
            if (tNear.Y > tFar.Y)
            {
                float temp = tNear.Y;
                tNear.Y = tFar.Y;
                tFar.Y = temp;
            }

            if(tNear.X > tFar.Y || tNear.Y > tFar.X)
                return null;

            float tHitFar = Math.Min(tFar.X, tFar.Y);
            if (tHitFar < 0)
                return null;

            float tHitNear = Math.Max(tNear.X, tNear.Y);
            if (tHitNear > 1f)
                return null;
            Vector2 contactPoint = rayOrigin + tHitNear * rayDirection;
            Vector2 contactNormal;
            if (tNear.X > tNear.Y)
            {
                if (rayDirection.X < 0)
                {
                    contactNormal = new Vector2(1, 0);
                }
                else
                {
                    contactNormal = new Vector2(-1, 0);
                }
            }else if (tNear.X < tNear.Y)
            {
                if (rayDirection.Y < 0)
                {
                    contactNormal = new Vector2(0, 1);
                }
                else
                {
                    contactNormal = new Vector2(0, -1);
                }
            }
            else
            {
                contactNormal = new Vector2(0, 0);
            }

            return new(contactPoint, contactNormal, tHitNear);
        }
    }
}

