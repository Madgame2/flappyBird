using UnityEngine;

namespace FlappyBird.Rintime.Core.Services.BirdMovment.Systems
{
    public interface IRotationConfig: IBaseMoveConfig
    {
        float MinAngle { get; }
        float MaxAngle { get; }
        float MaxFallSpeed { get; }
    }
}
