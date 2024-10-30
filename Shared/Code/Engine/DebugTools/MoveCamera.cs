using Microsoft.Xna.Framework.Input;
using RenderingLibrary;

public class MoveCamera
{
    public static void UpdatePositionCamera()
    {
        //move camera with arrows
        var keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.Left))
        {
            SystemManagers.Default.Renderer.Camera.Position.X -= 1;
        }
        if (keyboardState.IsKeyDown(Keys.Right))
        {
            SystemManagers.Default.Renderer.Camera.Position.X += 1;
        }
        if (keyboardState.IsKeyDown(Keys.Up))
        {
            SystemManagers.Default.Renderer.Camera.Position.Y -= 1;
        }
        if (keyboardState.IsKeyDown(Keys.Down))
        {
            SystemManagers.Default.Renderer.Camera.Position.Y += 1;
        }
    }
}