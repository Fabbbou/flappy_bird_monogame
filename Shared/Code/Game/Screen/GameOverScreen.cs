using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Screens;

using Gum.Wireframe;
using System;
using MonoGameGum.Forms;
using RenderingLibrary;
using GumFormsSample;

public class GameOverScreen : GameScreen
{
    private BackgroundGumWindowResizer _gumWindowResizer;
    private GraphicalUiElement _gumScreen;

    private SpriteBatch _spriteBatch;
    private BitmapFont _font;
    private ScoreManager.Score _score;

    public GameOverScreen(Game game) : base(game) { }

    public override void Initialize()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _gumScreen = MainRegistry.I.LoadGumScreen("GameOverScreen");
        _gumWindowResizer = new BackgroundGumWindowResizer(Game.Window, GraphicsDevice, _gumScreen);

        GumTransparentButton.AttachButton(_gumScreen.GetGraphicalUiElementByName("MenuButton"), OnClickMenu);
        GumTransparentButton.AttachButton(_gumScreen.GetGraphicalUiElementByName("PlayButton"), OnClickPlay);

        _gumWindowResizer.InitAndResizeOnce();
    }

    public override void UnloadContent()
    {
        //_gumScreen.RemoveFromManagers();
    }

    public override void Dispose()
    {
        _gumWindowResizer.Dispose();
        base.Dispose();
    }

    public override void Update(GameTime gameTime)
    {
        //gum update
        if (Game.IsActive) FormsUtilities.Update(gameTime, _gumScreen);
        SystemManagers.Default.Activity(gameTime.TotalGameTime.TotalSeconds);
    }
    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        SystemManagers.Default.Draw();
    }

    private void OnClickPlay(object not = null, EventArgs used = null)
    {
        MainRegistry.I.ScreenRegistry.LoadScreen(ScreenName.MainGameScreen);
    }

    private void OnClickMenu(object not = null, EventArgs used = null)
    {
        MainRegistry.I.ScreenRegistry.LoadScreen(ScreenName.MenuScreen);
    }
}