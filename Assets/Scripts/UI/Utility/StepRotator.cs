using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepRotator : MonoBehaviour
{
    [SerializeField] private float _rotStep;
    [SerializeField] private float _interval;

    private float _timer;

    private void Update()
    {
        if(_timer > _interval)
        {
            _timer = 0;
            transform.Rotate(0, 0, _rotStep);
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;
        _timer = 0;
    }
}
