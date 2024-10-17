using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

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
    public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
    public ViewportAdapter ViewportAdapter { get; private set; }
    public ScreenRegistry ScreenRegistry { get; private set; }
    public OrthographicCamera Camera { get; private set; }
    //public GizmosRegistry GizmosRegistry { get; private set; }
    //public PhysicsEngine PhysicsEngine { get; private set; }
    public GraphicsDevice GraphicsDevice => GraphicsDeviceManager.GraphicsDevice;

    public void Init(GraphicsDeviceManager graphicsDeviceManager, ScreenRegistry screenRegistry, ViewportAdapterFactory viewportAdapterFactory)
    {
        GraphicsDeviceManager = graphicsDeviceManager;
        ViewportAdapter = viewportAdapterFactory.BuildViewport();
        Camera = viewportAdapterFactory.BuildCamera();
        ScreenRegistry = screenRegistry;
        //GizmosRegistry = new GizmosRegistry();
        //GizmosRegistry.Start(GraphicsDevice, false);
        //PhysicsEngine = new PhysicsEngine();
    }

    public Matrix GetScaleMatrix()
    {
        return Camera.GetViewMatrix();
    }
}