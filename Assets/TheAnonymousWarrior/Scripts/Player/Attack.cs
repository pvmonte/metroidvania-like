using System;
using UnityEngine;

namespace TheAnonymousWarrior.Scripts.Player
{
    public abstract class Attack : MonoBehaviour
    {
        protected Collider2D _collider;
        [SerializeField] protected float _animationDelay = 0.25f;
        [SerializeField] protected float _attackDuration = 0.25f;

        protected virtual void Start()
        {
            _collider = GetComponent<Collider2D>();
        }

        protected async void ControllerOnAttackEvent()
        {
            await Awaitable.WaitForSecondsAsync(_animationDelay);
            _collider.enabled = true;
            await Awaitable.WaitForSecondsAsync(_attackDuration);
            _collider.enabled = false;
        }
        
        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(1);
            }
        }
    }
}