using FlappyBird.RunTime.Core.Movement.Interfaces;
using FlappyBird.RunTime.Core.Player.Configs;
using UnityEngine;
using VContainer.Unity;

namespace FlappyBird.RunTime.Core.Player.Systems
{
    public class PlayerRotateSystem: ITickable
    {
        private readonly IMoveable _playerObject;
        private readonly PlayerMovementConfig _rotationConfig;
        
        public PlayerRotateSystem(IMoveable playerObject, PlayerMovementConfig rotationConfig)
        {
            _playerObject = playerObject;
            _rotationConfig = rotationConfig;
        }
        
        public void Tick()
        {
            if (_playerObject == null)
            {
                Debug.LogError($"{nameof(_playerObject)} is null");
                return;
            }
            
            if (_rotationConfig == null)
            {
                Debug.LogError($"{nameof(_rotationConfig)} is null");
                return;
            }
            
            var velocity = _playerObject.Rigidbody2D.linearVelocity;
            var playerTransform = _playerObject.Transform;
                
            var lerpValue = (velocity.y - _rotationConfig.MaxFallSpeed) / (0f - _rotationConfig.MaxFallSpeed + _rotationConfig.MaxAngle);
            var angle = Mathf.Lerp(_rotationConfig.MinAngle, _rotationConfig.MaxAngle, Mathf.Clamp01(lerpValue));

            playerTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}