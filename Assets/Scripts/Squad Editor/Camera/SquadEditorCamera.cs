using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SquadEditorCamera : MonoBehaviour
{
    [SerializeField] private float _speed, _breakPower;
    [SerializeField] private float _horizontalOffset, _angleOffset;

    private PlayerInput _input;
    private Vector2 _mouseDelta;
    private Quaternion _leftRot, _rightRot;
    private Vector3 _leftMax, _rightMax;

    private bool _focused;
    private float _evaluation;

    private void Awake()
    {
        _leftMax = new Vector3(-_horizontalOffset, 0, 0);
        _rightMax = new Vector3(_horizontalOffset, 0, 0);
        _rightRot = Quaternion.Euler(0, -_angleOffset, 0);
        _leftRot = Quaternion.Euler(0, _angleOffset, 0);
        _evaluation = 0.5f;
    }

    private void Start()
    {
        _input = PlayerInputSingleton.Instance;
        if (_input)
        {
            _input.actions["Look"].performed += OnMouseLook;
            _input.actions["Focus"].started += OnFocusStart;
            _input.actions["Focus"].canceled += OnFocusEnd;

        }
    }
    private void OnDisable()
    {
        if (_input)
        {
            _input.actions["Look"].performed -= OnMouseLook;
            _input.actions["Focus"].started -= OnFocusStart;
            _input.actions["Focus"].canceled -= OnFocusEnd;
        }
    }

    private void Update()
    {
        if (_focused)
        {
            _evaluation += _mouseDelta.x * _speed * Time.deltaTime;
        }

        _evaluation = Mathf.Clamp01(_evaluation);      
        transform.position = Vector3.Lerp(_leftMax, _rightMax, _evaluation);
        transform.rotation = Quaternion.Lerp(_leftRot, _rightRot, _evaluation);
    }

    private void OnMouseLook(InputAction.CallbackContext context)
    {
        _mouseDelta = context.ReadValue<Vector2>();
    }
    private void OnFocusStart(InputAction.CallbackContext context)
    {
        _focused = true;
    }
    private void OnFocusEnd(InputAction.CallbackContext context)
    {
        _focused = false;
    }
}

