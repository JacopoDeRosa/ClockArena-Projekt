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
    [ShowInInspector]
    private bool _busy = false;


    public event ActionEventHandler onBegin;
    public event Action onEnd;
    public event Action onCancel;

    private void OnValidate()
    {
        if(_playerInput == null)
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
        FindObjectOfType<ActionsScheduler>().AddAction(this);
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
            if(_pointGetter.GetMousePoint(out Vector3 hit))
            {
                _turnManager.ActiveCharacter.Mover.MoveToPoint(hit);
                _turnManager.ActiveCharacter.Mover.onMoveEnd.AddListener(InvokeOnEnd);
            }

            _gizmo.gameObject.SetActive(false);
            _pathRenderer.ClearPath();
            
        }
    }

    private void Update()
    {
        if(_targeting)
        {
            if (_pointGetter.GetMousePoint(out Vector3 hit))
            {
                _gizmo.position = hit;
                if(_turnManager.ActiveCharacter.Mover.TryCalculatePath(hit, out Vector3[] points, out float lenght))
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
        _targeting = true;
        onBegin?.Invoke(this);
    }
    public bool Cancel()
    {
        if(_busy)
        {
            return false;
        }
        _targeting = false;
        onCancel?.Invoke();
        return true;
    }
    private void InvokeOnEnd()
    {
        onEnd?.Invoke();
        _turnManager.ActiveCharacter.Mover.onMoveEnd.RemoveListener(InvokeOnEnd);
        _busy = false;
    }
    public Sprite GetActionIcon()
    {
        return _actionSprite;
    }
}
