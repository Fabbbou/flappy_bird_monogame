using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

namespace Extensions
{
    public static class SpriteBatchExtensions
    {
        // An extension method for Texture2DRegion with additional parameters
        public static void Draw(this SpriteBatch spriteBatch, Texture2DRegion texture, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            spriteBatch.Draw(texture.Texture, position, texture.Bounds, color, rotation, origin, scale, effects, layerDepth);
        }

        //an extension method for texture2DRegion
        public static void Draw(this SpriteBatch spriteBatch, Texture2DRegion texture, Vector2 position, float layerDepth)
        {
            spriteBatch.Draw(texture.Texture, position, texture.Bounds, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layerDepth);
        }

        public static void Draw(this SpriteBatch spriteBatch, Texture2DRegion texture, Vector2 position, float scale, float layerDepth)
        {
            spriteBatch.Draw(texture.Texture, position, texture.Bounds, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, layerDepth);
        }
    }
}