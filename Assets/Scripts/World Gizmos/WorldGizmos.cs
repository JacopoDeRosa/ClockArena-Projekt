using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGizmos : MonoBehaviour
{
    [SerializeField] private RangeGizmo _rangeGizmo;
    [SerializeField] private PathRenderer _pathRenderer;
    [SerializeField] private GameObject _pointer;


    public void RenderPath(Vector3[] points, Gradient gradient)
    {
        _pathRenderer.RenderPath(points, gradient);
    }

    public void ClearPath()
    {
        _pathRenderer.ClearPath();
    }

    public void SetPathColor(Gradient color)
    {
        _pathRenderer.SetGizmoColor(color);
    }

    public void SetPointerPosition(Vector3 position)
    {
        if(_pointer.activeInHierarchy == false) _pointer.SetActive(true);
        _pointer.transform.position = position;
    }

    public void ResetPointer()
    {
        if (_pointer.activeInHierarchy == true) _pointer.SetActive(false);
    }
}
