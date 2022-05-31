using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;

public class TacticalCameraRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _rotationStep;

    private PlayerInput _input;
    private bool _busy;


    private void Start()
    {
        if (_input == null) _input = FindObjectOfType<PlayerInput>();
        if (_input != null)
        {
            _input.actions["Rotation"].started += OnRotation;
        }
    }

    private void OnDestroy()
    {
        if(_input != null)
        {
            _input.actions["Rotation"].started -= OnRotation;
        }
    }

    private void OnRotation(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        if(value > 0)
        {
            RotateLeft();
        }
        else if(value < 0)
        {
            RotateRight();
        }
        
    }

    [Button]
    public void RotateLeft()
    {
        if (_busy)
        {
            return;
        }
        _busy = true;
        StartCoroutine(SmoothRotation(_rotationStep));
    }

    [Button]
    public void RotateRight()
    {
        if (_busy)
        {
            return;
        }
        _busy = true;
        StartCoroutine(SmoothRotation(-_rotationStep));
    }


    private IEnumerator SmoothRotation(float rotation)
    {
        float totalRotation = 0;
        float direction = Mathf.Sign(rotation);
        Vector3 finalRotation = transform.localRotation.eulerAngles + new Vector3(0, rotation, 0);

        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

        while (totalRotation < Mathf.Abs(rotation))
        {
            float rotateBy = _rotationSpeed * direction * Time.deltaTime;
            transform.Rotate(new Vector3(0, rotateBy, 0));
            totalRotation += Mathf.Abs(rotateBy);
            yield return waitForEndOfFrame;
        }
        transform.localRotation = Quaternion.Euler(finalRotation);
        _busy = false;
    }
}
