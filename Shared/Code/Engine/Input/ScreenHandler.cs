using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using MonoGame.Extended.ViewportAdapters;
using static Constants;
using System.Diagnostics;
/// <summary>
/// A screen handler that helps with scaling and converting screen coordinates to world coordinates
/// WARNING: this class has to be initialized during the game's initialization (Initialize method)
/// 
/// </summary>
public class ScreenHandler
{
    //singleton
    private static ScreenHandler _instance;
    public static ScreenHandler I
    {
        get
        {
            _instance ??= new ScreenHandler();
            return _instance;
        }
    }
    public enum ScreenMode
    {
        CropWorldBoundaries,
        FitTopBottomScreen,
        Debug
    }
    private GraphicsDeviceManager _graphicsDeviceManager;
    private GraphicsDevice GraphicsDevice => _graphicsDeviceManager.GraphicsDevice;
    public int VirtualWidth { get; private set; }
    public int VirtualHeight { get; private set; }

    public Vector2 Origin;
    public Vector2 Position;
    private Vector2 ViewportDimensions => GraphicsDevice.Viewport.Bounds.Size.ToVector2();
    public Vector2 VirtualScreenDimensions => new(VirtualWidth, VirtualHeight);
    public Viewport Viewport => GraphicsDevice.Viewport;
    public Vector2 ScaledVirtualDimensions => ComputeScale() * VirtualScreenDimensions;

    private ScreenMode _screenMode;
    private int Width => Viewport.Width;
    private int Height => Viewport.Height;
    private Vector2 TopLeft => new(0, 0);
    private Vector2 TopMiddle => new(Width / 2, 0);
    private Vector2 TopRight => new(Width, 0);
    private Vector2 BottomLeft => new(0, Height);
    private Vector2 BottomMiddle => new(Width / 2, Height);
    private Vector2 BottomRight => new(Width, Height);
    private Vector2 MiddleLeft => new(0, Height / 2);
    private Vector2 Center => new(Width / 2, Height / 2);
    private Vector2 MiddleRight => new(Width, Height / 2);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="gameWindow"></param>
    /// <param name="virtualWidth"></param>
    /// <param name="virtualHeight"></param>
    public void Init(GraphicsDeviceManager graphicsDeviceManager, GameWindow gameWindow, int virtualWidth, int virtualHeight, ScreenMode screenMode = ScreenMode.CropWorldBoundaries)
    {
        _graphicsDeviceManager = graphicsDeviceManager;
        VirtualWidth = virtualWidth;
        VirtualHeight = virtualHeight;
        _screenMode = screenMode;
        gameWindow.ClientSizeChanged += OnScreenResize;
        Origin = new Vector2(VirtualWidth / 2f, VirtualHeight / 2f);
        //this center the game vertically on startup
        Position = (ScaledVirtualDimensions - ViewportDimensions) / 2;
    }

    private void OnScreenResize(object sender, EventArgs e)
    {
        switch (_screenMode)
        {
            case ScreenMode.CropWorldBoundaries:
                Position = Vector2.Zero;
                SetupViewportFitCropBoundaries();
                break;
            case ScreenMode.FitTopBottomScreen:
                Position = (ScaledVirtualDimensions - ViewportDimensions) / 2;
                SetupViewportVerticalFullScreen();
                break;
            case ScreenMode.Debug:
                //just to center the game in the screen but showing out bound items
                Position = ScaledVirtualDimensions / 2 - ViewportDimensions / 2;
                break;
        }
        Debug.WriteLine($"Screen resized to: {GraphicsDevice.Viewport.Width}x{GraphicsDevice.Viewport.Height}");
    }

    private void SetupViewportFitCropBoundaries()
    {
        var scale = ScaledVirtualDimensions;
        GraphicsDevice.Viewport = new Viewport(
            (int)(GraphicsDevice.Viewport.Width - scale.X) / 2,
            (int)(GraphicsDevice.Viewport.Height - scale.Y) / 2,
            (int)scale.X,
            (int)scale.Y
        );
    }

    private void SetupViewportVerticalFullScreen()
    {
        var scale = ScaledVirtualDimensions;
        GraphicsDevice.Viewport = new Viewport(
            (int)(GraphicsDevice.Viewport.Width - scale.X) / 2,
            0,
            (int)scale.X,
            GraphicsDevice.Viewport.Height
        );
    }

    public float Zoom { get; set; } = 1f;

    private Matrix GetScaleMatrix(bool forceXScale = false, bool forceYScale = false)
    {
        return Matrix.CreateScale(ComputeScale(forceXScale, forceYScale) * Zoom);
    }

    private float ComputeScale(bool forceXScale = false, bool forceYScale = false)
    {
        Vector2 virtualResolution = new(VirtualWidth, VirtualHeight);

        float scaleX = ViewportDimensions.X / virtualResolution.X;
        float scaleY = ViewportDimensions.Y / virtualResolution.Y;

        float scale = Math.Min(scaleX, scaleY);
        // Use the smaller scale to maintain aspect ratio
        if (forceXScale)
        {
            scale = scaleX;
        }
        else if (forceYScale)
        {
            scale = scaleY;
        }
        return scale;
    }

    public Matrix GetViewMatrix(bool forceXScale = false, bool forceYScale = false)
    {
        return GetScaleMatrix(forceXScale, forceYScale) *
            Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
            Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
            Matrix.CreateScale(Zoom, Zoom, 1) *
            Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
    }

    public virtual Point PointToScreen(int x, int y)
    {
        Matrix matrix = Matrix.Invert(GetScaleMatrix());
        return Vector2.Transform(new Vector2(x, y), matrix).ToPoint();
    }

    public Vector2 ScreenToWorld(Vector2 screenPosition)
    {
        return Vector2.Transform(screenPosition - new Vector2(Viewport.X, Viewport.Y),
            Matrix.Invert(GetViewMatrix()));
    }

    public Vector2 S2WFromTopMiddle(float x, float y)
    {
        return ScreenToWorld(TopMiddle) + ScreenToWorld(new Vector2(x, y));
    }
}