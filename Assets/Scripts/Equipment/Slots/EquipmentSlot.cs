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
    public bool IsValidFactionItem(Factions faction)
    {
        return Item.Data.UsableByFaction(faction);
    }
}
