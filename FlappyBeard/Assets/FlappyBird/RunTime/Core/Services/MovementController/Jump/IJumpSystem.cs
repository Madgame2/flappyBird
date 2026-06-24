using UnityEngine;

namespace FlappyBird.Rintime.Core.Services.BirdMovment.Systems
{
    public interface IJumpSystem: IMovementSystem
    {
        void Jump(Rigidbody2D rigidbody, float targetForce);
    }
}