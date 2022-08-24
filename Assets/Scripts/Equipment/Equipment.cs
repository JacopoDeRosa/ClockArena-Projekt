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

    private ArmourDB _armourDB;

    private void Awake()
    {
        GetArmourDB();
    }

    private void GetArmourDB()
    {
        _armourDB = GameItemDB.GetDbOfType<ArmourDB>(ArmourDB.Name);
    }

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

    public void SetHeadArmour(int index)
    {
        if (_armourDB == null) GetArmourDB();

        Armour armourPrefab = _armourDB.GetItem(index);

        if (armourPrefab == null || armourPrefab.Data == null || IsValidItemForUser(armourPrefab) == false || armourPrefab.Data.ArmourType != ArmourTypes.Head) return;

        Armour armour = Instantiate(armourPrefab, _armourContainer);

        armour.SetUser(_user);

        _headSlot.SetItem(armour);

        onArmourChanged?.Invoke();
    }

    public void SetBodyArmour(int index)
    {
        if (_armourDB == null) GetArmourDB();

        Armour armourPrefab = _armourDB.GetItem(index);

        if (armourPrefab == null || armourPrefab.Data == null || IsValidItemForUser(armourPrefab) == false || armourPrefab.Data.ArmourType != ArmourTypes.Body) return;

        Armour armour = Instantiate(armourPrefab, _armourContainer);

        armour.SetUser(_user);

        _bodySlot.SetItem(armour);

        onArmourChanged?.Invoke();
    }

    private bool IsValidItemForUser(GameItem item)
    {
        if (item == null || item.Data == null) return false;
        return item.Data.RequiredLevel <= _user.Level && item.Data.UsableByFaction(_user.Faction);
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
