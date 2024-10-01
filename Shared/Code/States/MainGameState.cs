using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;

public abstract class MainGameState : GameState
{
    protected MainGameScreen MainGameScreen { get; private set; }
    
    public MainGameState(MainGameScreen mainGameScreen)
    {
        MainGameScreen = mainGameScreen;
    }

    public abstract void Enter();
    public abstract void Update(GameTime gameTime);
    public abstract void Exit();
}