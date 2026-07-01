using System.Collections.Generic;
using FlappyBird.RunTime.Core.Services.UI.Components;

namespace FlappyBird.RunTime.Core.Services.UI.Routers
{
    public class UIPopupRouter
    {
        private readonly Stack<UIElement> _stack = new();

        public void Push(UIElement popup)
        {
            if (_stack.Count > 0)
                _stack.Peek().gameObject.SetActive(false);

            _stack.Push(popup);
        }

        public void Pop()
        {
            if (_stack.Count == 0)
                return;

            _stack.Pop().gameObject.SetActive(false);

            if (_stack.Count > 0)
                _stack.Peek().gameObject.SetActive(true);
        }

        public void Remove(UIElement popup)
        {
            var tempStack = new Stack<UIElement>();
            var removed = false;

            while (_stack.Count > 0)
            {
                var current = _stack.Pop();

                if (!removed && current == popup)
                {
                    current.gameObject.SetActive(false);
                    removed = true;
                    continue;
                }

                tempStack.Push(current);
            }

            while (tempStack.Count > 0)
                _stack.Push(tempStack.Pop());

            if (removed && _stack.Count > 0)
                _stack.Peek().gameObject.SetActive(true);
        }

        public void Clear()
        {
            while (_stack.Count > 0)
            {
                var popup = _stack.Pop();

                if (popup != null)
                    popup.gameObject.SetActive(false);
            }
        }
    }
}