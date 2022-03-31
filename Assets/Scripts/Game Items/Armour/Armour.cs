using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Armour : GameItem
{
    [SerializeField] private ItemTypes _targetSlot;

    new public ArmourData Data { get => (ArmourData) _data; }
    public ItemTypes TargetSlot { get => _targetSlot; }
}
