using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Constraints/Follow Rotation")]
public class FollowRotationConstraint : MonoBehaviour
{
    
    [SerializeField] private Axis _axes;

    [SerializeField] [Range(0, 1)] private float _weight;
    [SerializeField] private Transform _target;

    float _rotX, _rotY, _rotZ;

    private void Start()
    {
        _rotX = transform.localRotation.eulerAngles.x;
        _rotY = transform.localRotation.eulerAngles.y;
        _rotZ = transform.localRotation.eulerAngles.z;
    }

    private void Update()
    {
        if(_axes.HasFlag(Axis.X))
        {
            if (_target.localRotation.eulerAngles.x < 180)
            {
                _rotX = _target.localRotation.eulerAngles.x * _weight;
            }
            else
            {
                _rotX = ((360 - _target.localRotation.eulerAngles.x) * _weight) * -1;
            }
        }

        if(_axes.HasFlag(Axis.Y))
        {
            if(_target.localRotation.eulerAngles.y < 180)
            {
                _rotY = _target.localRotation.eulerAngles.y * _weight;
            }
            else
            {
                _rotY = ((360 - _target.localRotation.eulerAngles.y) * _weight) * - 1;
            }
        }

        if(_axes.HasFlag(Axis.Z))
        {
            if (_target.localRotation.eulerAngles.z < 180)
            {
                _rotZ = _target.localRotation.eulerAngles.z * _weight;
            }
            else
            {
                _rotZ = ((360 - _target.localRotation.eulerAngles.z) * _weight) * -1;
            }
        }
  
        transform.localRotation = Quaternion.Euler(_rotX, _rotY, _rotZ);
    }

}
