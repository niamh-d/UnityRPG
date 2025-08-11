using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInputSet _inputSet;
    private StateMachine stateMachine;
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }

    private void Awake()
    {
        stateMachine = new StateMachine();
        _inputSet = new PlayerInputSet();
        Debug.Log("Player Input Set Initialized");
        _inputSet.Player.Movement.performed += ctx => Debug.Log("Movement Input: " + ctx.ReadValue<Vector2>());

        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
    }

    private void OnEnable()
    {
        _inputSet.Enable();

        Debug.Log("Player Input Set Enabled");
    }

    private void OnDisable()
    {
        _inputSet.Disable();
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }
}
