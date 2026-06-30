using System;

namespace FlappyBird.RunTime.Core.Services.Score
{
    public class ScoreService
    {
        public event Action<int> OnScoreChanged;

        public int Score { get; private set; }

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
    }
}