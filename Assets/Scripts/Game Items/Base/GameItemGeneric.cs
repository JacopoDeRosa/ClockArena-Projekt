using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem<T> : GameItem where T : ItemData
{
    public new T Data { get => _data as T; }

    private void OverrideDataType()
    {
        if (_data is T == false)
        {
            _data = null;
        }
    }
    protected virtual void OnValidate()
    {
        OverrideDataType();
    }
}
