using System.Collections.Generic;

namespace FlappyBird.Runtime.Core.Location.Infrastructure
{
    public class ActiveBlocksRegistry
    {
        public readonly List<LocationBlock> Blocks = new();
    }
}