using FlappyBird.RunTime.Core.Services.UI.Components;

namespace FlappyBird.RunTime.Core.Services.UI.Provider
{
    public class UIRootProvider
    {
        public UiRoot Root { get; private set; }

        public void Set(UiRoot root)
        {
            Root = root;
        }

        public void Clear()
        {
            Root = null;
        }
    }
}