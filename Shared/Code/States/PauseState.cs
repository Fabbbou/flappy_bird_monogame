using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;

public class PauseState : MainGameState
{

    public PauseState(MainGameScreen mainGameScreen) : base(mainGameScreen) {}
    public override void Enter()
    {
        MainGameScreen.SoundUI.IsActive = true;
        MainGameScreen.PauseButton.IsActive = false;

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
        MainGameScreen.SoundUI.IsActive = false;
        MainGameScreen.PauseButton.IsActive = true;
        SettingsManager.Instance.UserSettings = new UserSettings(MainGameScreen.SoundUI.FxVolume*0.1f, MainGameScreen.SoundUI.MusicVolume*0.1f);

        MainGameScreen.Bird.IsPaused = false;
        MainGameScreen.PipesSpawner.IsPaused = false;
        MainGameScreen.Floor.IsPaused = false;
    }
}