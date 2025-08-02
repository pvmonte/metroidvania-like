using System;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    private PlayerController _controller;
    private Collider2D _collider;
    [SerializeField] private float _animationDelay = 0.25f;
    [SerializeField] private float _attackDuration = 0.25f;

    private void Start()
    {
        _controller = GetComponentInParent<PlayerController>();
        _collider = GetComponent<Collider2D>();
        _controller.OnAttackEvent += ControllerOnAttackEvent;
    }

    private async void ControllerOnAttackEvent()
    {
        await Awaitable.WaitForSecondsAsync(_animationDelay);
        _collider.enabled = true;
        await Awaitable.WaitForSecondsAsync(_attackDuration);
        _controller.OnIdle();
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(1);
        }
    }

    private void OnDestroy()
    {
        _controller.OnAttackEvent -= ControllerOnAttackEvent;
    }
}