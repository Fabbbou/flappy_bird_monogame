using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.ViewportAdapters;
using System;
using MonoGame.Extended;

public class VerticalBoxingViewportAdapter : ScalingViewportAdapter
{
    private readonly GameWindow _window;
    private readonly GraphicsDevice _graphicsDevice;
    public OrthographicCamera Camera;

    // Summary:
    //     Initializes a new instance of the MonoGame.Extended.ViewportAdapters.BoxingViewportAdapter.
    public VerticalBoxingViewportAdapter(GameWindow window, GraphicsDevice graphicsDevice, int virtualWidth, int virtualHeight)
        : base(graphicsDevice, virtualWidth, virtualHeight)
    {
        _window = window;
        _graphicsDevice = graphicsDevice;
        window.ClientSizeChanged += OnClientSizeChanged;
    }

    public override void Dispose()
    {
        _window.ClientSizeChanged -= OnClientSizeChanged;
        base.Dispose();
    }

    private void OnClientSizeChanged(object sender, EventArgs eventArgs)
    {
        float scale = ComputeScale();
        int scaledViewportWidthX = (int)(scale * (float)VirtualWidth +.5f);
        int scaledViewportHeightY = (int)(scale * (float)VirtualHeight + .5f);

        Rectangle clientBounds = _window.ClientBounds;
        int x = clientBounds.Width / 2 - scaledViewportWidthX / 2;
        int y = clientBounds.Height / 2 - scaledViewportHeightY / 2;
        if (Camera != null)
        {
           Camera.Position = new Vector2(0, -y/2);
        }
        base.GraphicsDevice.Viewport = new Viewport(x, 0, scaledViewportWidthX, clientBounds.Height);
    }

    private float ComputeScale()
    {
        Rectangle clientBounds = _window.ClientBounds;
        float scaleX = (float)clientBounds.Width / (float)VirtualWidth;
        float scaleY = (float)clientBounds.Height / (float)VirtualHeight;

        float scale = Math.Min(scaleX, scaleY);
        return scale;
    }

    public override Matrix GetScaleMatrix()
    {
        return Matrix.CreateScale(ComputeScale());
    }

    public override void Reset()
    {
        base.Reset();
        OnClientSizeChanged(this, EventArgs.Empty);
    }

    public override Point PointToScreen(int x, int y)
    {
        Viewport viewport = base.GraphicsDevice.Viewport;
        return base.PointToScreen(x - viewport.X, y - viewport.Y);
    }
}