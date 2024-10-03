using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class FilledRectangle : DrawableEntity
{
    private Color _color;
    private Rectangle _rectangle;
    private Texture2D _pixelTexture;
    private GraphicsDevice _graphicsDevice;
    public FilledRectangle(GraphicsDevice graphicsDevice, Rectangle rectangle, Color color)
    {
        _rectangle = rectangle;
        _color = color;
        _graphicsDevice = graphicsDevice;
        
    }
    public override void LoadContent(ContentManager content)
    {
        // Create a 1x1 pixel texture
        _pixelTexture = new Texture2D(_graphicsDevice, 1, 1);
        _pixelTexture.SetData(new[] { _color }); //the color here is not important, as we will change it later
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_pixelTexture, _rectangle, _color);
    }
}