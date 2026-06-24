using System;
using FlappyBird.Rintime.Core.Services.BirdMovment;
using FlappyBird.Rintime.Core.Services.BirdMovment.Meta;
using VContainer.Unity;

public class PlayerMovementBinder : IStartable, IDisposable
{
    private readonly InputService _inputService;
    private readonly IMovementController _movementController;
    private readonly BirdView _birdView;
    private readonly MovementConfig _jumpConfig; 

    public PlayerMovementBinder(
        InputService inputService, 
        IMovementController movementController, 
        BirdView birdView,
        MovementConfig jumpConfig) 
    {
        _inputService = inputService;
        _movementController = movementController;
        _birdView = birdView;
        _jumpConfig = jumpConfig;
    }

    public void Start()
    {
        _inputService.JumpRequested += OnJumpRequested;
    }

    private void OnJumpRequested()
    {
        if (_birdView == null) return;
        
        var jumpRule = new MovementRule(MovementType.Jump, _jumpConfig);
        
        var jumpContext = new MovementContext(
            _birdView, 
            GameObjectType.Player, 
            new MovementRule[] { jumpRule }
        );
        
        _movementController.EnqueueOneShot(jumpContext);
    }

    public void Dispose()
    {
        _inputService.JumpRequested -= OnJumpRequested;
    }
}