using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;

public class DeathState : GameState
{
    private MainGameScreen _mainGameScreen;
    private float _deathTimer = 0.0f;
    private const float DEATH_TIME = 1.5f;
    public DeathState(MainGameScreen mainGameScreen)
    {
        _mainGameScreen = mainGameScreen;
    }
    public void Enter()
    {
        _mainGameScreen.PauseButton.IsActive = false;
        _mainGameScreen.Bird.IsPaused = true;
        _mainGameScreen.PipesSpawner.IsPaused = true;
        _mainGameScreen.Floor.IsPaused = true;
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
            Main.Instance.LoadScreen(ScreenNames.MainGameScreen);
        }
    }
}