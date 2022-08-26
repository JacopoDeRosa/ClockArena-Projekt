using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponSlot : EquipmentSlot<Weapon>
{
    public new Weapon Item { get => _item as Weapon; }
}
