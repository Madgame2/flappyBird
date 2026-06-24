using FlappyBird.Rintime.Core.Services.BirdMovment;
using FlappyBird.Rintime.Core.Services.BirdMovment.Systems;
using System;
using FlappyBird.Rintime.Core.Services.BirdMovment.LinearMotion;
using FlappyBird.Rintime.Core.Services.BirdMovment.SystemsRegistry;
using FlappyBird.RunTime.Core.Services.Location;
using FlappyBird.RunTime.Core.Services.Spawn;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

public class GameLigeTimeScope  : LifetimeScope
{
    [SerializeField] private BirdView _birdView;
    [SerializeField] private LocationPrefabsStorage _prefabsStorage;
    [SerializeField] private ObstacleSpawnPointRoot _obstacleSpawnPointRoot;
    [FormerlySerializedAs("jumpConfig")] [FormerlySerializedAs("_birdConfig")] [SerializeField] private MovementConfig movementConfig;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_birdView);
        builder.RegisterComponent(movementConfig);
        builder.RegisterComponent(_obstacleSpawnPointRoot);
        
        builder.Register<PlayerControls>(Lifetime.Singleton);

        builder.RegisterEntryPoint<SceneEntryPoint>();
        builder.RegisterEntryPoint<InputService>().AsSelf();
        builder.RegisterEntryPoint<PlayerMovementBinder>();

        builder.Register<RotationSystem>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<JumpSystem>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<LinearMotionSystem>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<MovementSystemRegistry>(Lifetime.Singleton);
        
        builder.Register<IMovementController, MovementController>(Lifetime.Singleton);
    
        builder.RegisterComponent(_prefabsStorage);

        builder.Register<SpawnSystem>(Lifetime.Singleton).AsImplementedInterfaces();
        
        builder.Register<LocationController>(Lifetime.Singleton).AsImplementedInterfaces();
    }
}
