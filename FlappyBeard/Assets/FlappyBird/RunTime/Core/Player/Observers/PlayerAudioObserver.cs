using System;
using FlappyBird.RunTime.Core.Player.CollitionDetection;
using FlappyBird.RunTime.Core.Services;
using FlappyBird.RunTime.Core.Services.Audio;
using FlappyBird.RunTime.Core.Services.Audio.Meta;
using FlappyBird.RunTime.Core.Services.Score;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace FlappyBird.RunTime.Core.Player.Observers
{
    public class PlayerAudioObserver : IStartable, IDisposable
    {
        private readonly InputService _inputService;
        private readonly ScoreService _scoreService;
        private readonly IAudioService _audioService;
        private readonly AudioStorage _audioStorage;
        private readonly CollisionDetection _playerCollisionDetection;

        public PlayerAudioObserver(
            InputService inputService,
            IAudioService audioService,
            AudioStorage audioStorage,
            ScoreService scoreService,
            CollisionDetection playerCollisionDetection)
        {
            _inputService = inputService;
            _audioService = audioService;
            _audioStorage = audioStorage;
            _scoreService = scoreService;
            _playerCollisionDetection = playerCollisionDetection;
        }

        public void Start()
        {
            _inputService.OnJumpRequested += PlayJumpSound;
            _scoreService.OnScoreChanged += PlayScoreSound;
            _playerCollisionDetection.OnPlayerHit += PlayHitSound;
        }

        public void Dispose()
        {
            _inputService.OnJumpRequested -= PlayJumpSound;
            _scoreService.OnScoreChanged -= PlayScoreSound;
            _playerCollisionDetection.OnPlayerHit -= PlayHitSound;
        }

        private void PlayHitSound()
        {
            _audioService.PlaySFX(_audioStorage.HitSound);
        }

        private void PlayJumpSound()
        {
            _audioService.PlaySFX(_audioStorage.JumpSound);
        }

        private void PlayScoreSound(int obj)
        {
            _audioService.PlaySFX(_audioStorage.PointSound);
        }
    }
}