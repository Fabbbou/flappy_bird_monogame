using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens.Transitions;

public class DeathState : GameState
{
    private MainGameScreen _mainGameScreen;
    private float _deathTimer;
    private const float DEATH_TIME = 1.5f;
    public DeathState(MainGameScreen mainGameScreen)
    {
        _mainGameScreen = mainGameScreen;
    }
    public void Enter()
    {
        _mainGameScreen.PauseButtonMobile.Visible = false;
        _mainGameScreen.Bird.IsPaused = true;
        _mainGameScreen.PipesSpawner.IsPaused = true;
        _mainGameScreen.Floor.IsPaused = true;
        _mainGameScreen.EntityJumpClickRegion.IsActive = false;
        SoundManager.Instance.PlayHitSound();
    }

    public void Exit()
    {

    }

    public void Update(GameTime gameTime)
    {
        _deathTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_deathTimer >= DEATH_TIME)
        {
            MainRegistry.I.SceneRegistry.LoadScene(SceneName.GameOverScreen, new FadeTransition(_mainGameScreen.Game.GraphicsDevice, Color.Black));
        }
    }
}