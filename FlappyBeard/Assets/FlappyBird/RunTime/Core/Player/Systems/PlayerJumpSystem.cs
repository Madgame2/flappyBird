using FlappyBird.RunTime.Core.Movement.Interfaces;
using FlappyBird.RunTime.Core.Player.Configs;
using FlappyBird.RunTime.Core.Player.Input;
using UnityEngine;
using VContainer.Unity;

namespace FlappyBird.RunTime.Core.Player.Systems
{
    public class PlayerJumpSystem : ITickable
    {
        private readonly IMoveable _playerObject;
        private readonly IPlayerInput _playerInput;
        private readonly PlayerMovementConfig _jumpConfig;

        public PlayerJumpSystem(IMoveable playerObject, IPlayerInput playerInput, PlayerMovementConfig jumpConfig)
        {
            _playerObject = playerObject;
            _playerInput = playerInput;
            _jumpConfig = jumpConfig;
        }

        public void Tick()
        {
            if (_jumpConfig != null)
            {
                Debug.LogError($"{nameof(_jumpConfig)} is null");
                return;
            }

            if (_playerObject != null)
            {
                Debug.LogError($"{nameof(_playerObject)} is null");
                return;
            }

            if (!_playerInput.IsJumpRequested) return;
            
            var rigidbody = _playerObject.Rigidbody2D;

            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, _jumpConfig.JumpForce);

            _playerInput.ConsumeJump();
        }
    }
}