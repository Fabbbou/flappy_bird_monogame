using flappyrogue_mg.Core;
using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using System.Diagnostics;

public class PauseButton : GameEntity
{
    private static readonly Vector2 POSITION = new((int)Constants.START_POSITION_X_PAUSE_BUTTON, (int)Constants.START_POSITION_Y_PAUSE_BUTTON);
    private readonly Point SIZE = new(Constants.SPRITE_PAUSE_BUTTON_WIDTH, Constants.SPRITE_PAUSE_BUTTON_HEIGHT);

    private Texture2DRegion _pauseButtonTexture;
    private MainGameScreen _mainGameScreen;
    private ClickableRegionHandler _clickableRegionHandler;
    public PauseButton(MainGameScreen mainGameScreen)
    {
        _mainGameScreen = mainGameScreen;
        _clickableRegionHandler = new ClickableRegionHandler(_mainGameScreen.Camera, OnClick, new(POSITION.ToPoint(), SIZE));
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        spriteBatch.Draw(_pauseButtonTexture, POSITION, Color.White);
    }

    public override void LoadContent(ContentManager content)
    {
        _pauseButtonTexture = PreloadedAssets.Instance.PauseButton;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        _clickableRegionHandler.Update(gameTime);
    }

    public void OnClick()
    {
        _mainGameScreen.StateMachine.ChangeState(new PauseState(_mainGameScreen));
        Debug.WriteLine("Pause button clicked");
    }
}