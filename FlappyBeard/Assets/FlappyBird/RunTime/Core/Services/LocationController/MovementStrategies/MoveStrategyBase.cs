using FlappyBird.Rintime.Core.Services.BirdMovment;
using FlappyBird.Rintime.Core.Services.BirdMovment.Meta;
using UnityEngine;

public abstract  class MoveStrategyBase : ScriptableObject
{
    public abstract MovementType  MovementType { get; }
    public abstract IBaseConfig MoveConfig { get; }
}
