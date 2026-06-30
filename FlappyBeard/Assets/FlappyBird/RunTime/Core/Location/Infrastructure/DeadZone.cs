using UnityEngine;

namespace FlappyBird.RunTime.Core.Location.Infrastructure
{
    public class DeadZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<LocationBlock>(out var block))
            {
                block.Deactivate(); 
            }
        }
    }
}
