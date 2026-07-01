using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FlappyBird.RunTime.Core.Services.UI.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird.RunTime.Core.Services.UI.View
{
    public class WarningQuestionView : UIElement
    {
        [SerializeField] private TMP_Text _message;
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;

        public void SetMessage(string message)
        {
            _message.text = message;
        }
        
        public async UniTask<bool> WaitForDecisionAsync(CancellationToken ct = default)
        {
            int index = await UniTask.WhenAny(
                _yesButton.OnClickAsync(ct),
                _noButton.OnClickAsync(ct)
            );

            return index == 0;
        }
    }
}
