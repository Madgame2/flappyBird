using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.RunTime.Core.Location.Paralax
{
    public class ParallaxController : MonoBehaviour, IStopGameControllable
    {
        private enum MoveDirection { Left, Right }
    
        [SerializeField] private float _baselayerSpeed = 5f;

        [Tooltip("В какую сторону летят декорации фона (обычно Left, чтобы птица казалась летящей вправо)")]
        [SerializeField] private MoveDirection _movementDirection = MoveDirection.Left;

        private List<ParallaxLayer> _parallaxLayers = new List<ParallaxLayer>();
        private Vector3 _directionVector;
        private bool _isRunning = true;

        void IStopGameControllable.Stop()
        {
            _isRunning = false;
        }
        
        public void UpdateBaseSpeed(float newSpeed)
        {
            _baselayerSpeed = newSpeed;
        }
        
        private void Start()
        {
            _directionVector = _movementDirection == MoveDirection.Left ? Vector3.left : Vector3.right;

            foreach (Transform child in transform)
            {
                var layer = child.GetComponent<ParallaxLayer>();
                
                if (layer != null)
                {
                    _parallaxLayers.Add(layer);
                }
            }
        }

        private void Update()
        {
            if(!_isRunning) return;
        
            foreach (ParallaxLayer layer in _parallaxLayers)
            {
                var currentLayerSpeed = _baselayerSpeed * layer.Multiplier;
                
                layer.MoveLayer(_directionVector, currentLayerSpeed);
            }
        }
    }
}