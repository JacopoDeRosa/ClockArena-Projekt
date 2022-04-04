using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;


public class ActionsScheduler : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;

    [ShowInInspector]
    private List<IAction> _actions = new List<IAction>();

    [ShowInInspector]
    private IAction _currentAction;


    public IEnumerable<IAction> Actions { get => _actions; }
    [ShowInInspector]
    public bool Busy { get => _currentAction != null; }

    public event ActionEventHandler onActionAdded;
    public event ActionEventHandler onActionRemoved;

    private void Awake()
    {
        _input.actions["Cancel"].started += OnCancel;
        _actions = new List<IAction>();
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        CancelCurrentAction();
    }

    private void CancelCurrentAction()
    {
        if(_currentAction != null)
        {
            if (_currentAction.Cancel())
            {
                _currentAction = null;
            }
        }
    }

    private void SetActiveAction(IAction action)
    {
        if (Busy) return;
        _currentAction = action;
    }

    private void OnActionEnd()
    {
        _currentAction = null;
    }

    public void AddAction(IAction action)
    {
        if (action == null) return;
        action.onBegin += SetActiveAction;
        action.onEnd += OnActionEnd;
        _actions.Add(action);
        onActionAdded?.Invoke(action);
        
    }
    public void RemoveAction(IAction action)
    {
        if (action == null) return;
        action.onBegin -= SetActiveAction;
        action.onEnd -= OnActionEnd;
        _actions.Remove(action);
        onActionRemoved?.Invoke(action);
    }
}