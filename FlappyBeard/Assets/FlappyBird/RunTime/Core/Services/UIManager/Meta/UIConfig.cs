using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIConfig", menuName = "UI/Config")]
public class UIConfig : ScriptableObject
{
    public List<WindowData> Windows;
    
    private Dictionary<string, WindowData> _windowsDict;

    public WindowData GetWindowData(string id)
    {
        if (_windowsDict == null)
        {
            _windowsDict = new Dictionary<string, WindowData>();
            foreach (var windowData in Windows)
            {
                if (!_windowsDict.ContainsKey(windowData.ID))
                {
                    _windowsDict.Add(windowData.ID, windowData);
                }
                else
                {
                    Debug.LogError($"[UIConfig] Дубликат ID окна: {windowData.ID}");
                }
            }
        }

        if (_windowsDict.TryGetValue(id, out var data))
        {
            return data;
        }

        Debug.LogError($"[UIConfig] Окно с ID '{id}' не найдено в конфиге!");
        return default;
    }
}
