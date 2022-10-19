using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleLine : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 100);
    }
}
