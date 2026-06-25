using FlappyBird.Rintime.Core.Player.Input;
using FlappyBird.Rintime.Core.Player.Systems;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

public class GameLigeTimeScope : LifetimeScope
{
    [SerializeField] private BirdView _playerView;
    [SerializeField] private PlayerMovementConfig playerMovementConfig;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_playerView).AsImplementedInterfaces().AsSelf();
        
        builder.RegisterInstance(playerMovementConfig);

        builder.Register<PlayerControls>(Lifetime.Singleton);
        builder.RegisterEntryPoint<InputService>().AsSelf(); 
        builder.Register<PlayerInput>(Lifetime.Singleton).AsImplementedInterfaces();

        builder.RegisterEntryPoint<PlayerJumpSystem>(Lifetime.Scoped)
            .WithParameter(_playerView);
        builder.RegisterEntryPoint<PlayerRotateSystem>(Lifetime.Scoped)
            .WithParameter(_playerView);
    }
}
