using System.Collections.Generic;
using FlappyBird.RunTime.Core.Services.UI.Components;

namespace FlappyBird.RunTime.Core.Services.UI.Presenters
{
    public sealed class UIWindowPresenter
    {
        public void Show(UIElement window)
        {
            window.gameObject.SetActive(true);
        }

        public void Hide(UIElement window)
        {
            window.gameObject.SetActive(false);
        }

        public void HideAll(IEnumerable<UIElement> windows)
        {
            foreach (var window in windows)
                Hide(window);
        }
    }
}
