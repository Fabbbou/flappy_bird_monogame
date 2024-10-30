using Microsoft.Xna.Framework;

public class StateMachine
{
    public GameState CurrentState { get; private set; }
    private GameState _nextState;
    private bool _isTransitioning;

    public StateMachine(GameState initialState)
    {
        ChangeState(initialState);
    }

    public void Update(GameTime gameTime)
    {
        if (_isTransitioning)
        {
            CurrentState?.Exit(); //? cause is null on first launch
            CurrentState = _nextState;
            CurrentState.Enter();
            _isTransitioning = false;
        }

        CurrentState.Update(gameTime);
    }

    public void ChangeState(GameState newState)
    {
        _nextState = newState;
        _isTransitioning = true;
    }
}