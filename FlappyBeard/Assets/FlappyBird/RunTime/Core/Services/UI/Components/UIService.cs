using System.Collections.Generic;
using System.Linq;
using FlappyBird.RunTime.Core.Services.UI.Factory;
using FlappyBird.RunTime.Core.Services.UI.Interfaces;
using FlappyBird.RunTime.Core.Services.UI.Meta;
using FlappyBird.RunTime.Core.Services.UI.Presenters;
using FlappyBird.RunTime.Core.Services.UI.Provider;
using FlappyBird.RunTime.Core.Services.UI.Repository;
using FlappyBird.RunTime.Core.Services.UI.Routers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FlappyBird.RunTime.Core.Services.UI.Components
{
    public class UIService : IUIService
    {
        private readonly UIConfig _config;
        private readonly UIWindowFactory _factory;
        private readonly UIWindowRepository _repository;
        private readonly UIPopupRouter _popupRouter;
        private readonly UIWindowPresenter _presenter;
        private readonly UIRootProvider _rootProvider;
        
        public UIService(
            UIConfig config,
            UIWindowFactory factory,
            UIWindowRepository repository,
            UIPopupRouter popupRouter,
            UIWindowPresenter presenter,
            UIRootProvider rootProvider)
        {
            _config = config;
            _factory = factory;
            _repository = repository;
            _popupRouter = popupRouter;
            _presenter = presenter;
            _rootProvider = rootProvider;
        }

        public void SetUIRoot(UiRoot uiRoot)
        {
            _rootProvider.Set(uiRoot);
            
            _popupRouter.Clear();
            _repository.Clear();
        }

        public void ClearUIRoot()
        {
            _popupRouter.Clear();
            _repository.Clear();
            _rootProvider.Clear();
        }

        public void Open(string windowId)
        {
            Open<UIElement>(windowId);
        }

        public T Open<T>(string windowId) where T : UIElement
        {
            var data = _config.GetWindowData(windowId);

            if (!_repository.TryGet(windowId, out var window))
            {
                window = _factory.Create(data);
                _repository.Add(windowId, window);
            }

            if (data.Layer == WindowLayer.Popup)
                _popupRouter.Push(window);

            _presenter.Show(window);

            return (T)window;
        }

        public void CloseTop()
        {
            _popupRouter.Pop();
        }

        public void Close(string id)
        {
            if (!_repository.TryGet(id, out var window))
                return;

            _popupRouter.Remove(window);

            _presenter.Hide(window);
        }

        public void CloseAll()
        {
            _popupRouter.Clear();
            _presenter.HideAll(_repository.GetAll());
        }
        
        private void NormalizeRectTransform(RectTransform instance, RectTransform prefab)
        {
            instance.localScale = Vector3.one;
            instance.anchoredPosition = prefab.anchoredPosition;
            instance.offsetMin = prefab.offsetMin;
            instance.offsetMax = prefab.offsetMax;
        }
    }
}