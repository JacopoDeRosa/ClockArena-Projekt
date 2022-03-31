using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoverController : MonoBehaviour
{
    [SerializeField] private GameTurnManager _turnManager;
    [SerializeField] private MousePointGetter _pointGetter;
    [SerializeField] private Gradient _validPathColor, _invalidPathColor;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Transform _gizmo;
    [SerializeField] private PathRenderer _pathRenderer;
    [SerializeField] 
    private bool _targeting = false;

    private void OnValidate()
    {
        if(_playerInput == null)
        {
            _playerInput = FindObjectOfType<PlayerInput>();
        }
    }
    private void Awake()
    {
        _playerInput.actions["LeftMouse"].started += OnLeftMouseDown;
        _playerInput.actions["LeftMouse"].canceled += OnLeftMouseUp;

    }
    private void OnDestroy()
    {
        if (_playerInput != null)
        {
            _playerInput.actions["LeftMouse"].started -= OnLeftMouseDown;
            _playerInput.actions["LeftMouse"].canceled -= OnLeftMouseUp;
        }
    }

    private void OnLeftMouseDown(InputAction.CallbackContext context)
    {
        if (_turnManager.ActiveCharacter.IsBusy) return;
        if (_targeting == false)
        {
            _targeting = true;
            _gizmo.gameObject.SetActive(true);
        }
    }
    private void OnLeftMouseUp(InputAction.CallbackContext context)
    {
        if (_turnManager.ActiveCharacter.IsBusy) return;
        if (_targeting)
        {
            _targeting = false;
            if(_pointGetter.GetMousePoint(out Vector3 hit))
            {
                _turnManager.ActiveCharacter.Mover.MoveToPoint(hit);
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
}
