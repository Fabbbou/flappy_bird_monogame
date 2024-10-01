using flappyrogue_mg.Core;
using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

public class PauseButton : GameEntity
{
    private Texture2DRegion _pauseButtonTexture;
    private MainGameScreen _mainGameScreen;
    private ClickableRegionHandler _clickableRegionHandler;
    public PauseButton(MainGameScreen mainGameScreen)
    {
        _mainGameScreen = mainGameScreen;
        _clickableRegionHandler = new ClickableRegionHandler(_mainGameScreen.Camera, OnClick, new(Constants.POSITION_PAUSE_BUTTON.ToPoint(), Constants.SIZE_PAUSE_BUTTON.ToPoint()));
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_pauseButtonTexture, Constants.POSITION_PAUSE_BUTTON, Color.White);
    }

    public override void LoadContent(ContentManager content)
    {
        _pauseButtonTexture = PreloadedAssets.Instance.PauseButton;
    }

    public override void Update(GameTime gameTime)
    {
        _clickableRegionHandler.Update(gameTime);
    }

    public void OnClick()
    {
        if(_mainGameScreen.StateMachine.CurrentState is PlayState)
            _mainGameScreen.StateMachine.ChangeState(new PauseState(_mainGameScreen));
    }
}