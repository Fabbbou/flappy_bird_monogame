using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using System;

public class ScaledGumWindowResizer(GameWindow gameWindow, GraphicsDevice graphicsDevice, GraphicalUiElement _gumScreen) : IDisposable
{
    private GraphicalUiElement _portaitMarginColorsInstance;
    private GraphicalUiElement _bgPicInstance;
    private GraphicalUiElement _backgroundPic;

    public void Dispose()
    {
        gameWindow.ClientSizeChanged -= Resize;
    }

    public void InitAndResizeOnce()
    {
        _portaitMarginColorsInstance = _gumScreen.GetGraphicalUiElementByName("PortaitMarginColorsInstance");
        _bgPicInstance = _gumScreen.GetGraphicalUiElementByName("BGPicInstance");
        _backgroundPic = _bgPicInstance.GetGraphicalUiElementByName("BackgroundPic");
        //calling it once to make sure the screen is properly resized on app startup
        Resize();
        gameWindow.ClientSizeChanged += Resize;
    }
    public void Resize(object not = null, EventArgs used = null)
    {
        GraphicalUiElement.CanvasWidth = graphicsDevice.Viewport.Width;
        GraphicalUiElement.CanvasHeight = graphicsDevice.Viewport.Height;
        _gumScreen.UpdateLayout();
        
        float scaleX = (float)graphicsDevice.Viewport.Width / _backgroundPic.TextureWidth;
        float scaleY = (float)graphicsDevice.Viewport.Height / _backgroundPic.TextureHeight;
        float currentScale = Math.Min(scaleX, scaleY);
        bool IsWideScreen = currentScale == scaleY;
        if (IsWideScreen)
        {
            _portaitMarginColorsInstance.SetProperty("MarginBoxesVisible", "Disabled");
            _bgPicInstance.SetProperty("ScreenMode", "Landscape");
        }
        else
        {
            _portaitMarginColorsInstance.SetProperty("MarginBoxesVisible", "Enabled");
            _bgPicInstance.SetProperty("ScreenMode", "Portrait");
        }
    }
}