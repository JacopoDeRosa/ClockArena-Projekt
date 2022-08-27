using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ability Tree", menuName = "New Ability Tree")]
public class AbilityTree : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private AbilityTier[] _abilityTiers = new AbilityTier[10];

    public string Name { get => _name; }

    public Ability GetAbility(int tierIndex, int abilityIndex)
    {
        AbilityTier tier = _abilityTiers[tierIndex];
        return tier.GetAbility(abilityIndex);
    }

    public Ability GetAbility(AbilityDescriptor descriptor)
    {
        if (descriptor.available == false) return null;

        return GetAbility(descriptor.tierIndex, descriptor.abilityIndex);
    }

    public AbilityTier GetAbilityTier(int tier)
    {
        if (tier >= 10) return null;

        return _abilityTiers[tier];
    }

    private void OnValidate()
    {
        if(_abilityTiers.Length != 10)
        {
            _abilityTiers = new AbilityTier[10];
        }
    }

#if UNITY_EDITOR
   // TODO: Put editor stuff here in future.
#endif
}
