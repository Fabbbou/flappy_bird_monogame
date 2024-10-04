
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Linq;

public class ClickRegistry
{
    private static ClickRegistry _instance;
    public static ClickRegistry Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ClickRegistry();
            }
            return _instance;
        }
    }
    private List<ClickableRegionHandler> _clickableRegionHandlers = new();
    private ClickRegistry() { }
    private bool _hasClicked = false;

    public void Add(ClickableRegionHandler clickableRegionHandler)
    {
        _clickableRegionHandlers.Add(clickableRegionHandler);
    }

    public void RemoveClickableRegionHandler(ClickableRegionHandler clickableRegionHandler)
    {
        _clickableRegionHandlers.Remove(clickableRegionHandler);
    }

    public void Update(GameTime gametime)
    {
        var copyList = _clickableRegionHandlers.ToList(); // make a copy so we dont have issues with removing elements during iteration 
        foreach (var clickableRegionHandler in copyList)
        {
            if (!clickableRegionHandler.IsActive) continue;
            if (clickableRegionHandler.IsPaused) continue;

#if WINDOWS || PC || LINUX
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 worldPosition = clickableRegionHandler.Camera.ScreenToWorld(new Vector2(mouseState.X, mouseState.Y));
                if (!_hasClicked && clickableRegionHandler.Contains(worldPosition))
                {
                    clickableRegionHandler.Click(gametime);
                    _hasClicked = true;
                }
            }
            else
            {
                _hasClicked = false;
            }
#elif ANDROID || IOS
            TouchCollection touchCollection = TouchPanel.GetState();
            foreach (TouchLocation touch in touchCollection)
            {
                var worldPosition = clickableRegionHandler.Camera.ScreenToWorld(touch.Position);
                if (touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved)
                {
                    if(!_hasClicked &&clickableRegionHandler.Contains(worldPosition))
                    {
                        clickableRegionHandler.Click(gametime);
                        _hasClicked = true;
                    }
                }
                else
                {
                    _hasClicked = false;
                }
            }
#endif
        }
    }

    private Vector2 GetWorldPositionClick(GameTime gametime, ClickableRegionHandler clickableRegionHandler)
    {
#if WINDOWS || PC || LINUX
            // Check for mouse input
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                return clickableRegionHandler.Camera.ScreenToWorld(new Vector2(mouseState.X, mouseState.Y));
            }
            else
            {
                return Vector2.Zero;
            }
#elif ANDROID || IOS
        // Check for touch input
        var touchCollection = TouchPanel.GetState();
        foreach (TouchLocation touch in touchCollection)
        {
            if (touch.State == TouchLocationState.Pressed)
            {
                var worldPosition = clickableRegionHandler.Camera.ScreenToWorld(touch.Position);
                return worldPosition;
            }
        }
        return Vector2.Zero;
#endif
    }

    public void Clear()
    {
        _clickableRegionHandlers.Clear();
    }
}