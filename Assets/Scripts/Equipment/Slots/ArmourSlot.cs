using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class ArmourSlot : EquipmentSlot<Armour>
{
    [SerializeField]
    [ReadOnly]
    private ArmourTypes _armourType;

    public ArmourTypes ArmourType { get => _armourType; }
    new public Armour Item { get => _item as Armour; }

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
