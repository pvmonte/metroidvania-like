using System;
using UnityEngine;

public class EnemyDummy : MonoBehaviour , IDamageable
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator; 

    private float _attackTimer = 3;
    private float _currentTimer = 3;

    public event Action OnAttackEvent;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _currentTimer -= Time.deltaTime;

        if (_currentTimer <= 0)
        {
            _currentTimer = _attackTimer;
            _animator.Play("DummyAttack");
            OnAttackEvent?.Invoke();
        }
    }

    public void TakeDamage(int value)
    {
        ColorDamage();
        Debug.Log("Dummy Hit");
    }

    private async void ColorDamage()
    {
        _spriteRenderer.color = Color.red;
        await Awaitable.WaitForSecondsAsync(0.25f);
        _spriteRenderer.color = Color.white;
    }
}
