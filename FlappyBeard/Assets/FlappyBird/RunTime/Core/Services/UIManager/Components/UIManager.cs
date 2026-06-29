using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    [SerializeField] private Transform _uiRoot;
    private UIConfig _uiConfig;
    
    private readonly Dictionary<string, UIElement> _cachedWindows = new ();
    private readonly Stack<UIElement> _uiStack = new();

    public UIManager(UIConfig uiConfig)
    {
        _uiConfig = uiConfig;
    }

    public void Open(string windowId)
    {
        
    }

    public void CloseTop()
    {
        
    }

    public void CloseAll()
    {
        
    }

    private UIElement GetOrCreate(string windowId)
    {
        
    }
}
