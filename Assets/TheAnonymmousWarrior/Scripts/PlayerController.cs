using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputSystem_Actions InputAction { get; private set; }
    private IState _currentState;

    private Rigidbody2D _rigidbody;

    public event Action OnAtackEvent;
    public event Action OnJumpActionEvent;
    public event Action<float> OnRunEvent;
    public event Action OnIdleEvent;

    private void Awake()
    {
        InputAction = new InputSystem_Actions();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        InputAction.Enable();
    }

    void Start()
    {
        _currentState = new IdleState();
        _currentState.Enter(this);
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.OnUpdate(this);
    }

    private void StartNewState(IState newState)
    {
        _currentState.Exit(this);
        _currentState = newState;
        _currentState.Enter(this);
    }

    private void OnDisable()
    {
        InputAction.Disable();
    }

    public void OnAttackAction()
    {
        StartNewState(new AttackState());
    }

    public void OnJumpAction()
    {
        _rigidbody.AddForceY(200);
        OnJumpActionEvent?.Invoke();
    }

    public void OnMoveAction()
    {
        if (_currentState.GetType() != typeof(RunState))
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
}

public interface IState
{
    void Enter(PlayerController playerController);
    void OnUpdate(PlayerController playerController);
    void Exit(PlayerController playerController);
}

public class IdleState : IState
{
    private InputSystem_Actions.PlayerActions _inputActionPlayer;

    public void Enter(PlayerController playerController)
    {
        Debug.Log("Enter Idle state");
        _inputActionPlayer = playerController.InputAction.Player;
    }

    public void OnUpdate(PlayerController playerController)
    {
        if (_inputActionPlayer.Attack.triggered)
        {
            playerController.OnAttackAction();
        }

        if (_inputActionPlayer.Jump.triggered)
        {
            playerController.OnJumpAction();
        }

        if (_inputActionPlayer.Move.ReadValue<Vector2>().magnitude != 0)
        {
            playerController.OnMoveAction();
        }
    }

    public void Exit(PlayerController playerController)
    {
        Debug.Log("Exit Idle state");
    }
}

public class AttackState : IState
{
    public void Enter(PlayerController playerController)
    {
        Debug.Log("Enter Attack state");
    }

    public void OnUpdate(PlayerController playerController)
    {
        playerController.OnIdle();
    }

    public void Exit(PlayerController playerController)
    {
        Debug.Log("Exit Attack state");
    }
}


public class RunState : IState
{
    private InputSystem_Actions.PlayerActions _inputActionPlayer;

    public void Enter(PlayerController playerController)
    {
        Debug.Log("Enter Run state");
        _inputActionPlayer = playerController.InputAction.Player;
    }

    public void OnUpdate(PlayerController playerController)
    {
        if (_inputActionPlayer.Attack.triggered)
        {
            playerController.OnAttackAction();
        }

        if (_inputActionPlayer.Jump.triggered)
        {
            playerController.OnJumpAction();
        }

        var moveVector = _inputActionPlayer.Move.ReadValue<Vector2>();

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

public class AirbornState : IState
{
    private InputSystem_Actions.PlayerActions _inputActionPlayer;

    public void Enter(PlayerController playerController)
    {
        Debug.Log("Enter Airborn state");
        _inputActionPlayer = playerController.InputAction.Player;
    }

    public void OnUpdate(PlayerController playerController)
    {
        var moveVector = _inputActionPlayer.Move.ReadValue<Vector2>();

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
        Debug.Log("Exit Airborn state");
    }
}