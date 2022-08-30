using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySetSlot : MonoBehaviour
{
    [SerializeField] private CharacterCustomizationWindow _parent;
    [SerializeField] private Image _icon;
    [SerializeField] private TooltipController _tooltip;
    [SerializeField] private bool _primaryAbility;


    private IconsDB _iconsDB;
    private void Awake()
    {
        _iconsDB = GameItemDB.GetDbOfType<IconsDB>();
    }
    public void SetAbility(Ability ability, AbilityDescriptor descriptor)
    {
        ReadAbility(ability);
        Debug.Log(descriptor.abilityIndex + descriptor.tierIndex);

        if(_primaryAbility)
        {
            _parent.SetPrimaryAbility(descriptor);
        }
        else
        {
            _parent.SetSecondaryAbility(descriptor);
        }

    }

    public void ReadAbility(Ability ability)
    {
        if (ability == null)
        {
            _icon.sprite = _iconsDB.DefaultSprite;
            _tooltip.SetContents("No Ability", "");
        }
        else
        {
            _icon.sprite = ability.Icon;
            _tooltip.SetContents(ability.Name, ability.Description);
        }
    }

}
