using flappyrogue_mg.GameSpace;

using Microsoft.Xna.Framework;
using RenderingLibrary;

public class PauseState(MainGameScreen mainGameScreen) : MainGameState(mainGameScreen)
{
    public override void Enter()
    {
        MainGameScreen.SoundUI.Activate();
        MainGameScreen.CurrentPauseButton.Visible = false;

        MainGameScreen.Bird.Pause();
        MainGameScreen.PipesSpawner.IsPaused = true;
        MainGameScreen.Floor.IsPaused = true;
        MainGameScreen.ClickZone.Visible = false;
        MainGameScreen.ScoreText.Visible = false;
    }

    public override void Update(GameTime gameTime) {}

    public override void Exit()
    {
        MainGameScreen.SoundUI.Deactivate();
        MainGameScreen.CurrentPauseButton.Visible = true;

        MainGameScreen.Bird.Resume();
        MainGameScreen.PipesSpawner.IsPaused = false;
        MainGameScreen.Floor.IsPaused = false;
        MainGameScreen.ScoreText.Visible = true;
    }
}