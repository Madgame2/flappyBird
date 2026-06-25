using UnityEngine;

namespace FlappyBird.Rintime.Core.Player.Input
{
    public interface IPlayerInput
    {
        bool IsJumpRequested { get; } 
        
        void ConsumeJump();
    }
}