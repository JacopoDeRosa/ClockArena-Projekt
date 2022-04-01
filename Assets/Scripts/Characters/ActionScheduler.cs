using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ActionScheduler : MonoBehaviour
{
    [ShowInInspector]
    private bool _busy;


    private void Awake()
    {
        SubToActions();
    }

    private void SubToActions()
    {
        var actions = GetComponents<IAction>();
        foreach (var action in actions)
        {
            action.onActionStarted += OnActionStart;
            action.onActionEnded += OnActionEnd;
        }
    }

    private void OnActionStart()
    {
        _busy = true;
    }
    private void OnActionEnd()
    {
        _busy = false;
    }
}
