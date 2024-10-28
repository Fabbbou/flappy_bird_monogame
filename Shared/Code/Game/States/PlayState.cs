using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;

public class PlayState : MainGameState
{
    public PlayState(MainGameScreen mainGameScreen) : base(mainGameScreen){}

    public override void Enter()
    {
        MainGameScreen.PipesSpawner.IsPaused = false;
        MainGameScreen.Bird.IsActive = true;
        MainGameScreen.CurrentPauseButton.Visible = true;

        //the jump zone is now used to make the bird jump
        if (MainGameScreen.EntityJumpClickRegion != null)
        {
            MainGameScreen.EntityJumpClickRegion.IsActive = false;
        }
        MainGameScreen.EntityJumpClickRegion = new Entity();
        MainGameScreen.JumpBirdClickableRegionHandler = new ClickableRegionHandler(
            MainGameScreen.EntityJumpClickRegion,
            MainGameScreen.Bird.Jump,
            MainGameScreen.JumpRegion);
    }

    public override void Update(GameTime gameTime){}

    public override void Exit() {}
}