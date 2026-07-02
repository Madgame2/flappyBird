using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FlappyBird.RunTime.Core.Difficulty.Data;
using FlappyBird.RunTime.Core.Location.Infrastructure;
using FlappyBird.RunTime.Core.Location.Interfaces;
using UnityEngine;
using VContainer.Unity;

namespace FlappyBird.RunTime.Core.Location.Systems
{
    public class LocationSpawnSystem : IStartable, IDisposable, IGameControllable
    {
        private readonly ILocationBlockFactory _factory;
        private readonly DifficultyState _difficulty;
        private readonly Transform _spawnRoot;
        private readonly CancellationTokenSource _cts = new();
        private bool _isMoving = true;

        public LocationSpawnSystem(
            ILocationBlockFactory factory,
            DifficultyState difficulty,
            ObstacleSpawnPointRoot spawnRoot)
        {
            _factory = factory;
            _difficulty = difficulty;
            _spawnRoot = spawnRoot.transform;
        }
        
        void IGameControllable.Stop()
        {
            _isMoving = false;
        }
        
        void IStartable.Start()
        {
            _factory.Initialize();
            SpawnLoopAsync(_cts.Token).Forget();
        }
        
        void IDisposable.Dispose() => _cts.Cancel();

        private async UniTaskVoid SpawnLoopAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var currentInterval = _difficulty.SpawnInterval;
                
                await UniTask.Delay(TimeSpan.FromSeconds(currentInterval), cancellationToken: token);
            
                if (token.IsCancellationRequested) return;
         
                if(!_isMoving)
                    continue;
                    
                SpawnBlock();
            }
        }

        private void SpawnBlock()
        {
            _factory.GetRandomBlock(_spawnRoot.position);
        }
    }
}