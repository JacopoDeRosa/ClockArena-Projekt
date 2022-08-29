using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGizmos : MonoBehaviour
{
    [SerializeField] private RangeGizmo _rangeGizmo;
    [SerializeField] private PathRenderer _pathRenderer;
    [SerializeField] private NavPathRenderer _navPathRenderer;
    [SerializeField] private GameObject _pointer;
    private void OnValidate()
    {
        if(FindObjectsOfType<WorldGizmos>().Length > 1)
        {
            Debug.LogError("Too many world gizmos components in scene, There Can Be Only One");
        }
    }


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

    public void RenderNavPath(Vector3[] points, bool valid)
    {
        _navPathRenderer.RenderNavPath(points, valid);
    }

    public void ClearNavPath()
    {
        _navPathRenderer.ClearPath();
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

    public void SetRangeGizmo(Vector3 position, float range)
    {
        if(_rangeGizmo.gameObject.activeInHierarchy == false) _rangeGizmo.gameObject.SetActive(true);
        _rangeGizmo.SetRange(range);
        _rangeGizmo.transform.position = position;
    }

    public void ResetRangeGizmo()
    {
        _rangeGizmo.gameObject.SetActive(false);
    }
}
