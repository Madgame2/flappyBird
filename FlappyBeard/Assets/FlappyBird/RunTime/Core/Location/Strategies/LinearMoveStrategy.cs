using UnityEngine;

namespace FlappyBird.RunTime.Core.Location.Strategies
{
    [CreateAssetMenu(menuName = "Movement/Linear")]
    public class LinearMoveStrategy : MoveStrategyBase
    {
        public float Speed = 5f;
        public override Vector2 CalculateVelocity(float time, float speedModifier) 
            => Vector2.left * Speed * speedModifier;
    }
}