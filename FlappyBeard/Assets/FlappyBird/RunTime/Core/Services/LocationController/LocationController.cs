using System;
using System.Linq;
using FlappyBird.RunTime.Core.Services.Spawn;
using UnityEngine;
using VContainer.Unity;

namespace FlappyBird.RunTime.Core.Services.Location
{
    public class LocationController: IStartable, IDisposable
    {
        private readonly LocationPrefabsStorage _prefabsStorage;
        private readonly ISpawner _spawner;

        public LocationController(LocationPrefabsStorage prefabsStorage, ISpawner spawner)
        {
            _prefabsStorage = prefabsStorage;
            _spawner = spawner;
        }
        
        
        public void Start()
        {
            var count = _prefabsStorage.LocationPrefabs.Count;
            Debug.Log(count);
            
            _spawner.Spawn(_prefabsStorage.LocationPrefabs.First());
        }

        public void Dispose()
        {
            
        }
    }
}