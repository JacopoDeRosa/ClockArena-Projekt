using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New Amour Data", menuName = "Items/New Amour Data")]
public class ArmourData : ItemData
{
    [SerializeField] private int _protection;
    [SerializeField] private int _weight;
    [SerializeField] private ArmourTypes _armourType;


    public int Protection { get => _protection; }
    public int Weight { get => _weight; }
    public ArmourTypes ArmourType { get => _armourType; }

    public override string GetItemClass()
    {
        return _armourType.ToString() + " Armour";
    }
}

