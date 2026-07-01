using FlappyBird.RunTime.Core.Services;
using FlappyBird.RunTime.Core.Services.ScenesService.Infrastructure;
using FlappyBird.RunTime.Core.Services.ScenesService.Interfaces;
using FlappyBird.RunTime.Core.Services.Score;
using FlappyBird.RunTime.Core.Services.UI.Components;
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

namespace FlappyBird.RunTime.Core.Scopes
{
    public class AppRootScope : LifetimeScope
    {
        [SerializeField] private UIConfig _uiConfig;
    
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_uiConfig);
        
            builder.Register<SceneService>(Lifetime.Singleton)
                .As<ISceneService>();

            builder.Register<ScoreService>(Lifetime.Singleton);

            builder.Register<UIService>(Lifetime.Singleton).As<IUIService>();

            builder.Register<UIWindowFactory>(Lifetime.Singleton);

            builder.Register<UIWindowRepository>(Lifetime.Singleton);

            builder.Register<UIPopupRouter>(Lifetime.Singleton);

            builder.Register<UIWindowPresenter>(Lifetime.Singleton);

            builder.Register<UIRootProvider>(Lifetime.Singleton);
            builder.Register<DialogService>(Lifetime.Transient).AsImplementedInterfaces();
        
            builder.Register<PlayerControls>(Lifetime.Singleton);
            builder.RegisterEntryPoint<InputService>().AsSelf(); 
        }
    }
}
