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
    private GraphicsDevice _graphicsDevice;
    public int VirtualWidth { get; private set; }
    public int VirtualHeight { get; private set; }
    public int OriginalScreenWidth { get; private set; }
    public int OriginalScreenHeight { get; private set; }

    /// <summary>
    /// KeepFirstScreenSize is used to keep the first screen size when the window is resized
    /// </summary>
    public bool KeepFirstScreenSize { get; private set; }
    public Vector2 Origin;
    public Vector2 Position;
    private Vector2 ScreenDimensions => _graphicsDevice.Viewport.Bounds.Size.ToVector2();
    public Vector2 VirtualScreenDimensions => new(VirtualWidth, VirtualHeight);
    private Vector2 ActualResolution => KeepFirstScreenSize?
              new (OriginalScreenWidth, OriginalScreenHeight)
            : new (_graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height);
    public Viewport Viewport => _graphicsDevice.Viewport;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="graphicsDevice"></param>
    /// <param name="gameWindow"></param>
    /// <param name="virtualWidth"></param>
    /// <param name="virtualHeight"></param>
    /// <param name="keepFirstScreenSize"> is used to keep the first screen size when the window is resized (for desktop mainly)</param>
    public ScreenHandler(GraphicsDevice graphicsDevice, GameWindow gameWindow, int virtualWidth, int virtualHeight, bool keepFirstScreenSize = false)
    {
        _graphicsDevice = graphicsDevice;
        VirtualWidth = virtualWidth;
        VirtualHeight = virtualHeight;
        gameWindow.ClientSizeChanged += OnScreenResize;
        Origin = new Vector2(VirtualWidth / 2f, VirtualHeight / 2f);
        Position = Vector2.Zero;
        OriginalScreenWidth = _graphicsDevice.Viewport.Width;
        OriginalScreenHeight = _graphicsDevice.Viewport.Height;
        KeepFirstScreenSize = keepFirstScreenSize;
    }

    private void OnScreenResize(object sender, EventArgs e)
    {
        if (KeepFirstScreenSize)
        {
            Position = -ScreenDimensions / 2 + ActualResolution /2;
        }
        Debug.WriteLine($"Screen resized to: {_graphicsDevice.Viewport.Width}x{_graphicsDevice.Viewport.Height}");
    }

    public float Zoom { get; set; } = 1f;

    private Matrix GetScaleMatrix(bool forceXScale = false, bool forceYScale = false)
    {
        Vector2 virtualResolution = new(VirtualWidth, VirtualHeight);

        float scaleX = ActualResolution.X / virtualResolution.X;
        float scaleY = ActualResolution.Y / virtualResolution.Y;

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
        // Create a matrix to scale the UI elements
        return Matrix.CreateScale(scale * Zoom);
    }

    public Vector2 ScreenToWorld(Vector2 screenPosition)
    {
        return Vector2.Transform(screenPosition - new Vector2(Viewport.X, Viewport.Y),
            Matrix.Invert(GetViewMatrix()));
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
}