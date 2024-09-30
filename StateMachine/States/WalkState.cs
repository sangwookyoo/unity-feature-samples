public class WalkState : IState
{
    private PlayerController _player;
 
    public WalkState(PlayerController player)
    {
        _player = player;
    }
 
    public void Enter()
    {
        UnityEngine.Debug.Log("Enter Walk State");
    }
 
    public void Update()
    {
        UnityEngine.Debug.Log("Update Walk State");
    }
 
    public void Exit()
    {
        UnityEngine.Debug.Log("Exit Walk State");
    }
}