using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterEditorCamera : MonoBehaviour
{
    private const float breakPowerDead = 0.5f;

    [SerializeField] private float _speed, _breakPower;

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
        if (_focused)
        {
            _rotationSpeed += _mouseDelta.x * _speed * Time.deltaTime;
        }
        else
        {
            _mouseDelta.Set(0, 0);
        }

        if (_mouseDelta.x == 0)
        {
            if(_rotationSpeed > breakPowerDead || _rotationSpeed < -breakPowerDead)
            {
                if(_rotationSpeed > 0)
                {
                    _rotationSpeed -= _breakPower * Time.deltaTime;
                }
                else
                {
                    _rotationSpeed += _breakPower * Time.deltaTime;
                }
                
            }
            else
            {
                _rotationSpeed = 0;
            }
        }
    

        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
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
