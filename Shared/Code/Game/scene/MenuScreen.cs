using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using GumFormsSample;
using Gum.Wireframe;

public class MenuScreen : GameScreen
{
    private BackgroundGumHandler _backgroundHandler;

    private GraphicalUiElement _gumScreen;

    private GraphicalUiElement PlayButton;
    public MenuScreen(Game game) : base(game) {}

    public override void Initialize()
    {
        _gumScreen = MainRegistry.I.ChangeScreen("MenuScreen");
        _backgroundHandler = new BackgroundGumHandler(Game.Window, _gumScreen);
        _backgroundHandler.InitAndResizeOnce();
        GumTransparentButton.AttachButton("PlayButton", _gumScreen, actionClicked: OnClickPlayButton);
    }
    private void OnClickPlayButton() => MainRegistry.I.SceneRegistry.LoadScene(SceneName.MainGameScreen);

    public override void Update(GameTime gameTime)
    {
        _backgroundHandler.AnimateParallax(gameTime);
    }
    public override void Draw(GameTime gameTime){}

    public override void Dispose()
    {
        _backgroundHandler.Dispose();
    }

    public override void UnloadContent(){}
}