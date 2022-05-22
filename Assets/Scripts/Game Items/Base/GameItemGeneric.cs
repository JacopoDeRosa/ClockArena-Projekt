using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem<T> : GameItem where T : ItemData
{
    protected new T _data;
    public new T Data { get => _data; }
}
