using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;
using static Constants;
public class GetReadyState : MainGameState
{
    public GetReadyState(MainGameScreen mainGameScreen) : base(mainGameScreen) {}
    public override void Update(GameTime gameTime){}

    public override void Enter()
    {
        MainGameScreen.GetReadyUI.IsActive = true;
        MainGameScreen.Bird.IsActive = false;
        MainGameScreen.PipesSpawner.IsPaused = true;
        MainGameScreen.PauseButton.Visible = false;

        MainGameScreen.EntityJumpClickRegion = new Entity();
        MainGameScreen.JumpBirdClickableRegionHandler = new ClickableRegionHandler(
            MainGameScreen.EntityJumpClickRegion,
            FirstJump,
            MainGameScreen.JumpRegion);
    }

    public override void Exit()
    {
        MainGameScreen.Bird.IsActive = true;
        MainGameScreen.PipesSpawner.IsPaused = false;
        MainGameScreen.PauseButton.Visible = true;
        MainGameScreen.GetReadyUI.IsActive = false;
    }

    private void FirstJump()
    {
        MainGameScreen.Bird.Jump();
        MainGameScreen.StateMachine.ChangeState(new PlayState(MainGameScreen));
    }

}