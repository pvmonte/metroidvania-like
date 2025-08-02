using System;
using System.Runtime.CompilerServices;
using TheAnonymousWarrior.Scripts.PlayerStateMachine;
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
    }

    private void OnEnable()
    {
        _inputAction.Enable();
    }

    void Start()
    {
        _currentState = new IdleState();
        _currentState.Enter(this);

        _groundSensor.OnGroundEvent += OnGround;
        _groundSensor.OnAirbornEvent += OnAirborn;
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.OnUpdate(this, _inputAction.Player);
    }

    private void FixedUpdate()
    {
        _currentState.OnFixedUpdate(this);
    }

    private void StartNewState(IPlayerState newState)
    {
        _currentState.Exit(this);
        _currentState = newState;
        _currentState.Enter(this);
    }

    public void OnIdle()
    {
        StartNewState(new IdleState());
        OnIdleEvent?.Invoke();
    }

    public void OnAttackAction()
    {
        StartNewState(new AttackState());
    }

    public void OnJumpAction()
    {
        _rigidbody.AddForceY(200);
    }

    public void OnMoveAction()
    {
        if (_currentState.GetType() == typeof(PlayerAirbornState)) return;

        StartNewState(new RunState());
    }

    public void OnMoveInput(Vector2 input)
    {
        transform.Translate(input.x * Time.fixedDeltaTime, 0, 0);
        OnRunEvent?.Invoke(input.x);
    }

    private void OnGround()
    {
        OnIdle();
        OnGroundEvent?.Invoke();
    }

    private void OnAirborn()
    {
        StartNewState(new PlayerAirbornState());
        OnAirbornEvent?.Invoke();
    }

    private void OnDisable()
    {
        _inputAction.Disable();
    }
}