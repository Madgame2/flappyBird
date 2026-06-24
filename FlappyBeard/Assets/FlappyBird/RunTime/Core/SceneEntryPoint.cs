using FlappyBird.Rintime.Core.Services.BirdMovment;
using FlappyBird.Rintime.Core.Services.BirdMovment.Meta;
using VContainer.Unity;

public class SceneEntryPoint : IStartable
{
    private readonly IMovementController _movementController;
    private readonly BirdView _birdView;
    private readonly MovementConfig _movementConfig;
    
    public SceneEntryPoint(IMovementController movementController, BirdView birdView, MovementConfig movementConfig)
    {
        _movementController = movementController;
        _birdView = birdView;
        _movementConfig = movementConfig;
    }


    public void Start()
    {
        var birdPermanentMovingContext = new MovementContext(_birdView,
            GameObjectType.Player,
            new MovementRule[]
            {
                new MovementRule(
                    MovementType.Rotation,
                    _movementConfig
                )
            });
        
        _movementController.AddPermanent(birdPermanentMovingContext);
    }
}
