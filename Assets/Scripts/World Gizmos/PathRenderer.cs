using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    
    private Vector3[] vector3s;


    public void RenderPath(Vector3[] points)
    {

        vector3s = points;
        _lineRenderer.positionCount = points.Length;
        _lineRenderer.SetPositions(points);
    }

    public void RenderPath(Vector3[] points, Gradient gradient)
    {
        SetGizmoColor(gradient);
        RenderPath(points);
    }

    public void ClearPath()
    {
        _lineRenderer.positionCount = 0;
        _lineRenderer.SetPositions(new Vector3[0]);
    }

    public void SetGizmoColor(Gradient gradient)
    {
        _lineRenderer.colorGradient = gradient;
    }
}
