using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System.Collections.Generic;

public class ScreenRegistry
{
    private ScreenManager _screenManager { get; set; }
    private readonly Dictionary<ScreenName, GameScreen> _screens = new();
    public ScreenName CurrentScreen { get; private set; }

    public ScreenRegistry(Game game)
    {
        _screenManager = new ScreenManager();
        game.Components.Add(_screenManager);
        CurrentScreen = ScreenName.None;
    }
    public void AddScreen(ScreenName screen, GameScreen gameScreen)
    {
        _screens.Add(screen, gameScreen);
    }

    public void LoadScreen(ScreenName screen, Transition transition = null)
    {
        if(transition == null)
        {
            _screenManager.LoadScreen(_screens[screen]);
        }
        else
        {
            _screenManager.LoadScreen(_screens[screen], transition);
        }
        CurrentScreen = screen;
    }
}