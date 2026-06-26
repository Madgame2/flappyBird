using UnityEngine;

namespace FlappyBird.RunTime.Core.Movement.Interfaces
{
    public interface IMoveable
    {
        public Rigidbody2D Rigidbody2D { get; }
        public Transform Transform { get; }
    }
}