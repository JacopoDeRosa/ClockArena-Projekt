using UnityEngine;
using System.Collections;
using System;

public class CharacterAbilities : MonoBehaviour
{
    [SerializeField] private Ability _primaryAbility, _secondaryAbility;
    [SerializeField] private AbilityTree _activeTree;

    public event Action<Ability> onPrimaryChange, onSecondaryChange;


    public void SetPrimaryAbility(AbilityDescriptor descriptor)
    {
        _primaryAbility = _activeTree.GetAbility(descriptor);
        onPrimaryChange?.Invoke(_primaryAbility);
    }
    public void SetSecondaryAbility(AbilityDescriptor descriptor)
    {
        _secondaryAbility = _activeTree.GetAbility(descriptor);
        onSecondaryChange?.Invoke(_secondaryAbility);
    }
    public void SetAbilityTree(int tree)
    {
        _activeTree = GameItemDB.GetDbOfType<AbilityTreesDB>(AbilityTreesDB.Name).GetItem(tree);
    }
}
