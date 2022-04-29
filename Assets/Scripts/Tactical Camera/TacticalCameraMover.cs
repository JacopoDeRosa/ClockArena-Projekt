using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TacticalCameraMover : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float _speed;
    [SerializeField] private float _inputSmoothing;

    private Vector2 _moveInput;
    private Vector2 _currentInput;
    private float _inputSmoothReal;

    private void OnValidate()
    {
        if(_input == null)
        {
            _input = FindObjectOfType<PlayerInput>();
        }
    }

    private void Awake()
    {
        _input.actions["Move"].performed += OnMove;
        _inputSmoothReal = Mathf.Clamp01(_inputSmoothing * Time.deltaTime);
    }

    private void OnDestroy()
    {
        if (_input != null)
        {
            _input.actions["Move"].performed -= OnMove;
        }
    }

    private void Update()
    {       
        if(_currentInput != _moveInput)
        {
            _currentInput = Vector2.Lerp(_currentInput, _moveInput, _inputSmoothReal);
        }
        transform.Translate(new Vector3(_currentInput.x, 0, _currentInput.y) * _speed * Time.deltaTime);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
}
