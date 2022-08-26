using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GadgetSlot : EquipmentSlot<Gadget>
{
    public new Gadget Item { get => _item as Gadget; }
}
