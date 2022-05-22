using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;



public class ItemData : ScriptableObject
{
    [SerializeField] private int _cost;
    [SerializeField] private ItemRarity _rarity;
    [SerializeField] private int _reuqiredLevel;
    [SerializeField] private Factions _enabledFactions;

    [SerializeField][ReadOnly]
    private ItemTypes _itemType;

    public int Cost { get => _cost; }
    public ItemRarity Rarity { get => _rarity; }
    public int RequiredLevel { get => _reuqiredLevel; }
    public ItemTypes ItemType { get => _itemType; }

    public bool UsableByFaction(Factions faction)
    {
        return _enabledFactions.HasFlag(faction);

    }

    protected void ForceItemType(ItemTypes type)
    {
        _itemType = type;
    }
}
