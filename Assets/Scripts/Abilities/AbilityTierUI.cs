using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class AbilityTierUI : MonoBehaviour
{
    [SerializeField] private AbilityUiSlot[] _allSlots;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private GameObject _lockedView;

    private void Awake()
    {
        ResetAbilitySlots();   
    }

    private void ResetAbilitySlots()
    {
        foreach (AbilityUiSlot slot in _allSlots)
        {
            slot.gameObject.SetActive(false);
        }
    }

    public void ReadAbilityTier(AbilityTier abilityTier, int index)
    {
        ResetAbilitySlots();

        _nameText.text = abilityTier.Name;
        for (int i = 0; i < abilityTier.Abilities.Length; i++)
        {
            Ability ability = abilityTier.Abilities[i];
            AbilityUiSlot uiSlot = _allSlots[i];
            uiSlot.gameObject.SetActive(true);
            uiSlot.SetAbility(ability);
            uiSlot.SetAbilityDescriptor(new AbilityDescriptor(index, i));
        }
    }
    public void SetLocked(bool locked)
    {
        _lockedView.SetActive(locked);
    }
}
