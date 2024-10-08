using Microsoft.Xna.Framework;

public class StateMachine
{
    public GameState CurrentState { get; private set; }
    private GameState _nextState;
    private bool _isTransitioning;
    private bool _firstLaunch;

    public StateMachine(GameState initialState)
    {
        CurrentState = initialState;
        _isTransitioning = false;
        _firstLaunch = true;//tbr
    }

    public void Update(GameTime gameTime)
    {
        if(_firstLaunch)
        {
            CurrentState.Enter();
            _firstLaunch = false;
        } //tbr
        if (_isTransitioning)
        {
            CurrentState.Exit();
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