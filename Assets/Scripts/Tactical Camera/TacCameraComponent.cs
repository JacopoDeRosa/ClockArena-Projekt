using System;
using System.Collections.Generic;
using UnityEngine;

public class TacCameraComponent : MonoBehaviour
{
    [SerializeField] protected Transform _moveChecker;
    [SerializeField] protected float _checkDistance, _checkThickness;
    [SerializeField] protected LayerMask _checkMask;


    protected float CheckDirection(Vector3 direction)
    {
        Ray ray = new Ray(_moveChecker.position, direction);

        if (Physics.SphereCast(ray, _checkThickness, _checkDistance, _checkMask))
        {
            return 0;
        }

        return 1;
    }

    protected bool CheckDirectionBool(Vector3 direction)
    {
        Ray ray = new Ray(_moveChecker.position, direction);

        return Physics.SphereCast(ray, _checkThickness, _checkDistance, _checkMask);

    }


}