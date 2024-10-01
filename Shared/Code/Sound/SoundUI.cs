using flappyrogue_mg.Core;
using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using static Constants;

public class SoundUI : GameEntity
{
    private const int GAP_BARS = 5;
    private MainGameScreen _mainGameScreen;
    private Texture2DRegion _minusButton;
    private Texture2DRegion _plusButton;
    private Texture2DRegion _barSound;
    private Texture2DRegion _barSoundEmpty;
    private Texture2DRegion _logoMusic;
    private Texture2DRegion _logoFx;

    private Texture2DRegion _okButton;
    private ClickableRegionHandler _okClickableRegionHandler;

    private FilledRectangle _background;
    public SoundUI(MainGameScreen mainGameScreen)
    {
        IsActive = false;
        _mainGameScreen = mainGameScreen;
        _okClickableRegionHandler = new ClickableRegionHandler(mainGameScreen.Camera, OnClickOk, new(POSITION_OK_BUTTON.ToPoint(), POSITION_OK_BUTTON.ToPoint()));
        _background = new FilledRectangle(mainGameScreen.GraphicsDevice, new Rectangle(0, 0, WORLD_WIDTH, WORLD_HEIGHT), new Color(.6f, .6f, .6f, .9f)); 
    }

    public override void LoadContent(ContentManager content)
    {
        _background.LoadContent(content);
        _minusButton = PreloadedAssets.Instance.MinusButton;
        _plusButton = PreloadedAssets.Instance.PlusButton;
        _barSound = PreloadedAssets.Instance.BarSound;
        _barSoundEmpty = PreloadedAssets.Instance.BarSoundEmpty;
        _logoMusic = PreloadedAssets.Instance.LogoMusic;
        _logoFx = PreloadedAssets.Instance.LogoFx;
        _okButton = PreloadedAssets.Instance.OkButton;
    }

    public override void Update(GameTime gameTime)
    {
        _okClickableRegionHandler.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _background.Draw(spriteBatch);
        DrawFxSound(spriteBatch);
        spriteBatch.Draw(_okButton, POSITION_OK_BUTTON, Color.White);
        spriteBatch.Draw(_logoFx, new Vector2(WORLD_WIDTH * 0.5f - SIZE_BAR_SOUND.X * 5 - SIZE_LOGO_FX.X - 4 * GAP_BARS, WORLD_HEIGHT*0.5f - SIZE_LOGO_FX.Y*0.5f), Color.White);
    }

    //draw 5 bars when fx sound is 100 pourcent, and empty bars when it is less
    private void DrawFxSound(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < 5; i++)
        {
            spriteBatch.Draw(_barSound, new Vector2(WORLD_WIDTH * 0.5f - 50 + i * GAP_BARS, WORLD_HEIGHT * 0.5f - SIZE_BAR_SOUND.Y * 0.5f), Color.White);
        }
    }   

    public void OnClickOk()
    {
        _mainGameScreen.StateMachine.ChangeState(new PlayState(_mainGameScreen));
    }
}