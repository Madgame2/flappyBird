using FlappyBird.Rintime.Core.Services.BirdMovment;
using FlappyBird.Rintime.Core.Services.BirdMovment.Systems;
using System;
using FlappyBird.Rintime.Core.Services.BirdMovment.LinearMotion;
using FlappyBird.Rintime.Core.Services.BirdMovment.SystemsRegistry;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

public class GameLigeTimeScope : LifetimeScope
{
    [SerializeField] private BirdView _playerView;
    [SerializeField] private MovementConfig movementConfig;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_playerView);
        builder.RegisterComponent(movementConfig);
        
        builder.Register<PlayerControls>(Lifetime.Singleton);

        builder.RegisterEntryPoint<SceneEntryPoint>();
        builder.RegisterEntryPoint<InputService>().AsSelf();
        builder.RegisterEntryPoint<PlayerMovementBinder>();

        builder.Register<RotationSystem>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<JumpSystem>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<LinearMotionSystem>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<MovementSystemRegistry>(Lifetime.Singleton);
        
        builder.Register<IMovementController, MovementController>(Lifetime.Singleton);
    }
}
