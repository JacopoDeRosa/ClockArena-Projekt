using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _cost;
    [SerializeField] private int _reuqiredLevel;
    [SerializeField] private Factions _enabledFactions;

    public string Name { get => _name; }
    public Sprite Sprite { get => _sprite; }
    public int Cost { get => _cost; }
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
