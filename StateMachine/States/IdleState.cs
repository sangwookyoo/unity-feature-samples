public class IdleState : IState
{
    private PlayerController _player;
 
    public IdleState(PlayerController player)
    {
        _player = player;
    }
 
    public void Enter()
    {
        UnityEngine.Debug.Log("Enter Idle State");
    }
 
    public void Update()
    {
        UnityEngine.Debug.Log("Update Idle State");
    }
 
    public void Exit()
    {
        UnityEngine.Debug.Log("Exit Idle State");
    }
}