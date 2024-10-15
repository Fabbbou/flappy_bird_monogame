using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using static Constants;

public class GetReadyUI : DrawableEntity
{
    private Texture2DRegion _getReadyTitle;
    private Texture2DRegion _tapScreenTitle;

    public GetReadyUI(){}

    public override void Draw(SpriteBatch spriteBatch)
    {
        Extensions.SpriteBatchExtensions.Draw(spriteBatch, _getReadyTitle, SPRITE_POSITION_GETREADY_TITLE, LAYER_DEPTH_UI);
        Extensions.SpriteBatchExtensions.Draw(spriteBatch, _tapScreenTitle, SPRITE_POSITION_TAPSCREEN_TITLE, LAYER_DEPTH_UI);   
    }

    public override void LoadContent(ContentManager content)
    {
        _getReadyTitle = AssetsLoader.Instance.GetReadyTitle;
        _tapScreenTitle = AssetsLoader.Instance.TapScreenTitle;
    }
}