using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;

public class PauseState(MainGameScreen mainGameScreen) : MainGameState(mainGameScreen)
{
    public override void Enter()
    {
        MainGameScreen.GrayUIBackground.IsActive = true;
        MainGameScreen.SoundUI.IsActive = true;
        MainGameScreen.PauseButton.IsActive = false;
        MainGameScreen.EntityJumpClickRegion.IsActive = false;

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
        MainGameScreen.GrayUIBackground.IsActive = false;
        MainGameScreen.SoundUI.IsActive = false;
        MainGameScreen.PauseButton.IsActive = true;
        MainGameScreen.EntityJumpClickRegion.IsActive = true;

        MainGameScreen.Bird.IsPaused = false;
        MainGameScreen.PipesSpawner.IsPaused = false;
        MainGameScreen.Floor.IsPaused = false;

        SoundManager.Instance.Save(MainGameScreen.SoundUI.FxVolume*0.1f, MainGameScreen.SoundUI.MusicVolume*0.1f);
    }
}