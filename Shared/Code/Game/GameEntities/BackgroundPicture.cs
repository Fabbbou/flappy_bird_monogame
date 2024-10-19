using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using Extensions;

public class BackgroundPicture : GameEntity
{

    private Texture2DRegion _background;

    public override void Draw(SpriteBatch spriteBatch)
    {

        spriteBatch.Draw(_background, Vector2.Zero, Constants.LAYER_DEPTH_INGAME);
    }

    public override void LoadContent(ContentManager content)
    {
        _background = AssetsLoader.Instance.Background;
    }

    public override void Update(GameTime gameTime)
    {
    }
}