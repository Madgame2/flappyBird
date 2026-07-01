using System;
using FlappyBird.RunTime.Core.Movement.Interfaces;
using FlappyBird.RunTime.Core.Services.Score;
using UnityEngine;

namespace FlappyBird.RunTime.Core.View
{
    public class BirdView : MonoBehaviour, IMoveable
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        public Transform Transform => transform;
        
        public event Action OnScoreZonePassed;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ScoreZoneView>(out _))
            {
                OnScoreZonePassed?.Invoke();
            }
        }
    }
}
