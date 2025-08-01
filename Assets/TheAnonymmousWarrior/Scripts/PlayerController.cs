using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions _inputAction;
    private IPlayerState _currentState;

    private Rigidbody2D _rigidbody;
    [SerializeField] private GroundSensor _groundSensor;

    public event Action OnAtackEvent;
    public event Action OnJumpActionEvent;
    public event Action OnGroundEvent;
    public event Action OnAirbornEvent;
    public event Action<float> OnRunEvent;
    public event Action OnIdleEvent;

    private void Awake()
    {
        _inputAction = new InputSystem_Actions();
        _rigidbody = GetComponent<Rigidbody2D>();

        _groundSensor.OnGroundEvent += OnGround;
        _groundSensor.OnAirbornEvent += OnAirborn;
    }

    private void OnEnable()
    {
        _inputAction.Enable();
    }

    void Start()
    {
        _currentState = new IdleState();
        _currentState.Enter(this);
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.OnUpdate(this, _inputAction.Player);
    }

    private void StartNewState(IPlayerState newState)
    {
        _currentState.Exit(this);
        _currentState = newState;
        _currentState.Enter(this);
    }

    public void OnAttackAction()
    {
        StartNewState(new AttackState());
    }

    public void OnJumpAction()
    {
        _rigidbody.AddForceY(200);
        // OnAirbornEvent?.Invoke();
        // OnJumpActionEvent?.Invoke();
    }

    public void OnAirborn()
    {
        StartNewState(new AirbornPlayerState());
        OnAirbornEvent?.Invoke();
    }

    public void OnMoveAction()
    {
        if (_currentState.GetType() != typeof(AirbornPlayerState))
        {
            StartNewState(new RunState());
        }
    }

    public void OnMoveInput(Vector2 input)
    {
        transform.Translate(input.x * Time.deltaTime, 0, 0);
        OnRunEvent?.Invoke(input.x);
    }

    public void OnIdle()
    {
        StartNewState(new IdleState());
        OnIdleEvent?.Invoke();
    }

    public void OnGround()
    {
        OnIdle();
        OnGroundEvent?.Invoke();
    }
    
    private void OnDisable()
    {
        _inputAction.Disable();
    }
}

public class IdleState : IPlayerState
{
    public void Enter(PlayerController playerController)
    {
        Debug.Log("Enter Idle state");
    }

    public void OnUpdate(PlayerController playerController, InputSystem_Actions.PlayerActions input)
    {
        if (input.Attack.triggered)
        {
            playerController.OnAttackAction();
        }

        if (input.Jump.triggered)
        {
            playerController.OnJumpAction();
        }

        if (input.Move.ReadValue<Vector2>().magnitude != 0)
        {
            playerController.OnMoveAction();
        }
    }

    public void Exit(PlayerController playerController)
    {
        Debug.Log("Exit Idle state");
    }
}

public class AttackState : IPlayerState
{
    public void Enter(PlayerController playerController)
    {
        Debug.Log("Enter Attack state");
    }

    public void OnUpdate(PlayerController playerController, InputSystem_Actions.PlayerActions input)
    {
        playerController.OnIdle();
    }

    public void Exit(PlayerController playerController)
    {
        Debug.Log("Exit Attack state");
    }
}


public class RunState : IPlayerState
{
    public void Enter(PlayerController playerController)
    {
        Debug.Log("Enter Run state");
    }

    public void OnUpdate(PlayerController playerController, InputSystem_Actions.PlayerActions input)
    {
        if (input.Attack.triggered)
        {
            playerController.OnAttackAction();
        }

        if (input.Jump.triggered)
        {
            playerController.OnJumpAction();
        }

        var moveVector = input.Move.ReadValue<Vector2>();

        if (moveVector.magnitude != 0)
        {
            playerController.OnMoveInput(moveVector);
        }
        else
        {
            playerController.OnIdle();
        }
    }

    public void Exit(PlayerController playerController)
    {
        Debug.Log("Exit Run state");
    }
}