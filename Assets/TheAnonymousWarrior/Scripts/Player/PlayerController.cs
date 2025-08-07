using System;
using System.Runtime.CompilerServices;
using TheAnonymousWarrior.Scripts.PlayerStateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour , IDamageable
{
    private InputSystem_Actions _inputAction;
    private IPlayerState _currentState;

    private Rigidbody2D _rigidbody;
    [SerializeField] private GroundSensor _groundSensor;

    public event Action OnAttackEvent;
    public event Action OnJumpActionEvent;
    public event Action OnGroundEvent;
    public event Action OnAirbornEvent;
    public event Action OnAirAttackEvent;
    public event Action<float> OnRunEvent;
    public event Action OnIdleEvent;
    public event Action OnHurtEvent;


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

        _groundSensor.OnGroundEvent += OnGround;
        _groundSensor.OnAirbornEvent += OnAirborn;
    }

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
        if (_currentState.GetType() == newState.GetType()) return;
        
        _currentState = newState;
    }

    public void OnIdle()
    {
        StartNewState(new IdleState());
        OnIdleEvent?.Invoke();
    }

    public void OnAttackAction()
    {
        StartNewState(new AttackState());
        OnAttackEvent?.Invoke();
    }

    public void OnAirAttackAction()
    {
        StartNewState(new PlayerAirbornAttackState());
        OnAirAttackEvent?.Invoke();
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

    public void OnAirborn()
    {
        StartNewState(new PlayerAirbornState());
        OnAirbornEvent?.Invoke();
    }

    private void OnHurt()
    {
        StartNewState(new HurtState());
    }

    public void TakeDamage(int damage)
    {
        OnHurt();
        OnHurtEvent?.Invoke();
    }
    
    public void Recover()
    {
        if (_groundSensor.CheckGround())
        {
            OnIdle();
            return;
        }
        
        OnAirborn();
    }

    private void OnDisable()
    {
        _inputAction.Disable();
    }
}