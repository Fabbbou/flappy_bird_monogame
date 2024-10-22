using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using static Constants;
using Extensions;
using MonoGame.Extended;
using GumFormsSample;
using Gum.Wireframe;
using MonoGameGum.Forms;
using RenderingLibrary;
using System;

public class MenuScreen : GameScreen
{
    private ScaledGumWindowResizer _gumWindowResizer;

    private GraphicalUiElement _gumScreen;

    private GraphicalUiElement PlayButton;
    public MenuScreen(Game game) : base(game) {}

    public override void Initialize()
    {
        _gumScreen = MainRegistry.I.LoadGumScreen("MenuScreen");
        _gumWindowResizer = new ScaledGumWindowResizer(Game.Window, GraphicsDevice, _gumScreen);

        PlayButton = _gumScreen.GetGraphicalUiElementByName("PlayButton");
        var button = new GumTransparentButton();
        button.Push += OnClickPlayButton;
        PlayButton.Children.Add(button);

        _gumWindowResizer.InitAndResizeOnce();
    }
    private void OnClickPlayButton(object not, EventArgs used) => MainRegistry.I.ScreenRegistry.LoadScreen(ScreenName.MainGameScreen);

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

    public override void Dispose()
    {
        _gumWindowResizer.Dispose();
    }

    public override void UnloadContent()
    {
        _gumScreen.RemoveFromManagers();
    }
}