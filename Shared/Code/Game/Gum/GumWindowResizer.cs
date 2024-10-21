using Gum.Wireframe;
using Microsoft.Xna.Framework.Graphics;
using System;

public class GumWindowResizer(GraphicsDevice _graphicsDevice, GraphicalUiElement screen)
{
    public void Resize()
    {
        GraphicalUiElement.CanvasWidth = _graphicsDevice.Viewport.Width;
        GraphicalUiElement.CanvasHeight = _graphicsDevice.Viewport.Height;

        // Grab the rootmost object and tell it to resize:
        screen.UpdateLayout();
    }
}