using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    [SerializeField] protected ItemData _data;
    [SerializeField] private Character _user;

    public ItemData Data { get => _data; }
    protected Character User { get => _user; }

    public void SetUser(Character user)
    {
        _user = user;
    }

    protected void ForceDatatype<T>() where T: ItemData
    {
        if (_data == null) return;
        var test = _data as T;
        if(test == null)
        {
            _data = null;
            return;
        }
    }
}
