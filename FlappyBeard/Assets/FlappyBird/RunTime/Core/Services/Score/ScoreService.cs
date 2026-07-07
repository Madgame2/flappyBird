using System;
using UnityEngine;

namespace FlappyBird.RunTime.Core.Services.Score
{
    public class ScoreService : IStopGameControllable
    {
        private const string BestScoreKey = "BestScore";
        private bool _isRunnind = true;
        
        public int Score { get; private set; }
        public int BestScore { get; private set; }
        
        public event Action<int> OnScoreChanged;

        public void Reset()
        {
            _isRunnind = true;

            Score = 0;
            OnScoreChanged?.Invoke(Score);
        }

        public void AddPoint()
        {
            if (!_isRunnind) return;

            Score++;
            OnScoreChanged?.Invoke(Score);
        }

        public void SaveBestScoreIfPossible()
        {
            if (Score > BestScore)
            {
                BestScore = Score;
                PlayerPrefs.SetInt(BestScoreKey, BestScore);
                PlayerPrefs.Save();
            }
        }

        public void Stop()
        {
            _isRunnind = false;
        }
    }
}