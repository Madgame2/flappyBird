using System;
using FlappyBird.RunTime.Core.Location.Strategies;
using FlappyBird.RunTime.Core.Movement.Interfaces;
using UnityEngine;

namespace FlappyBird.RunTime.Core.Location.Infrastructure
{
    public class LocationBlock : MonoBehaviour, IMoveable
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private MoveStrategyBase[] _moveStrategies;

        private Action<LocationBlock> _releaseAction;

        public MoveStrategyBase[] MoveStrategies => _moveStrategies;
        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        public Transform Transform => transform;

        public void Initialize(Action<LocationBlock> releaseAction)
        {
            _releaseAction = releaseAction;
        }

        public void Deactivate()
        {
            _releaseAction?.Invoke(this);
        }
    }
}