namespace FlappyBird.RunTime.Core.Player.Input
{
    public interface IPlayerInput
    {
        bool IsJumpRequested { get; } 
        
        void ConsumeJump();
    }
}