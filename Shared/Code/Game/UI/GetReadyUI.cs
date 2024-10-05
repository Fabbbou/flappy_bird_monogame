using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using static Constants;

public class GetReadyUI : GameEntity
{
    private Texture2DRegion _getReadyTitle;
    private Texture2DRegion _tapScreenTitle;

    public GetReadyUI(){}

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_getReadyTitle, SPRITE_POSITION_GETREADY_TITLE, Color.White);
        spriteBatch.Draw(_tapScreenTitle, SPRITE_POSITION_TAPSCREEN_TITLE, Color.White);   
    }

    public override void LoadContent(ContentManager content)
    {
        _getReadyTitle = AssetsLoader.Instance.GetReadyTitle;
        _tapScreenTitle = AssetsLoader.Instance.TapScreenTitle;
    }

    public override void Update(GameTime gameTime)
    {
    }
}