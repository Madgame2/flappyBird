using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FlappyBird.RunTime.Core;
using FlappyBird.RunTime.Core.Services.ScenesService.Interfaces;
using FlappyBird.RunTime.Core.Services.Score;
using FlappyBird.RunTime.Core.Services.UI.Interfaces;
using FlappyBird.RunTime.Core.Services.UI.View;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPresenter: IDisposable
{
    private readonly IUIService _uiService;
    private readonly ScoreService _scoreService;
    private readonly ISceneService _sceneService;
    private GameOverView  _gameOverView;

    public GameOverPresenter(IUIService uiService, ScoreService scoreService, ISceneService sceneService)
    {
        _uiService = uiService;
        _scoreService = scoreService;
        _sceneService = sceneService;
    }
    
    public void Dispose()
    {
        if (_gameOverView != null)
        {
            _gameOverView.OnRestartClicked -= HandleRestart;
            _gameOverView.OnHomeClicked -= HandleHome;
        }
    }
    
    public void ShowGameOver()
    {
        _scoreService.SaveBestScoreIfPossible();
        
        var currentScore = _scoreService.Score;
        var bestScore = _scoreService.BestScore;
        
        _gameOverView =_uiService.Open<GameOverView>("GameOverUI");
        
        _gameOverView.OnRestartClicked += HandleRestart;
        _gameOverView.OnHomeClicked += HandleHome;
        
        _gameOverView.DisplayScore(currentScore, bestScore);
    }
    
    private void HandleRestart()
    {
        _sceneService.ReloadScene().Forget();
    }

    private void HandleHome()
    {
        _sceneService.LoadScene("MenuScene").Forget();
    }
}
