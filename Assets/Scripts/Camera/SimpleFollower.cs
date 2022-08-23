using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SimpleFollower : MonoBehaviour
{
    [SerializeField] private Transform _positionTarget, _rotationTarget;


    private void Update()
    {
        if(_positionTarget) transform.position = _positionTarget.position;
        if (_rotationTarget) transform.rotation = _rotationTarget.rotation;
    }
}
