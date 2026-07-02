using System;
using UnityEngine;

namespace FlappyBird.RunTime.Core.Services.Score
{
    public class ScoreService
    {
        private const string BestScoreKey = "BestScore";
        
        public event Action<int> OnScoreChanged;

        public int Score { get; private set; }
        public int BestScore { get; private set; }

        public void Reset()
        {
            Score = 0;
            OnScoreChanged?.Invoke(Score);
        }

        public void AddPoint()
        {
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
    }
}