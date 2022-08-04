using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class EquipmentSlot<T> where T: GameItem
{
    [SerializeField] protected T _item;

    public GameItem Item { get => _item; }

    public virtual void SetItem(T item)
    {
        _item = item;
    }
    public virtual ItemData ClearItem()
    {
        ItemData data = null;

        if (_item != null)
        {
            data = _item.Data;
            MonoBehaviour.Destroy(_item);
            _item = null;
        }

        return data;
    }
    public bool IsValidFactionItem(Factions faction)
    {
        return Item.Data.UsableByFaction(faction);
    }
}
