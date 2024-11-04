using flappyrogue_mg.GameSpace;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

public class BackgroundGumHandler(GameWindow gameWindow, GraphicalUiElement _gumScreen, Action onClientChanged = null) : IDisposable
{
    public bool IsParallaxPaused = false;
    private GraphicalUiElement _parallaxBackground;
    private List<GraphicalUiElement> _pics = new();
    private GraphicalUiElement _lastPicMoved = null;
    private float WidthImg;
    public void InitAndResizeOnce()
    {
        _parallaxBackground = _gumScreen.GetGraphicalUiElementByName("ParallaxBackgroundInstance");
        WidthImg = _parallaxBackground.Width;
        _pics.Add(_parallaxBackground.GetGraphicalUiElementByName("Pic1"));
        _pics.Add(_parallaxBackground.GetGraphicalUiElementByName("Pic2"));
        _pics.Add(_parallaxBackground.GetGraphicalUiElementByName("Pic3"));
        _lastPicMoved = _pics[^1];
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

    public void AnimateParallax(GameTime gameTime)
    {
        if (IsParallaxPaused) return;
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        float speedParallax = Pipes.GlobalPipesSpeed * 0.3f * deltaTime;
        foreach (var pic in _pics)
        {
            pic.X -= speedParallax;
            if (pic.X < -WidthImg)
            {
                pic.X = _lastPicMoved.X + _lastPicMoved.Width;
                _lastPicMoved = pic;
            }
        }
    }

    public void Dispose()
    {
        gameWindow.ClientSizeChanged -= Resize;
    }
}