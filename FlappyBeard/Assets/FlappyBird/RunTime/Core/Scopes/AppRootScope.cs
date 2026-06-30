using FlappyBird.RunTime.Core.Services;
using FlappyBird.RunTime.Core.Services.ScenesService.Infrastructure;
using FlappyBird.RunTime.Core.Services.ScenesService.Interfaces;
using FlappyBird.RunTime.Core.Services.Score;
using FlappyBird.RunTime.Core.Services.UI.Components;
using FlappyBird.RunTime.Core.Services.UI.Meta;
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
        
            builder.Register<UIManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<DialogService>(Lifetime.Transient).AsImplementedInterfaces();
        
            builder.Register<PlayerControls>(Lifetime.Singleton);
            builder.RegisterEntryPoint<InputService>().AsSelf(); 
        }
    }
}
