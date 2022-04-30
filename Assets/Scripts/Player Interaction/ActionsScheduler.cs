using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;


public class ActionsScheduler : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;

    [ShowInInspector]
    private List<CharacterAction> _actions = new List<CharacterAction>();

    [ShowInInspector]
    private CharacterAction _currentAction;


    public IEnumerable<CharacterAction> Actions { get => _actions; }

    [ShowInInspector]
    public bool Busy { get => _currentAction != null; }

    public event CharacterActionHandler onActionAdded;
    public event CharacterActionHandler onActionRemoved;

    private void Awake()
    {
        _input.actions["Cancel"].started += OnCancel;
        _actions = new List<CharacterAction>();
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

    private void SetActiveAction(CharacterAction action)
    {
        if (Busy) return;
        _currentAction = action;
    }
    private void OnActionEnd(CharacterAction action)
    {
        if (action == _currentAction)
        {
            _currentAction = null;
        }
    }

    public void AddAction(CharacterAction action)
    {
        if (action == null) return;
        action.onBegin += SetActiveAction;
        action.onEnd += OnActionEnd;
        _actions.Add(action);
        onActionAdded?.Invoke(action);   
    }
    public void RemoveAction(CharacterAction action)
    {
        if (action == null) return;
        action.onBegin -= SetActiveAction;
        action.onEnd -= OnActionEnd;
        _actions.Remove(action);
        onActionRemoved?.Invoke(action);
    }
}