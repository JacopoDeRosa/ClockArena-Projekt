using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IAction
{
    public event Action onStart;
    public event Action onEnd;
    public event Action onCancel;

    public void Begin();
    public void End();
    public void Cancel();
}
