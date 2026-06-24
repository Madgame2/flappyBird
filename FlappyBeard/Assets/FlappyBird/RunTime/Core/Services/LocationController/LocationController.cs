using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using FlappyBird.Rintime.Core.Services.BirdMovment;
using FlappyBird.Rintime.Core.Services.BirdMovment.Meta;
using FlappyBird.RunTime._Temp;
using FlappyBird.RunTime.Core.Services.Spawn;
using UnityEngine;
using UnityEngine.Pool;
using VContainer.Unity;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace FlappyBird.RunTime.Core.Services.Location
{
    public class LocationController: IStartable, IDisposable
    {
        private readonly IMovementController _movementController;
        private readonly LocationPrefabsStorage _prefabsStorage;
        private readonly Transform _spawnRoot;
        private readonly Transform _container;
        
        private readonly Dictionary<LocationBlock, ObjectPool<LocationBlock>> _pools = new();
        private readonly List<LocationBlock> _activeBlocks = new();

        private CancellationTokenSource _cts;
        
        private float spawnInterval = 2f;
        
        public LocationController(LocationPrefabsStorage prefabsStorage, IMovementController movementController,ObstacleSpawnPointRoot obstacleSpawnPointRoot)
        {
            _prefabsStorage = prefabsStorage;
            _movementController = movementController;
            _spawnRoot = obstacleSpawnPointRoot.transform;
            
            _container = new GameObject("[Location Container]").transform;
        }
        
        
        public void Start()
        {
            InitializePools();
            PrewarmPools();
            
            _cts = new CancellationTokenSource();
            SpawnLoopAsync(_cts.Token).Forget();
        }

        private async UniTaskVoid SpawnLoopAsync(CancellationToken cancellationToken)
        {

            while (!cancellationToken.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(spawnInterval), cancellationToken: cancellationToken);
                
                SpawnBlock();
            }
        }

        private void SpawnBlock()
        {
            if (_prefabsStorage.LocationBlocks.Count == 0) return;
            
            int randomIndex = Random.Range(0, _prefabsStorage.LocationBlocks.Count);
            var selectedPrefab = _prefabsStorage.LocationBlocks[randomIndex];
            
            var pool = _pools[selectedPrefab];
            var block = pool.Get();
            
            block.transform.position = _spawnRoot.position;
            _activeBlocks.Add(block);
            
            
            var rules = new MovementRule[selectedPrefab.MoveStrategies.Length];
            
            for (int i = 0; i < selectedPrefab.MoveStrategies.Length; i++)
            {
                var strategy = selectedPrefab.MoveStrategies[i];
                rules[i] = new MovementRule(strategy.MovementType, strategy.MoveConfig);
            }
            var blockMovementContext = new MovementContext(
                block.gameObject,
                GameObjectType.LocationBlock,
                rules
            );
            _movementController.AddPermanent(blockMovementContext);
        }
        
        private void InitializePools()
        {
            foreach (var prefab in _prefabsStorage.LocationBlocks)
            {
                var prefabReference = prefab; 
                ObjectPool<LocationBlock> pool = null;

                pool = new ObjectPool<LocationBlock>(
                    createFunc: () => Object.Instantiate(prefabReference, _container),
                    actionOnGet: block => {
                        block.gameObject.SetActive(true);
                        block.OnRequestRelease += pool.Release;
                    },
                    actionOnRelease: block => {
                        block.OnRequestRelease -= pool.Release;
                        block.gameObject.SetActive(false);
                        _movementController.RemoveAllByTarget(block.gameObject);
                    },
                    actionOnDestroy: block => {
                        block.OnRequestRelease -= pool.Release;
                        Object.Destroy(block.gameObject);
                        _movementController.RemoveAllByTarget(block.gameObject);
                    },
                    collectionCheck: true,
                    defaultCapacity: 2,
                    maxSize: 5
                );

                _pools.Add(prefabReference, pool);
            }
        }
        
        private void PrewarmPools()
        {
            var tempSpawned = new List<LocationBlock>();

            foreach (var pair in _pools)
            {
                var pool = pair.Value;
                
                for (int i = 0; i < 2; i++)
                {
                    tempSpawned.Add(pool.Get());
                }
                
                foreach (var block in tempSpawned)
                {
                    pool.Release(block);
                }
                tempSpawned.Clear();
            }
        }

        public void Dispose()
        {
            foreach (var pool in _pools.Values)
            {
                pool.Clear();
            }
            _pools.Clear();
            _activeBlocks.Clear();
            
            if (_container != null) 
                Object.Destroy(_container.gameObject);
        }
    }
}