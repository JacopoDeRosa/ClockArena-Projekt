using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class ActionScheduler : MonoBehaviour
{
    [SerializeField] private GameTurnManager _turnManager;

    private BarAction _currentAction;

    private List<BarAction> _activeActions;

    private PlayerInput _input;

    public event Action onActionsUpdated;

    public List<BarAction> ActiveActions { get => _activeActions; }

    private void Awake()
    {
        _turnManager.onNextCharacter.AddListener(ReadCharacterActions);
    }

    private void Start()
    {
        _input = FindObjectOfType<PlayerInput>();
        if (_input)
        {
            _input.actions["Cancel"].started += OnCancel;
        }
    }

    private void OnDestroy()
    {
        if (_input)
        {
            _input.actions["Cancel"].started -= OnCancel;
        }
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        CancelCurrentAction();
    }

    private void ReadCharacterActions(Character character)
    {
        if (_activeActions == null)
        {
            _activeActions = new List<BarAction>();
        }
        else
        {
            _activeActions.Clear();
        }

        IBarAction[] barActions = character.GetComponentsInChildren<IBarAction>();

        foreach (IBarAction action in barActions)
        {
            _activeActions.AddRange(action.GetBarActions());
        }

        onActionsUpdated?.Invoke();
    }

    public void StartAction(int index)
    {
        if (_currentAction != null) return;
        _currentAction = _activeActions[index];
        _currentAction.Parent.onActionEnd += ClearCurrentAction;
        _currentAction.BeginCallback.Invoke();

    }

    private void ClearCurrentAction()
    {
        if (_currentAction == null) return;
        _currentAction.Parent.onActionEnd -= ClearCurrentAction;
        _currentAction = null;
    }

    private void CancelCurrentAction()
    {
        if (_currentAction == null) return;

        _currentAction.Parent.onActionEnd -= ClearCurrentAction;
        _currentAction.CancelCallback.Invoke();
        _currentAction = null;
    }
}
