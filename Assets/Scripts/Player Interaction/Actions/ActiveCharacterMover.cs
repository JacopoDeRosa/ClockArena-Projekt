using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

public class ActiveCharacterMover : MonoBehaviour, IAction
{
    [SerializeField] private Sprite _actionSprite;
    [SerializeField] private GameTurnManager _turnManager;
    [SerializeField] private MousePointGetter _pointGetter;
    [SerializeField] private Gradient _validPathColor, _invalidPathColor;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Transform _gizmo;
    [SerializeField] private PathRenderer _pathRenderer;

    [ShowInInspector]
    private bool _targeting = false;


    private ActionsScheduler _actionsScheduler;


    public event ActionEventHandler onBegin;
    public event Action onEnd;
    public event Action onCancel;

    private void OnValidate()
    {
        if (_playerInput == null)
        {
            _playerInput = FindObjectOfType<PlayerInput>();
        }
    }
    private void Awake()
    {
        _playerInput.actions["Confirm"].started += OnConfirmDown;

    }
    private void Start()
    {
        _actionsScheduler = FindObjectOfType<ActionsScheduler>();
        if (_actionsScheduler != null)
        {
            _actionsScheduler.AddAction(this);
        }
    }
    private void OnDestroy()
    {
        if (_playerInput != null)
        {
            _playerInput.actions["Confirm"].started -= OnConfirmDown;
        }
    }

    private void OnConfirmDown(InputAction.CallbackContext context)
    {
        if (_targeting)
        {
            _targeting = false;
            if (_pointGetter.GetMousePoint(out Vector3 hit))
            {
                if (_turnManager.ActiveCharacter.Mover.TryCalculatePath(hit, out Vector3[] points, out float lenght))
                {
                    _turnManager.ActiveCharacter.Mover.MoveToPoint(hit);
                    _turnManager.ActiveCharacter.Mover.onMoveEnd.AddListener(InvokeOnMoveEnd);
                   
                }
                else
                {
                    onEnd?.Invoke();
                }
            }

            _gizmo.gameObject.SetActive(false);
            _pathRenderer.ClearPath();
        }
    }

    private void Update()
    {
        if (_targeting)
        {
            if (_pointGetter.GetMousePoint(out Vector3 hit))
            {
                _gizmo.position = hit;
                if (_turnManager.ActiveCharacter.Mover.TryCalculatePath(hit, out Vector3[] points, out float lenght))
                {
                    _pathRenderer.SetGizmoColor(_validPathColor);
                    _pathRenderer.RenderPath(points);
                }
                else
                {
                    _pathRenderer.SetGizmoColor(_invalidPathColor);
                }

            }
        }

    }

    public void Begin()
    {
        if (_actionsScheduler.Busy) return;
        if (_targeting)
        {
            Cancel();
            return;
        }
        _targeting = true;
        onBegin?.Invoke(this);
    }
    public bool Cancel()
    {
        _targeting = false;
        onCancel?.Invoke();
        _pathRenderer.ClearPath();
        return true;
    }
    private void InvokeOnMoveEnd()
    {
        onEnd?.Invoke();
        _turnManager.ActiveCharacter.Mover.onMoveEnd.RemoveListener(InvokeOnMoveEnd);
    }
    public Sprite GetActionIcon()
    {
        return _actionSprite;
    }
}
