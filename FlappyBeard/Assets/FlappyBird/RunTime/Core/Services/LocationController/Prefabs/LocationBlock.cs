using UnityEngine;

public class LocationBlock : MonoBehaviour
{
    [SerializeField] private float _width = 10f; 
    [SerializeField] private MoveStrategyBase[] moveStrateges;
    public float Width => _width;
    public  MoveStrategyBase[] MoveStrategies => moveStrateges;
}
