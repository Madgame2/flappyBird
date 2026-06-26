using FlappyBird.RunTime.Core;
using FlappyBird.RunTime.Core.Difficulty.Data;
using FlappyBird.Runtime.Core.Location.Infrastructure;
using UnityEngine;
using VContainer.Unity;

namespace FlappyBird.Runtime.Core.Location.Systems
{
    public class LinearMovementSystem : IFixedTickable
    {
        private readonly ActiveBlocksRegistry _registry;
        private readonly DifficultyState _difficulty;
        
        public LinearMovementSystem(ActiveBlocksRegistry registry, DifficultyState difficulty)
        {
            _registry = registry;
            _difficulty = difficulty;
        }

        public void FixedTick()
        {
            float time = Time.time;
            float speedMod = _difficulty.SpeedModifier;

            foreach (var block in _registry.Blocks)
            {
                Vector2 totalVelocity = Vector2.zero;
                
                foreach (var strategy in block.MoveStrategies)
                {
                    totalVelocity += strategy.CalculateVelocity(time, speedMod);
                }

                block.Rigidbody2D.linearVelocity = totalVelocity;
            }
        }
    }
}