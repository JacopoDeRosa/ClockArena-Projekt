using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponSlot : EquipmentSlot
{
   new public Weapon Item { get => _item as Weapon; }

    public override void OnValidate()
    {
        base.OnValidate();
        ForceItemType(ItemTypes.Weapon);
    }
}
