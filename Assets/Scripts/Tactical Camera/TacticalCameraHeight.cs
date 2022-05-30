using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TacticalCameraHeight : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private int _currentLayer;
    [SerializeField] private int _maxLayers;
    [SerializeField] private float _step;
    [SerializeField] private float _speed;

    private bool _busy;

    private void OnValidate()
    {
        if(_input == null)
        {
            _input = FindObjectOfType<PlayerInput>();
        }
    }


    private void Start()
    {
        if (_input == null) _input = FindObjectOfType<PlayerInput>();

        if (_input)
        {
            _input.actions["MouseWheel"].started += OnMouseWheel;
        }
    }
    private void OnDestroy()
    {
        if(_input)
        {
            _input.actions["MouseWheel"].started -= OnMouseWheel;
        }
    }

    private void OnMouseWheel(InputAction.CallbackContext context)
    {
        print("Mouse Wheel");
        Vector2 wheel = context.ReadValue<Vector2>();

        if (_busy) return;

        StartCoroutine(ChangeLevel(wheel.y));
    }

    private IEnumerator ChangeLevel(float direction)
    {
        _busy = true;
        float totalMove = 0;
        float startY = transform.position.y;
        float directionSign = Mathf.Sign(direction);

        if(_currentLayer <= 0 && directionSign < 0)
        {
            _busy = false;
            yield break;
        }
        else if(_currentLayer >= _maxLayers && directionSign > 0)
        {
            _busy = false;
            yield break;
        }

        while (totalMove < _step)
        {
            float move = _speed * Time.deltaTime * directionSign;
            transform.Translate(0, move, 0);
            totalMove += Mathf.Abs(move);
            yield return new WaitForEndOfFrame();
        }

        transform.position = new Vector3(transform.position.x, startY + (_step * directionSign), transform.position.z);

        _currentLayer += (int) directionSign;
        _busy = false;
    }
}
