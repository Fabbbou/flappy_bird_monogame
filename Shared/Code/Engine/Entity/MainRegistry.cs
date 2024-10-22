using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using RenderingLibrary;

public class MainRegistry
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
    public GraphicsDevice GraphicsDevice { get; private set; }
    private ViewportAdapterFactory ViewportAdapterFactory;
    private ViewportAdapter _viewportAdapter;
    public ScreenRegistry ScreenRegistry { get; private set; }
    public OrthographicCamera Camera { get; private set; }
    public GumProjectSave GumProject;
    public void Initialize(Game game, GraphicsDevice graphicsDevice, ScreenRegistry screenRegistry, ViewportAdapterFactory viewportAdapterFactory, string gumProjectPath = null)
    {
        Game = game;
        GraphicsDevice = graphicsDevice;
        ScreenRegistry = screenRegistry;
        ViewportAdapterFactory = viewportAdapterFactory;
        _viewportAdapter = viewportAdapterFactory.BuildViewport();
        Camera = viewportAdapterFactory.BuildCamera();
        if (gumProjectPath != null)
        {
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
        return Camera.GetViewMatrix();
    }

    public Point PointToScreen(int x, int y)
    {
        Matrix matrix = Matrix.Invert(GetScaleMatrix());
        return Vector2.Transform(new Vector2(x, y), matrix).ToPoint();
    }
}