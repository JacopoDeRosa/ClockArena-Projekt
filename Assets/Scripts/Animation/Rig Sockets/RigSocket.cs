using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigSocket : MonoBehaviour
{
    [SerializeField] private Socket _socketType;
    [SerializeField] private Transform _targetBone;

    public Socket SocketType { get => _socketType; }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;     
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.DrawWireSphere(Vector3.zero, 0.1f);
    }

    private void LateUpdate()
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        transform.position = _targetBone.position;
        transform.rotation = _targetBone.rotation;
    }


    public void AttachItemWithOffset(GameObject item, Vector3 offset, Vector3 rotOffset)
    {
        item.transform.parent = transform;
        item.transform.localPosition = offset;
        item.transform.localRotation = Quaternion.Euler(rotOffset);
    }
}
