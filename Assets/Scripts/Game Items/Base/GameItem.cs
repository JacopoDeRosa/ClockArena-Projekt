using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    protected ItemData _data;

    [SerializeField] private Character _user;

    public ItemData Data { get => _data; }
    protected Character User { get => _user; }

    public void SetUser(Character user)
    {
        _user = user;
    }
}

