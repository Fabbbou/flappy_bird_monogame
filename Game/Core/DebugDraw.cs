using flappyrogue_mg.Game.Core.Collider;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace flappyrogue_mg.Game.Core
{
    public class DebugDraw
    {
        public static void Draw(PhysicsObject physicsObject, SpriteBatch spriteBatch, SpriteFont font, float scale)
        {
            spriteBatch.DrawString(font, $"Position: {physicsObject.Position}", new Vector2(10, 10), Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, $"Velocity: {physicsObject.Velocity}", new Vector2(10, 30), Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, $"Acceleration: {physicsObject.Acceleration}", new Vector2(10, 50), Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        //Rectangle Draw
        public static void Draw(Rectangle rectangle, SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.DrawRectangle(rectangle, color, 5);
        }
    }
}
