using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PlayerController _controller;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<PlayerController>();
        _controller.OnIdleEvent += Controller_OnIdleEvent;
        _controller.OnJumpActionEvent += Controller_OnJumpActionEvent;
        _controller.OnRunEvent += Controller_OnRunEvent;
    }

    private void Controller_OnRunEvent(float input)
    {
        _animator.SetInteger("run", Mathf.RoundToInt(input));
    }

    private void Controller_OnJumpActionEvent()
    {
        _animator.Play("Jump");
    }

    private void Controller_OnIdleEvent()
    {
        _animator.SetInteger("run", 0);
        _animator.Play("Idle");
    }
}
