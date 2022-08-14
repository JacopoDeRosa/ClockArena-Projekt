using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ItemData : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _cost;
    [SerializeField] private ItemRarity _rarity;
    [SerializeField] private int _reuqiredLevel;
    [SerializeField] private Factions _enabledFactions;

    public Sprite Sprite { get => _sprite; }
    public int Cost { get => _cost; }
    public ItemRarity Rarity { get => _rarity; }
    public int RequiredLevel { get => _reuqiredLevel; }


    public bool UsableByFaction(Faction faction)
    {
        return _enabledFactions.HasFlag((Factions)faction);
    }

    public virtual string GetItemClass()
    {
        return "Unknown";
    }
}
