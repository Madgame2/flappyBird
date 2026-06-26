using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FlappyBird.RunTime.Core;
using FlappyBird.RunTime.Core.Difficulty.Data;
using FlappyBird.Runtime.Core.Location.Infrastructure;
using FlappyBird.Runtime.Core.Location.Interfaces;
using UnityEngine;
using VContainer.Unity;

namespace FlappyBird.Runtime.Core.Location.Systems
{
    public class LocationSpawnSystem : IStartable, IDisposable
    {
        private readonly ILocationBlockFactory _factory;
        private readonly DifficultyState _difficulty;
        private readonly Transform _spawnRoot;
        
        private readonly CancellationTokenSource _cts = new();

        public LocationSpawnSystem(
            ILocationBlockFactory factory,
            DifficultyState difficulty,
            ObstacleSpawnPointRoot spawnRoot)
        {
            _factory = factory;
            _difficulty = difficulty;
            _spawnRoot = spawnRoot.transform;
        }

        public void Start()
        {
            _factory.Initialize();
            SpawnLoopAsync(_cts.Token).Forget();
        }
        
        public void Dispose() => _cts.Cancel();

        private async UniTaskVoid SpawnLoopAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var currentInterval = _difficulty.SpawnInterval;
                
                await UniTask.Delay(TimeSpan.FromSeconds(currentInterval), cancellationToken: token);
            
                if (token.IsCancellationRequested) return;
            
                SpawnBlock();
            }
        }

        private void SpawnBlock()
        {
            _factory.GetRandomBlock(_spawnRoot.position);
        }
    }
}