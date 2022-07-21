using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItemDB<T> : GameItemDB where T: GameItem
{
    [SerializeField] private T _defaultItem;
    [SerializeField] private T[] _items;

  
    public T GetItem(int index)
    {
        if (index >= _items.Length) return _defaultItem;
        return _items[index];
    }
}
