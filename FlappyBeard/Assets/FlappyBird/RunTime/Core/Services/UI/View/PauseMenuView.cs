using System;
using FlappyBird.RunTime.Core.Services.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird.RunTime.Core.Services.UI.View
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class PauseMenuView : UIElement
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _exitButton;

        public event Action ResumeClicked;
        public event Action MenuClicked;
        public event Action ExitClicked;

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(HandleResumePressed);
            _menuButton.onClick.AddListener(HandleMenuPressed);
            _exitButton.onClick.AddListener(HandleExitPressed);
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(HandleResumePressed);
            _menuButton.onClick.RemoveListener(HandleMenuPressed);
            _exitButton.onClick.RemoveListener(HandleExitPressed);
        }

        private void HandleResumePressed()
        {
            ResumeClicked?.Invoke();
        }

        private void HandleMenuPressed()
        {
            MenuClicked?.Invoke();
        }

        private void HandleExitPressed()
        {
            ExitClicked?.Invoke();
        }
    }
}