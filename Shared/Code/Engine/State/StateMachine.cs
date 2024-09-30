using Microsoft.Xna.Framework;

public class StateMachine
{
    private GameState _currentState;
    private GameState _nextState;
    private bool _isTransitioning;

    public StateMachine(GameState initialState)
    {
        _currentState = initialState;
        _isTransitioning = false;
    }

    public void Update(GameTime gameTime)
    {
        if (_isTransitioning)
        {
            _currentState.Exit();
            _currentState = _nextState;
            _currentState.Enter();
            _isTransitioning = false;
        }

        _currentState.Update(gameTime);
    }

    public void ChangeState(GameState newState)
    {
        _nextState = newState;
        _isTransitioning = true;
    }
}