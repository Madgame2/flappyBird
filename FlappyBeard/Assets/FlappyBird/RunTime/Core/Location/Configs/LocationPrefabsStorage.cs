using System;
using System.Collections.Generic;
using System.Linq;
using FlappyBird.RunTime.Core.Location.Infrastructure;
using UnityEngine;

namespace FlappyBird.RunTime.Core.Location.Configs
{
    [CreateAssetMenu(fileName = "PrefabsStorage", menuName = "Location/PrefabsStorage")]
    public class LocationPrefabsStorage : ScriptableObject
    {
        [Serializable]
        public struct BlockSetup
        {
            public LocationBlock Prefab;
        
            [Min(0)] public int PrewarmCount;
            [Min(1)] public int MaxSize;
        }

        [SerializeField] private BlockSetup[] _blocksSetups;
    
        private GameObject[] _cachedPrefabs;
        private LocationBlock[] _cachedBlocks;

        public IReadOnlyList<BlockSetup> Setups => _blocksSetups;
        public IReadOnlyList<LocationBlock> LocationBlocks => _cachedBlocks;
        public IReadOnlyCollection<GameObject> LocationPrefabs => _cachedPrefabs;

        private void OnEnable()
        {
            if (_blocksSetups != null)
            {
                var validSetups = _blocksSetups.Where(b => b.Prefab != null).ToArray();
            
                _cachedBlocks = validSetups.Select(b => b.Prefab).ToArray();
                _cachedPrefabs = validSetups.Select(b => b.Prefab.gameObject).ToArray();
            }
            else
            {
                _cachedBlocks = Array.Empty<LocationBlock>();
                _cachedPrefabs = Array.Empty<GameObject>();
            }
        }
    }
}