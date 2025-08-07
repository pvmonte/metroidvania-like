using System;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private LayerMask _groundLayer;

    public event Action OnGroundEvent; 
    public event Action OnAirbornEvent;

    public bool CheckGround()
    {
        var result = Physics2D.OverlapBox(transform.position, _boxCollider.size, 0);
        return !(result is null);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            OnGroundEvent?.Invoke();
            
        }
        
        if (other.gameObject.layer == _groundLayer)
        {
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            OnAirbornEvent?.Invoke();
            
        }
    }
}
