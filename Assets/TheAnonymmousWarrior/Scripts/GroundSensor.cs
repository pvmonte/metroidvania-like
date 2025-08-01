using System;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;

    public event Action OnGroundEvent; 
    public event Action OnAirbornEvent; 
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == _groundLayer)
        {
            OnGroundEvent?.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == _groundLayer)
        {
            OnAirbornEvent?.Invoke();
        }
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
