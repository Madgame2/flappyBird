using UnityEngine;

namespace FlappyBird.RunTime.Core.Location.Paralax
{
    public class ParallaxLayer : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float multiplier = 0.5f;
    
        [SerializeField] private bool infiniteLoop = true;

        private float textureSizeX;
        private Vector3 startPosition;

        void Start()
        {
            startPosition = transform.position;
        
            var spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                textureSizeX = spriteRenderer.bounds.size.x;
            }
            else
            {
                Debug.LogWarning($"[ParallaxLayer] На объекте {gameObject.name} не найден SpriteRenderer. Зацикливание не сработает.");
            }
        }
    
        public void MoveLayer(Vector3 direction, float speed)
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        
            if (infiniteLoop && textureSizeX > 0)
            {
                var offsetPositionX = Mathf.Abs(transform.position.x - startPosition.x);
            
                if (offsetPositionX >= textureSizeX)
                {
                    float resetDirection = Mathf.Sign(startPosition.x - transform.position.x);
                
                    transform.position += new Vector3(resetDirection * textureSizeX, 0, 0);
                }
            }
        }
    }
}
