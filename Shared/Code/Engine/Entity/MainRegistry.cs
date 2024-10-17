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

    /// <summary>
    /// 
    /// Basically, this method converts a screen position to a world position.
    /// It is very convenient to use this method to convert an absolute position (that uses the Viewport borders for example) to a world position.
    /// 
    /// 
    /// This ScreenToWorld ignores Viewport position (as the current is always 0,0)
    /// There is also some rounding to avoid  1 pixel missing
    /// 
    /// Note that here, we are using the CameraViewMatrix to convert the screen position to a world position, and not the ScaleMatrix itself.
    ///
    /// </summary>
    /// <param name="screenPosition"></param>
    /// <returns>Ingame position</returns>
    public Vector2 ScreenToWorld(Vector2 screenPosition)
    {
        Viewport viewport = ViewportAdapter.Viewport;
        Vector2 avoidRoundingError = Vector2.One * .5f;
        return Vector2.Transform(screenPosition + avoidRoundingError, Matrix.Invert(GetScaleMatrix()));
    }

    public Vector2 ScreenToWorld(float x, float y)
    {
        return ScreenToWorld(new Vector2(x, y));
    }
}