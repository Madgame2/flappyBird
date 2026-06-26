using UnityEngine;

namespace FlappyBird.RunTime.Core.Location.Strategies
{
    public abstract class MoveStrategyBase : ScriptableObject
    {
        public abstract Vector2 CalculateVelocity(float time, float speedModifier);
    }
}