using System;
using FlappyBird.RunTime.Core.Services.UI.Interfaces;
using VContainer;
using VContainer.Unity;

namespace FlappyBird.RunTime.Core.Services.UI.Components
{
    public class SceneUiInitializer: IInitializable, IDisposable
    {
        private UiRoot _uiRoot;
        private IUIService _uiService;
        private IObjectResolver _objectResolver;

        public SceneUiInitializer(UiRoot uiRoot, Interfaces.IUIService uiService, IObjectResolver objectResolver)
        {
            _uiRoot = uiRoot;
            _uiService = uiService;
            _objectResolver = objectResolver;
        }
    
        public void Initialize()
        {
            _uiService.SetUIRoot(_uiRoot);
        }

        public void Dispose()
        {
            _uiService.ClearUIRoot();
        }
    }
}
