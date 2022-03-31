using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class EquipmentSlot
{
    [SerializeField] protected GameItem _item;
    [SerializeField][ReadOnly]
    private ItemTypes _itemType;

    public GameItem Item { get => _item; }

    public virtual void SetItem(GameItem item)
    {
        if (_item.Data.ItemType != _itemType) return;
        _item = item;
    }
    public bool IsValidFactionItem(Factions faction)
    {
        return Item.Data.UsableByFaction(faction);
    }
    public virtual void OnValidate()
    {

    }
    protected void ForceItemType(ItemTypes itemType)
    {
        if (_itemType != itemType) _itemType = itemType;
    }
}
