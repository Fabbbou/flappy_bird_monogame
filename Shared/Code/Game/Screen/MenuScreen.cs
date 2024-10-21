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
    private SpriteBatch _spriteBatch;
    private GumWindowResizer _gumWindowResizer;

    private GraphicalUiElement _gumScreen;
    private GraphicalUiElement BackgroundPic;
    private GraphicalUiElement MobileTopSkyRectangle;
    private GraphicalUiElement MobileBottomTreesRectangle;
    private GraphicalUiElement PlayButton;
    public MenuScreen(Game game) : base(game) {}

    public override void Initialize()
    {
        Game.IsMouseVisible = true;
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _gumScreen = MainRegistry.I.GetGumScreen("MenuScreen");
        _gumWindowResizer = new GumWindowResizer(GraphicsDevice, _gumScreen);

        BackgroundPic = _gumScreen.GetGraphicalUiElementByName("BackgroundPic");
        MobileTopSkyRectangle = _gumScreen.GetGraphicalUiElementByName("MobileTopSkyRectangle");
        MobileBottomTreesRectangle = _gumScreen.GetGraphicalUiElementByName("MobileBottomTreesRectangle");
        PlayButton = _gumScreen.GetGraphicalUiElementByName("PlayButton");
        var button = new GumTransparentButton();
        button.Push += OnClickPlayButton;
        PlayButton.Children.Add(button);
        Game.Window.ClientSizeChanged += OnClientResize;

        //calling it once to make sure the screen is properly resized on app startup
        OnClientResize(null, null);
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
        _gumScreen.RemoveFromManagers();
        Game.Window.ClientSizeChanged -= OnClientResize;
    }

    public override void UnloadContent()
    {
        _gumScreen.RemoveFromManagers();
        Game.Window.ClientSizeChanged -= OnClientResize;
    }

    private void OnClientResize(object sender, EventArgs e)
    {
        _gumWindowResizer.Resize();
        
        float scaleX = (float)GraphicsDevice.Viewport.Width / BackgroundPic.TextureWidth;
        float scaleY = (float)GraphicsDevice.Viewport.Height / BackgroundPic.TextureHeight;
        float currentScale = Math.Min(scaleX, scaleY);
        bool IsWideScreen = currentScale == scaleY;
        if (IsWideScreen)
        {
            //when the screen is a desktop (landscape) we want to maintain the aspect ratio of the background image
            BackgroundPic.WidthUnits = Gum.DataTypes.DimensionUnitType.MaintainFileAspectRatio;
            BackgroundPic.HeightUnits = Gum.DataTypes.DimensionUnitType.Percentage;
            MobileTopSkyRectangle.Visible = false;
            MobileBottomTreesRectangle.Visible = false;
        }
        else
        {
            //when the screen is phone (portrait) oriented
            BackgroundPic.WidthUnits = Gum.DataTypes.DimensionUnitType.Percentage;
            BackgroundPic.HeightUnits = Gum.DataTypes.DimensionUnitType.MaintainFileAspectRatio;
            MobileTopSkyRectangle.Visible = true;
            MobileBottomTreesRectangle.Visible = true;
        }
    }
}