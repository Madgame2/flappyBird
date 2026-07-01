using System;
using FlappyBird.RunTime.Core.Services.Score;
using FlappyBird.RunTime.Core.Services.UI.Interfaces;
using FlappyBird.RunTime.Core.Services.UI.View;
using VContainer.Unity;

namespace FlappyBird.RunTime.Core.Services.UI.Presenters
{
    public class ScorePresenter : IInitializable, IDisposable
    {
        private readonly IUIService _uiService;
        private readonly ScoreService _scoreService;

        private ScoreView _view;

        public ScorePresenter(IUIService uiService, ScoreService scoreService)
        {
            _uiService = uiService;
            _scoreService = scoreService;
        }

        public void Initialize()
        {
            _view = _uiService.Open<ScoreView>("ScoreUI");

            _view.SetScore(_scoreService.Score);

            _scoreService.OnScoreChanged += HandleScoreChanged;

            _scoreService.Reset();
        }

        public void Dispose()
        {
            _scoreService.OnScoreChanged -= HandleScoreChanged;
        }

        private void HandleScoreChanged(int value)
        {
            _view.SetScore(value);
        }
    }
}