using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TacticalCameraMover : MonoBehaviour
{
   
    [SerializeField] private float _speed;
    [SerializeField] private float _inputSmoothing;
    [SerializeField] private Transform _moveChecker;
    [SerializeField] private float _checkDistance, _checkThickness;
    [SerializeField] private LayerMask _checkMask;

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


    private void CheckCameraMultipliers()
    {
        _forwardMultiplier = CheckDirection(_moveChecker.forward);
        _backwardMultiplier = CheckDirection(_moveChecker.forward * -1);
        _rightMultiplier = CheckDirection(_moveChecker.right);
        _leftMultiplier = CheckDirection(_moveChecker.right * -1);
    }

    private float CheckDirection(Vector3 direction)
    {
        Ray ray = new Ray(_moveChecker.position, direction);

        if(Physics.SphereCast(ray, _checkThickness, _checkDistance, _checkMask))
        {
            return 0;
        }

        return 1;
        
    }

  
}
