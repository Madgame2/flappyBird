using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FlappyBird.Rintime.Core.Player.Input;
using FlappyBird.RunTime.Core.Movement.Interfaces;
using UnityEngine;
using VContainer.Unity;

namespace FlappyBird.Rintime.Core.Player.Systems
{
    public class PlayerJumpSystem: IStartable, IDisposable
    {
        private readonly IMoveable _playerObject;
        private readonly IPlayerInput _playerInput;
        private readonly PlayerMovementConfig _jumpConfig;

        private CancellationTokenSource _cancellationTokenSource = new();

        public PlayerJumpSystem(IMoveable playerObject, IPlayerInput playerInput, PlayerMovementConfig jumpConfig)
        {
            _playerObject = playerObject;
            _playerInput = playerInput;
            _jumpConfig = jumpConfig;
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

        public void Start()
        {
            JumpLoopAsync(_cancellationTokenSource.Token).Forget();
        }

        private async UniTaskVoid JumpLoopAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (_playerInput.IsJumpRequested)
                {
                    var rigidbody = _playerObject.Rigidbody2D;
                        
                    rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, _jumpConfig.JumpForce);
                   
                    _playerInput.ConsumeJump();
                }
                
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken);
            }
        }
    }
}
