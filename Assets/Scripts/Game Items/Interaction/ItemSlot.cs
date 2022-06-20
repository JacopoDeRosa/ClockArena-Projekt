using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemSlot
{
    [SerializeField] private GameItem _item;
    [SerializeField] private int _count;

    public GameItem Item { get => _item; }
    public int Count { get => _count; }

    public ItemSlot(GameItem item, int count)
    {
        _item = item;
        _count = count;
    }



}
