using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

public class MousePointGetter : MonoBehaviour
{
    [SerializeField] private Camera _eventCamera;
    [SerializeField] private PlayerInput _playerInput;
    [ShowInInspector] 
    [ReadOnly]
    private Vector2 _mousePosition;


    private void Awake()
    {
        _playerInput.actions["Mouse Position"].performed += OnMousePosition;
    }
    private void OnDestroy()
    {
        if (_playerInput != null)
        {
            _playerInput.actions["Mouse Position"].performed -= OnMousePosition;
        }
    }
    private void OnValidate()
    {
        if(_playerInput == null)
        {
            _playerInput = FindObjectOfType<PlayerInput>();
        }
    }

    private void OnMousePosition(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// Returns true if point was found
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public bool GetMousePoint(out Vector3 point)
    {
        point = Vector3.zero;
        Ray ray = _eventCamera.ScreenPointToRay(_mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            point = hit.point;
            return true;
        }
        return false;     
    }

    public bool GetMousePoint(out Vector3 point, LayerMask layerMask)
    {
        point = Vector3.zero;
        Ray ray = _eventCamera.ScreenPointToRay(_mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity,layerMask))
        {
            point = hit.point;
            return true;
        }
        return false;
    }




}
