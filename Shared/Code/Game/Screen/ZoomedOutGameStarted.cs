using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

public class ZoomedOutMainScreen : MainGameScreen
{
    public ZoomedOutMainScreen(Game game) : base(game){}

    public override void LoadContent()
    {
        GizmosRegistry.Instance.DrawGizmos(true);
        base.LoadContent();
        Camera.ZoomOut(0.5f);
    }
}