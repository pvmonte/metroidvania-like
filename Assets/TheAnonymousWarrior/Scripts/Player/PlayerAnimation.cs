using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PlayerController _playerController;
    private static readonly int Run = Animator.StringToHash("run");

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _playerController.OnIdleEvent += PlayerControllerOnIdleEvent;
        _playerController.OnGroundEvent += PlayerController_OnGroundEvent;
        _playerController.OnRunEvent += PlayerControllerOnRunEvent;
        _playerController.OnAirbornEvent += PlayerControllerOnAirbornEvent;
        _playerController.OnAirAttackEvent += PlayerController_OnAirAttackEvent;
        
        _playerController.OnAttackEvent += PlayerController_OnAttackEvent;
    }

    private void PlayerController_OnAirAttackEvent()
    {
        _animator.Play("Attack2");
    }

    private void PlayerController_OnAttackEvent()
    {
        _animator.Play("Attack1");
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
        _animator.SetInteger(Run, 0);
    }
    
    private void PlayerControllerOnRunEvent(float input)
    {
        _animator.SetInteger(Run, Mathf.RoundToInt(input));

        if (input == 0) return;

        var scaleVector = Vector3.one;
        scaleVector.x = Mathf.Ceil(input);
        transform.localScale = scaleVector;
    }
}
