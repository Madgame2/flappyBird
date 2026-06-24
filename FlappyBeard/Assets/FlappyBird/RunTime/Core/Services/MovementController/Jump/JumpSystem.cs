using FlappyBird.Rintime.Core.Services.BirdMovment.Meta;
using UnityEngine;

namespace FlappyBird.Rintime.Core.Services.BirdMovment.Systems
{
    public class JumpSystem : IJumpSystem
    {
        public MovementType Type =>  MovementType.Jump;
        
        public void Jump(Rigidbody2D rigidbody, float targetForce)
        {
            rigidbody.linearVelocity = Vector2.zero;

            rigidbody.AddForce(Vector2.up * targetForce, ForceMode2D.Impulse);
        }
        
        public void Process(IMoveable target, IBaseMoveConfig config)
        {
            if (config is not IJumpConfig jumpConfig)
            {
                return;
            }

            if (target.Rigidbody2D == null)
            {
                return;
            }
            
            Jump(target.Rigidbody2D, jumpConfig.JumpForce);
        }
    }
}