using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnAction : MonoBehaviour, IAction
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private GameTurnManager _turnManager;

    public event ActionEventHandler onBegin;
    public event Action onEnd;
    public event Action onCancel;

    private ActionsScheduler _actionsScheduler;

    private void Start()
    {
        _actionsScheduler = FindObjectOfType<ActionsScheduler>();
        if(_actionsScheduler != null)
        {
            _actionsScheduler.AddAction(this);
        }
    }

    public void Begin()
    {
        if (_actionsScheduler.Busy) return;
        onBegin?.Invoke(this);
        _turnManager.SetNextCharacter();
        onEnd?.Invoke();
    }

    public bool Cancel()
    {
        onCancel?.Invoke();
        return true;
    }

    public Sprite GetActionIcon()
    {
        return _sprite;
    }
}
