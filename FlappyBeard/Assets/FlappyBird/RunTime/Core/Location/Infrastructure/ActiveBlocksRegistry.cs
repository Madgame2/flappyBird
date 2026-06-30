using System.Collections.Generic;

namespace FlappyBird.RunTime.Core.Location.Infrastructure
{
    public class ActiveBlocksRegistry
    {
        public readonly List<LocationBlock> Blocks = new();
    }
}