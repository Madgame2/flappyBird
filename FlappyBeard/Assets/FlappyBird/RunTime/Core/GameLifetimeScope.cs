using FlappyBird.RunTime.Core;
using FlappyBird.RunTime.Core.Difficulty.Data;
using FlappyBird.RunTime.Core.Difficulty.Systems.FlappyBird.Runtime.Core.Difficulty.Systems;
using FlappyBird.Runtime.Core.Location.Infrastructure;
using FlappyBird.Runtime.Core.Location.Systems;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLigeTimeScope  : LifetimeScope
{
    [SerializeField] private LocationPrefabsStorage _prefabsStorage;
    [SerializeField] private ObstacleSpawnPointRoot _obstacleSpawnPointRoot;
    [SerializeField] private GlobalGameplayConfig _gameplayConfig;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_obstacleSpawnPointRoot);
        builder.RegisterComponent(_prefabsStorage);
        
        builder.RegisterInstance(_gameplayConfig);
        
        builder.Register<ActiveBlocksRegistry>(Lifetime.Scoped);
        builder.Register<DifficultyState>(Lifetime.Scoped);
        
        builder.RegisterEntryPoint<DifficultySystem>(Lifetime.Scoped);
        
        builder.Register<LocationBlockPool>(Lifetime.Scoped).AsImplementedInterfaces();
        
        builder.RegisterEntryPoint<LocationSpawnSystem>(Lifetime.Scoped)
            .WithParameter(_obstacleSpawnPointRoot.transform);
        
        builder.RegisterEntryPoint<LocationMovementSystem>(Lifetime.Scoped);
    }
}
