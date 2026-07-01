using System.Threading;
using Cysharp.Threading.Tasks;

namespace FlappyBird.RunTime.Core.Services.ScenesService.Interfaces
{
    public interface ISceneService
    {
        UniTask LoadScene(string sceneName, CancellationToken ct = default);
    }
}