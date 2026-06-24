using FlappyBird.Rintime.Core.Services.BirdMovment;
using FlappyBird.Rintime.Core.Services.BirdMovment.Meta;
using FlappyBird.Rintime.Core.Services.BirdMovment.Systems;
using UnityEngine;

public class RotationSystem : IRotationSystem
{
    public MovementType Type => MovementType.Rotation;
    
    public void Rotate(Transform birdTransform,IRotationConfig config, Vector2 velocity)
    {
        var lerpValue = (velocity.y - config.MaxFallSpeed) / (0f - config.MaxFallSpeed + config.MaxAngle);
        var angle = Mathf.Lerp(config.MinAngle, config.MaxAngle, Mathf.Clamp01(lerpValue));

        birdTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
    
    
    public void Process(IMoveable target, IBaseMoveConfig config)
    {
        if (config is not IRotationConfig rotationConfig)
        {
            return;
        }
        if (target.Rigidbody2D == null)
        {
            return;
        }
        
        Rotate(target.Transform, rotationConfig, target.Rigidbody2D.linearVelocity);
    }
}
