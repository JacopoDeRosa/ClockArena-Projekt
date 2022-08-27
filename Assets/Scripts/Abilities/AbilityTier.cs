using UnityEngine;
using System.Collections;

[System.Serializable]
public class AbilityTier
{
    [SerializeField] private string _name;
    [SerializeField] private Ability[] _abilities;

    public string Name { get => _name; }
    public Ability[] Abilities { get => _abilities; } 
    
    public Ability GetAbility(int index)
    {
        return _abilities[index];
    }

}
