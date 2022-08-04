using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneMirror : MonoBehaviour
{
    private Transform _sourceBone;

    public void SetSourceBone(Transform source)
    {
        _sourceBone = source;
    }

    public void Update()
    {
        if (_sourceBone != null)
        {
            transform.localPosition = _sourceBone.localPosition;
            transform.localRotation = _sourceBone.localRotation;
        }
    }

}
