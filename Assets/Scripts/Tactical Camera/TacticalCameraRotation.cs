using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TacticalCameraRotation : MonoBehaviour
{
    [SerializeField] private float _rotationStep;
    [SerializeField] private float _rotationSpeed;

    private bool _busy;

    [Button]
    public void RotateLeft()
    {
        if (_busy) return;
        _busy = true;
        StartCoroutine(SmoothRotation(_rotationStep));
    }

    [Button]
    public void RotateRight()
    {
        if (_busy) return;
        _busy = true;
        StartCoroutine(SmoothRotation(-_rotationStep));

    }


    private IEnumerator SmoothRotation(float rotation)
    {
        float totalRotation = 0;
        float direction = Mathf.Sign(rotation);
        Vector3 finalRotation = transform.rotation.eulerAngles + new Vector3(0, rotation, 0);

        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

        while (totalRotation < Mathf.Abs(rotation))
        {
            float rotateBy = _rotationSpeed * direction * Time.deltaTime;
            transform.Rotate(new Vector3(0, rotateBy, 0));
            totalRotation += Mathf.Abs(rotateBy);
            yield return waitForEndOfFrame;
        }
        transform.rotation = Quaternion.Euler(finalRotation);
        _busy = false;
    }
}
