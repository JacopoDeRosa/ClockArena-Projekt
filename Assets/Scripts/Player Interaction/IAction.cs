using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IAction
{
    public event ActionEventHandler onBegin;
    public event Action onEnd;
    public event Action onCancel;

    public Sprite GetActionIcon();

    public void Begin();

    /// <summary>
    /// Was the action Canceled?
    /// </summary>
    /// <returns></returns>
    public bool Cancel();
}

public delegate void ActionEventHandler(IAction action);
