using UnityEngine;

namespace FlappyBird.RunTime.Core.Player.Configs
{
    [CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "Configs/PlayerMovementConfig")]
    public class PlayerMovementConfig : ScriptableObject
    {
        [Header("Jump Settings")]
        [SerializeField] 
        [Min(0f)] 
        private float _jumpForce = 5f;
    
        [Header("Rotation Limits")]
        [SerializeField] 
        [Range(-180f, 0f)]
        private float _minAngle = -45f;
    
        [SerializeField] 
        [Range(0f, 180f)]
        private float _maxAngle = 30f;
    
        [SerializeField] 
        [Range(-50f, 0f)]
        private float _maxFallSpeed = -8f;
    
        public float JumpForce => _jumpForce;
        public float MinAngle => _minAngle;
        public float MaxAngle => _maxAngle;
        public float MaxFallSpeed => _maxFallSpeed;
    }
}
