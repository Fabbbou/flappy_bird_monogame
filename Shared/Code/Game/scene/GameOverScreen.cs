using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Gum.Wireframe;
using GumFormsSample;

public class GameOverScreen : GameScreen
{
    private BackgroundGumHandler _backgroundHandler;
    private GraphicalUiElement _gumScreen;

    private ScoreManager.Score _score;

    public GameOverScreen(Game game) : base(game) { }

    public override void Initialize()
    {
        _gumScreen = MainRegistry.I.ChangeScreen("GameOverScreen");
        _backgroundHandler = new BackgroundGumHandler(Game.Window, _gumScreen);
        _backgroundHandler.InitAndResizeOnce();

        GumTransparentButton.AttachButton("MenuButton", _gumScreen,  OnClickMenu);
        GumTransparentButton.AttachButton("PlayButton", _gumScreen, OnClickPlay);
    }
    public override void Dispose()
    {
        _backgroundHandler.Dispose();
        base.Dispose();
    }

    public override void Update(GameTime gameTime) 
    {
        _backgroundHandler.AnimateParallax(gameTime);
    }
    public override void Draw(GameTime gameTime){}

    private void OnClickPlay()
    {
        MainRegistry.I.SceneRegistry.LoadScene(SceneName.MainGameScreen);
    }

    private void OnClickMenu()
    {
        MainRegistry.I.SceneRegistry.LoadScene(SceneName.MenuScreen);
    }
}