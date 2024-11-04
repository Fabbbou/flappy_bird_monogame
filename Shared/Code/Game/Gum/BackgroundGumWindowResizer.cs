using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using RenderingLibrary;
using System;
using System.Diagnostics;

public class BackgroundGumWindowResizer(GameWindow gameWindow, GraphicalUiElement _gumScreen, Action onClientChanged = null) : IDisposable
{
    public void InitAndResizeOnce()
    {
        //calling it once to make sure the screen is properly resized on app startup
        Resize();
        gameWindow.ClientSizeChanged += Resize;
    }
    public void Resize(object not = null, EventArgs used = null)
    {
        MainRegistry.I.RefreshCenterScreen();
        var bg = _gumScreen.GetGraphicalUiElementByName("PortraitSkyAndTrees");
        if (bg != null)
        {
            if (MainRegistry.I.CurrentFrameScale.IsWideScreen)
            {
                bg.WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute;
                bg.Width = Constants.WORLD_WIDTH;
            }
            else
            {
                bg.HeightUnits = Gum.DataTypes.DimensionUnitType.Percentage;
                bg.Height = 100;
            }
        }
        onClientChanged?.Invoke();
        _gumScreen.UpdateLayout();
    }
    public void Dispose()
    {
        gameWindow.ClientSizeChanged -= Resize;
    }
}