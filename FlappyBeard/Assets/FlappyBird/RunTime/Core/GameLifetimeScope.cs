using FlappyBird.RunTime.Core;
using FlappyBird.RunTime.Core.Difficulty.Data;
using FlappyBird.RunTime.Core.Difficulty.Systems.FlappyBird.Runtime.Core.Difficulty.Systems;
using FlappyBird.Runtime.Core.Location.Infrastructure;
using FlappyBird.Runtime.Core.Location.Systems;
using FlappyBird.RunTime.Core.Services.Spawn;
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
        
        var locationContainer = new GameObject("[Location Container]").transform;
        
        builder.Register<LocationBlockPool>(Lifetime.Scoped)
            .WithParameter("poolContainer", locationContainer)
            .AsImplementedInterfaces();
        
        builder.RegisterEntryPoint<LocationSpawnSystem>(Lifetime.Scoped)
            .WithParameter("spawnRoot", _obstacleSpawnPointRoot.transform);
        
        builder.RegisterEntryPoint<LinearMovementSystem>(Lifetime.Scoped);
    }
}
