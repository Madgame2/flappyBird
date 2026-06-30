using FlappyBird.RunTime.Core.Location.Infrastructure;
using UnityEngine;

namespace FlappyBird.RunTime.Core.Location.Interfaces
{
    public interface ILocationBlockFactory
    {
        void Initialize();
        LocationBlock GetRandomBlock(Vector3 position);
    }
}
