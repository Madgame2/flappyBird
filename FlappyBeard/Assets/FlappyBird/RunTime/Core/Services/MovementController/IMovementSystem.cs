using FlappyBird.Rintime.Core.Services.BirdMovment.Meta;
using UnityEngine;

namespace FlappyBird.Rintime.Core.Services.BirdMovment
{
    public interface IMovementSystem
    {
        MovementType Type { get; }
        void Process(IMoveable target, IBaseConfig config);
    }
}