using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

public abstract class ViewportAdapterFactory
{
    public Game Game { get; private set; }
    public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
    public ViewportAdapterFactory(Game game, GraphicsDeviceManager graphicsDeviceManager)
    {
        GraphicsDeviceManager = graphicsDeviceManager;
        Game = game;
    }

    public abstract ViewportAdapter BuildViewport();
    public abstract OrthographicCamera BuildCamera();
}
public class VerticalViewportAdapterFactory : ViewportAdapterFactory
{
    private readonly int _virtualWidth;
    private readonly int _virtualHeight;
    private OrthographicCamera _camera;
    private VerticalBoxingViewportAdapter _verticalBoxingViewportAdapter;
    public VerticalViewportAdapterFactory(Game Game, GraphicsDeviceManager GraphicsDeviceManager, int virtualWidth, int virtualHeight) : base(Game, GraphicsDeviceManager)
    {
        _virtualWidth = virtualWidth;
        _virtualHeight = virtualHeight;
    }

    public override ViewportAdapter BuildViewport()
    {
        _verticalBoxingViewportAdapter = new VerticalBoxingViewportAdapter(Game.Window, GraphicsDeviceManager.GraphicsDevice, _virtualWidth, _virtualHeight);
        _camera = new OrthographicCamera(_verticalBoxingViewportAdapter);
        _verticalBoxingViewportAdapter.Camera = _camera;
        return _verticalBoxingViewportAdapter;
    }

    public override OrthographicCamera BuildCamera()
    {
        if(_camera == null)
        {
            throw new System.Exception("Build viewport first: Viewport not built, impossible to build camera");
        }
        return _camera;
    }
}