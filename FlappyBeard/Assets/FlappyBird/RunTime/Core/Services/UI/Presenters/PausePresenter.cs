using System;
using System.Threading;
using FlappyBird.RunTime.Core.Services.ScenesService.Interfaces;
using FlappyBird.RunTime.Core.Services.UI.Interfaces;
using FlappyBird.RunTime.Core.Services.UI.View;
using UnityEngine;
using VContainer.Unity;

namespace FlappyBird.RunTime.Core.Services.UI.Presenters
{
    public class PausePresenter : IInitializable, IDisposable
    {
        private readonly IUIService _uiService;
        private readonly InputService _inputService;
        private readonly ISceneService _sceneService;
        private readonly IDialogService _dialogService;
        private CancellationTokenSource _sceneCts;

        private PauseMenuView _view;
        private bool _isOpen;

        public PausePresenter(
            IUIService uiService,
            InputService inputService,
            ISceneService sceneService,
            IDialogService dialogService)
        {
            _uiService = uiService;
            _inputService = inputService;
            _sceneService = sceneService;
            _dialogService = dialogService;
        }

        public void Initialize()
        {
            _inputService.OnCancelRequested += TogglePause;
        }

        public void Dispose()
        {
            _inputService.OnCancelRequested -= TogglePause;

            if (_view != null)
            {
                Unsubscribe();
            }
        }

        private void TogglePause()
        {
            if (_isOpen)
            {
                ClosePause();
            }
            else
            {
                OpenPause();
            }
        }

        private void OpenPause()
        {
            _view = _uiService.Open<PauseMenuView>("PauseMenu");

            _view.ResumeClicked += OnResume;
            _view.MenuClicked += OnMenu;
            _view.ExitClicked += OnExit;

            Time.timeScale = 0f;
            _isOpen = true;
        }

        private void ClosePause()
        {
            Unsubscribe();

            _uiService.Close("PauseMenu");

            Time.timeScale = 1f;
            _isOpen = false;
            _view = null;
        }

        private void Unsubscribe()
        {
            _view.ResumeClicked -= OnResume;
            _view.MenuClicked -= OnMenu;
            _view.ExitClicked -= OnExit;
        }

        private void OnResume()
        {
            ClosePause();
        }

        private async void OnMenu()
        {
            bool result = await _dialogService.ShowWarning("Return to main menu?");

            if (!result)
                return;

            _sceneCts?.Cancel();
            _sceneCts = new CancellationTokenSource();

            Time.timeScale = 1f;

            try
            {
                await _sceneService.LoadScene("MenuScene", _sceneCts.Token);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Возврат в главное меню был отменен.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка при возврате в меню: {ex.Message}");
            }
        }

        private async void OnExit()
        {
            bool result = await _dialogService.ShowWarning("Exit game?");

            if (!result)
                return;

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}