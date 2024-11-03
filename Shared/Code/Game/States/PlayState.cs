using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;

public class PlayState : MainGameState
{
    public PlayState(MainGameScreen mainGameScreen) : base(mainGameScreen){}

    public override void Enter()
    {
        MainGameScreen.PipesSpawner.IsPaused = false;
        MainGameScreen.Bird.IsPaused = false;
        MainGameScreen.CurrentPauseButton.Visible = true;
        MainGameScreen.ClickZone.Visible = true;
    }

    public override void Update(GameTime gameTime){}

    public override void Exit() {}
}