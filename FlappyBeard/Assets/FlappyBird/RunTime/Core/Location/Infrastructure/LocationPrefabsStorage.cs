using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "PrefabsStorage", menuName = "Location/PrefabsStorage")]
public class LocationPrefabsStorage : ScriptableObject
{
    [SerializeField] private LocationBlock[] _locationBlocks;
    
    private GameObject[] _cachedPrefabs;
    
    public IReadOnlyList<LocationBlock> LocationBlocks => _locationBlocks;
    public IReadOnlyCollection<GameObject> LocationPrefabs => _cachedPrefabs;


    private void OnEnable()
    {
        if (_locationBlocks != null)
        {
            _cachedPrefabs = _locationBlocks
                .Where(block => block != null)
                .Select(block => block.gameObject)
                .ToArray();
        }
        else
        {
            _cachedPrefabs = Array.Empty<GameObject>();
        }
    }
}
