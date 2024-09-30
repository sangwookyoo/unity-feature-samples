using UnityEngine;
 
public class PlayerController : MonoBehaviour
{
    public StateMachine PlayerStateMachine { get; private set; }
 
    private void Awake()
    {
        PlayerStateMachine = new StateMachine(this);
    }
 
    private void Start()
    {
        PlayerStateMachine.Init(PlayerStateMachine.IdleState);
    }
 
    private void Update()
    {
        PlayerStateMachine.Update();
        TransitionTo();
    }
 
    private void TransitionTo()
    {
        // 예시: W 키를 누르면 IdleState로 전환
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayerStateMachine.TransitionTo(PlayerStateMachine.IdleState);
        }
 
        // 예시: S 키를 누르면 WalkState로 전환
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerStateMachine.TransitionTo(PlayerStateMachine.WalkState);
        }
    }
}