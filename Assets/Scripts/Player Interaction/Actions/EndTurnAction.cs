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

    private void Start()
    {
        FindObjectOfType<ActionsScheduler>().AddAction(this);
    }

    public void Begin()
    {
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
