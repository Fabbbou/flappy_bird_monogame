using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;
using static Constants;
public class GetReadyState : MainGameState
{
    public static readonly Rectangle JumpRegion = new Rectangle(CLICK_REGION_POSITION_JUMP_REGION.ToPoint(), CLICK_REGION_SIZE_JUMP_REGION.ToPoint());
    public GetReadyState(MainGameScreen mainGameScreen) : base(mainGameScreen) {}
    public override void Update(GameTime gameTime){}

    public override void Enter()
    {
        MainGameScreen.GetReadyUI.IsActive = true;
        MainGameScreen.Bird.IsActive = false;
        MainGameScreen.PipesSpawner.IsPaused = true;
        MainGameScreen.PauseButton.IsActive = false;

        MainGameScreen.EntityJumpClickRegion = new Entity();
        MainGameScreen.JumpBirdClickableRegionHandler = new ClickableRegionHandler(
            MainGameScreen.EntityJumpClickRegion,
            FirstJump,
            JumpRegion);
    }

    public override void Exit()
    {
        MainGameScreen.Bird.IsActive = true;
        MainGameScreen.PipesSpawner.IsPaused = false;
        MainGameScreen.PauseButton.IsActive = true;
        MainGameScreen.GetReadyUI.IsActive = false;

        //the jump zone is now used to make the bird jump
        MainGameScreen.EntityJumpClickRegion.IsActive = false;
        MainGameScreen.EntityJumpClickRegion = new Entity();
        MainGameScreen.JumpBirdClickableRegionHandler = new ClickableRegionHandler(
            MainGameScreen.EntityJumpClickRegion,
            MainGameScreen.Bird.Jump,
            JumpRegion);
    }

    private void FirstJump()
    {
        MainGameScreen.Bird.Jump();
        MainGameScreen.StateMachine.ChangeState(new PlayState(MainGameScreen));
    }

}