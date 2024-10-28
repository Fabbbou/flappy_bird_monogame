using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class FrameScaler : IDisposable
{
    private readonly GraphicsDevice GraphicsDevice;
    private readonly GameWindow Window;
    private readonly float VirtualWidth;
    private readonly float VirtualHeight;
    public FrameScale CurrentFrameScale { get; private set; }
    public class FrameScale

    {
        public readonly bool IsWideScreen;
        public readonly float Scale;
        public FrameScale(bool isWideScreen, float scale)
        {
            IsWideScreen = isWideScreen;
            Scale = scale;
        }
    }

    public FrameScaler(GraphicsDevice graphicsDevice, GameWindow window, float virtualWidth, float virtualHeight)
    {
        GraphicsDevice = graphicsDevice;
        Window = window;
        VirtualWidth = virtualWidth;
        VirtualHeight = virtualHeight;
        RefreshCurrentScale();
        window.ClientSizeChanged += RefreshCurrentScale;
    }

    public void RefreshCurrentScale(object not = null, EventArgs used = null)
    {
        float scaleX = (float)GraphicsDevice.Viewport.Width / VirtualWidth;
        float scaleY = (float)GraphicsDevice.Viewport.Height / VirtualHeight;
        float currentScale = Math.Min(scaleX, scaleY);
        bool IsWideScreen = currentScale == scaleY;
        CurrentFrameScale = new FrameScale(IsWideScreen, currentScale);
    }

    public void Dispose()
    {
        Window.ClientSizeChanged -= RefreshCurrentScale;
    }
}