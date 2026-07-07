using System;
using FlappyBird.RunTime.Core.Player.CollitionDetection.Components;
using UnityEngine;

namespace FlappyBird.RunTime.Core.Player.CollitionDetection
{
    public class CollisionDetection : MonoBehaviour
    {
        private bool _collided = false;
        public event Action OnPlayerHit;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!_collided)
            {
                HandleDeath();
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!_collided && collider.TryGetComponent<Obstacle>(out _))
            {
                HandleDeath();
            }
        }

        private void HandleDeath()
        {
            _collided = true;
            OnPlayerHit?.Invoke();
        }
    }
}