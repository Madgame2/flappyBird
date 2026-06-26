using FlappyBird.RunTime.Core.Player.Configs;
using FlappyBird.RunTime.Core.Player.Input;
using FlappyBird.RunTime.Core.Player.Systems;
using FlappyBird.RunTime.Core.Services;
using FlappyBird.RunTime.Core.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FlappyBird.RunTime.Core
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField] private BirdView _playerView;
        [SerializeField] private PlayerMovementConfig _playerMovementConfig;
    
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_playerView).AsImplementedInterfaces().AsSelf();
        
            builder.RegisterInstance(_playerMovementConfig);

            builder.Register<PlayerControls>(Lifetime.Singleton);
            builder.RegisterEntryPoint<InputService>().AsSelf(); 
            builder.Register<PlayerInput>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterEntryPoint<PlayerJumpSystem>(Lifetime.Scoped)
                .WithParameter(_playerView);
            builder.RegisterEntryPoint<PlayerRotateSystem>(Lifetime.Scoped)
                .WithParameter(_playerView);
        }
    }
}
