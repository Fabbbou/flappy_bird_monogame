using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using RenderingLibrary;
using MonoGame.Extended;

public class ClickableRegionHandler
{
    private Rectangle _clickableRegion;
    private Action _onRegionClicked;
    private OrthographicCamera _camera;
    private bool _hasClicked;
    public ClickableRegionHandler(OrthographicCamera camera, Action onRegionClicked, Rectangle clickableRegion)
    {
        _clickableRegion = clickableRegion;
        _onRegionClicked = onRegionClicked;
        _camera = camera;
    }

    public void Update(GameTime gameTime)
    {
#if WINDOWS || PC || LINUX
        // Check for mouse input
        var mouseState = Mouse.GetState();
        var worldPosition = _camera.ScreenToWorld(new Vector2(mouseState.X, mouseState.Y));
        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            if (!_hasClicked && _clickableRegion.Contains(worldPosition))
            {
                _hasClicked = true;
                _onRegionClicked();
            }
        }
        else
        {
            _hasClicked = false;    
        }
#elif ANDROID || IOS
        // Check for touch input
        var touchCollection = TouchPanel.GetState();
        foreach (var touch in touchCollection)
        {
            if (touch.State == TouchLocationState.Pressed)
            {
                Point touchPosition = new Point((int)touch.Position.X, (int)touch.Position.Y);
                if (!_hasClicked && _clickableRegion.Contains(touchPosition))
                {
                    _hasClicked = true;
                    _onRegionClicked();
                }
            }else{
                _hasClicked = false;
            }
        }
#endif
    }
}