using FlappyBird.Rintime.Core.Services.BirdMovment;
using FlappyBird.Rintime.Core.Services.BirdMovment.Systems;
using UnityEngine;
using VContainer;

public class BirdView : MonoBehaviour, IMoveable
{
    [SerializeField] private Rigidbody2D _rigidbody2D;

    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    public Transform Transform => this.transform;
    public GameObject GameObject =>this.gameObject;
}
