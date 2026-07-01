using FlappyBird.RunTime.Core.Services.UI.Components;
using VContainer;

namespace FlappyBird.RunTime.Core.Services.UI.Interfaces
{
    public interface IUIService
    {
        void SetUIRoot(UiRoot uiRoot);
        void ClearUIRoot();
        void Open(string windowId);
        T Open<T>(string windowId) where T : UIElement;
        void CloseTop();
        void Close(string windowId);
        void CloseAll();
    }
}
