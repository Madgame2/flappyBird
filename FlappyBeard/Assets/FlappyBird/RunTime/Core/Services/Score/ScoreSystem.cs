using System;
using FlappyBird.RunTime.Core.View;
using VContainer.Unity;

namespace FlappyBird.RunTime.Core.Services.Score
{
    public class ScoreSystem: IStartable, IDisposable
    {
        private readonly ScoreService _scoreService;
        private readonly BirdView _birdView;

        public ScoreSystem(ScoreService scoreService, BirdView birdView)
        {
            _scoreService = scoreService;
            _birdView = birdView;
        }
        
        public void Start()
        {
            _birdView.OnScoreZonePassed += OnScoreZonePassed;        
        }

        public void Dispose()
        {
            _birdView.OnScoreZonePassed -= OnScoreZonePassed;        
        }
        
        private void OnScoreZonePassed()
        {
            _scoreService.AddPoint();
        }
    }
}