using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FlappyBird.RunTime.Core.Services.ScenesService.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBird.RunTime.Core.Services.ScenesService.Infrastructure
{
    public class SceneService : ISceneService
    {
        private ISceneService _sceneServiceImplementation;

        public async UniTask LoadScene(string sceneName, CancellationToken token = default)
        {
            try
            {
                var operation = SceneManager.LoadSceneAsync(sceneName);
                
                await operation.ToUniTask(cancellationToken: token);
            }
            catch (OperationCanceledException)
            {
                Debug.Log($"Загрузка сцены {sceneName} была отменена.");
                throw; 
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка при загрузке сцены {sceneName}: {ex.Message}");
                throw;
            }
        }

        public async UniTask ReloadScene(CancellationToken token = default)
        {
            try
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
            
                await LoadScene(currentSceneName, token);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Перезагрузка текущей сцены была отменена.");
                throw;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка при перезагрузке текущей сцены: {ex.Message}");
                throw;
            }
        }
    }
}