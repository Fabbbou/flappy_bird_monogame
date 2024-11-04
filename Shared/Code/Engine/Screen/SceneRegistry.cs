using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using RenderingLibrary;
using System.Collections.Generic;
using System.Linq;

public class SceneRegistry
{
    private ScreenSave currentGumScreenSave;
    public GraphicalUiElement CurrentScreen { get; private set; }
    private ScreenManager _screenManager { get; set; } //lets say its a scene manager instead, I just cant rename it.
    private readonly Dictionary<SceneName, GameScreen> _screens = new();
    public SceneName CurrentScene { get; private set; }

    public SceneRegistry(Game game)
    {
        _screenManager = new ScreenManager();
        game.Components.Add(_screenManager);
        CurrentScene = SceneName.None;
    }
    public void AddScene(SceneName screen, GameScreen gameScreen)
    {
        _screens.Add(screen, gameScreen);
    }

    public void LoadScene(SceneName screen, Transition transition = null)
    {
        if(transition == null)
        {
            _screenManager.LoadScreen(_screens[screen]);
        }
        else
        {
            _screenManager.LoadScreen(_screens[screen], transition);
        }
        CurrentScene = screen;
    }


    private bool IsCurrentlyShown(string screenName)
    {
        ScreenSave newScreenElement = ObjectFinder.Self.GumProjectSave.Screens.FirstOrDefault(item => item.Name == screenName);

        bool isAlreadyShown = false;
        if (CurrentScreen != null)
        {
            isAlreadyShown = CurrentScreen.Tag == newScreenElement;
        }

        return isAlreadyShown;
    }

    public GraphicalUiElement ShowScreen(string screenName)
    {
        if (!IsCurrentlyShown(screenName))
        {
            ScreenSave newScreenElement = ObjectFinder.Self.GumProjectSave.Screens.FirstOrDefault(item => item.Name == screenName);
            currentGumScreenSave = newScreenElement;
            CurrentScreen?.RemoveFromManagers();
            var layers = SystemManagers.Default.Renderer.Layers;
            while (layers.Count > 1)
            {
                SystemManagers.Default.Renderer.RemoveLayer(SystemManagers.Default.Renderer.Layers.LastOrDefault());
            }

            CurrentScreen = currentGumScreenSave.ToGraphicalUiElement(SystemManagers.Default, addToManagers: true);
        }
        return CurrentScreen;
    }
}