using UnityEngine;

namespace FlappyBird.Runtime.Core.Location.Interfaces
{
    public interface ILocationBlockFactory
    {
        void Initialize();
        LocationBlock GetRandomBlock(Vector3 position);
    }
}
