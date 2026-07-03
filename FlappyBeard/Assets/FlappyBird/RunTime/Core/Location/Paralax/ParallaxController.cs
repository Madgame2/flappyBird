using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.RunTime.Core.Location.Paralax
{
    public class ParallaxController : MonoBehaviour, IGameControllable
    {
        public enum MoveDirection { Left, Right }
    
        public float gameSpeed = 5f;

        [Tooltip("В какую сторону летят декорации фона (обычно Left, чтобы птица казалась летящей вправо)")]
        [SerializeField] private MoveDirection movementDirection = MoveDirection.Left;

        private List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();
        private Vector3 directionVector;
        private IGameControllable _gameControllableImplementation;
        private bool _isRunning = true;

        void Start()
        {
            directionVector = movementDirection == MoveDirection.Left ? Vector3.left : Vector3.right;

            foreach (Transform child in transform)
            {
                var layer = child.GetComponent<ParallaxLayer>();
                
                if (layer != null)
                {
                    parallaxLayers.Add(layer);
                }
            }
        }

        void Update()
        {
            if(!_isRunning) return;
        
            foreach (ParallaxLayer layer in parallaxLayers)
            {
                var currentLayerSpeed = gameSpeed * layer.multiplier;
                
                layer.MoveLayer(directionVector, currentLayerSpeed);
            }
        }

        public void UpdateGameSpeed(float newSpeed)
        {
            gameSpeed = newSpeed;
        }

        void IGameControllable.Stop()
        {
            _isRunning = false;
        }
    }
}