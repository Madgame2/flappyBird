using System;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace FlappyBird.RunTime.Core.Services
{
    public class InputService : IDisposable, IStartable
    {
        private readonly PlayerControls _inputActions;

        public event Action OnJumpRequested;

        public InputService(PlayerControls inputActions)
        {
            _inputActions = inputActions;
        }

        public void Dispose()
        {
            _inputActions.Player.Disable();

            _inputActions.Player.Jump.performed -= OnJumpPerformed;
        }

        public void Start()
        {
            _inputActions.Player.Enable();

            _inputActions.Player.Jump.performed += OnJumpPerformed;
        }
        
        private void OnJumpPerformed(InputAction.CallbackContext ctx)
        {
            OnJumpRequested?.Invoke();
        }
    }
}