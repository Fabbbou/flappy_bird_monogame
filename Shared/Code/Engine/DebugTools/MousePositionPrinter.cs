using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RenderingLibrary;
using System.Diagnostics;

public class MousePrinter
{
    private static MouseState lastState;
    private static MouseState currentState;
    public static void PrintOnLeftPressed(GraphicsDevice GraphicsDevice)
    {
        var camera = SystemManagers.Default.Renderer.Camera;
        // Print mouse position
        lastState = currentState;
        currentState = Mouse.GetState();
        if(currentState.LeftButton == ButtonState.Pressed && lastState.LeftButton != ButtonState.Pressed)
        {
            Vector2 worldPositionMouse = new();
            camera.ScreenToWorld(currentState.X, currentState.Y, out worldPositionMouse.X, out worldPositionMouse.Y);
            Debug.WriteLine($"CurrentScale: {MainRegistry.I.CurrentFrameScale}");
            Debug.WriteLine($"Mouse position: {currentState.Position}, worldposition: {worldPositionMouse}, camera position: {SystemManagers.Default.Renderer.Camera.Position}");
        }
    }
}