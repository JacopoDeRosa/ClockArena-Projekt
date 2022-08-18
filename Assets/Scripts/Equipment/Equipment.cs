using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Equipment : MonoBehaviour
{
    [SerializeField] private Character _user;
    [SerializeField] private Transform _armourContainer;
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

    public void SetWeapon(int weapon)
    {
       // _weaponSlot.SetItem(weapon);
    }
    public void SetGadget(int gadget)
    {
       // _gadgetSlot.SetItem(gadget);
    }
    public void SetHeadArmour(int armour)
    {
        Debug.Log("Setting head to: " + armour);
        onArmourChanged?.Invoke();
        // _headSlot.SetItem(armour);
    }
    public void SetBodyArmour(int armour)
    {
        Debug.Log("Setting body to: " + armour);
        onArmourChanged?.Invoke();
        // _bodySlot.SetItem(armour);
    }

    public void ClearWeapon()
    {
        _weaponSlot.ClearItem();
    }
    public void ClearGadget()
    {
        _gadgetSlot.ClearItem();
    }
    public void ClearHeadArmour()
    {
        _headSlot.ClearItem();
    }
    public void ClearBodyArmour()
    {
        _bodySlot.ClearItem();
    }
}
