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

        private Dictionary<Guid, MovementContext> _permanentMovementsById = new();
        private Queue<MovementContext> _pendingOneShots = new();
        private MovementContext? _activeOneShotMovement; 
        private CancellationTokenSource _oneShotCancellation; 
        
        private CancellationTokenSource _movementCycleCancellation = new();
        
        public MovementController(MovementSystemRegistry systemRegistry)
        {
            _systemRegistry = systemRegistry;
            
            StartMovementCycle(_movementCycleCancellation.Token).Forget();
        }
        
        private async UniTaskVoid StartMovementCycle(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                foreach (var movementContext in _permanentMovementsById.Values)
                {
                    CalculateMovement(movementContext);
                }

                while (_pendingOneShots.Count > 0)
                {
                    var movementContext = _pendingOneShots.Dequeue();

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
        

        public void Dispose()
        {
            _movementCycleCancellation?.Cancel();
                
            _oneShotCancellation?.Cancel();
            _oneShotCancellation?.Dispose();
            
            _pendingOneShots.Clear();
            _permanentMovementsById.Clear();
        }

        public Guid AddPermanent(MovementContext context)
        {
            _permanentMovementsById.Add(context.Id, context);
            return context.Id;
        }

        public void RemovePermanent(Guid id)
        {
            _permanentMovementsById.Remove(id);
        }

        public Guid EnqueueOneShot(MovementContext context)
        {
            _pendingOneShots.Enqueue(context);
            return context.Id;
        }

        public void CancelOneShot(Guid id)
        {
            if (_activeOneShotMovement != null && _activeOneShotMovement.Value.Id == id)
            {
                _oneShotCancellation?.Cancel();
                return;
            }

            if (_pendingOneShots.Any(x => x.Id == id))
            {
                var remaining = _pendingOneShots.Where(x => x.Id != id).ToList();
                
                _pendingOneShots.Clear();
                
                foreach (var ctx in remaining) _pendingOneShots.Enqueue(ctx);
            }
        }

        public void RemoveAllByTarget(GameObject target)
        {
            if (target == null) return;

            var keysToRemove = _permanentMovementsById
                .Select(kvp => kvp.Key)
                .Where(key => _permanentMovementsById[key].TargetObject.GameObject == target)
                .ToList(); 

            foreach (var key in keysToRemove)
            {
                _permanentMovementsById.Remove(key);
            }
            
            if (_activeOneShotMovement != null && _activeOneShotMovement.Value.TargetObject.GameObject == target)
            {
                _oneShotCancellation?.Cancel();
                
                _activeOneShotMovement = null;
            }
            
            var queueCount = _pendingOneShots.Count;
            for (int i = 0; i < queueCount; i++)
            {
                var oneShot = _pendingOneShots.Dequeue();
                
                if (oneShot.TargetObject.GameObject != target)
                {
                    _pendingOneShots.Enqueue(oneShot);
                }
            }
        }
    }
}
