using System;
using FlappyBird.RunTime.Core.Services.UI.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird.RunTime.Core.Services.UI.View
{
    public class GameOverView: UIElement
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _bestScoreText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _homeButton;
        
        public event Action OnRestartClicked;
        public event Action OnHomeClicked;
        
        public void DisplayScore(int score, int bestScore)
        {
            if (_scoreText != null)
            {
                _scoreText.text = score.ToString();
            }
        
            if (_bestScoreText != null)
            {
                _bestScoreText.text = bestScore.ToString();
            }
        }
        
        private void Awake()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _homeButton.onClick.AddListener(OnHomeButtonClick);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClick);
            _homeButton.onClick.RemoveListener(OnHomeButtonClick);
        }
        
        private void OnRestartButtonClick()
        {
            OnRestartClicked?.Invoke();
        }

        private void OnHomeButtonClick()
        {
            OnHomeClicked?.Invoke();
        }
    }
}