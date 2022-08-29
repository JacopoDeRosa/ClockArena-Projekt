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

    public bool HasHeadArmour { get => _headSlot.Item != null; }
    public bool HasBodyArmour { get => _bodySlot.Item != null; }
    public bool HasWeapon { get => _weaponSlot.Item != null; }
    public bool HasGadget { get => _gadgetSlot.Item != null; }

    public Armour HeadArmour { get => _headSlot.Item; }
    public Armour BodyArmour { get => _bodySlot.Item; }
    public Weapon Weapon { get => _weaponSlot.Item; }
    public Gadget Gadget { get => _gadgetSlot.Item; }

    private void Awake()
    {
        GetArmourDB();
    }

    private void GetArmourDB()
    {
        _armourDB = GameItemDB.GetDbOfType<ArmourDB>();
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

    public Armour SetHeadArmour(int index)
    {
        if (_armourDB == null) GetArmourDB();

        Armour armourPrefab = _armourDB.GetItem(index);

        if (armourPrefab == null || armourPrefab.Data == null || IsValidItemForUser(armourPrefab) == false || armourPrefab.Data.ArmourType != ArmourTypes.Head) return null;

        Armour armour = Instantiate(armourPrefab, _armourContainer);

        armour.SetUser(_user);

        _headSlot.SetItem(armour);

        onArmourChanged?.Invoke();

        return armour;
    }

    public Armour SetBodyArmour(int index)
    {
        if (_armourDB == null) GetArmourDB();

        Armour armourPrefab = _armourDB.GetItem(index);

        if (armourPrefab == null || armourPrefab.Data == null || IsValidItemForUser(armourPrefab) == false || armourPrefab.Data.ArmourType != ArmourTypes.Body) return null;

        Armour armour = Instantiate(armourPrefab, _armourContainer);

        armour.SetUser(_user);

        _bodySlot.SetItem(armour);

        onArmourChanged?.Invoke();

        return armour;
    }

    private bool IsValidItemForUser(GameItem item)
    {
        if (item == null || item.Data == null) return false;
        return item.Data.RequiredLevel <= _user.Level && item.Data.UsableByFaction(_user.Faction);
    }

    public ItemData ClearWeapon()
    {
        ItemData data = _weaponSlot.ClearItem();
        return data;
       
    }
    public ItemData ClearGadget()
    {
        ItemData data = _gadgetSlot.ClearItem();
        return data;
    }
    public ItemData ClearHeadArmour(bool recaulculateMesh = true)
    {
        ItemData data = _headSlot.ClearItem();
        if (recaulculateMesh) onArmourChanged?.Invoke();
        return data; 
    }
    public ItemData ClearBodyArmour(bool recaulculateMesh = true)
    {
        ItemData data = _bodySlot.ClearItem();
        if(recaulculateMesh) onArmourChanged?.Invoke();
        return data;
    }
}
