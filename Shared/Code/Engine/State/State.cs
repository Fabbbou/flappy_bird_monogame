using Microsoft.Xna.Framework;

public interface GameState
{
    public abstract void Enter();
    public abstract void Update(GameTime gameTime);
    public abstract void Exit();
}