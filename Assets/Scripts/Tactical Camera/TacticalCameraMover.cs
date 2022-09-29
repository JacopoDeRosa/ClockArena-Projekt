using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TacticalCameraMover : TacCameraComponent
{
   
    [SerializeField] private float _speed;
    [SerializeField] private float _inputSmoothing;

    private PlayerInput _input;
    private Vector2 _moveInput;
    private Vector2 _currentInput;
    private Vector3 _translateVector;
    private float _inputSmoothReal;

    private float _forwardMultiplier, _backwardMultiplier, _rightMultiplier, _leftMultiplier;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(_moveChecker.position, _checkThickness);
    }

    private void Start()
    {
        _translateVector = Vector3.zero;
        _input = PlayerInputSingleton.Instance;

        if (_input != null)
        {
            _input.actions["Move"].performed += OnMove;
        }
        else
        {
            Debug.Log("Input is null");
        }
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
        MultiplyInput();
        SmoothInput();

        _translateVector.Set(_currentInput.x, 0, _currentInput.y);
        transform.Translate(_translateVector * _speed * Time.deltaTime);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void SmoothInput()
    {
        if (_currentInput != _moveInput)
        {
            _currentInput = Vector2.Lerp(_currentInput, _moveInput, _inputSmoothReal);
        }
    }

    private void MultiplyInput()
    {
        if (_moveInput.magnitude == 0) return;

        float xSign = Mathf.Sign(_moveInput.x);
        float ySign = Mathf.Sign(_moveInput.y);
        
        if(xSign > 0)
        {
            _moveInput.x *= CheckDirection(_moveChecker.right);
        }
        else
        {
            _moveInput.x *= CheckDirection(-_moveChecker.right);
        }

        if(ySign > 0)
        {
            _moveInput.y *= CheckDirection(_moveChecker.forward);
        }
        else
        {
            _moveInput.y *= CheckDirection(-_moveChecker.forward);
        }
    }
}
