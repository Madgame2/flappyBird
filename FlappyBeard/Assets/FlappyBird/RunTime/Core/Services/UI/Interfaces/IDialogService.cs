using System.Threading;
using Cysharp.Threading.Tasks;

namespace FlappyBird.RunTime.Core.Services.UI.Interfaces
{
    public interface IDialogService
    {
        UniTask<bool> ShowWarning(string message, CancellationToken ct = default);
    }
}
