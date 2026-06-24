using UnityEngine;

public interface IMoveable
{
    public Rigidbody2D Rigidbody2D { get; }
    public Transform Transform { get; }
    public GameObject GameObject { get; }
}
