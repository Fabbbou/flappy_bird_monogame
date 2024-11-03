using flappyrogue_mg.GameSpace;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using static Constants;
public class GetReadyState : MainGameState
{
    private GraphicalUiElement _getReadyContainerGraphicalUiElement;

    public GetReadyState(MainGameScreen mainGameScreen, GraphicalUiElement getReadyContainerGraphicalUiElement) : base(mainGameScreen) 
    {
        _getReadyContainerGraphicalUiElement = getReadyContainerGraphicalUiElement;
    }
    public override void Update(GameTime gameTime){}

    public override void Enter()
    {
        _getReadyContainerGraphicalUiElement.Visible = true;
        MainGameScreen.ClickZone.Visible = true;
        MainGameScreen.Bird.IsPaused = true;
        MainGameScreen.PipesSpawner.IsPaused = true;
        MainGameScreen.CurrentPauseButton.Visible = false;
    }

    public override void Exit()
    {
        _getReadyContainerGraphicalUiElement.Visible = false;
        MainGameScreen.Bird.Jump();
    }
}