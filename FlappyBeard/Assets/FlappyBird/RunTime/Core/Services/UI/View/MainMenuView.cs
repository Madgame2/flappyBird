using System;
using FlappyBird.RunTime.Core.Services.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird.RunTime.Core.Services.UI.View
{
    public class MainMenuView : UIElement
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;

        public event Action OnPlayClicked;
        public event Action OnExitClicked;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(HandlePlayPressed);
            _exitButton.onClick.AddListener(HandleExitPressed);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(HandlePlayPressed);
            _exitButton.onClick.RemoveListener(HandleExitPressed);
        }

        private void HandlePlayPressed()
        {
            OnPlayClicked?.Invoke();
        }

        private void HandleExitPressed()
        {
            OnExitClicked?.Invoke();
        }
    }
}