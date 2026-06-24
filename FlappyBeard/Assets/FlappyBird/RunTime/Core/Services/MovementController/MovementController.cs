using Cysharp.Threading.Tasks;
using FlappyBird.Rintime.Core.Services.BirdMovment.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FlappyBird.Rintime.Core.Services.BirdMovment.SystemsRegistry;
using UnityEngine;


namespace FlappyBird.Rintime.Core.Services.BirdMovment
{
    public class MovementController : IMovementController, IDisposable
    {

        private readonly MovementSystemRegistry _systemRegistry;

        private readonly Dictionary<Guid, MovementContext> _permanentMovements = new();
        private Queue<MovementContext> _oneShotQueue = new();
        private MovementContext? _currentOneShot; // Текущая выполняемая одноразовая задача
        private CancellationTokenSource _oneShotCts; // Для отмены текущей задачи

        private bool _isGameRunning;

        private readonly CancellationTokenSource _cts = new();


        public MovementController(MovementSystemRegistry systemRegistry)
        {
            _systemRegistry = systemRegistry;

            _isGameRunning = true;
            StartMovementCycle(_cts.Token).Forget();
        }


        private async UniTaskVoid StartMovementCycle(CancellationToken token)
        {
            while (_isGameRunning && !token.IsCancellationRequested)
            {

                foreach (var movementContext in _permanentMovements.Values)
                {
                    CalculateMovement(movementContext);
                }

                while (_oneShotQueue.Count > 0)
                {
                    var movementContext = _oneShotQueue.Dequeue();

                    CalculateMovement(movementContext);
                }

                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, token);
            }
        }

        private void CalculateMovement(MovementContext context)
        {
            foreach (var movementRule in context.Rules)
            {
                try
                {
                    var movementSystem = _systemRegistry.GetSystem(movementRule.Type);
                    movementSystem.Process(context.TargetObject, movementRule.Config);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }

        public void ProcessJump()
        {
            if (!_isGameRunning) return;


        }

        public void Dispose()
        {
            _isGameRunning = false;
        }

        public Guid AddPermanent(MovementContext context)
        {
            _permanentMovements.Add(context.Id, context);
            return context.Id;
        }

        public void RemovePermanent(Guid id)
        {
            _permanentMovements.Remove(id);
        }

        public Guid EnqueueOneShot(MovementContext context)
        {
            _oneShotQueue.Enqueue(context);
            return context.Id;
        }

        public void CancelOneShot(Guid id)
        {
            if (_currentOneShot != null && _currentOneShot.Value.Id == id)
            {
                _oneShotCts?.Cancel();
                return;
            }

            if (_oneShotQueue.Any(x => x.Id == id))
            {
                var remaining = _oneShotQueue.Where(x => x.Id != id).ToList();
                _oneShotQueue.Clear();
                foreach (var ctx in remaining) _oneShotQueue.Enqueue(ctx);
            }
        }

        public void RemoveAllByTarget(GameObject target)
        {
            if (target == null) return;


            var keysToRemove = _permanentMovements
                .Select(kvp => kvp.Key)
                .Where(key => _permanentMovements[key].TargetObject.GameObject == target)
                .ToList(); 

            foreach (var key in keysToRemove)
            {
                _permanentMovements.Remove(key);
            }
            
            if (_currentOneShot != null && _currentOneShot.Value.TargetObject.GameObject == target)
            {
                _oneShotCts?.Cancel();
                _currentOneShot = null;
            }
            
            var queueCount = _oneShotQueue.Count;
            for (int i = 0; i < queueCount; i++)
            {
                var oneShot = _oneShotQueue.Dequeue();
                
                if (oneShot.TargetObject.GameObject != target)
                {
                    _oneShotQueue.Enqueue(oneShot);
                }
            }
        }
    }
}
