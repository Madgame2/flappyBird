using System;
using UnityEngine;
using System.Collections.Generic;
using FlappyBird.Runtime.Core.Location.Interfaces;
using UnityEngine.Pool;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace FlappyBird.Runtime.Core.Location.Infrastructure
{
    public sealed class LocationBlockPool : ILocationBlockFactory, IDisposable
    {
        private readonly LocationPrefabsStorage _prefabStorage;
        private readonly ActiveBlocksRegistry _activeBlocksRegistry;
        private readonly Transform _poolContainer;

        private readonly Dictionary<LocationBlock, ObjectPool<LocationBlock>> _pools = new();

        public LocationBlockPool(
            ActiveBlocksRegistry activeBlocksRegistry,
            LocationPrefabsStorage prefabStorage,
            Transform poolContainer)
        {
            _activeBlocksRegistry = activeBlocksRegistry;
            _prefabStorage = prefabStorage;
            _poolContainer = poolContainer;
        }

        public void Initialize()
        {
            foreach (var prefab in _prefabStorage.LocationBlocks)
            {
                RegisterPool(prefab);
            }

            PrewarmPools();
        }

        public LocationBlock GetRandomBlock(Vector3 spawnPosition)
        {
            if (_prefabStorage.LocationBlocks.Count == 0)
                return null;

            var prefabIndex = Random.Range(0, _prefabStorage.LocationBlocks.Count);

            LocationBlock prefab = _prefabStorage.LocationBlocks[prefabIndex];

            LocationBlock instance = _pools[prefab].Get();

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

        private void RegisterPool(LocationBlock prefab)
        {
            ObjectPool<LocationBlock> objectPool = null;

            objectPool = new ObjectPool<LocationBlock>(
                createFunc: () => CreateBlock(prefab, objectPool),

                actionOnGet: ActivateBlock,

                actionOnRelease: DeactivateBlock,

                actionOnDestroy: DestroyBlock,

                collectionCheck: true,
                defaultCapacity: 2,
                maxSize: 5
            );

            _pools.Add(prefab, objectPool);
        }

        private LocationBlock CreateBlock(
            LocationBlock prefab,
            ObjectPool<LocationBlock> pool)
        {
            var block = Object.Instantiate(prefab, _poolContainer);

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

            block.Rigidbody2D.linearVelocity = Vector2.zero;

            _activeBlocksRegistry.Blocks.Remove(block);
        }

        private void DestroyBlock(LocationBlock block)
        {
            if (block != null)
            {
                Object.Destroy(block.gameObject);
            }
        }

        private void PrewarmPools()
        {
            foreach (ObjectPool<LocationBlock> pool in _pools.Values)
            {
                LocationBlock firstInstance = pool.Get();
                LocationBlock secondInstance = pool.Get();

                pool.Release(firstInstance);
                pool.Release(secondInstance);
            }
        }
    }
}