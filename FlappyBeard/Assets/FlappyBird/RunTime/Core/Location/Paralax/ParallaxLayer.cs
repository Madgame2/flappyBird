using UnityEngine;
using UnityEngine.Serialization;

namespace FlappyBird.RunTime.Core.Location.Paralax
{
    public class ParallaxLayer : MonoBehaviour
    {
        [FormerlySerializedAs("multiplier")] [Range(0f, 1f)] [SerializeField]
        private float _multiplier = 0.5f;

        [SerializeField] private bool _infiniteLoop = true;

        private float _textureSizeX;
        private Vector3 _startPosition;

        public float Multiplier => _multiplier;
        public bool InfiniteLoop => _infiniteLoop;

        private void Start()
        {
            _startPosition = transform.position;

            var spriteRenderer = GetComponent<SpriteRenderer>();
            
            if (spriteRenderer != null)
            {
                _textureSizeX = spriteRenderer.bounds.size.x;
            }
            else
            {
                Debug.LogWarning(
                    $"[ParallaxLayer] На объекте {gameObject.name} не найден SpriteRenderer. Зацикливание не сработает.");
            }
        }

        public void MoveLayer(Vector3 direction, float speed)
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            if (_infiniteLoop && _textureSizeX > 0)
            {
                var offsetPositionX = Mathf.Abs(transform.position.x - _startPosition.x);

                if (offsetPositionX >= _textureSizeX)
                {
                    float resetDirection = Mathf.Sign(_startPosition.x - transform.position.x);

                    transform.position += new Vector3(resetDirection * _textureSizeX, 0, 0);
                }
            }
        }
    }
}