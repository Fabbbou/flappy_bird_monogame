using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using GumFormsSample;
using GumRuntime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using RenderingLibrary;
using System;
using ToolsUtilities;

public class MainRegistry : IDisposable
{
    //singleton
    private static MainRegistry _instance;
    public static MainRegistry I
    {
        get
        {
            _instance ??= new MainRegistry();
            return _instance;
        }
    }
    public Game Game { get; private set; }
    public TouchscreenCursor TouchscreenCursor = new TouchscreenCursor();
    public GraphicsDevice GraphicsDevice { get; private set; }
    public ScreenRegistry ScreenRegistry { get; private set; }
    public GumProjectSave GumProject;
    private FrameScaler _frameScaler;
    public FrameScaler.FrameScale CurrentFrameScale => _frameScaler.CurrentFrameScale;
    public void RefreshScale() => _frameScaler.RefreshCurrentScale();

    public void RefreshCenterScreen()
    {
        GraphicalUiElement.CanvasWidth = GraphicsDevice.Viewport.Width / CurrentFrameScale.Scale;
        GraphicalUiElement.CanvasHeight = GraphicsDevice.Viewport.Height / CurrentFrameScale.Scale;
        SystemManagers.Default.Renderer.Camera.Zoom = CurrentFrameScale.Scale;
    }
    public void Initialize(Game game, GraphicsDevice graphicsDevice, ScreenRegistry screenRegistry, float virtualWidth, float virtualHeight, string gumProjectPath = null)
    {
        Game = game;
        GraphicsDevice = graphicsDevice;
        ScreenRegistry = screenRegistry;
        _frameScaler = new FrameScaler(graphicsDevice, Game.Window, virtualWidth, virtualHeight);
        if (gumProjectPath != null)
        {
            //FileManager.RelativeDirectory = "Content/Gum/";
            //Gum init
            GumProject = GumProjectSave.Load(gumProjectPath);
            ObjectFinder.Self.GumProjectSave = GumProject;
            GumProject.Initialize();
        }
    }

    public GraphicalUiElement LoadGumScreen(string screenName, bool andLoadTheScreen = true)
    {
        return GumProject.Screens.Find(item => item.Name == screenName).ToGraphicalUiElement(SystemManagers.Default, addToManagers: andLoadTheScreen);
    }

    public Matrix GetScaleMatrix()
    {
        return SystemManagers.Default.Renderer.Camera.GetTransformationMatrix();
    }

    public Point PointToScreen(int x, int y)
    {
        Matrix matrix = Matrix.Invert(GetScaleMatrix());
        return Vector2.Transform(new Vector2(x, y), matrix).ToPoint();
    }

    public void Dispose()
    {
        _frameScaler.Dispose();
    }
}