using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PlayerController _playerController;
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int Hurt = Animator.StringToHash("hurt");
    private static readonly int Attack = Animator.StringToHash("attack");

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _playerController.OnIdleEvent += PlayerControllerOnIdleEvent;
        _playerController.OnGroundEvent += PlayerController_OnGroundEvent;
        _playerController.OnRunEvent += PlayerControllerOnRunEvent;
        _playerController.OnAirbornEvent += PlayerControllerOnAirbornEvent;
        _playerController.OnAirAttackEvent += PlayerController_OnAttackEvent;
        _playerController.OnAttackEvent += PlayerController_OnAttackEvent;
        _playerController.OnHurtEvent += PlayerController_OnHurtEvent;
    }

    private void PlayerController_OnHurtEvent()
    {
        _animator.SetTrigger(Hurt);
    }

    private void PlayerController_OnAttackEvent()
    {
        _animator.SetTrigger(Attack);
    }

    private void PlayerController_OnGroundEvent()
    {
        _animator.Play("Idle");
    }

    private void PlayerControllerOnAirbornEvent()
    {
        _animator.Play("Jump");
    }

    private void PlayerControllerOnIdleEvent()
    {
        _animator.SetFloat(Run, 0);
    }
    
    private void PlayerControllerOnRunEvent(float input)
    {
        _animator.SetFloat(Run, input);

        if (input == 0) return;

        var scaleVector = Vector3.one;
        scaleVector.x = Mathf.Ceil(input);
        transform.localScale = scaleVector;
    }
}
