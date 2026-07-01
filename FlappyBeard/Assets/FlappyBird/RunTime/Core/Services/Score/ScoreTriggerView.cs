using System;
using FlappyBird.RunTime.Core.View;
using UnityEngine;
using VContainer;

namespace FlappyBird.RunTime.Core.Services.Score
{
    public class ScoreTriggerView : MonoBehaviour
    {
        private ScoreService _scoreService;

        public event Action OnPlayerPassed;
        
        [Inject]
        public void Construct(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<BirdView>(out _))
            {
                OnPlayerPassed.Invoke();
            }
        }
    }
}
