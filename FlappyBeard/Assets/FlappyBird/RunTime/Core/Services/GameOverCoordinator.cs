using System;
using System.Collections.Generic;
using FlappyBird.RunTime.Core.Player.CollitionDetection;
using FlappyBird.RunTime.Core.Services.UI.Presenters;
using VContainer.Unity;

namespace FlappyBird.RunTime.Core.Services
{
    public class GameOverCoordinator : IInitializable, IDisposable
    {
        private readonly CollisionDetection _player;
        private readonly GameOverPresenter _gameOverPresenter;
        private IReadOnlyList<IStopGameControllable> _gameControllables;

        public GameOverCoordinator(
            CollisionDetection player,
            GameOverPresenter gameOverPresenter,
            IReadOnlyList<IStopGameControllable> gameControllables
        )
        {
            _player = player;
            _gameOverPresenter = gameOverPresenter;
            _gameControllables = gameControllables;
        }

        public void Initialize()
        {
            _player.OnPlayerHit += HandleGameOver;
        }

        public void Dispose()
        {
            _player.OnPlayerHit -= HandleGameOver;
        }

        private void HandleGameOver()
        {
            foreach (var controllable in _gameControllables)
            {
                controllable.Stop();
            }

            _gameOverPresenter.ShowGameOver();
        }
    }
}