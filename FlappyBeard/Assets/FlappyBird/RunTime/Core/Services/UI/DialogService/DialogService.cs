using System.Threading;
using Cysharp.Threading.Tasks;
using FlappyBird.RunTime.Core.Services.UI.Interfaces;
using FlappyBird.RunTime.Core.Services.UI.View;

namespace FlappyBird.RunTime.Core.Services.UI.Components
{
    public class DialogService : IDialogService
    {
        private readonly IUIService _uiService;

        public DialogService(Interfaces.IUIService uiService)
        {
            _uiService = uiService;
        }
        
        public async UniTask<bool> ShowWarning(string message, CancellationToken ct = default)
        {
            var dialog = _uiService.Open<WarningQuestionView>("WarningQuestion");
            
            dialog.SetMessage(message);

            try
            {
                var isAccepted = await dialog.WaitForDecisionAsync(ct);
                
                return isAccepted;
            }
            finally
            {
                _uiService.Close("WarningQuestion");
            }
        }
    }
}
