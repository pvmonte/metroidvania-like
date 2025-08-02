using System;
using UnityEngine;

public class EnemyDummy : MonoBehaviour , IDamageable
{
    private SpriteRenderer _spriteRenderer;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
