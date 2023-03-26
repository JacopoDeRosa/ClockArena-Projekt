using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArmourSlot : EquipmentSlot<Armour>
{
    [SerializeField]
    private ArmourTypes _armourType;

    public ArmourTypes ArmourType { get => _armourType; }
    public new Armour Item { get => _item as Armour; }

    public void SetArmourType(ArmourTypes type)
    {
        _armourType = type;
    }
    public override void SetItem(Armour armour)
    {
        if (armour == null) return;
        if (armour.Data.ArmourType != _armourType) return;
        base.SetItem(armour);
    }
}
