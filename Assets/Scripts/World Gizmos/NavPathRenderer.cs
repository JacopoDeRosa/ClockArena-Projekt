using System;
using System.Collections.Generic;
using UnityEngine;


public class NavPathRenderer : PathRenderer
{
    [SerializeField] private Gradient _validColor, _invalidColor;

    public void RenderNavPath(Vector3[] points, bool valid)
    {
        if(valid)
        {
            RenderPath(points, _validColor);
        }
        else
        {
            RenderPath(points, _invalidColor);
        }

    }

}

