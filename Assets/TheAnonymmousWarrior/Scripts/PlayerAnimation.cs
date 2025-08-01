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
        _playerController.OnAirbornEvent += StateMachineOnAirbornEvent;
        _playerController.OnRunEvent += StateMachineOnRunEvent;
    }

    private void PlayerController_OnGroundEvent()
    {
        _animator.Play("Idle");
    }

    private void StateMachineOnAirbornEvent()
    {
        _animator.Play("Jump");
    }

    private void PlayerControllerOnIdleEvent()
    {
        _animator.SetInteger(Run, 0);
    }
    
    private void StateMachineOnRunEvent(float input)
    {
        _animator.SetInteger(Run, Mathf.RoundToInt(input));
    }
}
