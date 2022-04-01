using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IAction
{
    public event Action onActionStarted;
    public event Action onActionEnded;
}
