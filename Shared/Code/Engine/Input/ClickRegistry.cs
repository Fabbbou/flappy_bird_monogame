
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using flappyrogue_mg.GameSpace;
using MonoGame.Extended;
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
    private ClickRegistry() {}
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
        var copyList = new List<ClickableRegionHandler>(_clickableRegionHandlers); ; // make a copy so we dont have issues with removing elements during iteration 
        foreach (var clickableRegionHandler in copyList)
        {
            if (!clickableRegionHandler.IsActive) continue;
            if (clickableRegionHandler.IsPaused) continue;

#if WINDOWS || PC || LINUX
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 worldPosition = MainRegistry.I.Camera.ScreenToWorld(new Vector2(mouseState.X, mouseState.Y));
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
                var worldPosition = MainRegistry.I.Camera.ScreenToWorld(touch.Position);
                if (touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved)
                {
                    if(!_hasClicked && clickableRegionHandler.Contains(worldPosition))
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


    public void Clear()
    {
        _clickableRegionHandlers.Clear();
    }
}