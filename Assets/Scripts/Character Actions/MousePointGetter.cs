using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

public class MousePointGetter : MonoBehaviour
{
    [SerializeField] private Camera _eventCamera;

    private Vector2 _mousePosition;
    private PlayerInput _playerInput;

    public Vector2 MousePosition { get => _mousePosition; }

    private void Start()
    {
        _playerInput = PlayerInputSingleton.Instance;

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

    /// <summary>
    /// Returns true if an object was found
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public bool GetHoveredObject(out GameObject hover)
    {
        hover = null;
        Ray ray = _eventCamera.ScreenPointToRay(_mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hover = hit.transform.gameObject;
            return true;
        }
        return false;
    }
    public bool GetHoveredObject(out GameObject hover, LayerMask layerMask)
    {
        hover = null;
        Ray ray = _eventCamera.ScreenPointToRay(_mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            hover = hit.transform.gameObject;
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
