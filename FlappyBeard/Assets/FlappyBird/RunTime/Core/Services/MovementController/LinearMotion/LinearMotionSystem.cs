using FlappyBird.Rintime.Core.Services.BirdMovment.Meta;
using UnityEngine;

namespace FlappyBird.Rintime.Core.Services.BirdMovment.LinearMotion
{
    public class LinearMotionSystem: ILinearMotionSystem
    {
        public MovementType Type => MovementType.Linear;
        public void Process(IMoveable target, IBaseMoveConfig config)
        {
            if (config is not ILinearConfig linearConfig)
            {
                return;
            }
            
            Vector3 direction3D = new Vector3(linearConfig.Direction.x, linearConfig.Direction.y, 0f);
            target.Transform.position += direction3D.normalized * linearConfig.Speed * Time.deltaTime;
        }
    }
}