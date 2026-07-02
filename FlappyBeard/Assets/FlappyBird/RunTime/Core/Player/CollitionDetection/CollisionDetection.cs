using System;
using FlappyBird.RunTime.Core.Player.CollisionDetection.Components;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public event Action OnPlayerHit;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleDeath();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Obstacle>(out _))
        {
            HandleDeath();
        }
    }
    
    private void HandleDeath()
    {
        OnPlayerHit?.Invoke();
    }
}
