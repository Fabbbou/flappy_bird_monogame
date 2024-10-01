using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;

public class PauseState : MainGameState
{

    public PauseState(MainGameScreen mainGameScreen) : base(mainGameScreen) {}
    public override void Enter()
    {
        MainGameScreen.Bird.IsPaused = true;
        MainGameScreen.PipesSpawner.IsPaused = true;
        MainGameScreen.Floor.IsPaused = true;
    }

    public override void Update(GameTime gameTime)
    {
        // refresh the idle animation??
        // if the player presses the jump button, change the state to the game state
    }

    public override void Exit()
    {
        MainGameScreen.Bird.IsPaused = false;
        MainGameScreen.PipesSpawner.IsPaused = false;
        MainGameScreen.Floor.IsPaused = false;
    }
}