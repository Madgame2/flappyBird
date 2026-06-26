using FlappyBird.RunTime.Core.Difficulty.Data;
using UnityEngine;
using VContainer.Unity;

namespace FlappyBird.RunTime.Core.Difficulty.Systems
{
    namespace FlappyBird.Runtime.Core.Difficulty.Systems
    {
        public class DifficultySystem : ITickable
        {
            private readonly GlobalGameplayConfig _config;
            private readonly DifficultyState _state;
            private float _elapsedTime;

            public DifficultySystem(GlobalGameplayConfig config, DifficultyState state)
            {
                _config = config;
                _state = state;
            
                // Устанавливаем начальные значения
                _state.SpeedModifier = _config.StartSpeedModifier;
                _state.SpawnInterval = _config.StartSpawnInterval;
            }

            public void Tick()
            {
                _elapsedTime += Time.deltaTime;
                
                float progress = Mathf.Clamp01(_elapsedTime / _config.MaxDifficultyTime);
                
                _state.SpeedModifier = _config.StartSpeedModifier * _config.SpeedProgressionCurve.Evaluate(progress);
                _state.SpawnInterval = _config.StartSpawnInterval * _config.SpawnProgressionCurve.Evaluate(progress);
            }
        }
    }
}