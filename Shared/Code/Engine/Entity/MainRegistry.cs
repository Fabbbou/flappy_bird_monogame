using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
    public Game Game { get; private set; }
    public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
    public ViewportAdapter ViewportAdapter { get; private set; }
    public ScreenRegistry ScreenRegistry { get; private set; }
    public OrthographicCamera Camera { get; private set; }
    public GraphicsDevice GraphicsDevice => GraphicsDeviceManager.GraphicsDevice;
    public float VirtualScreenScale => ViewportAdapter.GetScaleMatrix().M11;

    public void Init(Game game, GraphicsDeviceManager graphicsDeviceManager, ScreenRegistry screenRegistry, ViewportAdapterFactory viewportAdapterFactory)
    {
        Game = game;
        GraphicsDeviceManager = graphicsDeviceManager;
        ViewportAdapter = viewportAdapterFactory.BuildViewport();
        Camera = viewportAdapterFactory.BuildCamera();
        ScreenRegistry = screenRegistry;
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
        return Camera.ScreenToWorld(screenPosition);
    }

    public Rectangle PointToScreen(Rectangle rectangle)
    {
        return new(ViewportAdapter.PointToScreen(rectangle.Location), ViewportAdapter.PointToScreen(rectangle.Size));
    }
    public Point PointToScreen(int x, int y)
    {
        Matrix matrix = Matrix.Invert(GetScaleMatrix());
        return Vector2.Transform(new Vector2(x, y), matrix).ToPoint();
    }
    public Point PointToScreen(Vector2 v)
    {
        return ViewportAdapter.PointToScreen(v.ToPoint());
    }


    public Vector2 ScreenToWorld(float x, float y)
    {
        return ScreenToWorld(new Vector2(x, y));
    }
}