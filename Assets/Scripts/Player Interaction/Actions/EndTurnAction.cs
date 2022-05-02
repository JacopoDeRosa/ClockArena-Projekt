using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnAction : PermanentAction
{
    [SerializeField] private GameTurnManager _turnManager;

    public override void Begin()
    {
        if (_actionsScheduler.Busy) return;
        base.Begin();
        performed?.Invoke();
        _turnManager.SetNextCharacter();
        End();
    }
}
