using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using GumFormsSample;
using Gum.Wireframe;

public class MenuScreen : GameScreen
{
    private BackgroundGumWindowResizer _gumWindowResizer;

    private GraphicalUiElement _gumScreen;

    private GraphicalUiElement PlayButton;
    public MenuScreen(Game game) : base(game) {}

    public override void Initialize()
    {
        _gumScreen = MainRegistry.I.ChangeScreen("MenuScreen");
        _gumWindowResizer = new BackgroundGumWindowResizer(Game.Window, _gumScreen);
        _gumWindowResizer.InitAndResizeOnce();
        GumTransparentButton.AttachButton("PlayButton", _gumScreen, actionClicked: OnClickPlayButton);
    }
    private void OnClickPlayButton() => MainRegistry.I.SceneRegistry.LoadScene(SceneName.MainGameScreen);

    public override void Update(GameTime gameTime){}
    public override void Draw(GameTime gameTime){}

    public override void Dispose()
    {
        _gumWindowResizer.Dispose();
    }

    public override void UnloadContent(){}
}