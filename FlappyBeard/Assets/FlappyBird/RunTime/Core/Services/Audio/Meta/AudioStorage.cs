using UnityEngine;

namespace FlappyBird.RunTime.Core.Services.Audio.Meta
{
    [CreateAssetMenu(fileName = "AudioStorage", menuName = "Audios/AudioStorage")]
    public class AudioStorage : ScriptableObject
    {
        [SerializeField] private AudioClip _jumpSound;
        [SerializeField] private AudioClip _pointSound;
        [SerializeField] private AudioClip _hitSound;
        public AudioClip JumpSound => _jumpSound;
        public AudioClip PointSound => _pointSound;
        public AudioClip HitSound => _hitSound;
    }
}
