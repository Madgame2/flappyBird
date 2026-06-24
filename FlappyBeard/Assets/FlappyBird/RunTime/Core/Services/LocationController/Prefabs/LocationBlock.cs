using System;
using UnityEngine;
using UnityEngine.Pool;

public class LocationBlock : MonoBehaviour
{
    [SerializeField] private float _width = 10f; 
    [SerializeField] private MoveStrategyBase[] moveStrateges;
    
    public float Width => _width;
    public  MoveStrategyBase[] MoveStrategies => moveStrateges;
    
    public event Action<LocationBlock> OnRequestRelease;

    public void Deactivate()
    {
        OnRequestRelease?.Invoke(this);
    }
}
