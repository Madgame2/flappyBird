using System;

namespace FlappyBird.Runtime.Core.Location.Interfaces
{
    public interface IDisposableBlock
    {
        event Action<IDisposableBlock> OnRequestRelease;
    }
}