using System;
using FlappyBird.Rintime.Core.Services.BirdMovment.Meta;
using UnityEngine;

public struct MovementContext
{
    public Guid Id { get;}
    public IMoveable TargetObject { get; }
    public GameObjectType Type { get; }
    public MovementRule[] Rules { get; }
    
    public MovementContext(IMoveable targetObject, GameObjectType type, MovementRule[] rules)
    {
        Id = Guid.NewGuid();
        TargetObject = targetObject;
        Type = type;
        Rules = rules ?? Array.Empty<MovementRule>();
    }
}
