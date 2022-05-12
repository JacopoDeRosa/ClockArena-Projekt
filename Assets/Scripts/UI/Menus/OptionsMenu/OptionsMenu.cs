using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public delegate void SettingsChangedHandler();

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Button _applyButton;

    private List<Action> _onOptionsConfirmActions;
    private bool _dirty;

    private void Start()
    {
        ClearSettingsDelegate();
    }

    private void ClearSettingsDelegate()
    {
        _onOptionsConfirmActions = new List<Action>();
    }

    public void AddSettingChange(Action change)
    {
        
        _onOptionsConfirmActions.Add(change);
    }

    public void ApplySettings()
    {
        foreach (var change in _onOptionsConfirmActions)
        {
            change.Invoke();
        }
    }

    public void Redraw()
    {
        foreach (var item in FindObjectsOfType<GameOptionSetter>())
        {
            item.Redraw();
        }  
    }
 
}
