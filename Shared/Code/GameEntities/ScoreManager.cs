using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;

public class ScoreManager : GameEntity
{
    //singleton
    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ScoreManager();
            }
            return _instance;
        }
    }
    public int CurrentScore { get; private set; }

    private BitmapFont _font;
    private SoundEffect _earnPointSound;

    private ScoreManager() {}

    public void IncreaseScore()
    {
        CurrentScore++;
        _earnPointSound.Play();
    }

    public void LoadContent(ContentManager content)
    {
        CurrentScore = 0;
        _font = PreloadedAssets.Instance.mainFont;
        _earnPointSound = content.Load<SoundEffect>("sounds/sfx_point");
    }

    public void Update(GameTime gameTime)
    {
        
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var text = CurrentScore.ToString();
        var rect = _font.GetStringRectangle(text, Vector2.Zero);
        spriteBatch.DrawString(_font, text, new Vector2(Constants.WORLD_MIDDLE_SCREEN_WIDTH - rect.Width * .5f, 10), Color.White);
    }
}