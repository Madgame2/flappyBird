using UnityEngine;
using VContainer;

namespace FlappyBird.RunTime.Core.Services.Score
{
    public class ScoreTrigger : MonoBehaviour
    {
        private ScoreService _scoreService;

        [Inject]
        public void Construct(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _scoreService.AddPoint();
            }
        }
    }
}
