using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

public class ZoomedOutMainScreen : MainGameScreen
{
    private OrthographicCamera _camera;

    public ZoomedOutMainScreen(Game game) : base(game){}

    public override void LoadContent()
    {
        base.LoadContent();
        _camera.ZoomOut(0.5f);
    }
}