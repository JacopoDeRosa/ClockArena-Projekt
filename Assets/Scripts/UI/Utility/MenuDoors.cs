using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MenuDoors : MonoBehaviour
{
    [SerializeField] private FoldingBar _leftDoor, _rightDoor;

    [Button]
    public void Open()
    {
        _leftDoor.Toggle(false);
        _rightDoor.Toggle(false);
    }

    [Button]
    public void Close()
    {
        _leftDoor.Toggle(true);
        _rightDoor.Toggle(true);
    }
}
