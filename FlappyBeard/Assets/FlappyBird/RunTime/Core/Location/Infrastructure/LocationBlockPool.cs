using System;
using System.Collections.Generic;
using FlappyBird.RunTime.Core.Location.Configs;
using FlappyBird.RunTime.Core.Location.Interfaces;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace FlappyBird.RunTime.Core.Location.Infrastructure
{
    public sealed class LocationBlockPool : ILocationBlockFactory, IDisposable
    {
        private readonly LocationPrefabsStorage _prefabStorage;
        private readonly ActiveBlocksRegistry _activeBlocksRegistry;
        private readonly IObjectResolver _objectResolver;
        private readonly Dictionary<LocationBlock, ObjectPool<LocationBlock>> _pools = new();
        private Transform _poolContainer;
        
        public LocationBlockPool(
            ActiveBlocksRegistry activeBlocksRegistry,
            LocationPrefabsStorage prefabStorage,
            IObjectResolver objectResolver)
        {
            _activeBlocksRegistry = activeBlocksRegistry;
            _prefabStorage = prefabStorage;
            _objectResolver = objectResolver;
        }

        public void Initialize()
        {
            _poolContainer = new GameObject("[Location Container]").transform;
            
            foreach (var setup in _prefabStorage.Setups)
            {
                RegisterPool(setup);
            }
        }

        public LocationBlock GetRandomBlock(Vector3 spawnPosition)
        {
            if (_prefabStorage.LocationBlocks.Count == 0)
                return null;

            var prefabIndex = Random.Range(0, _prefabStorage.LocationBlocks.Count);
            var prefab = _prefabStorage.LocationBlocks[prefabIndex];
            var instance = _pools[prefab].Get();
            
            instance.transform.position = spawnPosition;

            return instance;
        }

        public void Dispose()
        {
            foreach (ObjectPool<LocationBlock> pool in _pools.Values)
            {
                pool.Dispose();
            }

            _pools.Clear();
        }

        private void RegisterPool(LocationPrefabsStorage.BlockSetup setup)
        {
            LocationBlock prefab = setup.Prefab;
            ObjectPool<LocationBlock> objectPool = null;

             objectPool = new ObjectPool<LocationBlock>(
                createFunc: () => CreateBlock(prefab, objectPool),
                actionOnGet: ActivateBlock,
                actionOnRelease: DeactivateBlock,
                actionOnDestroy: DestroyBlock,
                collectionCheck: true,
                defaultCapacity: setup.PrewarmCount,
                maxSize: setup.MaxSize
            );

            _pools.Add(prefab, objectPool);
            
            PrewarmSinglePool(objectPool, setup.PrewarmCount);
        }

        private LocationBlock CreateBlock(
            LocationBlock prefab,
            ObjectPool<LocationBlock> pool)
        {
            var block = _objectResolver.Instantiate(prefab, _poolContainer);

            block.Initialize(pool.Release);

            return block;
        }

        private void ActivateBlock(LocationBlock block)
        {
            block.gameObject.SetActive(true);
            _activeBlocksRegistry.Blocks.Add(block);
        }

        private void DeactivateBlock(LocationBlock block)
        {
            block.gameObject.SetActive(false);
            _activeBlocksRegistry.Blocks.Remove(block);
        }

        private void DestroyBlock(LocationBlock block)
        {
            _activeBlocksRegistry.Blocks.Remove(block);
            
            if (block != null)
            {
                Object.Destroy(block.gameObject);
            }
        }

        private void PrewarmSinglePool(ObjectPool<LocationBlock> pool, int count)
        {
            if (count <= 0) return;
            
            var tempArray = new LocationBlock[count];
            
            for (int i = 0; i < count; i++)
            {
                tempArray[i] = pool.Get();
            }
            
            for (int i = 0; i < count; i++)
            {
                pool.Release(tempArray[i]);
            }
        }
    }
}