using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Gum.Wireframe;
using GumFormsSample;

public class GameOverScreen : GameScreen
{
    private BackgroundGumWindowResizer _gumWindowResizer;
    private GraphicalUiElement _gumScreen;

    private ScoreManager.Score _score;

    public GameOverScreen(Game game) : base(game) { }

    public override void Initialize()
    {
        _gumScreen = MainRegistry.I.ChangeScreen("GameOverScreen");
        _gumWindowResizer = new BackgroundGumWindowResizer(Game.Window, _gumScreen);
        _gumWindowResizer.InitAndResizeOnce();

        GumTransparentButton.AttachButton("MenuButton", _gumScreen,  OnClickMenu);
        GumTransparentButton.AttachButton("PlayButton", _gumScreen, OnClickPlay);
    }
    public override void Dispose()
    {
        _gumWindowResizer.Dispose();
        base.Dispose();
    }

    public override void Update(GameTime gameTime) {}
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