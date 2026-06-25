using System;
using UnityEngine;

namespace FlappyBird.Rintime.Core.Player.Input
{
    public class PlayerInput: IDisposable, IPlayerInput
    {
        private readonly InputService _inputService;
        public bool IsJumpRequested { get; private set; }
        
        public PlayerInput(InputService inputService)
        {
            _inputService = inputService;
            _inputService.JumpRequested += OnJumpRequested;
        }
        
        private void OnJumpRequested() => IsJumpRequested = true;
        public void ConsumeJump() => IsJumpRequested = false;
        
        public void Dispose() => _inputService.JumpRequested -= OnJumpRequested;
    }
}