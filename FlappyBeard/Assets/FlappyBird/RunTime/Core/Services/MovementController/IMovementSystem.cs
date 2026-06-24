using FlappyBird.Rintime.Core.Services.BirdMovment.Meta;

namespace FlappyBird.Rintime.Core.Services.BirdMovment
{
    public interface IMovementSystem
    {
        MovementType Type { get; }
        void Process(IMoveable target, IBaseMoveConfig config);
    }
}