using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class AbilityUiSlot: MonoBehaviour
{
    [SerializeField] private Image _abilityIcon;
    [SerializeField] private TMP_Text _abilityName;

    public void SetAbility(Ability ability)
    {
        _abilityIcon.sprite = ability.Icon;
        _abilityName.text = ability.Name;
    }

}

