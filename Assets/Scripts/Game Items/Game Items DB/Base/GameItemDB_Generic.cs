using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItemDB<T> : GameItemDB where T: GameItem
{
    [SerializeField] private T[] _items;

    private Dictionary<string, T> _itemLookUp;

    public void GenerateItemLookUp()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            T item = _items[i];
            if (item == null) continue;

            _itemLookUp.Add(Id + "_" + _items[i].name, _items[i]);
        }
    }

    public T GetItem(string itemName)
    {
        return _itemLookUp[Id + "_" + itemName];
    }
}
