using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class Equipment : MonoBehaviour
{
    [SerializeField] private Character _user;
    [SerializeField] private WeaponSlot _weaponSlot;
    [SerializeField] private GadgetSlot _gadgetSlot;
    [SerializeField] private ArmourSlot _headSlot;
    [SerializeField] private ArmourSlot _bodySlot;
    [SerializeField] private ArmourSlot _handsSlot;


    private void OnValidate()
    {
        ValidateUtilitySlots();
        ValidateArmourSlots();
    }

    private void ValidateUtilitySlots()
    {
        _weaponSlot.OnValidate();
        _gadgetSlot.OnValidate();       
    }

    private void ValidateArmourSlots()
    {
        _handsSlot.OnValidate();
        _bodySlot.OnValidate();
        _handsSlot.OnValidate();

        _handsSlot.SetArmourType(ArmourTypes.Hands);
        _bodySlot.SetArmourType(ArmourTypes.Body);
        _handsSlot.SetArmourType(ArmourTypes.Hands);
    }
}
