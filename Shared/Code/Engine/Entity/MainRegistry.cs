using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using System;

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
    public BoxingViewportAdapter2 ViewportAdapter { get; private set; }
    public ScreenRegistry ScreenRegistry { get; private set; }
    public OrthographicCamera Camera { get; private set; }
    //public GizmosRegistry GizmosRegistry { get; private set; }
    //public PhysicsEngine PhysicsEngine { get; private set; }

    public void Init(Game game, GraphicsDeviceManager graphicsDeviceManager, int virtualWidth, int virtualHeight, ScreenRegistry screenRegistry)
    {
        GraphicsDeviceManager = graphicsDeviceManager;
        ViewportAdapter = new BoxingViewportAdapter2(game.Window, GraphicsDeviceManager.GraphicsDevice, virtualWidth, virtualHeight);
        Camera = new OrthographicCamera(ViewportAdapter);
        ViewportAdapter.Camera = Camera;
        ScreenRegistry = screenRegistry;
        //GizmosRegistry = new GizmosRegistry();
        //GizmosRegistry.Start(GraphicsDevice, false);
        //PhysicsEngine = new PhysicsEngine();
    }
    public GraphicsDevice GraphicsDevice => GraphicsDeviceManager.GraphicsDevice;

    public Matrix GetScaleMatrix()
    {
        return Camera.GetViewMatrix();
    }
}