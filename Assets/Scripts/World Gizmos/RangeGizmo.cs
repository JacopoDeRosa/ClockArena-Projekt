using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeGizmo : MonoBehaviour
{
    public void SetRange(float range)
    {
        transform.localScale = new Vector3(range * 2, range * 2, range * 2);
    }
}
