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
    public enum ScreenMode
    {
        CropWorldBoundaries,
        FitTopBottomScreen,
        Debug
    }
    private GraphicsDevice _graphicsDevice;
    public int VirtualWidth { get; private set; }
    public int VirtualHeight { get; private set; }

    public Vector2 Origin;
    public Vector2 Position;
    private Vector2 ViewportDimensions => _graphicsDevice.Viewport.Bounds.Size.ToVector2();
    public Vector2 VirtualScreenDimensions => new(VirtualWidth, VirtualHeight);
    public Viewport Viewport => _graphicsDevice.Viewport;
    public Vector2 ScaledVirtualDimensions => ComputeScale() * VirtualScreenDimensions;
    private ScreenMode _screenMode;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="graphicsDevice"></param>
    /// <param name="gameWindow"></param>
    /// <param name="virtualWidth"></param>
    /// <param name="virtualHeight"></param>
    public ScreenHandler(GraphicsDevice graphicsDevice, GameWindow gameWindow, int virtualWidth, int virtualHeight, ScreenMode screenMode = ScreenMode.CropWorldBoundaries)
    {
        _graphicsDevice = graphicsDevice;
        VirtualWidth = virtualWidth;
        VirtualHeight = virtualHeight;
        _screenMode = screenMode;
        gameWindow.ClientSizeChanged += OnScreenResize;
        Origin = new Vector2(VirtualWidth / 2f, VirtualHeight / 2f);
        //this center the game vertically on startup
        Position = (ScaledVirtualDimensions - ViewportDimensions) / 2 ;
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
        Debug.WriteLine($"Screen resized to: {_graphicsDevice.Viewport.Width}x{_graphicsDevice.Viewport.Height}");
    }

    private void SetupViewportFitCropBoundaries()
    {
        _graphicsDevice.Viewport = new Viewport(
            (int)(_graphicsDevice.Viewport.Width - ScaledVirtualDimensions.X) / 2,
            (int)(_graphicsDevice.Viewport.Height - ScaledVirtualDimensions.Y) / 2,
            (int)ScaledVirtualDimensions.X,
            (int)ScaledVirtualDimensions.Y
        );
    }

    private void SetupViewportVerticalFullScreen()
    {
        _graphicsDevice.Viewport = new Viewport(
            (int)(_graphicsDevice.Viewport.Width - ScaledVirtualDimensions.X) / 2,
            0,
            (int)ScaledVirtualDimensions.X,
            _graphicsDevice.Viewport.Height
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