using FlappyBird.Rintime.Core.Services.BirdMovment.LinearMotion;
using UnityEngine;

namespace FlappyBird.RunTime._Temp
{
    public class linearConfig: ILinearConfig
    {
        public Vector2 Direction => new Vector2(-1, 0);
        public float Speed => 5f;
    }
}