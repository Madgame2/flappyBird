using FlappyBird.RunTime.Core.Services.UI.Components;
using FlappyBird.RunTime.Core.Services.UI.Meta;
using FlappyBird.RunTime.Core.Services.UI.Provider;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FlappyBird.RunTime.Core.Services.UI.Factory
{
    public class UIWindowFactory
    {
        private readonly IObjectResolver _resolver;
        private readonly UIRootProvider _uiRootProvider;

        public UIWindowFactory(
            IObjectResolver resolver,
            UIRootProvider uiRootProvider)
        {
            _resolver = resolver;
            _uiRootProvider = uiRootProvider;
        }

        public UIElement Create(WindowData data)
        {
            var instance =
                _resolver.Instantiate(data.Prefab,
                    _uiRootProvider.Root.transform);

            Normalize(
                instance.GetComponent<RectTransform>(),
                data.Prefab.GetComponent<RectTransform>());

            return instance;
        }

        private void Normalize(RectTransform instance, RectTransform prefab)
        {
            instance.localScale = Vector3.one;
            instance.anchoredPosition = prefab.anchoredPosition;
            instance.offsetMin = prefab.offsetMin;
            instance.offsetMax = prefab.offsetMax;
        }
    }
}