using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SquadEditorCamera : MonoBehaviour
{
    [SerializeField] private float _speed, _breakPower;
    [SerializeField] private float _horizontalOffset;

    private PlayerInput _input;
    private Vector2 _mouseDelta;

    private float _rotationSpeed;
    private bool _focused;

    private void Start()
    {
        _input = FindObjectOfType<PlayerInput>();
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

