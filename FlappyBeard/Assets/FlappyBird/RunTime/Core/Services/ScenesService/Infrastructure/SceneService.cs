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
        public async UniTask LoadScene(string sceneName, CancellationToken ct = default)
        {
            try
            {
                var operation = SceneManager.LoadSceneAsync(sceneName);
                
                await operation.ToUniTask(cancellationToken: ct);
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
    }
}