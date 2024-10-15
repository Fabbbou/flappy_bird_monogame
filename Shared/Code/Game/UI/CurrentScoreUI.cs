using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;

public class CurrentScoreUI : DrawableEntity
{
    private BitmapFont _font;
    public override void LoadContent(ContentManager content)
    {
        _font = AssetsLoader.Instance.Font;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var text = ScoreManager.Instance.CurrentScore.ToString();
        var textToRect = _font.GetStringRectangle(text, Vector2.Zero);
        spriteBatch.DrawString(_font, text, new Vector2(Constants.WORLD_MIDDLE_SCREEN_WIDTH - textToRect.Width * .5f, 10), Color.White, layerDepth:Constants.LAYER_DEPTH_UI);
    }
}