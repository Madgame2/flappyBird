using System.Collections.Generic;
using System.Linq;
using FlappyBird.RunTime.Core.Services.UI.Components;

namespace FlappyBird.RunTime.Core.Services.UI.Repository
{
    public class UIWindowRepository
    {
        private readonly Dictionary<string, UIElement> _windows = new();

        public bool TryGet(string id, out UIElement window)
            => _windows.TryGetValue(id, out window);

        public void Add(string id, UIElement window)
            => _windows[id] = window;

        public void RemoveDestroyed()
        {
            var destroyed = _windows
                .Where(x => x.Value == null)
                .Select(x => x.Key)
                .ToList();

            foreach (var key in destroyed)
                _windows.Remove(key);
        }

        public IEnumerable<UIElement> GetAll()
            => _windows.Values;

        public void Clear()
        {
            _windows.Clear();
        }
    }
}