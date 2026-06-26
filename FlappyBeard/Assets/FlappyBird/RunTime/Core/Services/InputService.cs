using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

public class InputService: IDisposable, IStartable
{
    private readonly PlayerControls _inputActions;
    
    public event Action JumpRequested;

    public InputService(PlayerControls inputActions)
    {
        _inputActions = inputActions;
    }

    public void Dispose()
    {
        _inputActions.Player.Disable();
        
        _inputActions.Player.Jump.performed -= OnJumpPerformed;
    }

    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        JumpRequested?.Invoke();
    }

    public void Start()
    {
        _inputActions.Player.Enable();
        
        _inputActions.Player.Jump.performed += OnJumpPerformed;
    }
}
