using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

public class FullscreenRectangleEntity(GraphicsDevice graphicsDevice, Color color) : DrawableEntity
{
    private int ScreenWidth => graphicsDevice.Viewport.Width;
    private int ScreenHeight => graphicsDevice.Viewport.Height;

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.FillRectangle(new Rectangle(0, 0, ScreenWidth, ScreenHeight), color);
    }

    public override void LoadContent(ContentManager content) {}
}