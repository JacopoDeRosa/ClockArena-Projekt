using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Equipment : MonoBehaviour
{
    [SerializeField] private Character _user;
    [SerializeField] private WeaponSlot _weaponSlot;
    [SerializeField] private GadgetSlot _gadgetSlot;
    [SerializeField] private ArmourSlot _headSlot;
    [SerializeField] private ArmourSlot _bodySlot;

    public event Action onArmourChanged;

    private void OnValidate()
    {
        ValidateArmourSlots();
    }

    private void ValidateArmourSlots()
    {
        _headSlot.SetArmourType(ArmourTypes.Head);
        _bodySlot.SetArmourType(ArmourTypes.Body);
    }

    public void SetWeapon(Weapon weapon)
    {
        _weaponSlot.SetItem(weapon);
    }

    public void SetGadget(Gadget gadget)
    {
        gadget.Data.UsableByFaction(Factions.DeathRow);
    }

    public void SetHeadArmour(Armour armour)
    {
        _headSlot.SetItem(armour);
    }

    public void SetBodyArmour(Armour armour)
    {
        _bodySlot.SetItem(armour);
    }
}
