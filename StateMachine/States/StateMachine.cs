using System;

public interface IState
{
    void Enter();
    void Update();
    void Exit();
}

[Serializable]
public class StateMachine
{
    public IState CurrentState { get; private set; }
    public IdleState IdleState { get; private set; }
    public WalkState WalkState { get; private set; }
 
    public StateMachine(PlayerController player)
    {
        IdleState = new IdleState(player);
        WalkState = new WalkState(player);
    }
 
    public void Init(IState state)
    {
        CurrentState = state;
        CurrentState.Enter();
    }
 
    public void TransitionTo(IState state)
    {
        CurrentState.Exit();
        CurrentState = state;
        CurrentState.Enter();
    }
 
    public void Update()
    {
        CurrentState?.Update();
    }
}