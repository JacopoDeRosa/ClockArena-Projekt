using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BarAction
{
    private string _name;
    private string _description;
    private Sprite _icon;
    private Action _beginActionCallback;
    private CancelAction _cancelActionCallback;
    private IBarAction _parentAction;
    
    public string Name { get => _name; }
    public string Description { get => _description; }
    public Sprite Icon { get => _icon; }
    public Action BeginCallback { get => _beginActionCallback; }
    public CancelAction CancelCallback { get => _cancelActionCallback; }
    public IBarAction Parent { get => _parentAction; }
   
    public BarAction(Action beginCallback, CancelAction cancelCallback, IBarAction parent, string name = "Unknown", string description = "No Description Available", Sprite icon = null)
    {
        _name = name;
        _description = description;
        _icon = icon;
        _beginActionCallback = beginCallback;
        _cancelActionCallback = cancelCallback;
        _parentAction = parent;
    }
}

public delegate bool CancelAction();
