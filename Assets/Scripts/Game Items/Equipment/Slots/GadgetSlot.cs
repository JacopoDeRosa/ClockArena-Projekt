using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GadgetSlot : EquipmentSlot
{
    new public Gadget Item { get => _item as Gadget; }

    public override void OnValidate()
    {
        base.OnValidate();
        ForceItemType(ItemTypes.Gadget);
    }
}
