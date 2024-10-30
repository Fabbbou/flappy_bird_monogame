using Gum.Wireframe;
using Microsoft.Xna.Framework.Graphics;
using System;

public class GumWindowResizer(GraphicsDevice _graphicsDevice, GraphicalUiElement screen)
{
    public void Resize(object not = null, EventArgs used = null)
    {
        MainRegistry.I.RefreshCenterScreen();
        screen.UpdateLayout();
    }
}