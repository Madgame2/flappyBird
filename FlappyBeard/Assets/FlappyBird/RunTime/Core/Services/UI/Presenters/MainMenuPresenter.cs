using System;
using System.Threading;
using FlappyBird.RunTime.Core.Services.ScenesService.Interfaces;
using FlappyBird.RunTime.Core.Services.UI.Interfaces;
using FlappyBird.RunTime.Core.Services.UI.View;
using UnityEngine;
using VContainer.Unity;

namespace FlappyBird.RunTime.Core.Services.UI.Presenters
{
    public class MainMenuPresenter : IStartable, IDisposable
    {
        private readonly IUIService _uiService;
        private readonly IDialogService _dialogService;
        private readonly ISceneService _sceneService;
        private CancellationTokenSource _sceneCts;
        private MainMenuView _view;

        public MainMenuPresenter(IUIService uiService, IDialogService dialogService, ISceneService sceneService)
        {
            _uiService = uiService;
            _dialogService = dialogService;
            _sceneService = sceneService;
        }

        public void Start()
        {
            _view = _uiService.Open<MainMenuView>("MainMenu");

            _view.OnPlayClicked += HandlePlay;
            _view.OnExitClicked += HandleExit;
        }

        private async void HandlePlay()
        {
            _sceneCts?.Cancel();
            _sceneCts = new CancellationTokenSource();

            try
            {
                await _sceneService.LoadScene("GameScene", _sceneCts.Token);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Загрузка GameScene была отменена.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка при переходе в игру: {ex.Message}");
            }
        }

        private async void HandleExit()
        {
            if (!await _dialogService.ShowWarning("Are you sure you want to exit the game?"))
                return;

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
        }

        public void Dispose()
        {
            _view.OnPlayClicked -= HandlePlay;
            _view.OnExitClicked -= HandleExit;
        }
    }
}