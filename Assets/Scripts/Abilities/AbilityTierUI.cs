using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class AbilityTierUI : MonoBehaviour
{
    [SerializeField] private AbilityUiSlot[] _allSlots;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private GameObject _lockedView;

    private Queue<AbilityUiSlot> _freeSlots;

    private void Awake()
    {
        ResetAbilitySlots();   
    }

    private void ResetAbilitySlots()
    {
        if(_freeSlots == null)
        {
            _freeSlots = new Queue<AbilityUiSlot>();
        }
        else
        {
            _freeSlots.Clear();
        }

        foreach (AbilityUiSlot slot in _allSlots)
        {
            _freeSlots.Enqueue(slot);
            slot.gameObject.SetActive(false);
        }
    }

    public void ReadAbilityTier(AbilityTier abilityTier)
    {
        ResetAbilitySlots();

        _nameText.text = abilityTier.Name;
        foreach (Ability ability in abilityTier.Abilities)
        {
            AbilityUiSlot uiSlot = _freeSlots.Dequeue();
            uiSlot.gameObject.SetActive(true);
            uiSlot.SetAbility(ability);
        }
    }
    public void SetLocked(bool locked)
    {
        _lockedView.SetActive(locked);
    }
}
