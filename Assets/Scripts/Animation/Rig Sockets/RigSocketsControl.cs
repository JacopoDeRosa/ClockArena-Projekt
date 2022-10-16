using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigSocketsControl : MonoBehaviour
{
    [SerializeField] private RigSocket[] _sockets;

    private void OnValidate()
    {
        _sockets = GetComponentsInChildren<RigSocket>();
        foreach (RigSocket item in _sockets)
        {
            item.UpdatePosition();
        }
    }

    public void AttachItemToSocket(Socket socket, GameObject item, Vector3 offset, Vector3 rotOffset)
    {
        RigSocket rigSocket = GetRigSocket(socket);

        if (rigSocket == null) return;

        rigSocket.AttachItemWithOffset(item, offset, rotOffset);
    }

    private RigSocket GetRigSocket(Socket socket)
    {
        foreach (RigSocket rigSocket in _sockets)
        {
            if(rigSocket.SocketType == socket)
            {
                return rigSocket;
            }
        }

        return null;
    }
}
