using Microsoft.Xna.Framework;

public class MenuState : GameState
{
    public void Enter()
    {
        // make the bird non moving and non collidable, idle in the middle of the screen
    }

    public void Update(GameTime gameTime)
    {
        // refresh the idle animation??
        // if the player presses the jump button, change the state to the game state
    }

    public void Exit()
    {
        // make the bird collidable and moving again
    }
}