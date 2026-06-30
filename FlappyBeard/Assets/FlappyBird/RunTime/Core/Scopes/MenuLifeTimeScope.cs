using FlappyBird.RunTime.Core.Services.UI.Components;
using FlappyBird.RunTime.Core.Services.UI.Presenters;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FlappyBird.RunTime.Core.Scopes
{
    public class MenuLifeTimeScope: LifetimeScope
    {
        [SerializeField] private UiRoot _uiRoot;
    
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_uiRoot);

            builder.Register<MainMenuPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
        
            builder.RegisterEntryPoint<SceneUiInitializer>();
        }
    }
}
