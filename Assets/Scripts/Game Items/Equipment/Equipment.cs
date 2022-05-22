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
        ValidateArmourSlots();
    }


    private void ValidateArmourSlots()
    {
        _handsSlot.SetArmourType(ArmourTypes.Hands);
        _bodySlot.SetArmourType(ArmourTypes.Body);
        _handsSlot.SetArmourType(ArmourTypes.Hands);
    }
}
